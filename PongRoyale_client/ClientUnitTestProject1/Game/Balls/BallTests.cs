using Microsoft.VisualStudio.TestTools.UnitTesting;
using PongRoyale_client.Game.ArenaObjects.Powerups;
using PongRoyale_client.Game.Paddles;
using PongRoyale_client.Game.Balls;
using PongRoyale_client.Singleton;
using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Text;
using PongRoyale_client.Menu;
using PongRoyale_client.Game.Builders;
using System.Drawing;

namespace PongRoyale_client.Game.Balls.Tests
{
    [TestClass()]
    public class BallTests
    {

        [TestInitialize()]
        public void Initialize()
        {
            GameManager.StartLocalGame();
            ArenaFacade.Instance.UpdateDimensions(new Vector2(451, 451), new Vector2(225.5,225.5), 200.5f);
        }

        [TestCleanup()]
        public void Cleanup() { }

        [TestMethod()]
        public void GetDirectionTest()
        {
            Vector2 testDirection = new Vector2(0.5, -0.33);
            Ball testBall = Ball.CreateBall(0, BallType.Normal, new Vector2(0,0), GameData.DefaultBallSpeed, testDirection, GameData.DefaultBallSize);
            Assert.AreEqual(testDirection, testBall.GetDirection());
        }

        [TestMethod()]
        public void GetPositionTest()
        {
            Vector2 testPosition = new Vector2(3.43, -6.5);
            Ball testBall = Ball.CreateBall(0, BallType.Normal, testPosition, GameData.DefaultBallSpeed, Vector2.RandomInUnitCircle(), GameData.DefaultBallSize);
            Assert.AreEqual(testPosition, testBall.GetPosition());
        }

        [TestMethod()]
        public void GetDiameterTest()
        {
            float testDiameter = 15.55f;
            Ball testBall = Ball.CreateBall(0, BallType.Normal, new Vector2(0, 0), GameData.DefaultBallSpeed, Vector2.RandomInUnitCircle(), testDiameter);
            Assert.AreEqual(testDiameter, testBall.GetDiameter());
        }

        [TestMethod()]
        public void GetPoweredUpDataTest()
        {
            PoweredUpData powerups = new PoweredUpData();
            Ball testBall = Ball.CreateBall(0, BallType.Normal, new Vector2(0, 0), GameData.DefaultBallSpeed, Vector2.RandomInUnitCircle(), GameData.DefaultBallSize);
            testBall.ApplyPowerup(powerups);
            Assert.IsNotNull(testBall.GetPoweredUpData());
        }

        [TestMethod()]
        public void CreateBallTest()
        {
            byte testId = (byte)0;
            BallType btypeTest = BallType.Normal;
            Vector2 testPosition = new Vector2(-5.64, 7.84);
            float testDiameter = 4.5f;
            Vector2 testDirection = new Vector2(0.985, 0.456);
            float testSpeed = 3.33f;
            Ball testBall = Ball.CreateBall(testId, btypeTest, testPosition, testSpeed, testDirection, testDiameter);
            Assert.IsTrue(testBall.Id == testId && testBall.Position == testPosition && testBall.Diameter == testDiameter
                && testBall.Direction == testDirection && testBall.Speed == testSpeed && testBall.bType == btypeTest);
            
        }

        [TestMethod()]
        public void LocalMoveTest()
        {
            Ball testBall = Ball.CreateBall(0, BallType.Normal, new Vector2(0, 0), GameData.DefaultBallSpeed, Vector2.RandomInUnitCircle(), GameData.DefaultBallSize);
            Vector2 positionBefore = testBall.Position;
            testBall.LocalMove();
            Vector2 positionAfter = testBall.Position;
            Assert.AreNotEqual(positionBefore, positionAfter);
        }

        [TestMethod()]
        public void SetPositionTest()
        {
            Vector2 testPosition = new Vector2(4.12, 2.51);
            Ball testBall = Ball.CreateBall(0, BallType.Normal, new Vector2(0, 0), GameData.DefaultBallSpeed, Vector2.RandomInUnitCircle(), GameData.DefaultBallSize);
            testBall.SetPosition(testPosition);
            Assert.AreEqual(testPosition, testBall.Position);
        }

        [TestMethod()]
        public void GetBallTypeTest()
        {
            BallType testBType = BallType.Deadly;
            Ball testBall = Ball.CreateBall(0, testBType, new Vector2(0, 0), GameData.DefaultBallSpeed, Vector2.RandomInUnitCircle(), GameData.DefaultBallSize);
            Assert.AreEqual(testBType, testBall.bType);
        }

        [TestMethod()]
        public void SetDirectionTest()
        {
            Vector2 testDirection = new Vector2(-0.12, 0.45);
            Ball testBall = Ball.CreateBall(0, BallType.Normal, new Vector2(0, 0), GameData.DefaultBallSpeed, Vector2.RandomInUnitCircle(), GameData.DefaultBallSize);
            testBall.SetDirection(testDirection);
            Assert.AreEqual(testDirection, testBall.Direction);
        }

        [TestMethod()]
        public void MoveTest()
        {
            Vector2 testPosition = new Vector2(5, 3);
            Vector2 move = new Vector2(-2, 6);
            Ball testBall = Ball.CreateBall(0, BallType.Normal, testPosition, GameData.DefaultBallSpeed, Vector2.RandomInUnitCircle(), GameData.DefaultBallSize);
            testBall.Move(move);
            Assert.AreEqual(testPosition + move, testBall.Position);
        }

        [TestMethod()]
        public void CheckCollisionWithPaddlesTest()
        {
            Vector2 startingDir = new Vector2(1, 1);
            Paddle paddle = PaddleFactory.CreatePaddle(PaddleType.Normal, (byte)0);
            paddle.SetPosition(-5);
            Ball testBall = Ball.CreateBall(0, BallType.Normal, new Vector2(225.5+200.5, 225.5), GameData.DefaultBallSpeed, startingDir, GameData.DefaultBallSize);
            Dictionary<byte, Paddle> paddleDict = new Dictionary<byte, Paddle>();
            paddleDict.Add(paddle.Id,paddle);
            testBall.CheckCollisionWithPaddles(paddleDict);
            Vector2 dirAfterCollision = testBall.Direction;
            Assert.AreNotEqual(startingDir, dirAfterCollision);
        }

        [TestMethod()]
        public void CheckCollisionWithArenaObjectsTest()
        {
            Vector2 startingDir = new Vector2(1, 1);
            Ball testBall = Ball.CreateBall(0, BallType.Normal, new Vector2(225.5 + 200.5, 225.5), GameData.DefaultBallSpeed, startingDir, GameData.DefaultBallSize);
            ObstacleBuilder objBuilder = new ObstacleBuilder().AddDuration(10).AddHeigth(1000).AddPosX(225.5f).AddWidth(1000).AddPosY(225.5f);
            var Factories = new NonPassableArenaObjectFactory();
            var obstacle = Factories.CreateObstacle(objBuilder);
            Dictionary<byte, ArenaObject> obsDict = new Dictionary<byte, ArenaObject>();
            obsDict.Add(obstacle.Id, obstacle);
            testBall.CheckCollisionWithArenaObjects(obsDict);
            Vector2 dirAfterCollision = testBall.Direction;
            Assert.AreNotEqual(startingDir, dirAfterCollision);
        }

        [TestMethod()]
        public void GetBoundsTest()
        {
            float testDiameter = 25f;
            float offset = testDiameter / 2f;
            Vector2 testPosition = new Vector2(5, 5);
            Ball testBall = Ball.CreateBall(0, BallType.Normal, testPosition, GameData.DefaultBallSpeed, Vector2.RandomInUnitCircle(), testDiameter);
            Rect2D testBounds = new Rect2D(testPosition.X - offset, testPosition.Y - offset, testDiameter, testDiameter);
            Assert.AreEqual(testBounds, testBall.GetBounds());
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
            Dictionary<byte, Paddle> paddleDict = new Dictionary<byte, Paddle>();
            paddleDict.Add(paddle.Id, paddle);
            paddleDict.Add(paddle2.Id, paddle2);
            paddleDict.Add(paddle3.Id, paddle3);
            bool isOutOfBounds = testBall.CheckOutOfBounds(0,paddleDict, out byte paddleId);
            Assert.IsTrue(isOutOfBounds);
        }

        [TestMethod()]
        public void ApplyPowerupTest()
        {
            PoweredUpData powerups = new PoweredUpData();
            powerups.GivePlayerLife = true;
            Ball testBall = Ball.CreateBall(0, BallType.Normal, new Vector2(0, 0), GameData.DefaultBallSpeed, Vector2.RandomInUnitCircle(), GameData.DefaultBallSize);
            testBall.ApplyPowerup(powerups);
            Assert.AreEqual(powerups.GivePlayerLife, testBall.GetPoweredUpData().GivePlayerLife);
        }

        [TestMethod()]
        public void RemovePowerUpDataTest()
        {
            PoweredUpData powerups = new PoweredUpData();
            powerups.GivePlayerLife = true;
            Ball testBall = Ball.CreateBall(0, BallType.Normal, new Vector2(0, 0), GameData.DefaultBallSpeed, Vector2.RandomInUnitCircle(), GameData.DefaultBallSize);
            testBall.ApplyPowerup(powerups);
            testBall.RemovePowerUpData(powerups);
            Assert.AreNotEqual(powerups.GivePlayerLife, testBall.GetPoweredUpData().GivePlayerLife);
        }

        [TestMethod()]
        public void GetIdTest()
        {
            byte testID = (byte)2;
            Ball testBall = Ball.CreateBall(testID, BallType.Normal, new Vector2(0, 0), GameData.DefaultBallSpeed, Vector2.RandomInUnitCircle(), GameData.DefaultBallSize);
            Assert.AreEqual(testID, testBall.Id);
        }

        [TestMethod()]
        public void GetColorTest()
        {
            BallType bTypeTest = BallType.Normal;
            Color c = Color.Black;  // balltype: normal = Black, deadly = OrangeRed
            Ball testBall = Ball.CreateBall(0, bTypeTest, new Vector2(0, 0), GameData.DefaultBallSpeed, Vector2.RandomInUnitCircle(), GameData.DefaultBallSize);
            Assert.AreEqual(c, testBall.GetColor());
        }

        [TestMethod()]
        public void OnCollisionTest()
        {
            Vector2 startingDir = new Vector2(1, 1);
            Paddle paddle = PaddleFactory.CreatePaddle(PaddleType.Normal, (byte)0);
            Ball testBall = Ball.CreateBall(0, BallType.Normal, new Vector2(225.5 + 200.5, 225.5), GameData.DefaultBallSpeed, startingDir, GameData.DefaultBallSize);
            testBall.OnCollision(paddle, null);
            Vector2 dirAfterCollision = testBall.Direction;
            Assert.AreNotEqual(startingDir, dirAfterCollision);
        }
    }
}