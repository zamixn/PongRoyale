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
            Assert.IsTrue(SharedUtilities.SinF(0).Equals((float)Math.Sin(0)));
        }

        [TestMethod()]
        public void CosFTest()
        {
            Assert.IsTrue(SharedUtilities.CosF(0).Equals((float)Math.Cos(0)));
        }

        [TestMethod()]
        public void DegToRadTest()
        {
            Assert.IsTrue(SharedUtilities.DegToRad(0) == 0);
        }

        [TestMethod()]
        public void RadToDegTest()
        {
            Assert.IsTrue(SharedUtilities.RadToDeg(0) == 0);
        }

        [TestMethod()]
        public void ClampTest()
        {
            Assert.IsTrue(SharedUtilities.Clamp(10, 0, 20) == 10 && SharedUtilities.Clamp(10, 15, 20) == 15 && SharedUtilities.Clamp(30, 0, 20) == 20 &&
                SharedUtilities.Clamp(10f, 0f, 20f) == 10f && SharedUtilities.Clamp(10f, 15f, 20f) == 15f && SharedUtilities.Clamp(30f, 0f, 20f) == 20f);
        }

        [TestMethod()]
        public void MinTest()
        {
            Assert.IsTrue(SharedUtilities.Min(1f, 10f) == 1f);
        }

        [TestMethod()]
        public void MaxTest()
        {
            Assert.IsTrue(SharedUtilities.Max(1f, 10f) == 10f);
        }
        [TestMethod()]
        public void LerpTest()
        {
            Assert.IsTrue(SharedUtilities.Lerp(1f, 2f, 3f) == 4f && SharedUtilities.Lerp(1, 2, 3) == 4);
        }
    }
}