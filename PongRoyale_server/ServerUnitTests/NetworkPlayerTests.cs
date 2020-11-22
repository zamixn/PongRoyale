using Microsoft.VisualStudio.TestTools.UnitTesting;
using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace PongRoyale_shared.Tests
{
    [TestClass()]
    public class NetworkPlayerTests
    {
        [TestMethod()]
        public void SetLifeTest()
        {
            NetworkPlayer player = new NetworkPlayer(0, 0, PaddleType.Long);
            player.SetLife(30);
            Assert.AreEqual(30, player.Life);
        }

        [TestMethod()]
        public void NetworkPlayerTest()
        {
            NetworkPlayer player = new NetworkPlayer(0, 0, PaddleType.Normal);
            Assert.IsTrue(player.Id == 0 && player.Life == 0 && player.PaddleType == PaddleType.Normal);
        }
    }
}