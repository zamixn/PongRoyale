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

                    Console.WriteLine(string.Format("Data received from client id: {0}:\n{1}", networkMessage.SenderId, networkMessage.Type));

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
            switch (networkMessage.Type)
            {
                case NetworkMessage.MessageType.GameStart:
                case NetworkMessage.MessageType.Chat:
                    {
                        NetworkMessage responseMessage = GetResponse(sender, networkMessage);
                        if (responseMessage != null)
                            foreach (Player p in Players)
                                SendMessage(p, responseMessage);
                        break;
                    }
                case NetworkMessage.MessageType.playerSync:
                    {
                        NetworkMessage responseMessage = GetResponse(sender, networkMessage);
                        if (responseMessage != null)
                            foreach (Player p in Players) {
                                if (p.Id != networkMessage.SenderId)
                                    SendMessage(p, responseMessage);
                            }
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
                case NetworkMessage.MessageType.playerSync:
                    return new NetworkMessage(player.Id, NetworkMessage.MessageType.playerSync, networkMessage.ByteContents);
                case NetworkMessage.MessageType.GameStart:
                    byte[] playerIds = Players.Select(p => p.Id).ToArray();
                    byte[] paddleTypes = RandomNumber.GetArray(playerIds.Length, 
                        () => RandomNumber.NextByte((byte)PaddleType.Normal, (byte)(PaddleType.Short + 1)));
                    byte ballType = RandomNumber.NextByte((byte)BallType.Normal, (byte)(BallType.Deadly + 1));
                    return new NetworkMessage(player.Id, NetworkMessage.MessageType.GameStart, NetworkMessage.EncodeGameStartMessage(playerIds, paddleTypes, ballType) );
                default:
                    Debug.WriteLine($"Exception: could not get repsonse to message of type {networkMessage.Type}");
                    break;
            }
            return null;
        }

        public static void SendMessage(Player player, NetworkMessage networkMessage)
        {
            NetworkStream stream = player.TcpClient.GetStream();
            byte[] buffer = networkMessage.ToBytes();
            stream.Write(buffer, 0, buffer.Length);
        }

    }
}
