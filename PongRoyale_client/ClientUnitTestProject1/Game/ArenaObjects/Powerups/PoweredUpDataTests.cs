using Microsoft.VisualStudio.TestTools.UnitTesting;
using PongRoyale_client.Game.ArenaObjects.Powerups;
using System;
using System.Collections.Generic;
using System.Text;

namespace PongRoyale_client.Game.ArenaObjects.Powerups.Tests
{
    [TestClass()]
    public class PoweredUpDataTests
    {
        [TestMethod()]
        public void PoweredUpDataTest()
        {
            PoweredUpData data = new PoweredUpData();
            Assert.IsTrue(data.RndDirection != null && data.RndSpeed != 0);
        }

        [TestMethod()]
        public void GetDurationOnBallTest()
        {
            PoweredUpData data = new PoweredUpData();
            data.MakeBallDeadly = true;
            data.ChangeBallDirection = true;
            data.ChangeBallSpeed = true;
            data.GivePlayerLife = true;
            data.ChangePaddleSpeed = true;
            data.UndoPlayerMove = true;

            Assert.AreEqual(26, data.GetDurationOnBall());
        }
        [TestMethod()]
        public void GetDurationOnBallTest1()
        {
            PoweredUpData data = new PoweredUpData();
            data.MakeBallDeadly = true;
            data.ChangeBallDirection = false;
            data.ChangeBallSpeed = true;
            data.GivePlayerLife = false;
            data.ChangePaddleSpeed = true;
            data.UndoPlayerMove = false;

            Assert.AreEqual(15, data.GetDurationOnBall());
        }
        [TestMethod()]
        public void GetDurationOnBallTest2()
        {
            PoweredUpData data = new PoweredUpData();
            data.MakeBallDeadly = false;
            data.ChangeBallDirection = false;
            data.ChangeBallSpeed = false;
            data.GivePlayerLife = false;
            data.ChangePaddleSpeed = false;
            data.UndoPlayerMove = false;

            Assert.AreEqual(0, data.GetDurationOnBall());
        }

        [TestMethod()]
        public void GetDurationOnPaddleTest()
        {
            PoweredUpData data = new PoweredUpData();
            data.GivePlayerLife = true;
            data.ChangePaddleSpeed = true;
            data.UndoPlayerMove = true;

            Assert.AreEqual(7, data.GetDurationOnPaddle());
        }
        [TestMethod()]
        public void GetDurationOnPaddleTest1()
        {
            PoweredUpData data = new PoweredUpData();
            data.GivePlayerLife = false;
            data.ChangePaddleSpeed = true;
            data.UndoPlayerMove = false;

            Assert.AreEqual(5, data.GetDurationOnPaddle());
        }
        [TestMethod()]
        public void GetDurationOnPaddleTest2()
        {
            PoweredUpData data = new PoweredUpData();
            data.GivePlayerLife = false;
            data.ChangePaddleSpeed = false;
            data.UndoPlayerMove = false;

            Assert.AreEqual(0, data.GetDurationOnPaddle());
        }

        [TestMethod()]
        public void GetAsArrayTest()
        {
            PoweredUpData data = new PoweredUpData();
            bool[] test = new bool[]{ true, true, false, false, true, false};
            data.MakeBallDeadly = true;
            data.ChangeBallSpeed = true;
            data.ChangeBallDirection = false;
            data.GivePlayerLife = false;
            data.ChangePaddleSpeed = true;
            data.UndoPlayerMove = false;

            CollectionAssert.AreEqual(test, data.GetAsArray());
        }

        [TestMethod()]
        public void ToByteArrayTest()
        {
            PoweredUpData data = new PoweredUpData();
            byte[] test = new byte[] { 1, 1, 0, 0, 1, 0 };
            data.MakeBallDeadly = true;
            data.ChangeBallSpeed = true;
            data.ChangeBallDirection = false;
            data.GivePlayerLife = false;
            data.ChangePaddleSpeed = true;
            data.UndoPlayerMove = false;

            CollectionAssert.AreEqual(test, data.ToByteArray());
        }

        [TestMethod()]
        public void FromArrayTest()
        {
            PoweredUpData data = new PoweredUpData();
            bool[] test = new bool[] { true, true, false, false, true, false };
            data.MakeBallDeadly = true;
            data.ChangeBallSpeed = true;
            data.ChangeBallDirection = false;
            data.GivePlayerLife = false;
            data.ChangePaddleSpeed = true;
            data.UndoPlayerMove = false;

            PoweredUpData result = PoweredUpData.FromArray(test);

            Assert.IsTrue(
                result.MakeBallDeadly == true && result.ChangeBallSpeed == true && 
                result.ChangeBallDirection == false && result.GivePlayerLife == false && 
                result.ChangePaddleSpeed == true && result.UndoPlayerMove == false
                );
        }

        [TestMethod()]
        public void FromByteArrayTest()
        {
            PoweredUpData data = new PoweredUpData();
            byte[] test = new byte[] { 1, 1, 0, 0, 1, 0 };
            data.MakeBallDeadly = true;
            data.ChangeBallSpeed = true;
            data.ChangeBallDirection = false;
            data.GivePlayerLife = false;
            data.ChangePaddleSpeed = true;
            data.UndoPlayerMove = false;

            PoweredUpData result = PoweredUpData.FromByteArray(test);

            Assert.IsTrue(
                result.MakeBallDeadly == true && result.ChangeBallSpeed == true &&
                result.ChangeBallDirection == false && result.GivePlayerLife == false &&
                result.ChangePaddleSpeed == true && result.UndoPlayerMove == false
                );
        }

        [TestMethod()]
        public void RollRandomTest()
        {
            PoweredUpData data = PoweredUpData.RollRandom();

            Assert.IsTrue(data != null);
        }

        [TestMethod()]
        public void ToStringTest()
        {
            PoweredUpData data = new PoweredUpData();
            data.MakeBallDeadly = true;
            data.ChangeBallSpeed = true;
            data.ChangeBallDirection = false;
            data.GivePlayerLife = false;
            data.ChangePaddleSpeed = true;
            data.UndoPlayerMove = false;
            String s = $"(MakeBallDeadly: 1; ChangeBallSpeed: 1; ChangeBallDirection: 0; GivePlayerLife: 0; ChangePaddleSpeed: 1); UndoPlayerMove: 0";
            String test = data.ToString();

            Assert.AreEqual(s, test);
        }

        [TestMethod()]
        public void AddTest()
        {
            PoweredUpData data = new PoweredUpData();
            data.MakeBallDeadly = true;
            data.ChangeBallSpeed = true;
            data.ChangeBallDirection = true;
            data.GivePlayerLife = true;
            data.ChangePaddleSpeed = true;
            data.UndoPlayerMove = true;

            PoweredUpData data2 = new PoweredUpData();
            data.MakeBallDeadly = false;
            data.ChangeBallSpeed = false;
            data.ChangeBallDirection = false;
            data.GivePlayerLife = false;
            data.ChangePaddleSpeed = false;
            data.UndoPlayerMove = false;

            data.Add(data2);

            Assert.IsTrue(
                data.MakeBallDeadly == false && data.ChangeBallSpeed == false
                && data.ChangeBallDirection == false && data.GivePlayerLife == false
                && data.ChangePaddleSpeed == false && data.UndoPlayerMove == false
                );
        }

        [TestMethod()]
        public void RemoveTest()
        {
            PoweredUpData data = new PoweredUpData();
            data.MakeBallDeadly = true;
            data.ChangeBallSpeed = true;
            data.ChangeBallDirection = true;
            data.GivePlayerLife = true;
            data.ChangePaddleSpeed = true;
            data.UndoPlayerMove = true;

            data.Remove(data);

            Assert.IsTrue(
                data.MakeBallDeadly == false && data.ChangeBallSpeed == false
                && data.ChangeBallDirection == false && data.GivePlayerLife == false
                && data.ChangePaddleSpeed == false && data.UndoPlayerMove == false
                );
        }

        [TestMethod()]
        public void IsValidTest()
        {
            PoweredUpData data = new PoweredUpData();
            data.MakeBallDeadly = true;
            data.ChangeBallSpeed = true;
            data.ChangeBallDirection = true;
            data.GivePlayerLife = true;
            data.ChangePaddleSpeed = true;
            data.UndoPlayerMove = true;

            Assert.AreEqual(true, data.IsValid());
        }
        [TestMethod()]
        public void IsValidTest1()
        {
            PoweredUpData data = new PoweredUpData();
            data.MakeBallDeadly = false;
            data.ChangeBallSpeed = false;
            data.ChangeBallDirection = false;
            data.GivePlayerLife = false;
            data.ChangePaddleSpeed = false;
            data.UndoPlayerMove = false;

            Assert.AreEqual(false, data.IsValid());
        }
    }
}