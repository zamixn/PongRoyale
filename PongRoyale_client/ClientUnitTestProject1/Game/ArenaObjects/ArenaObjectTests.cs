using Microsoft.VisualStudio.TestTools.UnitTesting;
using PongRoyale_client.Game;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PongRoyale_client.Game.Tests
{
    [TestClass()]
    public class ArenaObjectTests
    {
        [TestMethod()]
        public void SetIdTest()
        {
            ArenaObject obj = new Obstacles.Obstacle(0, 0, 0, 0, 0);
            obj.SetId(132);

            Assert.AreEqual(132, obj.Id);
        }
    }
}