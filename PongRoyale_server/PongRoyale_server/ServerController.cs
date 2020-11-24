using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Linq;
using System.Diagnostics;
using System.Threading;

namespace PongRoyale_server
{
    public class ServerController : Singleton<ServerController>
    {
        public ByteNetworkDataConverter Converter = new ByteNetworkDataConverter();
        public List<ServerSidePlayer> Players = new List<ServerSidePlayer>();
        public ServerSidePlayer RoomMaster;
        public TcpListener listener;
        public bool AcceptPlayers;
        private readonly object Lock = new object();

        public void Clean()
        {
            Players = new List<ServerSidePlayer>();
        }

        public void Start()
        {
            listener = new TcpListener(System.Net.IPAddress.Any, 6969);
            listener.Start();
            AcceptPlayers = true;
            while (AcceptPlayers)
            {
                TcpClient client = listener.AcceptTcpClient();
                var t = new Thread(new ParameterizedThreadStart(HandleClientConnected));
                t.Start(client);
            }
        }

        public int GetNewPlayerID()
        {
            lock (Lock)
            {
                if (Players.Count == 0)
                    return 100;
                return Players.Select(p => p.Id).Max() + 1;
            }
        }
        public void AddNewPlayer(ServerSidePlayer p)
        {
            lock (Lock)
            {
                if (RoomMaster == null)
                    RoomMaster = p;
                Players.Add(p);
            }
        }
        public void RemovePlayer(ServerSidePlayer p)
        {
            lock (Lock)
            {
                Players.Remove(p);
            }
        }
        public void SetAcceptPlayers(bool value)
        {
            lock (Lock)
            {
                AcceptPlayers = value;
            }
        }

        public void HandleClientConnected(object clientObject)
        {
            TcpClient client = clientObject as TcpClient;
            ServerSidePlayer player = new ServerSidePlayer((byte)GetNewPlayerID(), client);
            AddNewPlayer(player);
            byte[] d = Converter.EncodeString("200");
            NetworkMessage connectedMessage = new NetworkMessage(player.Id, NetworkMessage.MessageType.ConnectedToServer, d);
            foreach (ServerSidePlayer p in Players)
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

        private void HandleRespondingToMessage(ServerSidePlayer sender, NetworkMessage networkMessage)
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
                case NetworkMessage.MessageType.PowerupSpawned:
                case NetworkMessage.MessageType.ObstacleSpawned:
                case NetworkMessage.MessageType.BallSync:
                case NetworkMessage.MessageType.PlayerSync:
                case NetworkMessage.MessageType.BallPoweredUp:
                case NetworkMessage.MessageType.PaddlePowerUp:
                    {
                        SendMessageToPlayersExceptSender(responseMessage);
                        break;
                    }
                default:
                    Debug.WriteLine($"Exception: could not get repsonse to message of type {networkMessage.Type}");
                    break;
            }
        }


        private NetworkMessage GetResponse(ServerSidePlayer sender, NetworkMessage networkMessage)
        {
            switch (networkMessage.Type)
            {
                case NetworkMessage.MessageType.Chat:
                case NetworkMessage.MessageType.PlayerSync:
                case NetworkMessage.MessageType.BallSync:
                case NetworkMessage.MessageType.GameEnd:
                case NetworkMessage.MessageType.ObstacleSpawned:
                case NetworkMessage.MessageType.PowerupSpawned:
                case NetworkMessage.MessageType.BallPoweredUp:
                case NetworkMessage.MessageType.PaddlePowerUp:
                    return networkMessage.ShallowClone();
                case NetworkMessage.MessageType.GameStart:
                    {
                        NetworkMessage message = networkMessage.DeepClone();
                        byte[] playerIds = Players.Select(p => p.Id).ToArray();
                        byte[] paddleTypes = RandomNumber.GetArray(playerIds.Length,
                            () => RandomNumber.NextByte((byte)PaddleType.Normal, (byte)(PaddleType.Short + 1)));
                        byte ballType = (byte)BallType.Normal;
                        byte[] byteArray = Converter.EncodeGameStartMessage(playerIds, paddleTypes, ballType, RoomMaster.Id);
                        message.ByteContents = byteArray;
                        return message;
                    }
                case NetworkMessage.MessageType.RoundReset:
                    {
                        NetworkMessage message = networkMessage.DeepClone();
                        Converter.DecodeRoundOverData(networkMessage.ByteContents, out BallType[] oldBalls, out byte[] oldIds, out byte[] playerIDs, out byte[] playerLifes);

                        int ballCount = SharedUtilities.Clamp(
                            playerIDs.Length + RandomNumber.NextInt(-1, 2), 1, playerIDs.Length + 1);

                        BallType[] newBallTypes = new BallType[ballCount];
                        byte[] newIds = new byte[newBallTypes.Length];
                        for (int i = 0; i < newBallTypes.Length; i++)
                        {
                            newBallTypes[i] = i == 1 ? BallType.Normal :
                                (BallType)RandomNumber.NextByte((byte)BallType.Normal, (byte)(BallType.Deadly + 1));
                            newIds[i] = (byte)i;
                        }
                        byte[] byteArray = Converter.EncodeRoundOverData(newBallTypes, newIds, playerIDs, playerLifes);
                        message.ByteContents = byteArray;
                        return message;
                    }
                default:
                    Debug.WriteLine($"Exception: could not get repsonse to message of type {networkMessage.Type}");
                    break;
            }
            return null;
        }

        private void SendMessageToAllPlayers(NetworkMessage message)
        {
            if (message != null)
                foreach (ServerSidePlayer p in Players)
                    SendMessage(p, message);
        }
        private void SendMessageToPlayersExceptSender(NetworkMessage message)
        {
            if (message != null)
                foreach (ServerSidePlayer p in Players)
                    if (p.Id != message.SenderId)
                        SendMessage(p, message);
        }

        public void SendMessage(ServerSidePlayer player, NetworkMessage networkMessage)
        {
            NetworkStream stream = player.TcpClient.GetStream();
            byte[] buffer = networkMessage.ToBytes();
            stream.Write(buffer, 0, buffer.Length);
        }

        private void LogMessageReceived(NetworkMessage networkMessage)
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
