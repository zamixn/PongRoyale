using Microsoft.VisualStudio.TestTools.UnitTesting;
using PongRoyale_client.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PongRoyale_client.Extensions.Tests
{
    [TestClass()]
    public class MiscExtensionsTests
    {
        [TestMethod()]
        public void AbsTest()
        {
            Assert.IsTrue(MiscExtensions.Abs(-69) == 69);
        }

        [TestMethod()]
        public void ClampAngleTest()
        {
            Assert.IsTrue(MiscExtensions.ClampAngle(420) >= 0 && MiscExtensions.ClampAngle(420) <= 360);
        }

        [TestMethod()]
        public void ClampTest()
        {
            Assert.IsTrue(MiscExtensions.Clamp(10, 0, 20) == 10 && MiscExtensions.Clamp(10, 15, 20) == 15 && MiscExtensions.Clamp(10, 0, 5) == 5);
        }
    }
}