using Microsoft.VisualStudio.TestTools.UnitTesting;
using PongRoyale_client.Game.Obstacles;
using PongRoyale_client.Game.Paddles;
using PongRoyale_client.Singleton;
using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace PongRoyale_client.Game.Tests
{
    [TestClass()]
    public class ArenaObjectSpawnerTests
    {
        [TestInitialize()]
        public void Initialize()
        {
            GameManager.StartLocalGame();
            ArenaFacade.Instance.UpdateDimensions(new Vector2(451, 451), new Vector2(225.5, 225.5), new Vector2(0, 0), 200.5f);
            ArenaFacade.Instance.PlayerPaddles.Add(0, new NormalPaddle(0, PaddleDataFactory.GetPaddleData(PaddleType.Normal)));
        }

        [TestCleanup()]
        public void Cleanup() { ArenaFacade.Instance.PlayerPaddles.Clear(); }

        [TestMethod()]
        public void UpdateTest()
        {
            ArenaFacade.Instance.UpdateDimensions(new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), 10f);
            ArenaObjectSpawner spawner = new ObstacleSpawner(GameData.ObstacleSpawnerParams, new AbstractArenaObjectFactory[] { new NonPassableArenaObjectFactory()});
            GameManager.Instance.SetTimeSinceLastFrame(0.69f);
            spawner.Update();
            Assert.IsTrue(spawner.Time.Equals(0.69f));
        }

        [TestMethod()]
        public void SpawnTest()
        {
            ArenaObjectSpawner spawner = new ObstacleSpawner(GameData.ObstacleSpawnerParams, new AbstractArenaObjectFactory[] { new NonPassableArenaObjectFactory() });

            // a long time ensures spawning
            GameManager.Instance.SetTimeSinceLastFrame(100f);
            spawner.Update();
            Assert.IsTrue(ArenaFacade.Instance.ArenaObjects.Count > 0);
        }
    }
}