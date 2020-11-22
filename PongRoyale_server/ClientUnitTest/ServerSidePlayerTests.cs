using Microsoft.VisualStudio.TestTools.UnitTesting;
using PongRoyale_server;
using System;
using System.Collections.Generic;
using System.Text;

namespace PongRoyale_server.Tests
{
    [TestClass()]
    public class ServerSidePlayerTests
    {
        [TestMethod()]
        public void ServerSidePlayerTest()
        {
            ServerSidePlayer player = new ServerSidePlayer(0, null);

            Assert.IsTrue(player.Id == 0 && player.TcpClient == null);
        }
    }
}