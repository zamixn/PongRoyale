using Microsoft.VisualStudio.TestTools.UnitTesting;
using PongRoyale_client.Game.Balls.ReboundStrategy;
using PongRoyale_client.Game.Builders;
using PongRoyale_client.Game.Obstacles;
using PongRoyale_client.Game.Paddles;
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
            Ball b = Ball.CreateBall(0, BallType.Normal, vector, 10, vector, 10);
            Vector2 result = strategy.ReboundDirection(b, vector, null, null);
            Vector2 dir = (vector + vector).Normalize();
            Assert.AreEqual(dir, result);
        }
        [TestMethod()]
        public void ReboundDirectionTest1()
        {
            NonPassableObstacleStrategy strategy = new NonPassableObstacleStrategy();
            Vector2 vector = new Vector2(10, 10);
            Ball b = Ball.CreateBall(0, BallType.Normal, vector, 10, -vector, 10);
            Vector2 result = strategy.ReboundDirection(b, vector, null, null);
            Assert.AreEqual(vector, result);
        }
        [TestMethod()]
        public void ReboundPositionTest()
        {
            NonPassableObstacleStrategy strategy = new NonPassableObstacleStrategy();
            Paddle paddle = new NormalPaddle(10, PaddleDataFactory.GetPaddleData(PaddleType.Normal));
            Vector2 vector = new Vector2(10, 10);
            Vector2 normalised = new Vector2(1, 0);
            Ball b = Ball.CreateBall(0, BallType.Deadly, vector, 10, vector, 10);

            NonPassableArenaObjectFactory factory = new NonPassableArenaObjectFactory();
            ObstacleBuilder objBuilder = new ObstacleBuilder().AddDuration(10).AddHeigth(10).AddPosX(5).AddWidth(10).AddPosY(5);
            Obstacle obstacle = factory.CreateObstacle(objBuilder);

            Vector2 result = strategy.ReboundPosition(b, normalised, paddle, obstacle);
            Assert.IsFalse(vector == result);
            
        }
    }
}