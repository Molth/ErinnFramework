using System;
using UnityEngine;

namespace Mirror
{
    // need to send time every sendInterval.
    // batching automatically includes remoteTimestamp.
    // all we need to do is ensure that an empty message is sent.
    // and react to it.
    // => we don't want to insert a snapshot on every batch.
    // => do it exactly every sendInterval on every TimeSnapshotMessage.
    public struct TimeSnapshotMessage : INetworkMessage {}

    public struct ReadyMessage : INetworkMessage {}

    public struct NotReadyMessage : INetworkMessage {}

    public struct AddPlayerMessage : INetworkMessage {}

    public struct SceneMessage : INetworkMessage
    {
        public string sceneName;
        // Normal = 0, LoadAdditive = 1, UnloadAdditive = 2
        public SceneOperation sceneOperation;
        public bool customHandling;
    }

    public enum SceneOperation : byte
    {
        Normal,
        LoadAdditive,
        UnloadAdditive
    }

    public struct CommandMessage : INetworkMessage
    {
        public uint netId;
        public byte componentIndex;
        public ushort functionHash;
        // the parameters for the Cmd function
        // -> ArraySegment to avoid unnecessary allocations
        public ArraySegment<byte> payload;
    }

    public struct RpcMessage : INetworkMessage
    {
        public uint netId;
        public byte componentIndex;
        public ushort functionHash;
        // the parameters for the Cmd function
        // -> ArraySegment to avoid unnecessary allocations
        public ArraySegment<byte> payload;
    }

    public struct SpawnMessage : INetworkMessage
    {
        // netId of new or existing object
        public uint netId;
        public bool isLocalPlayer;
        // Sets hasAuthority on the spawned object
        public bool isOwner;
        public ulong sceneId;
        // If sceneId != 0 then it is used instead of assetId
        public uint assetId;
        // Local position
        public Vector3 position;
        // Local rotation
        public Quaternion rotation;
        // Local scale
        public Vector3 scale;
        // serialized component data
        // ArraySegment to avoid unnecessary allocations
        public ArraySegment<byte> payload;
    }

    public struct ChangeOwnerMessage : INetworkMessage
    {
        public uint netId;
        public bool isOwner;
        public bool isLocalPlayer;
    }

    public struct ObjectSpawnStartedMessage : INetworkMessage {}

    public struct ObjectSpawnFinishedMessage : INetworkMessage {}

    public struct ObjectDestroyMessage : INetworkMessage
    {
        public uint netId;
    }

    public struct ObjectHideMessage : INetworkMessage
    {
        public uint netId;
    }

    public struct EntityStateMessage : INetworkMessage
    {
        public uint netId;
        // the serialized component data
        // -> ArraySegment to avoid unnecessary allocations
        public ArraySegment<byte> payload;
    }

    // whoever wants to measure rtt, sends this to the other end.
    public struct NetworkPingMessage : INetworkMessage
    {
        // local time is used to calculate round trip time,
        // and to calculate the predicted time offset.
        public double localTime;

        // predicted time is sent to compare the final error, for debugging only
        public double predictedTimeAdjusted;

        public NetworkPingMessage(double localTime, double predictedTimeAdjusted)
        {
            this.localTime = localTime;
            this.predictedTimeAdjusted = predictedTimeAdjusted;
        }
    }

    // the other end responds with this message.
    // we can use this to calculate rtt.
    public struct NetworkPongMessage : INetworkMessage
    {
        // local time is used to calculate round trip time.
        public double localTime;

        // predicted error is used to adjust the predicted timeline.
        public double predictionErrorUnadjusted;
        public double predictionErrorAdjusted; // for debug purposes

        public NetworkPongMessage(double localTime, double predictionErrorUnadjusted, double predictionErrorAdjusted)
        {
            this.localTime = localTime;
            this.predictionErrorUnadjusted = predictionErrorUnadjusted;
            this.predictionErrorAdjusted = predictionErrorAdjusted;
        }
    }
}
