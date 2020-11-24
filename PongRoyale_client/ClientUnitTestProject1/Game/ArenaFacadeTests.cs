using Microsoft.VisualStudio.TestTools.UnitTesting;
using PongRoyale_client.Game;
using System;
using System.Collections.Generic;
using System.Text;

namespace PongRoyale_client.Game.Tests
{
    [TestClass()]
    public class ArenaFacadeTests
    {
        [TestMethod()]
        public void UpdateDimensionsTest()
        {
            ArenaFacade.Instance.UpdateDimensions(new PongRoyale_shared.Vector2(0, 0), new PongRoyale_shared.Vector2(0, 0), 10f);
            Assert.IsTrue(ArenaFacade.Instance.ArenaDimensions.Size.Equals(new PongRoyale_shared.Vector2(0, 0)) && ArenaFacade.Instance.ArenaDimensions.Center.Equals(new PongRoyale_shared.Vector2(0, 0)) &&
                ArenaFacade.Instance.ArenaDimensions.Radius.Equals(10f));
        }
    }
}