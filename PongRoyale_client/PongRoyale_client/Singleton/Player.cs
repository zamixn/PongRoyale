﻿using PongRoyale_client.Game;
using PongRoyale_client.Game.Balls;
using PongRoyale_client.Observers;
using PongRoyale_shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static PongRoyale_shared.NetworkMessage;

namespace PongRoyale_client.Singleton
{
    public class Player : Singleton<Player>
    {
        private ArrayList observers;
        public string PlayerName { get { return ConstructName(Id); } }
        public static string ConstructName(byte id) { return $"Player{id}"; }

        public byte Id { get; private set; }
        public bool IsRoomMaster { get { return IdMatches(RoomSettings.Instance.RoomMaster.Id); } }
        public int Life { get; private set; }

        public Player()
        {
            observers = new ArrayList();
        }
        public void SendChatMessage(string message)
        {
            if (!ServerConnection.Instance.IsConnected())
                return;

            NetworkMessage chatMessage = new NetworkMessage(Id, MessageType.Chat, NetworkMessage.EncodeString(message));
            ServerConnection.Instance.SendDataToServer(chatMessage);
        }

        public void SyncWithServer()
        {
            if (!ServerConnection.Instance.IsConnected())
                return;

            Paddle localPlayer = GameplayManager.Instance.LocalPaddle;
            NetworkMessage message = new NetworkMessage(Id, MessageType.PlayerSync, NetworkMessage.EncodeFloat(localPlayer.AngularPosition));
            ServerConnection.Instance.SendDataToServer(message);

            if (IsRoomMaster)
            {
                var balls = GameplayManager.Instance.ArenaBalls;
                message = new NetworkMessage(Id, MessageType.BallSync, 
                    NetworkMessage.EncodeBallData(balls.Select(b => b.Value.Id).ToArray(), balls.Select(b => b.Value.Position).ToArray()));
                ServerConnection.Instance.SendDataToServer(message);
            }
        }
        public void SendStartGameMessage()
        {
            if (!ServerConnection.Instance.IsConnected())
                return;

            NetworkMessage message = new NetworkMessage(Id, MessageType.GameStart, new byte[0]);
            ServerConnection.Instance.SendDataToServer(message);
        }

        public void SendEndGameMessage()
        {
            if (!ServerConnection.Instance.IsConnected())
                return;

            NetworkMessage message = new NetworkMessage(Id, MessageType.GameEnd, new byte[] { RoomSettings.Instance.GetPlayerWonId() });
            ServerConnection.Instance.SendDataToServer(message);
        }

        public void SendPlayerLostLifeMessage(byte id, byte life)
        {
            if (!ServerConnection.Instance.IsConnected())
                return;

            NetworkMessage message = new NetworkMessage(Id, MessageType.PlayerLostLife, new byte[] { id, life });
            ServerConnection.Instance.SendDataToServer(message);
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
