using System;
using UnityEngine;

namespace SISConnect.Resources
{
    [Serializable]
    public class SocketMessage : ISocketMessage
    {
        public string Command;
        public string GameTime;

        public string Message;
        public string Player;

        public string SaveToString()
        {
            return JsonUtility.ToJson(this);
        }

        public static SocketMessage CreateFromJson(string jsonString)
        {
            return JsonUtility.FromJson<SocketMessage>(jsonString);
        }
    }
}