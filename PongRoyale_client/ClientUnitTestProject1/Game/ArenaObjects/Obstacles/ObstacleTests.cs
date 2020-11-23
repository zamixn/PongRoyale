using Microsoft.VisualStudio.TestTools.UnitTesting;
using PongRoyale_client.Game.Builders;
using PongRoyale_client.Game.Obstacles;
using PongRoyale_client.Singleton;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Text;

namespace PongRoyale_client.Game.Obstacles.Tests
{
    [TestClass()]
    public class ObstacleTests
    {
        [TestMethod()]
        public void ObstacleTest()
        {
            Obstacle obstacle = new Obstacle(20, 20, 30, 10, 10);

            Assert.IsTrue(obstacle.PosX == 20 && obstacle.PosY == 20 && obstacle.Duration == 30 && obstacle.Width == 10 && obstacle.Heigth == 10);
        }

        [TestMethod()]
        public void InitTest()
        {
            Obstacle obstacle = new Obstacle(20, 20, 30, 10, 10);

            obstacle.Init(Color.Blue);

            Assert.AreEqual(Color.Blue, obstacle.Color);
        }

        [TestMethod()]
        public void GetBoundsTest()
        {
            Obstacle obstacle = new Obstacle(20, 20, 30, 10, 10);

            Rect2D bounds = new Rect2D(20 - 10 / 2f, 20 - 10 / 2f, 10, 10);

            Assert.AreEqual(bounds, obstacle.GetBounds());
        }

        [TestMethod()]
        public void GetReboundStrategyTest()
        {
            PassableArenaObjectFactory factory = new PassableArenaObjectFactory();
            ObstacleBuilder objBuilder = new ObstacleBuilder().AddDuration(10).AddHeigth(10).AddPosX(10).AddWidth(10).AddPosY(10);
            Obstacle obstacle = factory.CreateObstacle(objBuilder);

            Assert.AreEqual(ArenaObjectType.Passable, obstacle.Type);
        }

        [TestMethod()]
        public void RenderTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void GetCollisionNormalTest()
        {
            throw new NotImplementedException();
        }
    }
}