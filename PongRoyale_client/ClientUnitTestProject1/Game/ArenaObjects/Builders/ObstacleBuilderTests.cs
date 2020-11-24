using Microsoft.VisualStudio.TestTools.UnitTesting;
using PongRoyale_client.Game.Builders;
using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace PongRoyale_client.Game.Builders.Tests
{
    [TestClass()]
    public class ObstacleBuilderTests
    {
        [TestMethod()]
        public void AddDurationTest()
        {
            ObstacleBuilder builder = new ObstacleBuilder();
            builder.AddDuration(10);
            ArenaObject obj = builder.CreateObject();
            Assert.AreEqual(10, obj.Duration);
        }

        [TestMethod()]
        public void AddPosXTest()
        {
            ObstacleBuilder builder = new ObstacleBuilder();
            builder.AddPosX(10);
            ArenaObject obj = builder.CreateObject();
            Assert.AreEqual(10, obj.PosX);
        }

        [TestMethod()]
        public void AddPosYTest()
        {
            ObstacleBuilder builder = new ObstacleBuilder();
            builder.AddPosY(10);
            ArenaObject obj = builder.CreateObject();
            Assert.AreEqual(10, obj.PosY);
        }

        [TestMethod()]
        public void AddPosTest()
        {
            ObstacleBuilder builder = new ObstacleBuilder();
            Vector2 pos = new Vector2(10, 10);
            builder.AddPos(pos);
            ArenaObject obj = builder.CreateObject();
            Assert.IsTrue(obj.PosX == 10 && obj.PosY == 10);
        }

        [TestMethod()]
        public void AddWidthTest()
        {
            ObstacleBuilder builder = new ObstacleBuilder();
            builder.AddWidth(10);
            ArenaObject obj = builder.CreateObject();
            Assert.AreEqual(10, obj.Width);
        }

        [TestMethod()]
        public void AddHeigthTest()
        {
            ObstacleBuilder builder = new ObstacleBuilder();
            builder.AddHeigth(10);
            ArenaObject obj = builder.CreateObject();
            Assert.AreEqual(10, obj.Heigth);
        }

        [TestMethod()]
        public void CreateObjectTest()
        {
            ObstacleBuilder builder = new ObstacleBuilder();
            builder.AddDuration(10).AddHeigth(10).AddPosX(10).AddPosY(10).AddWidth(10);
            ArenaObject obj = builder.CreateObject();
            Assert.IsTrue(obj.Duration == 10 && obj.Heigth == 10 && obj.PosX == 10 && obj.PosY == 10 && obj.Width == 10);
        }
    }
}