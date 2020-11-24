using Microsoft.VisualStudio.TestTools.UnitTesting;
using PongRoyale_client.Game.Balls.ReboundStrategy;
using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace PongRoyale_client.Game.Balls.ReboundStrategy.Tests
{
    [TestClass()]
    public class NonPassableObstacleStrategyTests
    {
        [TestMethod()]
        public void ReboundDirectionTest()
        {
            NonPassableObstacleStrategy strategy = new NonPassableObstacleStrategy();
            Vector2 vector = new Vector2(10, 10);
            Ball b = Ball.CreateBall(0, PongRoyale_shared.BallType.Normal, vector, 10, vector, 10);
            Vector2 result = strategy.ReboundDirection(b, vector, null, null);
            Vector2 dir = (vector + vector).Normalize();
            Assert.AreEqual(dir, result);
        }
        [TestMethod()]
        public void ReboundDirectionTest1()
        {
            NonPassableObstacleStrategy strategy = new NonPassableObstacleStrategy();
            Vector2 vector = new Vector2(10, 10);
            Ball b = Ball.CreateBall(0, PongRoyale_shared.BallType.Normal, vector, 10, -vector, 10);
            Vector2 result = strategy.ReboundDirection(b, vector, null, null);
            Assert.AreEqual(vector, result);
        }
    }
}