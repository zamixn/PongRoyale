using Microsoft.VisualStudio.TestTools.UnitTesting;
using PongRoyale_client.Game;
using PongRoyale_client.Game.ArenaObjects.Powerups;
using PongRoyale_client.Game.Paddles;
using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace PongRoyale_client.Game.Tests
{
    [TestClass()]
    public class PaddleTests
    {
        public object Powerupdata { get; private set; }

        [TestMethod()]
        public void PaddleTest()
        {
            Paddle paddle = new LongPaddle(0, PaddleDataFactory.GetPaddleData(PaddleType.Long));
            Assert.IsTrue(paddle.AngularSize == PaddleDataFactory.GetPaddleData(PaddleType.Long).Size && paddle.Thickness == PaddleDataFactory.GetPaddleData(PaddleType.Long).Thickness);
        }

        [TestMethod()]
        public void AddClampAnglesTest()
        {
            Paddle paddle = new LongPaddle(0, PaddleDataFactory.GetPaddleData(PaddleType.Long));
            paddle.AddClampAngles(0, 10);
            Assert.IsTrue(paddle.MinAngle > paddle.MaxAngle);
        }

        [TestMethod()]
        public void GetCenterAngleTest()
        {
            Paddle paddle = new NormalPaddle(0, PaddleDataFactory.GetPaddleData(PaddleType.Normal));
            Assert.IsTrue(SharedUtilities.DegToRad(paddle.AngularPosition + paddle.AngularSize / 2f) == paddle.GetCenterAngle());
        }

        [TestMethod()]
        public void AddLifeTest()
        {
            Paddle paddle = new NormalPaddle(0, PaddleDataFactory.GetPaddleData(PaddleType.Normal));
            paddle.SetLife(0);
            paddle.AddLife(1);
            Assert.IsTrue(paddle.Life == 1);
        }

        [TestMethod()]
        public void IsAliveTest()
        {
            Paddle paddle = new NormalPaddle(0, PaddleDataFactory.GetPaddleData(PaddleType.Normal));
            paddle.SetLife(0);
            Paddle paddle1 = new NormalPaddle(0, PaddleDataFactory.GetPaddleData(PaddleType.Normal));
            paddle1.SetLife(1);
            Assert.IsTrue(paddle.IsAlive() == false && paddle1.IsAlive() == true);
        }

        [TestMethod()]
        public void OnPosSyncTest()
        {
            Paddle paddle = new NormalPaddle(0, PaddleDataFactory.GetPaddleData(PaddleType.Normal));
            float a = paddle.AngularPosition;
            paddle.OnPosSync(0);
            Assert.IsTrue(paddle.AngularPosition == a);
        }

        [TestMethod()]
        public void MoveTest()
        {
            Paddle paddle = new NormalPaddle(0, PaddleDataFactory.GetPaddleData(PaddleType.Normal));
            float a = paddle.AngularPosition;
            paddle.AddClampAngles(0, 100);
            paddle.Move(1);
            Assert.IsFalse(paddle.AngularPosition == a);
        }

        [TestMethod()]
        public void MoveTest1()
        {
            Paddle paddle = new NormalPaddle(0, PaddleDataFactory.GetPaddleData(PaddleType.Normal));
            float a = paddle.AngularPosition;
            paddle.AddClampAngles(0, 100);
            paddle.TransferPowerUp(new PoweredUpData() { ChangePaddleSpeed = true});
            paddle.Move(1);
            Assert.IsFalse(paddle.AngularPosition == a);
        }

        [TestMethod()]
        public void TransferPowerUpTest()
        {
            PoweredUpData data = new PoweredUpData();
            data.ChangePaddleSpeed = true;
            data.GivePlayerLife = true;
            data.UndoPlayerMove = true;
            Paddle paddle = new ShortPaddle(0, PaddleDataFactory.GetPaddleData(PaddleType.Short));
            Assert.ThrowsException<System.TypeInitializationException>(()=> { paddle.TransferPowerUp(data); });
        }
    }
}