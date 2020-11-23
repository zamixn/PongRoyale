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
            Paddle paddle = new LongPaddle(0);
            Assert.IsTrue(paddle.AngularSize == GameData.PaddleSettingsDict[typeof(LongPaddle)].Size && paddle.Thickness == GameData.PaddleSettingsDict[typeof(LongPaddle)].Thickness);
        }

        [TestMethod()]
        public void AddClampAnglesTest()
        {
            Paddle paddle = new LongPaddle(0);
            paddle.AddClampAngles(0, 10);
            Assert.IsTrue(paddle.MinAngle > paddle.MaxAngle);
        }

        [TestMethod()]
        public void GetCenterAngleTest()
        {
            Paddle paddle = new Paddles.NormalPaddle(0);
            Assert.IsTrue(SharedUtilities.DegToRad(paddle.AngularPosition + paddle.AngularSize / 2f) == paddle.GetCenterAngle());
        }

        [TestMethod()]
        public void AddLifeTest()
        {
            Paddle paddle = new Paddles.NormalPaddle(0);
            paddle.SetLife(0);
            paddle.AddLife(1);
            Assert.IsTrue(paddle.Life == 1);
        }

        [TestMethod()]
        public void IsAliveTest()
        {
            Paddle paddle = new Paddles.NormalPaddle(0);
            paddle.SetLife(0);
            Paddle paddle1 = new Paddles.NormalPaddle(0);
            paddle1.SetLife(1);
            Assert.IsTrue(paddle.IsAlive() == false && paddle1.IsAlive() == true);
        }

        [TestMethod()]
        public void OnPosSyncTest()
        {
            Paddle paddle = new Paddles.NormalPaddle(0);
            float a = paddle.AngularPosition;
            paddle.OnPosSync(0);
            Assert.IsTrue(paddle.AngularPosition == a);
        }

        [TestMethod()]
        public void MoveTest()
        {
            Paddle paddle = new Paddles.NormalPaddle(0);
            float a = paddle.AngularPosition;
            paddle.AddClampAngles(0, 100);
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
            Paddle paddle = new ShortPaddle(0);
            Assert.ThrowsException<System.TypeInitializationException>(()=> { paddle.TransferPowerUp(data); });
        }
    }
}