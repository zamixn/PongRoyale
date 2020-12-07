using Microsoft.VisualStudio.TestTools.UnitTesting;
using PongRoyale_client.Game.ArenaObjects;
using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace PongRoyale_client.Game.ArenaObjects.Tests
{
    [TestClass()]
    public class ArenaObjectSpawnerParamsTests
    {
        [TestMethod()]
        public void RollIntervalTest()
        {
            ArenaObjectSpawnerParams p = new ArenaObjectSpawnerParams();
            p.MinInterval = 1;
            p.MaxInterval = 10;
            Assert.IsTrue(p.RollInterval() <= p.MaxInterval && p.RollInterval() >= p.MinInterval);
        }

        [TestMethod()]
        public void RollWidthTest()
        {
            ArenaObjectSpawnerParams p = new ArenaObjectSpawnerParams();
            p.MinWidth= 1;
            p.MaxWidth = 10;
            Assert.IsTrue(p.RollWidth() <= p.MaxWidth && p.RollWidth() >= p.MinWidth);
        }

        [TestMethod()]
        public void RollHeightTest()
        {
            ArenaObjectSpawnerParams p = new ArenaObjectSpawnerParams();
            p.MinHeight = 1;
            p.MaxHeight = 10;
            Assert.IsTrue(p.RollHeight() <= p.MaxHeight && p.RollHeight() >= p.MinHeight);
        }

        [TestMethod()]
        public void RollDurationTest()
        {
            ArenaObjectSpawnerParams p = new ArenaObjectSpawnerParams();
            p.MinDuration = 1;
            p.MaxDuration = 10;
            Assert.IsTrue(p.RollDuration() <= p.MaxDuration && p.RollDuration() >= p.MinDuration);
        }

        [TestMethod()]
        public void RollPositionTest()
        {
            ArenaFacade.Instance.UpdateDimensions(new Vector2(10, 10), new Vector2(0, 0), new Vector2(0, 0), 10f);
            ArenaObjectSpawnerParams p = new ArenaObjectSpawnerParams();
            Vector2 vec = p.RollPosition();
            var dims = ArenaFacade.Instance.ArenaDimensions;
            var halfSize = new Vector2(dims.Radius, dims.Radius);
            var min = dims.Center - halfSize;
            var max = dims.Center + halfSize;
            Assert.IsTrue(vec.X <= max.X && vec.X >= min.X && vec.Y <= max.Y && vec.Y >= min.Y);
        }
    }
}