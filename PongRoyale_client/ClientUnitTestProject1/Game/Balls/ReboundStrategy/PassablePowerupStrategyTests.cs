﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using PongRoyale_client.Game.Balls.ReboundStrategy;
using PongRoyale_shared;
using PongRoyale_client.Game.Paddles;
using System;
using System.Collections.Generic;
using System.Text;

namespace PongRoyale_client.Game.Balls.ReboundStrategy.Tests
{
    [TestClass()]
    public class PassablePowerupStrategyTests
    {
        [TestMethod()]
        public void ReboundDirectionTest()
        {
            Assert.ThrowsException<System.NullReferenceException>(() =>
            {
                PassablePowerupStrategy strategy = new PassablePowerupStrategy();
                Vector2 vector = new Vector2(10, 10);
                Ball b = Ball.CreateBall(0, BallType.Normal, vector, 10, vector, 10);
                Paddle p = new NormalPaddle(10, PaddleDataFactory.GetPaddleData(PaddleType.Normal));
                Obstacles.Obstacle obstacle = new Obstacles.Obstacle(10, 10, 10, 5, 5);
                Vector2 result = strategy.ReboundDirection(b, vector, p, obstacle);
            });
        }

        [TestMethod()]
        public void ReboundPositionTest()
        {
            PassablePowerupStrategy strategy = new PassablePowerupStrategy();
            Vector2 vector = new Vector2(10, 10);
            Ball b = Ball.CreateBall(0, BallType.Normal, vector, 10, vector, 10);
            Paddle p = new NormalPaddle(10, PaddleDataFactory.GetPaddleData(PaddleType.Normal));
            Obstacles.Obstacle obstacle = new Obstacles.Obstacle(10, 10, 10, 5, 5);
            Vector2 result = strategy.ReboundPosition(b, vector, p, obstacle);

            Assert.AreEqual(vector, result);
        }
    }
}