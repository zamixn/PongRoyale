using Microsoft.VisualStudio.TestTools.UnitTesting;
using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace PongRoyale_shared.Tests
{
    [TestClass()]
    public class SharedUtilitiesTests
    {
        [TestMethod()]
        public void SinFTest()
        {
            float angle = 100;
            float expected = MathF.Sin(angle);
            float result = SharedUtilities.SinF(angle);
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void CosFTest()
        {
            float angle = 100;
            float expected = MathF.Cos(angle);
            float result = SharedUtilities.CosF(angle);
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void DegToRadTest()
        {
            float angle = 100;
            float expected = (float)(angle * (Math.PI / 180));
            float result = SharedUtilities.DegToRad(angle);
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void RadToDegTest()
        {
            float angle = 100;
            float expected = (float)(angle * (180 / Math.PI));
            float result = SharedUtilities.RadToDeg(angle);
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void ClampFloatTest1()
        {
            float f = 100;
            float expected = f;
            float result = SharedUtilities.Clamp(f, 0, 101);
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void ClampFloatTest2()
        {
            float f = 100;
            float expected = 50;
            float result = SharedUtilities.Clamp(f, 0, 50);
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void ClampFloatTest3()
        {
            float f = 0;
            float expected = 30;
            float result = SharedUtilities.Clamp(f, 30, 101);
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void ClampIntTest1()
        {
            int f = 100;
            int expected = f;
            int result = SharedUtilities.Clamp(f, 0, 101);
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void ClampIntTest2()
        {
            int f = 100;
            int expected = 50;
            int result = SharedUtilities.Clamp(f, 0, 50);
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void ClampIntTest3()
        {
            int f = 0;
            int expected = 30;
            int result = SharedUtilities.Clamp(f, 30, 101);
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void MinTest()
        {
            Assert.AreEqual(SharedUtilities.Min(3.14f, 6.28f), 3.14f);
        }

        [TestMethod()]
        public void MaxTest()
        {
            Assert.AreEqual(SharedUtilities.Max(3.14f, 6.28f), 6.28f);
        }

        [TestMethod()]
        public void GetBounceDirectionTest()
        {
            Vector2 dir = new Vector2(1, 1).Normalize();
            Vector2 normal = new Vector2(-1, 1);
            Vector2 expected = new Vector2(-1, 1).Normalize();
            Vector2 result = SharedUtilities.GetBounceDirection(dir, normal);
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void GetSurfaceNormalOfLineTest()
        {
            Vector2 p1 = new Vector2(1, 0);
            Vector2 p2 = new Vector2(2, 0);
            Vector2 expected = new Vector2(0, 1);
            Vector2 result = SharedUtilities.GetSurfaceNormalOfLine(p1, p2);
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void LerpIntTest()
        {
            Assert.AreEqual(SharedUtilities.Lerp(0, 10, 0.5f), 5);
        }

        [TestMethod()]
        public void LerpFloatTest()
        {
            Assert.AreEqual(SharedUtilities.Lerp(0f, 5f, 0.5f), 2.5f);
        }
    }
}