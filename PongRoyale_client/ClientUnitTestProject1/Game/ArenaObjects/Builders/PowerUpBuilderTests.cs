using Microsoft.VisualStudio.TestTools.UnitTesting;
using PongRoyale_client.Game.Builders;
using PongRoyale_client.Game.Powerups;
using System;
using System.Collections.Generic;
using System.Text;

namespace PongRoyale_client.Game.Builders.Tests
{
    [TestClass()]
    public class PowerUpBuilderTests
    {
        [TestMethod()]
        public void AddPosXTest()
        {
            PowerUpBuilder builder = new PowerUpBuilder();
            builder.AddPosX(10);
            ArenaObject a = builder.CreateObject();
            Assert.IsTrue(a.PosX == 10);
        }

        [TestMethod()]
        public void AddPosYTest()
        {
            PowerUpBuilder builder = new PowerUpBuilder();
            builder.AddPosY(10);
            ArenaObject a = builder.CreateObject();
            Assert.IsTrue(a.PosY == 10);
        }
    }
}