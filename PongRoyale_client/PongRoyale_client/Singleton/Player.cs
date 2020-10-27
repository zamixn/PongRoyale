using PongRoyale_client.Game;
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
        private readonly NetworkDataConverterAdapter Converter = NetworkDataConverterAdapter.Instance;
        public string PlayerName { get { return ConstructName(Id); } }
        public static string ConstructName(byte id) { return $"Player{id}"; }

        public byte Id { get; private set; }
        public bool IsRoomMaster { get { return IdMatches(RoomSettings.Instance.RoomMaster.Id); } }
        public int Life { get; private set; }

        public void SendChatMessage(string message)
        {
            if (!ServerConnection.Instance.IsConnected())
                return;

            NetworkMessage chatMessage = new NetworkMessage(Id, MessageType.Chat, Converter.EncodeString(message));
            ServerConnection.Instance.SendDataToServer(chatMessage);
        }

        public void SyncWithServer()
        {
            if (!ServerConnection.Instance.IsConnected() || ArenaFacade.Instance.IsPaused)
                return;

            Paddle localPlayer = ArenaFacade.Instance.LocalPaddle;
            NetworkMessage message = new NetworkMessage(Id, MessageType.PlayerSync, Converter.EncodeFloat(localPlayer.AngularPosition));
            ServerConnection.Instance.SendDataToServer(message);

            if (IsRoomMaster)
            {
                var balls = ArenaFacade.Instance.ArenaBalls;
                var ids = balls.Select(b => b.Key).ToArray();
                var positions = balls.Select(b => b.Value.Position).ToArray();

                message = new NetworkMessage(Id, MessageType.BallSync,
                    Converter.EncodeBallData(ids, positions));
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

        public void SendRoundReset(BallType[] ballTypes, byte[] ballIds)
        {
            if (!ServerConnection.Instance.IsConnected())
                return;

            byte[] playerIds = RoomSettings.Instance.Players.Select(kvp => kvp.Key).ToArray();
            byte[] playerLifes = RoomSettings.Instance.Players.Select(kvp => kvp.Value.Life).ToArray();
            byte[] data = Converter.EncodeRoundOverData(ballTypes, ballIds, playerIds, playerLifes);
            NetworkMessage message = new NetworkMessage(Id, MessageType.RoundReset, data);
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
    }
}
