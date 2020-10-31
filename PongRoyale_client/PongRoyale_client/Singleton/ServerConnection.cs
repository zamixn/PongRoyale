using PongRoyale_client.Game;
using PongRoyale_client.Game.ArenaObjects.Powerups;
using PongRoyale_client.Game.Obstacles;
using PongRoyale_client.Game.Powerups;
using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PongRoyale_client.Singleton
{
    public class ServerConnection : Singleton<ServerConnection>
    {
        private readonly NetworkDataConverterAdapter Converter = NetworkDataConverterAdapter.Instance;
        private TcpClient TcpClient;
        private Thread ServerMessageHandler;

        public void Connect(string connectionString = "", Action onConnected = null, Action<Exception> onException = null)
        {
            if (IsConnected())
            {
                Debug.WriteLine("Could not connect, because we are already connected to a server :)");
                return;
            }
            try
            {
                connectionString = string.IsNullOrEmpty(connectionString) ? 
                    $"{Constants.ServerIp}:{Constants.ServerPort}" : connectionString;

                string[] parts = connectionString.Split(':');
                string ip = parts[0];
                int port = int.Parse(parts[1]);
                TcpClient client = new TcpClient(ip, port);
                TcpClient = client;
                NetworkStream stream = TcpClient.GetStream();
                byte[] buffer = new byte[1024];
                stream.Read(buffer, 0, buffer.Length);
                NetworkMessage responeMessage = NetworkMessage.FromBytes(buffer);
                if (responeMessage.Type != NetworkMessage.MessageType.ConnectedToServer)
                    throw new Exception("Invalid response from server after connection established");
                Player.Instance.SetId(responeMessage.SenderId);

                ServerMessageHandler = new Thread(ListenForServerMessages);
                ServerMessageHandler.Start();

                onConnected?.Invoke();
            }
            catch (Exception ex)
            {
                LogConnectionException(ex);
                onException?.Invoke(ex);
            }
        }

        /// <summary>
        /// started in a thread
        /// </summary>
        private void ListenForServerMessages()
        {
            NetworkStream stream = TcpClient.GetStream();
            try
            {
                while (IsConnected())
                {
                    byte[] buffer = new byte[1024];
                    stream.Read(buffer, 0, buffer.Length);
                    NetworkMessage message = NetworkMessage.FromBytes(buffer);

                    LogConnectionDataReceived(message);
                    HandleNetworkMessageFromServer(message);
                }
            }
            catch (Exception ex)
            {
                stream.Close();
                Debug.WriteLine($"Exception in server message handler: {ex.Message ?? "null"}");
            }
        }
        private void HandleNetworkMessageFromServer(NetworkMessage message)
        {
            switch (message.Type)
            {
                case NetworkMessage.MessageType.ConnectedToServer:
                    SafeInvoke.Instance.Invoke(() =>
                    {
                        ChatController.Instance.LogInfo($"Player: {Player.ConstructName(message.SenderId)} Joined the room");
                    });
                    break;
                case NetworkMessage.MessageType.Chat:
                    SafeInvoke.Instance.Invoke(() =>
                    {
                        ChatController.Instance.LogChatMessage(message.SenderId, Converter.DecodeString(message.ByteContents));
                    });
                    break;
                case NetworkMessage.MessageType.PlayerSync:
                    SafeInvoke.Instance.Invoke(() =>
                    {
                        ArenaFacade.Instance.PlayerSyncMessageReceived(message);
                    });
                    break;
                case NetworkMessage.MessageType.BallSync:
                    SafeInvoke.Instance.Invoke(() =>
                    {
                        ArenaFacade.Instance.BallSyncMessageReceived(message);
                    });
                    break;
                case NetworkMessage.MessageType.GameStart:
                    SafeInvoke.Instance.Invoke(() =>
                    {
                        Converter.DecodeGameStartMessage(message.ByteContents,
                            out byte[] playerIds, out PaddleType[] paddleTypes, out BallType ballType, out byte roomMasterId);
                        RoomSettings.Instance.SetRoomSettings(playerIds, paddleTypes, ballType, roomMasterId);
                        GameManager.Instance.SetGameState(GameManager.GameState.InGame);
                    });
                    break;
                case NetworkMessage.MessageType.GameEnd:
                    SafeInvoke.Instance.Invoke(() =>
                    {
                        RoomSettings.Instance.SetPlayerWon(message.ByteContents[0]);
                        GameManager.Instance.SetGameState(GameManager.GameState.GameEnded);
                    });
                    break;
                case NetworkMessage.MessageType.RoundReset:
                    SafeInvoke.Instance.Invoke(() =>
                    {
                        Converter.DecodeRoundOverData(message.ByteContents, 
                            out BallType[] ballTypes, out byte[] ballIds, out byte[] playerIds, out byte[] playerLifes);
                        ArenaFacade.Instance.ResetRoundMessageReceived(ballTypes, ballIds, playerIds, playerLifes);
                    });
                    break;
                case NetworkMessage.MessageType.ObstacleSpawned:
                    SafeInvoke.Instance.Invoke(() =>
                    {
                        Obstacle obs = Converter.DecodeObstacleData(message.ByteContents, out byte id);
                        ArenaFacade.Instance.ObstacleSpawnedMessageReceived(id, obs);
                    });
                    break;
                case NetworkMessage.MessageType.PowerupSpawned:
                    SafeInvoke.Instance.Invoke(() =>
                    {
                        Powerup pwu = Converter.DecodePowerupData(message.ByteContents, out byte id, out PowerUppedData data);
                        ArenaFacade.Instance.PowerupSpawnedMessageReceived(id, pwu, data);
                    });
                    break;
                default:
                    break;
            }
        }


        public void Disconnect(Action onConnected = null, Action<Exception> onException = null)
        {
            if (!IsConnected())
            {
                Debug.WriteLine("Could not disconnect, because we are not connected to a server :)");
                return;
            }
            try
            {
                TcpClient.Close();
                TcpClient.Dispose();
                TcpClient = null;
                ServerMessageHandler.Abort();
                ServerMessageHandler = null;
                onConnected?.Invoke();
            }
            catch (Exception ex)
            {
                LogConnectionException(ex);
                onException?.Invoke(ex);
            }
        }

        public bool IsConnected()
        {
            return TcpClient != null;
        }

        public void SendDataToServer(NetworkMessage message, Action onDataSent = null, Action<Exception> onException = null)
        {
            try
            {
                byte[] byteData = message.ToBytes();
                NetworkStream stream = TcpClient.GetStream();
                stream.Write(byteData, 0, byteData.Length);
                onDataSent?.Invoke();
            }
            catch (Exception ex)
            {
                onException?.Invoke(ex);
            }
        }

        public void LogConnectionException(Exception ex)
        {
            Debug.WriteLine(string.Format("Exception occured: {0}", ex.Message ?? "null"));
        }
        public void LogConnectionDataReceived(NetworkMessage response)
        {
            switch (response.Type)
            {
                case NetworkMessage.MessageType.PlayerSync:
                case NetworkMessage.MessageType.BallSync:
                    break;
                case NetworkMessage.MessageType.Invalid:
                    Debug.WriteLine("Invalid network message received");
                    break;
                default:
                    Debug.WriteLine($"Received net message: Type: {response.Type}.");
                    break;
            }
        }
    }
}
