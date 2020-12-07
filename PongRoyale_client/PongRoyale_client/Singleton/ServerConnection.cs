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
using static PongRoyale_shared.NetworkMessage;

namespace PongRoyale_client.Singleton
{
    public class ServerConnection : Singleton<ServerConnection>
    {
        private readonly NetworkDataAdapter Converter = NetworkDataAdapter.Instance;
        public string PlayerName { get { return ConstructName(Id); } }
        public static string ConstructName(byte id) { return $"Player{id}"; }
        public byte Id { get; private set; }
        public bool IsRoomMaster { get { return IdMatches(RoomSettings.Instance.RoomMaster.Id); } }

        private TcpClient TcpClient;
        private Thread ServerMessageHandler;

        #region Player
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
                var positions = balls.Select(b => b.Value.GetPosition()).ToArray();
                var directions = balls.Select(b => b.Value.GetDirection()).ToArray();

                byte[] data = Converter.EncodeBallData(ids, positions, directions);
                message = new NetworkMessage(Id, MessageType.BallSync, data);
                ServerConnection.Instance.SendDataToServer(message);
            }
        }

        public void SendBallPoweredUpMessage(byte ballId, byte powerUpId, PoweredUpData poweredUp)
        {
            if (!ServerConnection.Instance.IsConnected())
                return;

            var data = Converter.EncodeBallPoweredUpData(ballId, powerUpId, poweredUp);
            NetworkMessage message = new NetworkMessage(Id, MessageType.BallPoweredUp, data);
            ServerConnection.Instance.SendDataToServer(message);
        }

        public void SendTranferPowerUpToPaddle(byte paddleId, byte ballId, PoweredUpData poweredUp)
        {
            if (!ServerConnection.Instance.IsConnected())
                return;

            var data = Converter.EncodePaddlePoweredUpData(paddleId, ballId, poweredUp);
            NetworkMessage message = new NetworkMessage(Id, MessageType.PaddlePowerUp, data);
            ServerConnection.Instance.SendDataToServer(message);
        }

        public void SendObstacleSpawnedMessage(byte id, Obstacle obstacle)
        {
            if (!ServerConnection.Instance.IsConnected())
                return;

            var data = Converter.EncodeObstacleData(id, obstacle);
            NetworkMessage message = new NetworkMessage(Id, MessageType.ObstacleSpawned, data);
            ServerConnection.Instance.SendDataToServer(message);
        }

        public void SendPowerupSpawnedMessage(byte id, PowerUp powerup)
        {
            if (!ServerConnection.Instance.IsConnected())
                return;

            var data = Converter.EncodePowerupData(id, powerup);
            NetworkMessage message = new NetworkMessage(Id, MessageType.PowerupSpawned, data);
            ServerConnection.Instance.SendDataToServer(message);
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
        #endregion
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
                ServerConnection.Instance.SetId(responeMessage.SenderId);

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
                        ChatManager.Instance.Proxy.LogInfo($"Player: {ServerConnection.ConstructName(message.SenderId)} Joined the room");
                    });
                    break;
                case NetworkMessage.MessageType.Chat:
                    SafeInvoke.Instance.Invoke(() =>
                    {
                        ChatManager.Instance.Proxy.LogChatMessage(message.SenderId, Converter.DecodeString(message.ByteContents));
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
                        GameManager.Instance.SetGameState(GameState.InGame);
                    });
                    break;
                case NetworkMessage.MessageType.GameEnd:
                    SafeInvoke.Instance.Invoke(() =>
                    {
                        RoomSettings.Instance.SetPlayerWon(message.ByteContents[0]);
                        GameManager.Instance.SetGameState(GameState.GameEnded);
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
                        PowerUp pwu = Converter.DecodePowerupData(message.ByteContents, out byte id, out PoweredUpData data);
                        ArenaFacade.Instance.PowerUpSpawnedMessageReceived(id, pwu, data);
                    });
                    break;
                case NetworkMessage.MessageType.BallPoweredUp:
                    SafeInvoke.Instance.Invoke(() =>
                    {
                        Converter.DecodeBallPoweredUpData(message.ByteContents, out byte ballId, out byte powerupId, out PoweredUpData data);
                        ArenaFacade.Instance.OnReceivedBallPowerUpMessage(ballId, powerupId, data);
                    });
                    break;
                case NetworkMessage.MessageType.PaddlePowerUp:
                    SafeInvoke.Instance.Invoke(() =>
                    {
                        Converter.DecodePaddlePoweredUpData(message.ByteContents, out byte paddleId, out byte ballId, out PoweredUpData data);
                        ArenaFacade.Instance.OnReceivedTransferPowerUpMessage(paddleId, ballId, data);
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

        public void LogConnectionException(Exception exception)
        {
            Debug.WriteLine(string.Format("Exception occured: {0}", exception.Message ?? "null"));
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
