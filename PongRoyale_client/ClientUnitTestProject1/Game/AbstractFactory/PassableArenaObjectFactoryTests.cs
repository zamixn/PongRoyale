﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using PongRoyale_client.Game;
using PongRoyale_client.Game.Builders;
using PongRoyale_client.Game.Obstacles;
using PongRoyale_client.Game.Powerups;
using System;
using System.Collections.Generic;
using System.Text;

namespace PongRoyale_client.Game.Tests
{
    [TestClass()]
    public class PassableArenaObjectFactoryTests
    {
        [TestMethod()]
        public void CreateObstacleTest()
        {
            PassableArenaObjectFactory factory = new PassableArenaObjectFactory();
            ObstacleBuilder objBuilder = new ObstacleBuilder().AddDuration(10).AddHeigth(10).AddPosX(10).AddWidth(10).AddPosY(10);
            Obstacle obstacle = factory.CreateObstacle(objBuilder);

            Assert.IsTrue(obstacle.Type == ArenaObjectType.Passable
                && obstacle.Duration == 10 && obstacle.Heigth == 10 && obstacle.PosX == 10
                && obstacle.Width == 10 && obstacle.PosY == 10);
        }

        [TestMethod()]
        public void CreatePowerupTest()
        {
            PassableArenaObjectFactory factory = new PassableArenaObjectFactory();
            PowerUpBuilder powerupBuilder = new PowerUpBuilder().AddDiameter(10).AddDuration(10).AddPos(new PongRoyale_shared.Vector2(10, 10));
            PowerUp powerup = factory.CreatePowerup(powerupBuilder);

            Assert.IsTrue(powerup.Type == ArenaObjectType.Passable
                && powerup.Diameter == 10 && powerup.Duration == 10 && powerup.PosX == 10
                && powerup.PosY == 10);
        }
    }
}