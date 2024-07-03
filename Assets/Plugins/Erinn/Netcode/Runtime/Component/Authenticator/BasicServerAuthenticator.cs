//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using Sirenix.OdinInspector;
using Unity.Netcode;
using UnityEngine;

namespace Erinn
{
    /// <summary>
    ///     Server validator
    /// </summary>
    [DisallowMultipleComponent]
    public class BasicServerAuthenticator : MonoBehaviour
    {
        /// <summary>
        ///     Response data
        /// </summary>
        [LabelText("Response data")] public ApprovalResponseData ResponseMessage;

        /// <summary>
        ///     Start When calling
        /// </summary>
        private void Start()
        {
            NetcodeSystem.SetConnectionApprovalCallback(ApprovalCheck);
            NetcodeSystem.SetConnectionApproval(true);
        }

        /// <summary>
        ///     Server verifies client connection response
        /// </summary>
        /// <param name="request">Client request</param>
        /// <param name="response">Client response</param>
        protected virtual void ApprovalCheck(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response) => ServerAccept(response);

        /// <summary>
        ///     Server settings response data
        /// </summary>
        /// <param name="response">Response</param>
        protected void ServerResponse(NetworkManager.ConnectionApprovalResponse response)
        {
            response.CreatePlayerObject = ResponseMessage.CreatePlayerObject;
            if (ResponseMessage.CreatePlayerObject)
            {
                if (ResponseMessage.EnablePlayerPrefabHash)
                    response.PlayerPrefabHash = ResponseMessage.PlayerPrefabHash;
                if (ResponseMessage.EnablePosition)
                    response.Position = ResponseMessage.Position;
                if (ResponseMessage.EnableRotation)
                    response.Rotation = ResponseMessage.Rotation;
            }
        }

        /// <summary>
        ///     Server accepts clients
        /// </summary>
        protected void ServerAccept(NetworkManager.ConnectionApprovalResponse response)
        {
            response.Approved = true;
            response.Pending = false;
        }

        /// <summary>
        ///     Server rejects client
        /// </summary>
        protected void ServerReject(NetworkManager.ConnectionApprovalResponse response, string reason = null)
        {
            response.Approved = false;
            response.Reason = reason;
            response.Pending = false;
        }

        /// <summary>
        ///     Verify response data
        /// </summary>
        [Serializable]
        public struct ApprovalResponseData
        {
            /// <summary>
            ///     Create player objects
            /// </summary>
            [LabelText("Create player object")] public bool CreatePlayerObject;

            /// <summary>
            ///     Enable player prefabricated hash value
            /// </summary>
            [LabelText("Enable player prefab hash")]
            public bool EnablePlayerPrefabHash;

            /// <summary>
            ///     Player Prefabricated Hash Value
            /// </summary>
            [LabelText("Player prefab hash")] public uint PlayerPrefabHash;

            /// <summary>
            ///     Enable Location
            /// </summary>
            [LabelText("Enable Position")] public bool EnablePosition;

            /// <summary>
            ///     Position
            /// </summary>
            [LabelText("Position")] public Vector3 Position;

            /// <summary>
            ///     Enable rotation
            /// </summary>
            [LabelText("Enable Rotation")] public bool EnableRotation;

            /// <summary>
            ///     Rotate
            /// </summary>
            [LabelText("Rotation")] public Quaternion Rotation;
        }
    }
}