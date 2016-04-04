using System;
using UnityEngine;
using UnityEngine.Networking;
using Game.Player;

namespace Game.Networking
{
    /// <summary>
    /// The defined network message types, this automatically updates as we go through engine updates.
    /// </summary>
    public class NetworkPlayerInfo : MonoBehaviour
    {
        /// <summary>
        /// The net information message.
        /// </summary>
        public class NetPlayerInfoMessage : MessageBase
        {
            public string mPlayerName;
            public string mClientVersion;
            public string mAuthToken;
        }

        /// <summary>
        /// The network client.
        /// </summary>
        public NetworkClient mClient;

        /// <summary>
        /// Register the message 
        /// </summary>
        public void RegisterMessagesToServer()
        {
            // Register the handler.
            NetworkServer.RegisterHandler((short)GameNetworkMessages.PlayerInfo, OnPlayerInformationRecieved);
        }

        /// <summary>
        /// The send message function for the player information.
        /// </summary>
        /// <param name="mPlayerName"></param>
        /// <param name="mClientVersion"></param>
        /// <param name="mAuthToken"></param>
        public void SendPlayerInformation(string playerName, string clientVersion, string authToken)
        {
            NetPlayerInfoMessage message = new NetPlayerInfoMessage();
            message.mPlayerName = playerName;
            message.mClientVersion = clientVersion;
            message.mAuthToken = authToken;

            // Make sure the client is not null.
            if (mClient == null)
            {
                // Update it.
                //mClient = PlayerAPI.GetNetworkClient();
            }

            // Send the player information.
            mClient.Send((short)GameNetworkMessages.PlayerInfo, message);
        }

        /// <summary>
        /// When the server recieves a player info from a player it can check this.
        /// </summary>
        /// <param name="message"></param>
        public void OnPlayerInformationRecieved(NetworkMessage message)
        {
            NetPlayerInfoMessage msg = message.ReadMessage<NetPlayerInfoMessage>();
            Console.Log("Player information " + msg.mPlayerName);
        }
    }
}
