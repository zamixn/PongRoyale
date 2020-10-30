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
        public static ByteNetworkDataConverter Converter = new ByteNetworkDataConverter();
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
            byte[] d = Converter.EncodeString("200");
            NetworkMessage connectedMessage = new NetworkMessage(player.Id, NetworkMessage.MessageType.ConnectedToServer, d);
            foreach (Player p in Players)
                SendMessage(p, connectedMessage);

            Console.WriteLine(string.Format("Client connected: {0}. Id: {1}", client.Client.RemoteEndPoint.ToString(), player.Id));

            NetworkStream stream = client.GetStream();

            while (client.Connected)
            {
                try
                {
                    byte[] buffer = new byte[NetworkMessage.MAX_MESSAGE_BYTE_LENGTH];
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
                case NetworkMessage.MessageType.RoundReset:
                    {
                        SendMessageToAllPlayers(responseMessage);
                        break;
                    }
                case NetworkMessage.MessageType.ObstacleSpawned:
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


        private static NetworkMessage GetResponse(Player sender, NetworkMessage networkMessage)
        {
            switch (networkMessage.Type)
            {
                case NetworkMessage.MessageType.Chat:
                case NetworkMessage.MessageType.PlayerSync:
                case NetworkMessage.MessageType.BallSync:
                case NetworkMessage.MessageType.GameEnd:
                case NetworkMessage.MessageType.ObstacleSpawned:
                    return new NetworkMessage(sender.Id, networkMessage.Type, networkMessage.ByteContents);
                case NetworkMessage.MessageType.GameStart:
                    {
                        byte[] playerIds = Players.Select(p => p.Id).ToArray();
                        byte[] paddleTypes = RandomNumber.GetArray(playerIds.Length,
                            () => RandomNumber.NextByte((byte)PaddleType.Normal, (byte)(PaddleType.Short + 1)));
                        byte ballType = (byte)BallType.Normal;
                        return new NetworkMessage(sender.Id, NetworkMessage.MessageType.GameStart, Converter.EncodeGameStartMessage(playerIds, paddleTypes, ballType, RoomMaster.Id));
                    }
                case NetworkMessage.MessageType.RoundReset:
                    {
                        Converter.DecodeRoundOverData(networkMessage.ByteContents, out BallType[] oldBalls, out byte[] oldIds, out byte[] playerIDs, out byte[] playerLifes);

                        int ballCount = SharedUtilities.Clamp(
                            playerIDs.Length + RandomNumber.RandomNumb(-1, 2), 1, playerIDs.Length + 1);

                        BallType[] newBallTypes = new BallType[ballCount];
                        byte[] newIds = new byte[newBallTypes.Length];
                        for (int i = 0; i < newBallTypes.Length; i++)
                        {
                            newBallTypes[i] = i == 1 ? BallType.Normal :
                                (BallType)RandomNumber.NextByte((byte)BallType.Normal, (byte)(BallType.Deadly + 1));
                            newIds[i] = (byte)i;
                        }
                        return new NetworkMessage(sender.Id, NetworkMessage.MessageType.RoundReset, Converter.EncodeRoundOverData(newBallTypes, newIds, playerIDs, playerLifes));
                    }
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
                case NetworkMessage.MessageType.PlayerSync:
                case NetworkMessage.MessageType.BallSync:
                    break;
                case NetworkMessage.MessageType.Invalid:
                    Debug.WriteLine("Invalid network message received");
                    break;
                default:
                    Console.WriteLine(string.Format("Data received from client id: {0}:\n{1}", networkMessage.SenderId, networkMessage.Type));
                    break;
            }
        }

    }
}
