// PredictedRigidbody which stores & indidvidually rewinds history per Rigidbody.
//
// This brings significant performance savings because:
// - if a scene has 1000 objects
// - and a player interacts with say 3 objects at a time
// - Physics.Simulate() would resimulate 1000 objects
// - where as this component only resimulates the 3 changed objects
//
// The downside is that history rewinding is done manually via Vector math,
// instead of real physics. It's not 100% correct - but it sure is fast!
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mirror
{
    public enum CorrectionMode
    {
        Set,               // rigidbody.position/rotation = ...
        Move,              // rigidbody.MovePosition/Rotation
    }

    // [RequireComponent(typeof(Rigidbody))] <- RB is moved out at runtime, can't require it.
    public class PredictedRigidbody : NetworkBehaviour
    {
        Transform tf; // this component is performance critical. cache .transform getter!
        Rigidbody rb; // own rigidbody on server. this is never moved to a physics copy.
        Vector3 lastPosition;

        // [Tooltip("Broadcast changes if position changed by more than ... meters.")]
        // public float positionSensitivity = 0.01f;

        // client keeps state history for correction & reconciliation.
        // this needs to be a SortedList because we need to be able to insert inbetween.
        // RingBuffer would be faster iteration, but can't do insertions.
        [Header("State History")]
        public int stateHistoryLimit = 32; // 32 x 50 ms = 1.6 seconds is definitely enough
        readonly SortedList<double, RigidbodyState> stateHistory = new SortedList<double, RigidbodyState>();
        public float recordInterval = 0.050f;

        [Tooltip("(Optional) performance optimization where FixedUpdate.RecordState() only inserts state into history if the state actually changed.\nThis is generally a good idea.")]
        public bool onlyRecordChanges = true;

        [Tooltip("(Optional) performance optimization where received state is compared to the LAST recorded state first, before sampling the whole history.\n\nThis can save significant traversal overhead for idle objects with a tiny chance of missing corrections for objects which revisisted the same position in the recent history twice.")]
        public bool compareLastFirst = true;

        [Header("Reconciliation")]
        [Tooltip("Correction threshold in meters. For example, 0.1 means that if the client is off by more than 10cm, it gets corrected.")]
        public double positionCorrectionThreshold = 0.10;
        [Tooltip("Correction threshold in degrees. For example, 5 means that if the client is off by more than 5 degrees, it gets corrected.")]
        public double rotationCorrectionThreshold = 5;

        [Tooltip("Applying server corrections one frame ahead gives much better results. We don't know why yet, so this is an option for now.")]
        public bool oneFrameAhead = true;

        [Header("Smoothing")]
        [Tooltip("Configure how to apply the corrected state.")]
        public CorrectionMode correctionMode = CorrectionMode.Move;

        [Tooltip("Snap to the server state directly when velocity is < threshold. This is useful to reduce jitter/fighting effects before coming to rest.\nNote this applies position, rotation and velocity(!) so it's still smooth.")]
        public float snapThreshold = 2; // 0.5 has too much fighting-at-rest, 2 seems ideal.

        [Header("Visual Interpolation")]
        [Tooltip("After creating the visual interpolation object, keep showing the original Rigidbody with a ghost (transparent) material for debugging.")]
        public bool showGhost = true;
        public float ghostDistanceThreshold = 0.1f;
        public float ghostEnabledCheckInterval = 0.2f;

        [Tooltip("After creating the visual interpolation object, replace this object's renderer materials with the ghost (ideally transparent) material.")]
        public Material localGhostMaterial;
        public Material remoteGhostMaterial;

        [Tooltip("How fast to interpolate to the target position, relative to how far we are away from it.\nHigher value will be more jitter but sharper moves, lower value will be less jitter but a little too smooth / rounded moves.")]
        public float positionInterpolationSpeed = 15; // 10 is a little too low for billiards at least
        public float rotationInterpolationSpeed = 10;

        [Tooltip("Teleport if we are further than 'multiplier x collider size' behind.")]
        public float teleportDistanceMultiplier = 10;

        [Header("Bandwidth")]
        [Tooltip("Reduce sends while velocity==0. Client's objects may slightly move due to gravity/physics, so we still want to send corrections occasionally even if an object is idle on the server the whole time.")]
        public bool reduceSendsWhileIdle = true;

        [Header("Debugging")]
        public float lineTime = 10;

        // Rigidbody & Collider are moved out into a separate object.
        // this way the visual object can smoothly follow.
        protected GameObject physicsCopy;
        protected Transform physicsCopyTransform; // caching to avoid GetComponent
        protected Rigidbody physicsCopyRigidbody; // caching to avoid GetComponent
        protected Collider physicsCopyCollider;   // caching to avoid GetComponent
        float smoothFollowThreshold;    // caching to avoid calculation in LateUpdate

        // we also create one extra ghost for the exact known server state.
        protected GameObject remoteCopy;

        void Awake()
        {
            tf = transform;
            rb = GetComponent<Rigidbody>();
            if (rb == null) throw new InvalidOperationException($"Prediction: {name} is missing a Rigidbody component.");
        }

        protected virtual void CopyRenderersAsGhost(GameObject destination, Material material)
        {
            // find the MeshRenderer component, which sometimes is on a child.
            MeshRenderer originalMeshRenderer = GetComponentInChildren<MeshRenderer>(true);
            MeshFilter originalMeshFilter = GetComponentInChildren<MeshFilter>(true);
            if (originalMeshRenderer != null && originalMeshFilter != null)
            {
                MeshFilter meshFilter = destination.AddComponent<MeshFilter>();
                meshFilter.mesh = originalMeshFilter.mesh;

                MeshRenderer meshRenderer = destination.AddComponent<MeshRenderer>();
                meshRenderer.material = originalMeshRenderer.material;

                // renderers often have multiple materials. copy all.
                if (originalMeshRenderer.materials != null)
                {
                    Material[] materials = new Material[originalMeshRenderer.materials.Length];
                    for (int i = 0; i < materials.Length; ++i)
                    {
                        materials[i] = material;
                    }
                    meshRenderer.materials = materials; // need to reassign to see it in effect
                }
            }
            // if we didn't find a renderer, show a warning
            else Debug.LogWarning($"PredictedRigidbody: {name} found no renderer to copy onto the visual object. If you are using a custom setup, please overwrite PredictedRigidbody.CreateVisualCopy().");
        }

        // instantiate a physics-only copy of the gameobject to apply corrections.
        // this way the main visual object can smoothly follow.
        // it's best to separate the physics instead of separating the renderers.
        // some projects have complex rendering / animation setups which we can't touch.
        // besides, Rigidbody+Collider are two components, where as renders may be many.
        protected virtual void CreateGhosts()
        {
            // skip if host mode or already separated
            if (isServer || physicsCopy != null) return;

            Debug.Log($"Separating Physics for {name}");

            // create an empty GameObject with the same name + _Physical
            // it's important to copy world position/rotation/scale, not local!
            // because the original object may be a child of another.
            //
            // for example:
            //    parent (scale=1.5)
            //      child (scale=0.5)
            //
            // if we copy localScale then the copy has scale=0.5, where as the
            // original would have a global scale of ~1.0.
            physicsCopy = new GameObject($"{name}_Physical");
            physicsCopy.transform.position = tf.position;     // world position!
            physicsCopy.transform.rotation = tf.rotation;     // world rotation!
            physicsCopy.transform.localScale = tf.lossyScale; // world scale!

            // assign the same Layer for the physics copy.
            // games may use a custom physics collision matrix, layer matters.
            physicsCopy.layer = gameObject.layer;

            // add the PredictedRigidbodyPhysical component
            PredictedRigidbodyPhysicsGhost physicsGhostRigidbody = physicsCopy.AddComponent<PredictedRigidbodyPhysicsGhost>();
            physicsGhostRigidbody.target = tf;
            physicsGhostRigidbody.ghostDistanceThreshold = ghostDistanceThreshold;
            physicsGhostRigidbody.ghostEnabledCheckInterval = ghostEnabledCheckInterval;

            // move the rigidbody component & all colliders to the physics GameObject
            PredictionUtils.MovePhysicsComponents(gameObject, physicsCopy);

            // show ghost by copying all renderers / materials with ghost material applied
            if (showGhost)
            {
                // one for the locally predicted rigidbody
                CopyRenderersAsGhost(physicsCopy, localGhostMaterial);
                physicsGhostRigidbody.ghostDistanceThreshold = ghostDistanceThreshold;
                physicsGhostRigidbody.ghostEnabledCheckInterval = ghostEnabledCheckInterval;

                // one for the latest remote state for comparison
                // it's important to copy world position/rotation/scale, not local!
                // because the original object may be a child of another.
                //
                // for example:
                //    parent (scale=1.5)
                //      child (scale=0.5)
                //
                // if we copy localScale then the copy has scale=0.5, where as the
                // original would have a global scale of ~1.0.
                remoteCopy = new GameObject($"{name}_Remote");
                remoteCopy.transform.position = tf.position;     // world position!
                remoteCopy.transform.rotation = tf.rotation;     // world rotation!
                remoteCopy.transform.localScale = tf.lossyScale; // world scale!
                PredictedRigidbodyRemoteGhost predictedGhost = remoteCopy.AddComponent<PredictedRigidbodyRemoteGhost>();
                predictedGhost.target = tf;
                predictedGhost.ghostDistanceThreshold = ghostDistanceThreshold;
                predictedGhost.ghostEnabledCheckInterval = ghostEnabledCheckInterval;
                CopyRenderersAsGhost(remoteCopy, remoteGhostMaterial);
            }

            // cache components to avoid GetComponent calls at runtime
            physicsCopyTransform = physicsCopy.transform;
            physicsCopyRigidbody = physicsCopy.GetComponent<Rigidbody>();
            physicsCopyCollider = physicsCopy.GetComponentInChildren<Collider>();
            if (physicsCopyRigidbody == null) throw new Exception("SeparatePhysics: couldn't find final Rigidbody.");
            if (physicsCopyCollider == null) throw new Exception("SeparatePhysics: couldn't find final Collider.");

            // cache some threshold to avoid calculating them in LateUpdate
            float colliderSize = physicsCopyCollider.bounds.size.magnitude;
            smoothFollowThreshold = colliderSize * teleportDistanceMultiplier;
        }

        protected virtual void DestroyGhosts()
        {
            // move the copy's Rigidbody back onto self.
            // important for scene objects which may be reused for AOI spawn/despawn.
            // otherwise next time they wouldn't have a collider anymore.
            if (physicsCopy != null)
            {
                PredictionUtils.MovePhysicsComponents(physicsCopy, gameObject);
                Destroy(physicsCopy);
            }

            // simply destroy the remote copy
            if (remoteCopy != null) Destroy(remoteCopy);
        }

        // this shows in profiler LateUpdates! need to make this as fast as possible!
        protected virtual void SmoothFollowPhysicsCopy()
        {
            // hard follow:
            // tf.position = physicsCopyCollider.position;
            // tf.rotation = physicsCopyCollider.rotation;

            // ORIGINAL VERSION: CLEAN AND SIMPLE
            /*
            // if we are further than N colliders sizes behind, then teleport
            float colliderSize = physicsCopyCollider.bounds.size.magnitude;
            float threshold = colliderSize * teleportDistanceMultiplier;
            float distance = Vector3.Distance(tf.position, physicsCopyRigidbody.position);
            if (distance > threshold)
            {
                tf.position = physicsCopyRigidbody.position;
                tf.rotation = physicsCopyRigidbody.rotation;
                Debug.Log($"[PredictedRigidbody] Teleported because distance to physics copy = {distance:F2} > threshold {threshold:F2}");
                return;
            }

            // smoothly interpolate to the target position.
            // speed relative to how far away we are
            float positionStep = distance * positionInterpolationSpeed;
            tf.position = Vector3.MoveTowards(tf.position, physicsCopyRigidbody.position, positionStep * Time.deltaTime);

            // smoothly interpolate to the target rotation.
            // Quaternion.RotateTowards doesn't seem to work at all, so let's use SLerp.
            tf.rotation = Quaternion.Slerp(tf.rotation, physicsCopyRigidbody.rotation, rotationInterpolationSpeed * Time.deltaTime);
            */

            // FAST VERSION: this shows in profiler a lot, so cache EVERYTHING!
            tf.GetPositionAndRotation(out Vector3 currentPosition, out Quaternion currentRotation);                   // faster than tf.position + tf.rotation
            physicsCopyTransform.GetPositionAndRotation(out Vector3 physicsPosition, out Quaternion physicsRotation); // faster than physicsCopyRigidbody.position + physicsCopyRigidbody.rotation
            float deltaTime = Time.deltaTime;

            float distance = Vector3.Distance(currentPosition, physicsPosition);
            if (distance > smoothFollowThreshold)
            {
                tf.SetPositionAndRotation(physicsPosition, physicsRotation); // faster than .position and .rotation manually
                Debug.Log($"[PredictedRigidbody] Teleported because distance to physics copy = {distance:F2} > threshold {smoothFollowThreshold:F2}");
                return;
            }

            // smoothly interpolate to the target position.
            // speed relative to how far away we are.
            // => speed increases by distance² because the further away, the
            //    sooner we need to catch the fuck up
            // float positionStep = (distance * distance) * interpolationSpeed;
            float positionStep = distance * positionInterpolationSpeed;
            Vector3 newPosition = Vector3.MoveTowards(currentPosition, physicsPosition, positionStep * deltaTime);

            // smoothly interpolate to the target rotation.
            // Quaternion.RotateTowards doesn't seem to work at all, so let's use SLerp.
            Quaternion newRotation = Quaternion.Slerp(currentRotation, physicsRotation, rotationInterpolationSpeed * deltaTime);

            // assign position and rotation together. faster than accessing manually.
            tf.SetPositionAndRotation(newPosition, newRotation);
        }

        // creater visual copy only on clients, where players are watching.
        public override void OnStartClient()
        {
            // OnDeserialize may have already created this
            if (physicsCopy == null) CreateGhosts();
        }

        // destroy visual copy only in OnStopClient().
        // OnDestroy() wouldn't be called for scene objects that are only disabled instead of destroyed.
        public override void OnStopClient()
        {
            DestroyGhosts();
        }

        void UpdateServer()
        {
            // bandwidth optimization while idle.
            if (reduceSendsWhileIdle)
            {
                // while moving, always sync every frame for immediate corrections.
                // while idle, only sync once per second.
                //
                // we still need to sync occasionally because objects on client
                // may still slide or move slightly due to gravity, physics etc.
                // and those still need to get corrected if not moving on server.
                //
                // TODO
                // next round of optimizations: if client received nothing for 1s,
                // force correct to last received state. then server doesn't need
                // to send once per second anymore.
                bool moving = rb.velocity != Vector3.zero; // on server, always use .rb. it has no physicsRigidbody.
                syncInterval = moving ? 0 : 1;
            }

            // always set dirty to always serialize in next sync interval.
            SetDirty();
        }

        void Update()
        {
            if (isServer) UpdateServer();
        }

        void LateUpdate()
        {
            // only follow on client-only, not in server or host mode
            if (isClientOnly) SmoothFollowPhysicsCopy();
        }

        void FixedUpdate()
        {
            // on clients (not host) we record the current state every FixedUpdate.
            // this is cheap, and allows us to keep a dense history.
            if (isClientOnly)
            {
                // OPTIMIZATION: RecordState() is expensive because it inserts into a SortedList.
                // only record if state actually changed!
                // risks not having up to date states when correcting,
                // but it doesn't matter since we'll always compare with the 'newest' anyway.
                //
                // we check in here instead of in RecordState() because RecordState() should definitely record if we call it!
                if (onlyRecordChanges)
                {
                    // TODO maybe don't reuse the correction thresholds?
                    tf.GetPositionAndRotation(out Vector3 position, out Quaternion rotation);
                    if (Vector3.Distance(lastRecorded.position, position) < positionCorrectionThreshold &&
                        Quaternion.Angle(lastRecorded.rotation, rotation) < rotationCorrectionThreshold)
                    {
                        // Debug.Log($"FixedUpdate for {name}: taking optimized early return instead of recording state.");
                        return;
                    }
                }

                RecordState();
            }
        }

        // manually store last recorded so we can easily check against this
        // without traversing the SortedList.
        RigidbodyState lastRecorded;
        double lastRecordTime;
        void RecordState()
        {
            // instead of recording every fixedupdate, let's record in an interval.
            // we don't want to record every tiny move and correct too hard.
            if (NetworkTime.time < lastRecordTime + recordInterval) return;
            lastRecordTime = NetworkTime.time;

            // NetworkTime.time is always behind by bufferTime.
            // prediction aims to be on the exact same server time (immediately).
            // use predictedTime to record state, otherwise we would record in the past.
            double predictedTime = NetworkTime.predictedTime;

            // FixedUpdate may run twice in the same frame / NetworkTime.time.
            // for now, simply don't record if already recorded there.
            // previously we checked ContainsKey which is O(logN) for SortedList
            //   if (stateHistory.ContainsKey(predictedTime))
            //       return;
            // instead, simply store the last recorded time and don't insert if same.
            if (predictedTime == lastRecorded.timestamp) return;

            // keep state history within limit
            if (stateHistory.Count >= stateHistoryLimit)
                stateHistory.RemoveAt(0);

            // grab current position/rotation/velocity only once.
            // this is performance critical, avoid calling .transform multiple times.
            tf.GetPositionAndRotation(out Vector3 currentPosition, out Quaternion currentRotation); // faster than accessing .position + .rotation manually
            Vector3 currentVelocity = physicsCopyRigidbody.velocity;

            // calculate delta to previous state (if any)
            Vector3 positionDelta = Vector3.zero;
            Vector3 velocityDelta = Vector3.zero;
            // Quaternion rotationDelta = Quaternion.identity; // currently unused
            if (stateHistory.Count > 0)
            {
                RigidbodyState last = stateHistory.Values[stateHistory.Count - 1];
                positionDelta = currentPosition - last.position;
                velocityDelta = currentVelocity - last.velocity;
                // rotationDelta = currentRotation * Quaternion.Inverse(last.rotation); // this is how you calculate a quaternion delta (currently unused, don't do the computation)

                // debug draw the recorded state
                // Debug.DrawLine(last.position, currentPosition, Color.red, lineTime);
            }

            // create state to insert
            RigidbodyState state = new RigidbodyState(
                predictedTime,
                positionDelta,
                currentPosition,
                // rotationDelta, // currently unused
                currentRotation,
                velocityDelta,
                currentVelocity
            );

            // add state to history
            stateHistory.Add(predictedTime, state);

            // manually remember last inserted state for faster .Last comparisons
            lastRecorded = state;
        }

        // optional user callbacks, in case people need to know about events.
        protected virtual void OnSnappedIntoPlace() {}
        protected virtual void OnCorrected() {}

        void ApplyState(double timestamp, Vector3 position, Quaternion rotation, Vector3 velocity)
        {
            // fix rigidbodies seemingly dancing in place instead of coming to rest.
            // hard snap to the position below a threshold velocity.
            // this is fine because the visual object still smoothly interpolates to it.
            if (physicsCopyRigidbody.velocity.magnitude <= snapThreshold)
            {
                // Debug.Log($"Prediction: snapped {name} into place because velocity {physicsCopyRigidbody.velocity.magnitude:F3} <= {snapThreshold:F3}");

                // apply server state immediately.
                // important to apply velocity as well, instead of Vector3.zero.
                // in case an object is still slightly moving, we don't want it
                // to stop and start moving again on client - slide as well here.
                physicsCopyRigidbody.position = position;
                physicsCopyRigidbody.rotation = rotation;
                physicsCopyRigidbody.velocity = velocity;

                // clear history and insert the exact state we just applied.
                // this makes future corrections more accurate.
                stateHistory.Clear();
                stateHistory.Add(timestamp, new RigidbodyState(
                    timestamp,
                    Vector3.zero,
                    position,
                    // Quaternion.identity, // rotationDelta: currently unused
                    rotation,
                    Vector3.zero,
                    velocity
                ));

                // user callback
                OnSnappedIntoPlace();
                return;
            }

            // Rigidbody .position teleports, while .MovePosition interpolates
            // TODO is this a good idea? what about next capture while it's interpolating?
            if (correctionMode == CorrectionMode.Move)
            {
                physicsCopyRigidbody.MovePosition(position);
                physicsCopyRigidbody.MoveRotation(rotation);
            }
            else if (correctionMode == CorrectionMode.Set)
            {
                physicsCopyRigidbody.position = position;
                physicsCopyRigidbody.rotation = rotation;
            }

            // there's only one way to set velocity
            physicsCopyRigidbody.velocity = velocity;
        }

        // process a received server state.
        // compares it against our history and applies corrections if needed.
        void OnReceivedState(double timestamp, RigidbodyState state)
        {
            // always update remote state ghost
            if (remoteCopy != null)
            {
                Transform remoteCopyTransform = remoteCopy.transform;
                remoteCopyTransform.SetPositionAndRotation(state.position, state.rotation); // faster than .position + .rotation setters
                remoteCopyTransform.localScale = tf.lossyScale; // world scale! see CreateGhosts comment.
            }

            // OPTIONAL performance optimization when comparing idle objects.
            // even idle objects will have a history of ~32 entries.
            // sampling & traversing through them is unnecessarily costly.
            // instead, compare directly against the current rigidbody position!
            // => this is technically not 100% correct if an object runs in
            //    circles where it may revisit the same position twice.
            // => but practically, objects that didn't move will have their
            //    whole history look like the last inserted state.
            // => comparing against that is free and gives us a significant
            //    performance saving vs. a tiny chance of incorrect results due
            //    to objects running in circles.
            // => the RecordState() call below is expensive too, so we want to
            //    do this before even recording the latest state. the only way
            //    to do this (in case last recorded state is too old), is to
            //    compare against live rigidbody.position without any recording.
            //    this is as fast as it gets for skipping idle objects.
            //
            // if this ever causes issues, feel free to disable it.
            if (compareLastFirst &&
                Vector3.Distance(state.position, physicsCopyRigidbody.position) < positionCorrectionThreshold &&
                Quaternion.Angle(state.rotation, physicsCopyRigidbody.rotation) < rotationCorrectionThreshold)
            {
                // Debug.Log($"OnReceivedState for {name}: taking optimized early return!");
                return;
            }

            // we only capture state every 'interval' milliseconds.
            // so the newest entry in 'history' may be up to 'interval' behind 'now'.
            // if there's no latency, we may receive a server state for 'now'.
            // sampling would fail, if we haven't recorded anything in a while.
            // to solve this, always record the current state when receiving a server state.
            RecordState();

            // correction requires at least 2 existing states for 'before' and 'after'.
            // if we don't have two yet, drop this state and try again next time once we recorded more.
            if (stateHistory.Count < 2) return;

            RigidbodyState oldest = stateHistory.Values[0];
            RigidbodyState newest = stateHistory.Values[stateHistory.Count - 1];

            // edge case: is the state older than the oldest state in history?
            // this can happen if the client gets so far behind the server
            // that it doesn't have a recored history to sample from.
            // in that case, we should hard correct the client.
            // otherwise it could be out of sync as long as it's too far behind.
            if (state.timestamp < oldest.timestamp)
            {
                // when starting, client may only have 2-3 states in history.
                // it's expected that server states would be behind those 2-3.
                // only show a warning if it's behind the full history limit!
                if (stateHistory.Count >= stateHistoryLimit)
                    Debug.LogWarning($"Hard correcting client object {name} because the client is too far behind the server. History of size={stateHistory.Count} @ t={timestamp:F3} oldest={oldest.timestamp:F3} newest={newest.timestamp:F3}. This would cause the client to be out of sync as long as it's behind.");

                // force apply the state
                ApplyState(state.timestamp, state.position, state.rotation, state.velocity);
                return;
            }

            // edge case: is it newer than the newest state in history?
            // this can happen if client's predictedTime predicts too far ahead of the server.
            // in that case, log a warning for now but still apply the correction.
            // otherwise it could be out of sync as long as it's too far ahead.
            //
            // for example, when running prediction on the same machine with near zero latency.
            // when applying corrections here, this looks just fine on the local machine.
            if (newest.timestamp < state.timestamp)
            {
                // the correction is for a state in the future.
                // we clamp it to 'now'.
                // but only correct if off by threshold.
                // TODO maybe we should interpolate this back to 'now'?
                if (Vector3.Distance(state.position, physicsCopyRigidbody.position) >= positionCorrectionThreshold)
                {
                    double ahead = state.timestamp - newest.timestamp;
                    Debug.Log($"Hard correction because the client is ahead of the server by {(ahead*1000):F1}ms. History of size={stateHistory.Count} @ t={timestamp:F3} oldest={oldest.timestamp:F3} newest={newest.timestamp:F3}. This can happen when latency is near zero, and is fine unless it shows jitter.");
                    ApplyState(state.timestamp, state.position, state.rotation, state.velocity);
                }
                return;
            }

            // find the two closest client states between timestamp
            if (!Prediction.Sample(stateHistory, timestamp, out RigidbodyState before, out RigidbodyState after, out int afterIndex, out double t))
            {
                // something went very wrong. sampling should've worked.
                // hard correct to recover the error.
                Debug.LogError($"Failed to sample history of size={stateHistory.Count} @ t={timestamp:F3} oldest={oldest.timestamp:F3} newest={newest.timestamp:F3}. This should never happen because the timestamp is within history.");
                ApplyState(state.timestamp, state.position, state.rotation, state.velocity);
                return;
            }

            // interpolate between them to get the best approximation
            RigidbodyState interpolated = RigidbodyState.Interpolate(before, after, (float)t);

            // calculate the difference between where we were and where we should be
            // TODO only position for now. consider rotation etc. too later
            float positionDifference = Vector3.Distance(state.position, interpolated.position);
            float rotationDifference = Quaternion.Angle(state.rotation, interpolated.rotation);
            // Debug.Log($"Sampled history of size={stateHistory.Count} @ {timestamp:F3}: client={interpolated.position} server={state.position} difference={difference:F3} / {correctionThreshold:F3}");

            // too far off? then correct it
            if (positionDifference >= positionCorrectionThreshold ||
                rotationDifference >= rotationCorrectionThreshold)
            {
                // Debug.Log($"CORRECTION NEEDED FOR {name} @ {timestamp:F3}: client={interpolated.position} server={state.position} difference={difference:F3}");

                // show the received correction position + velocity for debugging.
                // helps to compare with the interpolated/applied correction locally.
                //Debug.DrawLine(state.position, state.position + state.velocity * 0.1f, Color.white, lineTime);

                // insert the correction and correct the history on top of it.
                // returns the final recomputed state after rewinding.
                RigidbodyState recomputed = Prediction.CorrectHistory(stateHistory, stateHistoryLimit, state, before, after, afterIndex);

                // log, draw & apply the final position.
                // always do this here, not when iterating above, in case we aren't iterating.
                // for example, on same machine with near zero latency.
                // int correctedAmount = stateHistory.Count - afterIndex;
                // Debug.Log($"Correcting {name}: {correctedAmount} / {stateHistory.Count} states to final position from: {rb.position} to: {last.position}");
                //Debug.DrawLine(physicsCopyRigidbody.position, recomputed.position, Color.green, lineTime);
                ApplyState(recomputed.timestamp, recomputed.position, recomputed.rotation, recomputed.velocity);

                // user callback
                OnCorrected();
            }
        }

        // send state to clients every sendInterval.
        // reliable for now.
        // TODO we should use the one from FixedUpdate
        public override void OnSerialize(NetworkWriter writer, bool initialState)
        {
            // Time.time was at the beginning of this frame.
            // NetworkLateUpdate->Broadcast->OnSerialize is at the end of the frame.
            // as result, client should use this to correct the _next_ frame.
            // otherwise we see noticeable resets that seem off by one frame.
            //
            // to solve this, we can send the current deltaTime.
            // server is technically supposed to be at a fixed frame rate, but this can vary.
            // sending server's current deltaTime is the safest option.
            // client then applies it on top of remoteTimestamp.


            // FAST VERSION: this shows in profiler a lot, so cache EVERYTHING!
            tf.GetPositionAndRotation(out Vector3 position, out Quaternion rotation);  // faster than tf.position + tf.rotation. server's rigidbody is on the same transform.
            writer.WriteFloat(Time.deltaTime);
            writer.WriteVector3(position);
            writer.WriteQuaternion(rotation);
            writer.WriteVector3(rb.velocity); // own rigidbody on server, it's never moved to physics copy
        }

        // read the server's state, compare with client state & correct if necessary.
        public override void OnDeserialize(NetworkReader reader, bool initialState)
        {
            // this may be called before OnStartClient.
            // in that case, separate physics first before applying state.
            if (physicsCopy == null) CreateGhosts();

            // deserialize data
            // we want to know the time on the server when this was sent, which is remoteTimestamp.
            double timestamp = NetworkClient.connection.remoteTimeStamp;

            // server send state at the end of the frame.
            // parse and apply the server's delta time to our timestamp.
            // otherwise we see noticeable resets that seem off by one frame.
            double serverDeltaTime = reader.ReadFloat();
            timestamp += serverDeltaTime;

            // however, adding yet one more frame delay gives much(!) better results.
            // we don't know why yet, so keep this as an option for now.
            // possibly because client captures at the beginning of the frame,
            // with physics happening at the end of the frame?
            if (oneFrameAhead) timestamp += serverDeltaTime;

            // parse state
            Vector3 position    = reader.ReadVector3();
            Quaternion rotation = reader.ReadQuaternion();
            Vector3 velocity    = reader.ReadVector3();

            // process received state
            OnReceivedState(timestamp, new RigidbodyState(timestamp, Vector3.zero, position, /*Quaternion.identity,*/ rotation, Vector3.zero, velocity));
        }

        protected override void OnValidate()
        {
            base.OnValidate();

            // force syncDirection to be ServerToClient
            syncDirection = SyncDirection.ServerToClient;

            // state should be synced immediately for now.
            // later when we have prediction fully dialed in,
            // then we can maybe relax this a bit.
            syncInterval = 0;
        }
    }
}