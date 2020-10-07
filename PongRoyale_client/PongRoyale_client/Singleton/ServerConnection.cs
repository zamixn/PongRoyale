﻿using System;
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
        private TcpClient TcpClient;
        private Thread ServerMessageHandler;

        public void Connect(Action onConnected = null, Action<Exception> onException = null)
        {
            try
            {
                TcpClient client = new TcpClient(Constants.ServerIp, Constants.ServerPort);
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
                    SafeInvoke.Instance.Invoke(() => {
                        ChatController.Instance.LogInfo($"Player: {Player.ConstructName(message.SenderId)} Joined the room");
                    });
                    break;
                case NetworkMessage.MessageType.Chat:
                    SafeInvoke.Instance.Invoke(() => {                     
                        ChatController.Instance.LogChatMessage(message.SenderId, message.Contents);
                    });
                    break;
                default:
                    break;
            }
        }


        public void Disconnect(Action onConnected = null, Action<Exception> onException = null)
        {
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
            Debug.WriteLine($"Received: Type: {response.Type}. Response: {response.Contents}");
        }
    }
}