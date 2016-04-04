using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;


// This might be useful in the future, TBD later.

namespace Game
{
    namespace Player
    {
        /// <summary>
        /// The player API used for quickly handling player functions and default functionality, will be used with the lua api too.
        /// </summary>
        class PlayerAPI
        {
            /// <summary>
            /// The network manager reference, since it won't be changing to another instance, any time.
            /// </summary>
            public static NetworkManager mNetworkManager;

            /// <summary>
            /// The local player reference, since once it has been found it can be cached and referenced.
            /// warning: this might need reset if the game is shut down.
            /// </summary>
            public static GameObject mLocalPlayer = null;

            //public static NetworkClient mNetworkClient;

            /*public static NetworkClient GetNetworkClient()
            {
                return mNetworkClient;
            }*/

            /// <summary>
            /// Obtain the local network player, this should always be accurate and usable.
            /// Caches the value after first use.
            /// </summary>
            /// <returns></returns>
            public static GameObject GetLocalPlayer()
            {
                // If we've not obtained this yet it will be null.
                if (mNetworkManager == null)
                {
                    mNetworkManager = MonoBehaviour.FindObjectOfType<NetworkManager>();
                    
                    // Is the client connected.
                    if(!mNetworkManager.IsClientConnected())
                    {
                        return null;
                    }
                }

                if(mNetworkManager != null)
                {
                    Console.Log("Network manager validated");
                }

                if(mLocalPlayer == null)
                {
                    Console.Log("mLocal player null");
                    
                    // Loop through all player controllers in the scene, not to be mistaken for my own class...
                    foreach (UnityEngine.Networking.PlayerController ply in mNetworkManager.client.connection.playerControllers)
                    {
                        Console.Log("Ply GetLocalPlayer()");
                        // Find the behavior attached to that object.
                        NetworkBehaviour behavior = ply.gameObject.GetComponent<NetworkBehaviour>();

                        Console.Log("Behavior is network ply" + behavior.isLocalPlayer);
                        // Is this the local player.
                        if (behavior.isLocalPlayer)
                        {
                            Console.Log("Ply returning.");

                            // Return this object as it is the local player we are looking for.
                            return ply.gameObject;
                        }
                    }

                    // Return null as it is empty.
                    Console.Log("[Network] Game not ready, GetLocalPlayer() this can be ignored.");
                    return null;
                }
                else
                {
                    Console.Log("Returning local player.");
                    return mLocalPlayer;
                }
            }
        }
    }
}
