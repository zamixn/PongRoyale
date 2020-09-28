using System;
using System.Collections.Generic;
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
        private static readonly object Lock = new object();
        public static int GetNewPlayerID()
        {
            lock (Lock)
            {
                if (Players.Count == 0)
                    return 0;
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

        static void Main(string[] args)
        {
            TcpListener listener = new TcpListener(System.Net.IPAddress.Any, 6969);
            listener.Start();
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                var t = new Thread(new ParameterizedThreadStart(HandleClientConnected));
                t.Start(client);
            }
        }

        public static void HandleClientConnected(object clientObject)
        {
            TcpClient client = clientObject as TcpClient;
            Player p = new Player(GetNewPlayerID(), client);
            AddNewPlayer(p);

            Console.WriteLine(string.Format("Client connected: {0}. Id: {1}", client.Client.RemoteEndPoint.ToString(), p.Id));

            NetworkStream stream = client.GetStream();
            StreamWriter sw = new StreamWriter(client.GetStream());

            while (client.Connected)
            {
                try
                {
                    byte[] buffer = new byte[1024];
                    stream.Read(buffer, 0, buffer.Length);
                    int recv = 0;
                    foreach (byte b in buffer)
                    {
                        if (b != 0)
                            recv++;
                    }
                    string data = Encoding.UTF8.GetString(buffer, 0, recv);

                    Console.WriteLine(string.Format("Data received from client id: {0}:\n{1}", p.Id, data));

                    string response = "Pong: " + data;
                    sw.WriteLine(response);
                    sw.Flush();
                }
                catch
                {
                    stream.Close();
                    client.Dispose();
                }
            }
            Console.WriteLine("Disconnected from client id: {0}", p.Id);
            RemovePlayer(p);
            stream.Close();
            client.Dispose();
        }
    }
}
