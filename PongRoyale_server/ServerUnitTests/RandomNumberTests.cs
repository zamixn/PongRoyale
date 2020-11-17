using Microsoft.VisualStudio.TestTools.UnitTesting;
using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace PongRoyale_shared.Tests
{
    [TestClass()]
    public class RandomNumberTests
    {
        [TestMethod()]
        public void NextIntTest()
        {
            int min = 0;
            int max = 100;
            int testInt = RandomNumber.NextInt(min, max);
            Assert.IsTrue(testInt >= min && testInt < max);
        }

        [TestMethod()]
        public void NextDoubleTest()
        {
            double testDouble = RandomNumber.NextDouble();
            Assert.IsTrue(testDouble >= 0 && testDouble < 1);
        }

        [TestMethod()]
        public void NextFloatTest()
        {
            float min = 0;
            float max = 100;
            float testFloat = RandomNumber.NextFloat(min, max);
            Assert.IsTrue(testFloat >= min && testFloat < max);
        }

        [TestMethod()]
        public void NextFloatTest1()
        {
            float testFloat = RandomNumber.NextFloat();
            Assert.IsTrue(testFloat >= 0 && testFloat < 1);
        }

        [TestMethod()]
        public void NextByteTest()
        {
            byte min = (byte)0;
            byte max = (byte)100;
            byte testByte = RandomNumber.NextByte(min, max);
            Assert.IsTrue(testByte > min && testByte < max);
        }

        [TestMethod()]
        public void RandomVectorTest()
        {
            Vector2 min = new Vector2(0, 0);
            Vector2 max = new Vector2(100, 100);
            Vector2 testVector = RandomNumber.RandomVector(min, max);
            Assert.IsTrue(testVector.X > 0 && testVector.X < 100 && testVector.Y > 0 && testVector.Y < 100);
        }

        [TestMethod()]
        public void GetArrayTest()
        {
            int [] a = RandomNumber.GetArray<int>(100, () => 1);
            Assert.AreEqual(a.Length, 100);
        }
    }
}