using Microsoft.VisualStudio.TestTools.UnitTesting;
using PongRoyale_client.Game.Paddles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PongRoyale_client.Game.Paddles.Tests
{
    [TestClass()]
    public class PaddleColorBlackTests
    {
        [TestMethod()]
        public void ApplyColorTest()
        {
            PaddleColorBlack obj = new PaddleColorBlack();
            Color c = obj.ApplyColor();
            Assert.AreEqual(Color.Black, c);
        }
    }
}