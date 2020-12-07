using Microsoft.VisualStudio.TestTools.UnitTesting;
using PongRoyale_client;
using System;
using System.Collections.Generic;
using System.Text;

namespace PongRoyale_client.Tests
{
    [TestClass()]
    public class ArenaDimensionsTests
    {
        [TestMethod()]
        public void ArenaDimensionsTest()
        {
            ArenaDimensions arena = new ArenaDimensions(new PongRoyale_shared.Vector2(10, 10), new PongRoyale_shared.Vector2(0, 0), new PongRoyale_shared.Vector2(0, 0), 10);
            Assert.IsTrue(arena.Size.Equals(new PongRoyale_shared.Vector2(10, 10)) && arena.Center.Equals(new PongRoyale_shared.Vector2(0, 0)) && arena.RenderOrigin.Equals(new PongRoyale_shared.Vector2(0, 0)) && arena.Radius == 10);
        }

        [TestMethod()]
        public void GetDistanceFromCenterTest()
        {
            ArenaDimensions arena = new ArenaDimensions(new PongRoyale_shared.Vector2(10, 10), new PongRoyale_shared.Vector2(0, 0), new PongRoyale_shared.Vector2(0, 0), 10);
            PongRoyale_shared.Vector2 v = new PongRoyale_shared.Vector2(0, 10);
            Assert.IsTrue(arena.GetDistanceFromCenter(v) == 10);
        }
    }
}