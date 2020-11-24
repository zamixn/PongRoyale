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

        [TestMethod()]
        public void InitGameTest()
        {
            //ArenaFacade.Instance.InitGame(null);
        }

        [TestMethod()]
        public void OnArenaObjectCreatedTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void OnArenaObjectExpiredTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void BallHasCollectedPowerUpTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void OnReceivedBallPowerUpMessageTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void TransferPowerUpToPaddleTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void OnReceivedTransferPowerUpMessageTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void DestroyGameTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ObstacleSpawnedMessageReceivedTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void PowerUpSpawnedMessageReceivedTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void PlayerSyncMessageReceivedTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void BallSyncMessageReceivedTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void HandleOutOfBoundsTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void KillPaddleTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void PauseGameTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ResetRoundMessageReceivedTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ResetBallTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void UpdateGameLoopTest()
        {
            throw new NotImplementedException();
        }
    }
}