using Microsoft.VisualStudio.TestTools.UnitTesting;
using PongRoyale_client.Game.Balls.ReboundStrategy;
using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace PongRoyale_client.Game.Balls.ReboundStrategy.Tests
{
    [TestClass()]
    public class BallDeadlyStrategyTests
    {
        [TestMethod()]
        public void ReboundDirectionTest()
        {
            BallDeadlyStrategy strategy = new BallDeadlyStrategy();
            Vector2 vector = new Vector2(10, 10);
            Ball b = Ball.CreateBall(0, PongRoyale_shared.BallType.Deadly, vector, 10, vector, 10);
            Vector2 result = strategy.ReboundDirection(b, vector, null, null);
            Assert.AreEqual(vector, result);

        }

        [TestMethod()]
        public void ReboundPositionTest()
        {
            BallDeadlyStrategy strategy = new BallDeadlyStrategy();
            Vector2 vector = new Vector2(10, 10);
            Ball b = Ball.CreateBall(0, PongRoyale_shared.BallType.Deadly, vector, 10, vector, 10);
            Vector2 result = strategy.ReboundPosition(b, vector, null, null);
            Assert.AreEqual(vector, result);
        }
    }
}