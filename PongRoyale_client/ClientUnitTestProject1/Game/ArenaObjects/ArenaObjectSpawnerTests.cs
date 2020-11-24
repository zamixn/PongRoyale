using Microsoft.VisualStudio.TestTools.UnitTesting;
using PongRoyale_client.Game;
using PongRoyale_client.Game.Obstacles;
using PongRoyale_client.Singleton;
using System;
using System.Collections.Generic;
using System.Text;

namespace PongRoyale_client.Game.Tests
{
    [TestClass()]
    public class ArenaObjectSpawnerTests
    {
        [TestMethod()]
        public void UpdateTest()
        {
            ArenaFacade.Instance.UpdateDimensions(new PongRoyale_shared.Vector2(0, 0), new PongRoyale_shared.Vector2(0, 0), 10f);
            ArenaObjectSpawner spawner = new ObstacleSpawner(GameData.ObstacleSpawnerParams, new AbstractArenaObjectFactory[] { new NonPassableArenaObjectFactory()});
            GameManager.Instance.SetTimeSinceLastFrame(0.69f);
            spawner.Update();
            Assert.IsTrue(spawner.Time.Equals(0.69f));
        }
    }
}