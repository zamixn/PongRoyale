using Microsoft.VisualStudio.TestTools.UnitTesting;
using PongRoyale_client.Game.Balls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PongRoyale_client.Game.Balls.Tests
{
    [TestClass()]
    public class NormalBallTests
    {
        [TestMethod()]
        public void GetColorTest()
        {
            NormalBall ball = new NormalBall();

            Color color = ball.GetColor();

            Assert.AreEqual(Color.Black, color);
        }
    }
}