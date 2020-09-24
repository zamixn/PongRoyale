using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client
{
    public class Player
    {
        private TcpClient TcpClient;

        public Player()
        {
        }

        public void Connect(Action onConnected = null, Action<Exception> onException = null)
        {
            try
            {
                TcpClient client = new TcpClient(Constants.ServerIp, Constants.ServerPort);
                TcpClient = client;
                onConnected?.Invoke();

            }
            catch (Exception ex)
            {
                onException?.Invoke(ex);
            }
        }

        public void Disconnect(Action onConnected = null, Action<Exception> onException = null)
        {
            try
            {
                TcpClient.Close();
                TcpClient.Dispose();
                TcpClient = null;
                onConnected?.Invoke();
            }
            catch (Exception ex)
            {
                onException?.Invoke(ex);
            }
        }

        public bool IsConnected()
        {
            return TcpClient != null;
        }

        public void SendDataToServer(string message, bool waitForResponse, Action onDataSent = null, Action<string> onResponse = null, Action<Exception> onException = null)
        {
            try
            {
                int byteCount = Encoding.UTF8.GetByteCount(message + 1);
                byte[] byteData = new byte[byteCount];
                byteData = Encoding.ASCII.GetBytes(message);

                NetworkStream stream = TcpClient.GetStream();
                stream.Write(byteData, 0, byteData.Length);
                onDataSent?.Invoke();

                if (waitForResponse)
                {
                    StreamReader sr = new StreamReader(stream);
                    string response = sr.ReadLine();
                    onResponse?.Invoke(response);
                }
            }
            catch (Exception ex)
            {
                onException?.Invoke(ex);
            }
        }
    }
}
