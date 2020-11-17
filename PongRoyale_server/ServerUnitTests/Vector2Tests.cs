using Microsoft.VisualStudio.TestTools.UnitTesting;
using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace PongRoyale_shared.Tests
{
    [TestClass()]
    public class Vector2Tests
    {
        [TestMethod()]
        public void Vector2Test()
        {
            var result = new Vector2();
            var expected = Vector2.Zero;
            Assert.AreEqual(result, expected);
        }

        [TestMethod()]
        public void Vector2Test1()
        {
            var result = new Vector2(0, 0);
            var expected = Vector2.Zero;
            Assert.AreEqual(result, expected);
        }

        [TestMethod()]
        public void Vector2Test2()
        {
            var result = new Vector2(0.0, 0.0);
            var expected = Vector2.Zero;
            Assert.AreEqual(result, expected);
        }

        [TestMethod()]
        public void Vector2Test3()
        {
            var result = Vector2.Zero;
            var expected = new Vector2(0.0f, 0.0f);
            Assert.AreEqual(result, expected);
        }

        [TestMethod()]
        public void Vector2Test4()
        {
            var result = Vector2.Right;
            var expected = new Vector2(1.0f, 0.0f);
            Assert.AreEqual(result, expected);
        }

        [TestMethod()]
        public void Vector2Test5()
        {
            var result = Vector2.Left;
            var expected = new Vector2(-1.0f, 0.0f);
            Assert.AreEqual(result, expected);
        }

        [TestMethod()]
        public void Vector2Test6()
        {
            var result = Vector2.Up;
            var expected = new Vector2(0.0f, 1.0f);
            Assert.AreEqual(result, expected);
        }

        [TestMethod()]
        public void Vector2Test7()
        {
            var result = Vector2.Down;
            var expected = new Vector2(0.0f, -1.0f);
            Assert.AreEqual(result, expected);
        }

        [TestMethod()]
        public void DistanceTest()
        {
            var result = Vector2.Distance(Vector2.Zero, new Vector2(0, 2));
            var expected = 2f;
            Assert.AreEqual(result, expected);
        }

        [TestMethod()]
        public void AngleTest()
        {
            var result = Vector2.Angle(Vector2.Right, Vector2.Up);
            var expected = MathF.PI / 2f;
            Assert.AreEqual(result, expected);
        }

        [TestMethod()]
        public void SignedAngleTest()
        {
            var result = Vector2.SignedAngle(Vector2.Right, Vector2.Down);
            var expected = -MathF.PI / 2f;
            Assert.AreEqual(result, expected);
        }

        [TestMethod()]
        public void SignedAngleDegTest()
        {
            var result = Vector2.AngleDeg(Vector2.Right, Vector2.Up);
            var expected = 90;
            Assert.AreEqual(result, expected);
        }

        [TestMethod()]
        public void AngleDegTest()
        {
            var result = Vector2.SignedAngleDeg(Vector2.Right, Vector2.Down);
            var expected = -90;
            Assert.AreEqual(result, expected);
        }

        [TestMethod()]
        public void MagnitudeTest()
        {
            var result = new Vector2(3, 4).Magnitude();
            var expected = 5;
            Assert.AreEqual(result, expected);
        }

        [TestMethod()]
        public void NormalizeTest()
        {
            var result = new Vector2(0, 5).Normalize();
            var expected = new Vector2(0, 1);
            Assert.AreEqual(result, expected);
        }

        [TestMethod()]
        public void RandomInUnitCircleTest()
        {
            var result = Vector2.RandomInUnitCircle();
            var expected = result.Normalize();
            Assert.AreEqual(result, expected);
        }

        [TestMethod()]
        public void ToStringTest()
        {
            var result = Vector2.Left.ToString();
            var expected = "-1, 0";
            Assert.AreEqual(result, expected);
        }

        [TestMethod()]
        public void EqualsTest()
        {
            var result = Vector2.Zero.Equals(new Vector2(0, 0));
            var expected = true;
            Assert.AreEqual(result, expected);
        }

        [TestMethod()]
        public void EqualsTest2()
        {
            var result = Vector2.Zero.Equals(new Vector2(0, 1));
            var expected = false;
            Assert.AreEqual(result, expected);
        }

        [TestMethod()]
        public void EqualsTest3()
        {
            var result = Vector2.Zero.Equals(new Vector2(1, 1));
            var expected = false;
            Assert.AreEqual(result, expected);
        }

        [TestMethod()]
        public void EqualsTest4()
        {
            var result = Vector2.Zero.Equals(5);
            var expected = false;
            Assert.AreEqual(result, expected);
        }

        [TestMethod()]
        public void LerpTest()
        {
            var result = Vector2.Lerp(Vector2.Zero, Vector2.One, 0.5f);
            var expected = new Vector2(0.5f, 0.5f);
            Assert.AreEqual(result, expected);
        }

        [TestMethod()]
        public void LerpTest1()
        {
            var result = Vector2.Lerp(Vector2.Zero, Vector2.One, -1f);
            var expected = new Vector2(0, 0);
            Assert.AreEqual(result, expected);
        }

        [TestMethod()]
        public void LerpTest2()
        {
            var result = Vector2.Lerp(Vector2.Zero, Vector2.One, 2f);
            var expected = new Vector2(1, 1);
            Assert.AreEqual(result, expected);
        }


        [TestMethod()]
        public void MultiplicationTest()
        {
            var result = Vector2.One * 5;
            var expected = new Vector2(5, 5);
            Assert.AreEqual(result, expected);
        }

        [TestMethod()]
        public void SubtractionTest()
        {
            var result = Vector2.One - Vector2.Right;
            var expected = Vector2.Up;
            Assert.AreEqual(result, expected);
        }

        [TestMethod()]
        public void SubtractionTest2()
        {
            var result = -Vector2.One;
            var expected = new Vector2(-1, -1);
            Assert.AreEqual(result, expected);
        }
    }
}