using Microsoft.VisualStudio.TestTools.UnitTesting;
using PongRoyale_client.Game.Balls.ReboundStrategy;
using PongRoyale_client.Game.Builders;
using PongRoyale_client.Game.Paddles;
using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace PongRoyale_client.Game.Balls.ReboundStrategy.Tests
{
    [TestClass()]
    public class NonPassablePowerupStrategyTests
    {
        [TestMethod()]
        public void ReboundDirectionTest()
        {
            NonPassablePowerupStrategy strategy = new NonPassablePowerupStrategy();
            Vector2 vector = new Vector2(10, 10);
            Ball b = Ball.CreateBall(0, BallType.Normal, vector, 10, vector, 10);
            Vector2 result = strategy.ReboundDirection(b, -vector, null, null);
            Assert.AreEqual(-vector, result);
        }
        [TestMethod()]
        public void ReboundDirectionTest1()
        {
            Assert.ThrowsException<NullReferenceException>(() =>
            {
                NonPassablePowerupStrategy strategy = new NonPassablePowerupStrategy();
                Vector2 vector = new Vector2(10, 10);
                Ball b = Ball.CreateBall(0, BallType.Normal, vector, 10, vector, 10);
                Obstacles.Obstacle obstacle = new Obstacles.Obstacle(10, 10, 10, 5, 5);
                Paddle p = new NormalPaddle(10, PaddleDataFactory.GetPaddleData(PaddleType.Normal));
                Vector2 result = strategy.ReboundDirection(b, vector, p, obstacle);
            });
        }

        [TestMethod()]
        public void ReboundPositionTest()
        {
            NonPassablePowerupStrategy strategy = new NonPassablePowerupStrategy();
            Paddle paddle = new NormalPaddle(10, PaddleDataFactory.GetPaddleData(PaddleType.Normal));
            Vector2 vector = new Vector2(10, 10);
            Vector2 normalised = new Vector2(1, 0);
            Ball b = Ball.CreateBall(0, BallType.Deadly, vector, 10, vector, 10);

            NonPassableArenaObjectFactory factory = new NonPassableArenaObjectFactory();
            PowerUpBuilder objBuilder = new PowerUpBuilder().AddDuration(10).AddPosX(5).AddPosY(5).AddDiameter(10);
            Powerups.PowerUp obstacle = factory.CreatePowerup(objBuilder);

            Vector2 result = strategy.ReboundPosition(b, normalised, paddle, obstacle);
            Assert.IsFalse(vector == result);
        }
    }
}