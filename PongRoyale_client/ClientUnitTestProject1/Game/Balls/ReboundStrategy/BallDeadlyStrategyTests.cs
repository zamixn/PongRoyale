using Microsoft.VisualStudio.TestTools.UnitTesting;
using PongRoyale_client.Game.Balls.ReboundStrategy;
using PongRoyale_client.Game.Paddles;
using PongRoyale_client.Singleton;
using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace PongRoyale_client.Game.Balls.ReboundStrategy.Tests
{
    [TestClass()]
    public class BallDeadlyStrategyTests
    {
        [TestInitialize()]
        public void Initialize()
        {
            GameManager.StartLocalGame();
            ArenaFacade.Instance.UpdateDimensions(new Vector2(451, 451), new Vector2(225.5, 225.5), new Vector2(0, 0), 200.5f);
            ArenaFacade.Instance.PlayerPaddles.Add(0, new NormalPaddle(0, PaddleDataFactory.GetPaddleData(PaddleType.Normal)));
        }

        [TestCleanup()]
        public void Cleanup() 
        { 
            ArenaFacade.Instance.PlayerPaddles.Clear(); 
        }

        [TestMethod()]
        public void ReboundDirectionTest()
        {
            BallDeadlyStrategy strategy = new BallDeadlyStrategy();
            Vector2 vector = new Vector2(10, 10);
            Ball b = Ball.CreateBall(0, BallType.Deadly, vector, 10, vector, 10);
            Vector2 result = strategy.ReboundDirection(b, vector, null, null);
            Assert.AreEqual(vector, result);
        }
        [TestMethod()]
        public void ReboundDirectionTest1()
        {
            BallDeadlyStrategy strategy = new BallDeadlyStrategy();
            Paddle paddle = ArenaFacade.Instance.PlayerPaddles[0];
            Vector2 vector = new Vector2(10, 10);
            Obstacles.Obstacle obstacle = new Obstacles.Obstacle(10, 10, 10, 5, 5);
            Ball b = Ball.CreateBall(0, BallType.Deadly, vector, 10, vector, 10);
            Vector2 result = strategy.ReboundDirection(b, vector, paddle, obstacle);
        }

        [TestMethod()]
        public void ReboundPositionTest()
        {
            BallDeadlyStrategy strategy = new BallDeadlyStrategy();
            Vector2 vector = new Vector2(10, 10);
            Ball b = Ball.CreateBall(0, BallType.Deadly, vector, 10, vector, 10);
            Vector2 result = strategy.ReboundPosition(b, vector, null, null);
            Assert.AreEqual(vector, result);
        }
    }
}