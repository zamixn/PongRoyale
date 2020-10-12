using PongRoyale_client.Observers;
using System;
using System.Collections;
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
        private ArrayList observers;
        public string PlayerName { get { return ConstructName(Id); } }
        public static string ConstructName(byte id) { return $"Player{id}"; }

        public byte Id { get; private set; }
        public int Life;

        public Player()
        {
            observers = new ArrayList();
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
        public void Register(IObserver observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
        }
        public void Unregister(IObserver observer)
        {
            observers.Remove(observer);
        }
        public void Lose()
        {
            foreach (IObserver observer in observers)
            {
                observer.Update();
            }
        }
    }
}
