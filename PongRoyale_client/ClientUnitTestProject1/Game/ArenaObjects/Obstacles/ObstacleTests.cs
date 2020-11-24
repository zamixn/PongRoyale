using Microsoft.VisualStudio.TestTools.UnitTesting;
using PongRoyale_client.Game.Builders;
using PongRoyale_client.Game.Obstacles;
using PongRoyale_client.Singleton;
using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        public void GetReboundStrategyTest1()
        {
            NonPassableArenaObjectFactory factory = new NonPassableArenaObjectFactory();
            ObstacleBuilder objBuilder = new ObstacleBuilder().AddDuration(10).AddHeigth(10).AddPosX(10).AddWidth(10).AddPosY(10);
            Obstacle obstacle = factory.CreateObstacle(objBuilder);

            Assert.AreEqual(ArenaObjectType.NonPassable, obstacle.Type);
        }

        [TestMethod()]
        [DataRow(10, 10, 10, 10, 10, 10, 10, 1, 1)]   // point is in bounds
        [DataRow(10, 10, 10, 10, 10, 100, 100, 1, 1)]   // point outside of bounds
        [DataRow(10, 10, 50, 50, 10, 5, 100, 1, 1)]   // point x is in bounds
        [DataRow(10, 10, 50, 50, 10, 100, 5, 1, 1)]   // point y is in bounds
        [DataRow(10, 10, 50, 50, 10, 40, 100, 1, 1)]   // point x is in bounds
        [DataRow(10, 10, 50, 50, 10, 100, 40, 1, 1)]   // point y is in bounds
        public void GetCollisionNormalTest(float posX, float posY, float duration, float width, float height, float impactPointX, float impactPointY, float impactDirectionX, float impactDirectionY)
        {
            Obstacle obstacle = new Obstacle(posX, posY, duration, width, height);
            Vector2 impactPoint = new Vector2(impactPointX, impactPointY);
            Vector2 impactDirection = new Vector2(impactDirectionX, impactDirectionY);

            Vector2 collision = obstacle.GetCollisionNormal(impactPoint, impactDirection);

            Assert.AreEqual(Math.Round(impactPoint.Normalize().Magnitude(), 2), Math.Round(collision.Magnitude(), 2));
        }
    }
}