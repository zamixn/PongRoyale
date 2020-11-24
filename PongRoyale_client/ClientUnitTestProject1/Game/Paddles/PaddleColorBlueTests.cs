using Microsoft.VisualStudio.TestTools.UnitTesting;
using PongRoyale_client.Game.Paddles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PongRoyale_client.Game.Paddles.Tests
{
    [TestClass()]
    public class PaddleColorBlueTests
    {
        [TestMethod()]
        public void ApplyColorTest()
        {
            PaddleColorBlue obj = new PaddleColorBlue();
            Color c = obj.ApplyColor();
            Assert.AreEqual(Color.Blue, c);
        }
    }
}