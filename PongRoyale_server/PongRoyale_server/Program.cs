using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace PongRoyale_server
{
    class Program
    {
        public static List<Player> Players = new List<Player>();
        public static Player RoomMaster;
        public static bool AcceptPlayers;
        private static readonly object Lock = new object();
        public static int GetNewPlayerID()
        {
            lock (Lock)
            {
                if (Players.Count == 0)
                    return 100;
                return Players.Select(p => p.Id).Max() + 1;
            }
        }
        public static void AddNewPlayer(Player p)
        {
            lock (Lock)
            {
                if (RoomMaster == null)
                    RoomMaster = p;
                Players.Add(p);
            }
        }
        public static void RemovePlayer(Player p)
        {
            lock (Lock)
            {
                Players.Remove(p);
            }
        }
        public static void SetAcceptPlayers(bool value)
        {
            lock (Lock)
            {
                AcceptPlayers = value;
            }
        }

        static void Main(string[] args)
        {
            TcpListener listener = new TcpListener(System.Net.IPAddress.Any, 6969);
            listener.Start();
            AcceptPlayers = true;
            while (AcceptPlayers)
            {
                TcpClient client = listener.AcceptTcpClient();
                var t = new Thread(new ParameterizedThreadStart(HandleClientConnected));
                t.Start(client);
            }
        }

        public static void HandleClientConnected(object clientObject)
        {
            TcpClient client = clientObject as TcpClient;
            Player player = new Player((byte)GetNewPlayerID(), client);
            AddNewPlayer(player);
            byte[] d = NetworkMessage.EncodeString("200");
            NetworkMessage connectedMessage = new NetworkMessage(player.Id, NetworkMessage.MessageType.ConnectedToServer, d);
            foreach (Player p in Players)
                SendMessage(p, connectedMessage);

            Console.WriteLine(string.Format("Client connected: {0}. Id: {1}", client.Client.RemoteEndPoint.ToString(), player.Id));

            NetworkStream stream = client.GetStream();

            while (client.Connected)
            {
                try
                {
                    byte[] buffer = new byte[1024];
                    stream.Read(buffer, 0, buffer.Length);
                    NetworkMessage networkMessage = NetworkMessage.FromBytes(buffer);
                    if (!networkMessage.ValidateSender())
                        break;

                    LogMessageReceived(networkMessage);

                    HandleRespondingToMessage(player, networkMessage);
                }
                catch
                {
                    Console.WriteLine("error");
                    stream.Close();
                    client.Dispose();
                }
            }
            Console.WriteLine("Disconnected from client id: {0}", player.Id);
            RemovePlayer(player);
            stream.Close();
            client.Dispose();
        }

        private static void HandleRespondingToMessage(Player sender, NetworkMessage networkMessage)
        {
            NetworkMessage responseMessage = GetResponse(sender, networkMessage);
            switch (networkMessage.Type)
            {
                case NetworkMessage.MessageType.GameEnd:
                case NetworkMessage.MessageType.GameStart:
                case NetworkMessage.MessageType.Chat:
                    {
                        SendMessageToAllPlayers(responseMessage);
                        break;
                    }
                case NetworkMessage.MessageType.PlayerLostLife:
                case NetworkMessage.MessageType.BallSync:
                case NetworkMessage.MessageType.PlayerSync:
                    {
                        SendMessageToPlayersExceptSender(responseMessage);
                        break;
                    }
                default:
                    Debug.WriteLine($"Exception: could not get repsonse to message of type {networkMessage.Type}");
                    break;
            }
        }


        private static NetworkMessage GetResponse(Player player, NetworkMessage networkMessage)
        {
            switch (networkMessage.Type)
            {
                case NetworkMessage.MessageType.Chat:
                    return new NetworkMessage(player.Id, NetworkMessage.MessageType.Chat, networkMessage.ByteContents);
                case NetworkMessage.MessageType.PlayerSync:
                    return new NetworkMessage(player.Id, NetworkMessage.MessageType.PlayerSync, networkMessage.ByteContents);
                case NetworkMessage.MessageType.BallSync:
                    return new NetworkMessage(player.Id, NetworkMessage.MessageType.BallSync, networkMessage.ByteContents);
                case NetworkMessage.MessageType.GameStart:
                    byte[] playerIds = Players.Select(p => p.Id).ToArray();
                    byte[] paddleTypes = RandomNumber.GetArray(playerIds.Length, 
                        () => RandomNumber.NextByte((byte)PaddleType.Normal, (byte)(PaddleType.Short + 1)));
                    byte ballType = RandomNumber.NextByte((byte)BallType.Normal, (byte)(BallType.Deadly + 1));
                    return new NetworkMessage(player.Id, NetworkMessage.MessageType.GameStart, NetworkMessage.EncodeGameStartMessage(playerIds, paddleTypes, ballType, RoomMaster.Id) );
                case NetworkMessage.MessageType.GameEnd:
                    return new NetworkMessage(player.Id, NetworkMessage.MessageType.GameEnd, networkMessage.ByteContents);
                case NetworkMessage.MessageType.PlayerLostLife:
                    return new NetworkMessage(player.Id, NetworkMessage.MessageType.PlayerLostLife, networkMessage.ByteContents);
                default:
                    Debug.WriteLine($"Exception: could not get repsonse to message of type {networkMessage.Type}");
                    break;
            }
            return null;
        }

        private static void SendMessageToAllPlayers(NetworkMessage message)
        {
            if (message != null)
                foreach (Player p in Players)
                    SendMessage(p, message);
        }
        private static void SendMessageToPlayersExceptSender(NetworkMessage message)
        {
            if (message != null)
                foreach (Player p in Players)
                    if (p.Id != message.SenderId)
                        SendMessage(p, message);
        }

        public static void SendMessage(Player player, NetworkMessage networkMessage)
        {
            NetworkStream stream = player.TcpClient.GetStream();
            byte[] buffer = networkMessage.ToBytes();
            stream.Write(buffer, 0, buffer.Length);
        }

        private static void LogMessageReceived(NetworkMessage networkMessage)
        {
            switch (networkMessage.Type)
            {
                case NetworkMessage.MessageType.GameStart:
                case NetworkMessage.MessageType.ConnectedToServer:
                case NetworkMessage.MessageType.Chat:
                    Console.WriteLine(string.Format("Data received from client id: {0}:\n{1}", networkMessage.SenderId, networkMessage.Type));
                    break;
                case NetworkMessage.MessageType.PlayerSync:
                case NetworkMessage.MessageType.BallSync:
                    break;
                default:
                    break;
            }
        }

    }
}
