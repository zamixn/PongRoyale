using Microsoft.VisualStudio.TestTools.UnitTesting;
using PongRoyale_server;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_server.Tests
{
    [TestClass()]
    public class ServerControllerTests
    {
        [TestMethod()]
        public void StartTest()
        {
            Assert.ThrowsException<System.Net.Sockets.SocketException>(() => 
            {
                Task.Delay(1000).ContinueWith(task => {
                    ServerController.Instance.listener.Stop();
                    ServerController.Instance.SetAcceptPlayers(false);
                });
                ServerController.Instance.Start();
            });


        }

        [TestMethod()]
        public void GetNewPlayerIDTest()
        {
            int playerId = ServerController.Instance.GetNewPlayerID();
            Assert.AreEqual(100, playerId);
        }
        [TestMethod()]
        public void GetNewPlayerIDTest1()
        {
            ServerSidePlayer player = new ServerSidePlayer(100, null);
            ServerController.Instance.AddNewPlayer(player);
            int playerId = ServerController.Instance.GetNewPlayerID();

            Assert.AreEqual(101, playerId);
        }

        [TestMethod()]
        public void AddNewPlayerTest()
        {
            ServerSidePlayer player = new ServerSidePlayer(100, null);
            ServerController.Instance.AddNewPlayer(player);

            Assert.IsTrue(ServerController.Instance.Players.Contains(player));
        }

        [TestMethod()]
        public void RemovePlayerTest()
        {
            ServerSidePlayer player = new ServerSidePlayer(100, null);
            ServerController.Instance.AddNewPlayer(player);
            ServerController.Instance.RemovePlayer(player);

            Assert.IsFalse(ServerController.Instance.Players.Contains(player));
        }
        [TestMethod()]
        public void HandleClientConnectedTest()
        {

            Assert.ThrowsException<System.InvalidOperationException>(() =>
            {
                TcpClient client = new TcpClient();
                ServerController.Instance.HandleClientConnected(client);
            });

        }
    }
}