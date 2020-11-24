using Microsoft.VisualStudio.TestTools.UnitTesting;
using PongRoyale_client.Game.Paddles;
using System;
using System.Collections.Generic;
using System.Text;

namespace PongRoyale_client.Game.Paddles.Tests
{
    [TestClass()]
    public class PaddleFactoryTests
    {
        [TestMethod()]
        public void CreatePaddleTest()
        {
            Paddle p = PaddleFactory.CreatePaddle(PongRoyale_shared.PaddleType.Long, 10);
            Type t = typeof(LongPaddle);
            Assert.IsTrue(p.Id == 10 && p.GetType().Equals(t));
        }
        [TestMethod()]
        public void CreatePaddleTest1()
        {
            Paddle p = PaddleFactory.CreatePaddle(PongRoyale_shared.PaddleType.Normal, 10);
            Type t = typeof(NormalPaddle);
            Assert.IsTrue(p.Id == 10 && p.GetType().Equals(t));
        }
        [TestMethod()]
        public void CreatePaddleTest2()
        {
            Paddle p = PaddleFactory.CreatePaddle(PongRoyale_shared.PaddleType.Short, 10);
            Type t = typeof(ShortPaddle);
            Assert.IsTrue(p.Id == 10 && p.GetType().Equals(t));
        }
    }
}