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
        public void ReboundDirectionTest1()
        {
            Assert.ThrowsException<System.ArgumentNullException>(() =>
            {
                BallDeadlyStrategy strategy = new BallDeadlyStrategy();
                Paddle paddle = new Paddles.NormalPaddle(10);
                Vector2 vector = new Vector2(10, 10);
                Obstacles.Obstacle obstacle = new Obstacles.Obstacle(10, 10, 10, 5, 5);
                Ball b = Ball.CreateBall(0, PongRoyale_shared.BallType.Deadly, vector, 10, vector, 10);
                Vector2 result = strategy.ReboundDirection(b, vector, paddle, obstacle);
            });

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