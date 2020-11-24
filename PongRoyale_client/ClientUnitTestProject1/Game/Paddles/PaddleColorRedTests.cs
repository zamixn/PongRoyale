using Microsoft.VisualStudio.TestTools.UnitTesting;
using PongRoyale_client.Game.Paddles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PongRoyale_client.Game.Paddles.Tests
{
    [TestClass()]
    public class PaddleColorRedTests
    {
        [TestMethod()]
        public void ApplyColorTest()
        {
            PaddleColorRed obj = new PaddleColorRed();
            Color c = obj.ApplyColor();
            Assert.AreEqual(Color.Red, c);
        }
    }
}