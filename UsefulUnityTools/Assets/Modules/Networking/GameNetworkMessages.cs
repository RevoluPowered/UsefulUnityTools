using System;
using UnityEngine;
using UnityEngine.Networking;


namespace Game.Networking
{ 
    /// <summary>
    /// The defined network message types, this automatically updates as we go through engine updates.
    /// </summary>
    public enum GameNetworkMessages : short
    {
        ServerPassword = MsgType.Highest,
        PlayerInfo,
        SteamID,
        ChatMessage,
        ChangeLevelInfo
    }
}