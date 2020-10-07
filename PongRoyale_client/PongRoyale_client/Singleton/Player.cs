using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Singleton
{
    public class Player : Singleton<Player>
    {
        public string PlayerName { get { return ConstructName(Id); } }
        public static string ConstructName(byte id) { return $"Player{id}"; }

        public byte Id { get; private set; }

        public Player()
        {
        }

        public void SendChatMessage(string message)
        {
            NetworkMessage chatMessage = new NetworkMessage(Id, NetworkMessage.MessageType.Chat, message);
            ServerConnection.Instance.SendDataToServer(chatMessage);
        }

        public void SetId(byte id)
        {
            Id = id;
        }

        public bool IdMatches(byte id)
        {
            return id.Equals(Id);
        }
    }
}
