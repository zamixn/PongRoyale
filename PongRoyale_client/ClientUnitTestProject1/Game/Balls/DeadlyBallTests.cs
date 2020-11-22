using Microsoft.VisualStudio.TestTools.UnitTesting;
using PongRoyale_client.Game.Balls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PongRoyale_client.Game.Balls.Tests
{
    [TestClass()]
    public class DeadlyBallTests
    {
        [TestMethod()]
        public void GetColorTest()
        {
            DeadlyBall ball = new DeadlyBall();

            Color color = ball.GetColor();

            Assert.AreEqual(Color.OrangeRed, color);
        }
    }
}