using Microsoft.VisualStudio.TestTools.UnitTesting;
using PongRoyale_client.Game.ArenaObjects.Powerups;
using PongRoyale_client.Game.Builders;
using PongRoyale_client.Game.Obstacles;
using PongRoyale_client.Game.Powerups;
using PongRoyale_client.Singleton;
using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PongRoyale_client.Game.Powerups.Tests
{
    [TestClass()]
    public class PowerUpTests
    {
        [TestMethod()]
        public void PowerUpTest()
        {
            PowerUp powerUp = new PowerUp(10, 10, 10, 10, 10);

            Assert.IsTrue(powerUp.Duration == 10 && powerUp.PosX == 10 && powerUp.PosY == 10 && powerUp.Width == 10 && powerUp.Heigth == 10);
        }

        [TestMethod()]
        public void InitTest()
        {
            PoweredUpData data = new PoweredUpData();
            PowerUp powerUp = new PowerUp(10, 10, 10, 10, 10);

            powerUp.Init(Color.Red, data);

            Assert.IsTrue(powerUp.Color == Color.Red && powerUp.PowerUppedData != null);
        }

        [TestMethod()]
        public void GetBoundsTest()
        {
            PowerUp powerUp = new PowerUp(10, 10, 10, 10, 10);
            float d = 10 * .8f;
            float r = d / 2f;
            Rect2D rect = powerUp.GetBounds();
            Rect2D test = new Rect2D(10 - r, 10 - r, d, d);

            Assert.AreEqual(test, rect);
        }

        [TestMethod()]
        public void GetReboundStrategyTest()
        {
            PassableArenaObjectFactory factory = new PassableArenaObjectFactory();
            PowerUpBuilder objBuilder = new PowerUpBuilder().AddDuration(10).AddDiameter(10).AddPosX(10).AddPosY(10);
            PowerUp powerUp = factory.CreatePowerup(objBuilder);

            Assert.AreEqual(ArenaObjectType.Passable, powerUp.Type);
        }

        [TestMethod()]
        public void GetReboundStrategyTest1()
        {
            NonPassableArenaObjectFactory factory = new NonPassableArenaObjectFactory();
            PowerUpBuilder objBuilder = new PowerUpBuilder().AddDuration(10).AddDiameter(10).AddPosX(10).AddPosY(10);
            PowerUp powerUp = factory.CreatePowerup(objBuilder);

            Assert.AreEqual(ArenaObjectType.NonPassable, powerUp.Type);
        }

        [TestMethod()]
        [DataRow(ArenaObjectType.NonPassable)]
        [DataRow(ArenaObjectType.Passable)]
        public void GetReboundStrategyTest(ArenaObjectType type)
        {
            PowerUpBuilder builder = new PowerUpBuilder();
            PowerUp a = builder.CreateObject() as PowerUp;
            a.SetTypeParams(type);
            var str = a.GetReboundStrategy();

            Assert.IsTrue(a.Type == type);
        }
    }
}