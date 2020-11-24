using Microsoft.VisualStudio.TestTools.UnitTesting;
using PongRoyale_client.Game.ArenaObjects.Powerups;
using PongRoyale_client.Game.Balls.Decorator;
using PongRoyale_client.Game.Builders;
using PongRoyale_client.Game.Paddles;
using PongRoyale_client.Singleton;
using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PongRoyale_client.Game.Balls.Decorator.Tests
{
    [TestClass()]
    public class BallDecoratorTests
    {
        [TestInitialize()]
        public void Initialize()
        {
            GameManager.StartLocalGame();
            ArenaFacade.Instance.UpdateDimensions(new Vector2(451, 451), new Vector2(225.5, 225.5), 200.5f);
        }

        [TestCleanup()]
        public void Cleanup() { }

        [TestMethod()]
        public void BallDecoratorTest()
        {
            Vector2 pos = new Vector2(5, 5);
            Ball ball = Ball.CreateBall(0, BallType.Normal, pos, 10, pos, 10);

            BallDecorator dec = new BallDecorator(ball);

            Assert.IsTrue(dec.GetBallType() == BallType.Normal);
        }

        [TestMethod()]
        public void CheckCollisionWithArenaObjectsTest()
        {
            Vector2 startingDir = new Vector2(1, 1);
            Ball testBall = Ball.CreateBall(0, BallType.Normal, new Vector2(225.5 + 200.5, 225.5), GameData.DefaultBallSpeed, startingDir, GameData.DefaultBallSize);
            ObstacleBuilder objBuilder = new ObstacleBuilder().AddDuration(10).AddHeigth(1000).AddPosX(225.5f).AddWidth(1000).AddPosY(225.5f);
            BallDecorator decorator = new BallDecorator(testBall);
            var Factories = new NonPassableArenaObjectFactory();
            var obstacle = Factories.CreateObstacle(objBuilder);
            Dictionary<byte, ArenaObject> obsDict = new Dictionary<byte, ArenaObject>();
            obsDict.Add(obstacle.Id, obstacle);
            decorator.CheckCollisionWithArenaObjects(obsDict);
            Vector2 dirAfterCollision = testBall.Direction;
            Assert.AreNotEqual(startingDir, dirAfterCollision);
        }

        [TestMethod()]
        public void CheckCollisionWithPaddlesTest()
        {
            Vector2 startingDir = new Vector2(1, 1);
            Paddle paddle = PaddleFactory.CreatePaddle(PaddleType.Normal, (byte)0);
            paddle.SetPosition(-5);
            Ball testBall = Ball.CreateBall(0, BallType.Normal, new Vector2(225.5 + 200.5, 225.5), GameData.DefaultBallSpeed, startingDir, GameData.DefaultBallSize);
            BallDecorator decorator = new BallDecorator(testBall);
            Dictionary<byte, Paddle> paddleDict = new Dictionary<byte, Paddle>();
            paddleDict.Add(paddle.Id, paddle);
            decorator.CheckCollisionWithPaddles(paddleDict);
            Vector2 dirAfterCollision = testBall.Direction;
            Assert.AreNotEqual(startingDir, dirAfterCollision);
        }

        [TestMethod()]
        public void CheckOutOfBoundsTest()
        {
            Paddle paddle = PaddleFactory.CreatePaddle(PaddleType.Normal, (byte)0);
            Paddle paddle2 = PaddleFactory.CreatePaddle(PaddleType.Short, (byte)1);
            Paddle paddle3 = PaddleFactory.CreatePaddle(PaddleType.Long, (byte)2);
            paddle.SetPosition(-5);
            paddle2.SetPosition(-10);
            Ball testBall = Ball.CreateBall(0, BallType.Normal, new Vector2(225.5 + 200.5, 225.5), GameData.DefaultBallSpeed, Vector2.RandomInUnitCircle(), GameData.DefaultBallSize);
            BallDecorator decorator = new BallDecorator(testBall);
            Dictionary<byte, Paddle> paddleDict = new Dictionary<byte, Paddle>();
            paddleDict.Add(paddle.Id, paddle);
            paddleDict.Add(paddle2.Id, paddle2);
            paddleDict.Add(paddle3.Id, paddle3);
            bool isOutOfBounds = decorator.CheckOutOfBounds(0, paddleDict, out byte paddleId);
            Assert.IsTrue(isOutOfBounds);
        }

        [TestMethod()]
        public void GetBallTypeTest()
        {
            Vector2 pos = new Vector2(5, 5);
            Ball ball = Ball.CreateBall(0, BallType.Normal, pos, 10, pos, 10);

            BallDecorator decorator = new BallDecorator(ball);

            Assert.AreEqual(BallType.Normal, decorator.GetBallType());
        }

        [TestMethod()]
        public void GetDiameterTest()
        {
            Vector2 pos = new Vector2(5, 5);
            Ball ball = Ball.CreateBall(0, BallType.Normal, pos, 10, pos, 10);

            BallDecorator decorator = new BallDecorator(ball);

            Assert.AreEqual(10, decorator.GetDiameter());
        }

        [TestMethod()]
        public void GetDirectionTest()
        {
            Vector2 pos = new Vector2(5, 5);
            Ball ball = Ball.CreateBall(0, BallType.Normal, pos, 10, pos, 10);

            BallDecorator decorator = new BallDecorator(ball);

            Assert.AreEqual(pos, decorator.GetDirection());
        }

        [TestMethod()]
        public void GetPositionTest()
        {
            Vector2 pos = new Vector2(5, 5);
            Ball ball = Ball.CreateBall(0, BallType.Normal, pos, 10, pos, 10);

            BallDecorator decorator = new BallDecorator(ball);

            Assert.AreEqual(pos, decorator.GetPosition());
        }

        [TestMethod()]
        public void LocalMoveTest()
        {
            Ball testBall = Ball.CreateBall(0, BallType.Normal, new Vector2(0, 0), GameData.DefaultBallSpeed, Vector2.RandomInUnitCircle(), GameData.DefaultBallSize);
            BallDecorator decorator = new BallDecorator(testBall);
            Vector2 positionBefore = decorator.GetPosition();
            decorator.LocalMove();
            Vector2 positionAfter = decorator.GetPosition();
            Assert.AreNotEqual(positionBefore, positionAfter);
        }

        [TestMethod()]
        public void SetPositionTest()
        {
            Vector2 pos = new Vector2(5, 5);
            Vector2 pos2 = new Vector2(10, 10);
            Ball ball = Ball.CreateBall(0, BallType.Normal, pos, 10, pos, 10);

            BallDecorator decorator = new BallDecorator(ball);

            decorator.SetPosition(pos2);

            Assert.AreEqual(pos2, decorator.GetPosition());
        }

        [TestMethod()]
        public void SetDirectionTest()
        {
            Vector2 pos = new Vector2(5, 5);
            Vector2 pos2 = new Vector2(10, 10);
            Ball ball = Ball.CreateBall(0, BallType.Normal, pos, 10, pos, 10);

            BallDecorator decorator = new BallDecorator(ball);

            decorator.SetDirection(pos2);

            Assert.AreEqual(pos2, decorator.GetDirection());
        }

        [TestMethod()]
        public void GetBoundsTest()
        {
            float testDiameter = 25f;
            float offset = testDiameter / 2f;
            Vector2 testPosition = new Vector2(5, 5);
            Ball testBall = Ball.CreateBall(0, BallType.Normal, testPosition, GameData.DefaultBallSpeed, Vector2.RandomInUnitCircle(), testDiameter);
            BallDecorator decorator = new BallDecorator(testBall);
            Rect2D testBounds = new Rect2D(testPosition.X - offset, testPosition.Y - offset, testDiameter, testDiameter);
            Assert.AreEqual(testBounds, decorator.GetBounds());
        }

        [TestMethod()]
        public void ApplyPowerupTest()
        {
            PoweredUpData powerups = new PoweredUpData();
            powerups.GivePlayerLife = true;
            Ball testBall = Ball.CreateBall(0, BallType.Normal, new Vector2(0, 0), GameData.DefaultBallSpeed, Vector2.RandomInUnitCircle(), GameData.DefaultBallSize);
            BallDecorator decorator = new BallDecorator(testBall);
            decorator.ApplyPowerup(powerups);
            Assert.AreEqual(powerups.GivePlayerLife, decorator.GetPoweredUpData().GivePlayerLife);
        }

        [TestMethod()]
        public void GetIdTest()
        {
            byte testID = 2;
            Ball testBall = Ball.CreateBall(testID, BallType.Normal, new Vector2(0, 0), GameData.DefaultBallSpeed, Vector2.RandomInUnitCircle(), GameData.DefaultBallSize);
            BallDecorator decorator = new BallDecorator(testBall);
            Assert.AreEqual(testID, decorator.GetId());
        }

        [TestMethod()]
        public void GetColorTest()
        {
            BallType type = BallType.Normal;
            Color c = Color.Black;
            Ball testBall = Ball.CreateBall(0, type, new Vector2(0, 0), GameData.DefaultBallSpeed, Vector2.RandomInUnitCircle(), GameData.DefaultBallSize);
            BallDecorator decorator = new BallDecorator(testBall);
            Assert.AreEqual(c, decorator.GetColor());
        }
        [TestMethod()]
        public void GetPoweredUpDataTest()
        {
            PoweredUpData powerups = new PoweredUpData();
            Ball testBall = Ball.CreateBall(0, BallType.Normal, new Vector2(0, 0), GameData.DefaultBallSpeed, Vector2.RandomInUnitCircle(), GameData.DefaultBallSize);
            BallDecorator decorator = new BallDecorator(testBall);
            decorator.ApplyPowerup(powerups);
            Assert.IsNotNull(decorator.GetPoweredUpData());
        }

        [TestMethod()]
        public void RemovePowerUpDataTest()
        {
            PoweredUpData powerups = new PoweredUpData();
            powerups.GivePlayerLife = true;
            Ball testBall = Ball.CreateBall(0, BallType.Normal, new Vector2(0, 0), GameData.DefaultBallSpeed, Vector2.RandomInUnitCircle(), GameData.DefaultBallSize);
            BallDecorator decorator = new BallDecorator(testBall);
            decorator.ApplyPowerup(powerups);
            decorator.RemovePowerUpData(powerups);
            Assert.AreNotEqual(powerups.GivePlayerLife, decorator.GetPoweredUpData().GivePlayerLife);
        }
    }
}