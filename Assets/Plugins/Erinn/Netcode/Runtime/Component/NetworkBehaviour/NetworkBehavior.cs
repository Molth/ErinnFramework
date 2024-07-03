//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using Unity.Netcode;
using UnityEngine;

namespace Erinn
{
    /// <summary>
    ///     Network Entity Base Class
    /// </summary>
    public abstract class NetworkBehavior : NetworkBehaviour, INetworkIdentity, IActor
    {
        /// <summary>
        ///     Obtain ClientRpcParams
        /// </summary>
        /// <returns>Obtained ClientRpcParams</returns>
        public ClientRpcParams OwnerClientRpcParams => new() { Send = new ClientRpcSendParams { TargetClientIds = new[] { OwnerClientId } } };

        /// <summary>
        ///     Server permission verification
        /// </summary>
        /// <returns>Having permissions</returns>
        public virtual bool IsServerAuthoritative() => IsOwner;

        /// <summary>
        ///     Determines if the specified clientId matches the owner
        /// </summary>
        /// <param name="ownerClientId">The clientId to check</param>
        /// <returns>Whether the specified clientId matches the owner</returns>
        public bool WasOwner(ulong ownerClientId) => NetworkManager.LocalClientId == ownerClientId;

        /// <summary>
        ///     Initialization
        /// </summary>
        public void InitializeStateMachineBehaviours<T>(Animator animator) where T : StateMachineBehaviour, IStateMachineBehaviourInitialize
        {
            foreach (var behaviour in animator.GetBehaviours<T>())
                behaviour.Initialize(this);
        }

        /// <summary>
        ///     Clean up
        /// </summary>
        public void DisposeStateMachineBehaviours<T>(Animator animator) where T : StateMachineBehaviour, IStateMachineBehaviourInitialize
        {
            foreach (var behaviour in animator.GetBehaviours<T>())
                behaviour.Dispose();
        }

        /// <summary>
        ///     Called during network generation
        /// </summary>
        public override void OnNetworkSpawn()
        {
            if (IsServer)
                OnStartServer();
            if (IsOwner)
                OnStartOwner();
            if (IsLocalPlayer)
                OnStartLocalPlayer();
        }

        /// <summary>
        ///     Server Called during network generation
        /// </summary>
        public virtual void OnStartServer()
        {
        }

        /// <summary>
        ///     Owner Called during network generation
        /// </summary>
        public virtual void OnStartOwner()
        {
        }

        /// <summary>
        ///     LocalPlayer Called during network generation
        /// </summary>
        public virtual void OnStartLocalPlayer()
        {
        }

        /// <summary>
        ///     Call upon network destruction
        /// </summary>
        public override void OnNetworkDespawn()
        {
            if (IsServer)
                OnStopServer();
            if (IsOwner)
                OnStopOwner();
            if (IsLocalPlayer)
                OnStopLocalPlayer();
        }

        /// <summary>
        ///     Server Call upon network destruction
        /// </summary>
        public virtual void OnStopServer()
        {
        }

        /// <summary>
        ///     Owner Call upon network destruction
        /// </summary>
        public virtual void OnStopOwner()
        {
        }

        /// <summary>
        ///     LocalPlayer Call upon network destruction
        /// </summary>
        public virtual void OnStopLocalPlayer()
        {
        }

        /// <summary>
        ///     Update When calling
        /// </summary>
        public virtual void OnUpdate()
        {
            if (IsServer)
                OnUpdateServer();
            if (IsOwner)
                OnUpdateOwner();
            if (IsLocalPlayer)
                OnUpdateLocalPlayer();
        }

        /// <summary>
        ///     Server Update When calling
        /// </summary>
        public virtual void OnUpdateServer()
        {
        }

        /// <summary>
        ///     Owner Update When calling
        /// </summary>
        public virtual void OnUpdateOwner()
        {
        }

        /// <summary>
        ///     LocalPlayer Update When calling
        /// </summary>
        public virtual void OnUpdateLocalPlayer()
        {
        }

        /// <summary>
        ///     LateUpdate When calling
        /// </summary>
        public virtual void OnLateUpdate()
        {
            if (IsServer)
                OnLateUpdateServer();
            if (IsOwner)
                OnLateUpdateOwner();
            if (IsLocalPlayer)
                OnLateUpdateLocalPlayer();
        }

        /// <summary>
        ///     Server LateUpdate When calling
        /// </summary>
        public virtual void OnLateUpdateServer()
        {
        }

        /// <summary>
        ///     Owner LateUpdate When calling
        /// </summary>
        public virtual void OnLateUpdateOwner()
        {
        }

        /// <summary>
        ///     LocalPlayer LateUpdate When calling
        /// </summary>
        public virtual void OnLateUpdateLocalPlayer()
        {
        }

        /// <summary>
        ///     FixedUpdate When calling
        /// </summary>
        public virtual void OnFixedUpdate()
        {
            if (IsServer)
                OnFixedUpdateServer();
            if (IsOwner)
                OnFixedUpdateOwner();
            if (IsLocalPlayer)
                OnFixedUpdateLocalPlayer();
        }

        /// <summary>
        ///     Server FixedUpdate When calling
        /// </summary>
        public virtual void OnFixedUpdateServer()
        {
        }

        /// <summary>
        ///     Owner FixedUpdate When calling
        /// </summary>
        public virtual void OnFixedUpdateOwner()
        {
        }

        /// <summary>
        ///     LocalPlayer FixedUpdate When calling
        /// </summary>
        public virtual void OnFixedUpdateLocalPlayer()
        {
        }
    }
}