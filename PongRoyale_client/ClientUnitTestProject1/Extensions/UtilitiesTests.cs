using Microsoft.VisualStudio.TestTools.UnitTesting;
using PongRoyale_client.Extensions;
using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PongRoyale_client.Extensions.Tests
{
    [TestClass()]
    public class UtilitiesTests
    {
        [TestMethod()]
        public void GetPointOnCircleTest()
        {
            PointF point = Utilities.GetPointOnCircle(new PointF(0, 0), 10, 10);
            Assert.IsTrue(point.ToVector2().Magnitude() == 10);
        }

        [TestMethod()]
        public void GetPointOnCircleTest1()
        {
            Vector2 point = Utilities.GetPointOnCircle(new Vector2(0, 0), 10, 10);
            Assert.IsTrue(point.Magnitude() == 10);
        }

        [TestMethod()]
        public void ToVector2Test()
        {
            Assert.IsTrue(Utilities.ToVector2(new PointF(6, 9)).Equals(new Vector2(6, 9)));
        }

        [TestMethod()]
        public void IsInsideAngleTest()
        {
            Assert.IsTrue(Utilities.IsInsideAngle(10, 0, 20) == true && Utilities.IsInsideAngle(21, 0, 20) == false && Utilities.IsInsideAngle(1, 10, 20) == false);
        }

        [TestMethod()]
        public void IsInsideRangeTest()
        {
            Assert.IsTrue(Utilities.IsInsideRange(10, 0, 20) == true && Utilities.IsInsideRange(21, 0, 20) == false && Utilities.IsInsideRange(1, 10, 20) == false);
        }

        [TestMethod()]
        public void LerpTest()
        {
            Color first = Color.FromArgb(0, 0, 0, 0);
            Color second = Color.FromArgb(100, 100, 100, 100);
            Assert.IsTrue(Utilities.Lerp(first, second, -0.5f).Equals(first) && Utilities.Lerp(first, second, 1.5f).Equals(second) && Utilities.Lerp(first, second, 0.5f).Equals(Color.FromArgb(50, 50, 50, 50)));
        }
    }
}