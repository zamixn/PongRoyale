using Microsoft.VisualStudio.TestTools.UnitTesting;
using PongRoyale_client.Game;
using PongRoyale_client.Game.ArenaObjects.Powerups;
using PongRoyale_client.Game.Balls;
using PongRoyale_client.Game.Balls.Decorator;
using PongRoyale_client.Game.Obstacles;
using PongRoyale_client.Game.Paddles;
using PongRoyale_client.Game.Powerups;
using PongRoyale_client.Singleton;
using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PongRoyale_client.Game.Tests
{
    [TestClass()]
    public class ArenaFacadeTests
    {
        [TestInitialize()]
        public void Initialize()
        {
            GameManager.StartLocalGame();
            ArenaFacade.Instance.UpdateDimensions(new Vector2(451, 451), new Vector2(225.5, 225.5), new Vector2(0, 0) ,200.5f);
            ArenaFacade.Instance.PlayerPaddles.Add(0, new NormalPaddle(0, PaddleDataFactory.GetPaddleData(PaddleType.Normal)));
        }

        [TestCleanup()]
        public void Cleanup()
        {
            ArenaFacade.Instance.DestroyGameLogic();
        }


        [TestMethod()]
        public void UpdateDimensionsTest()
        {
            ArenaFacade.Instance.UpdateDimensions(new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), 10f);
            Assert.IsTrue(ArenaFacade.Instance.ArenaDimensions.Size.Equals(new Vector2(0, 0)) && ArenaFacade.Instance.ArenaDimensions.Center.Equals(new Vector2(0, 0)) 
                && ArenaFacade.Instance.ArenaDimensions.RenderOrigin.Equals(new Vector2(0, 0)) 
                && ArenaFacade.Instance.ArenaDimensions.Radius.Equals(10f));
        }

        [TestMethod()]
        public void InitLogicTest()
        {
            ArenaFacade.Instance.InitLogic(new Dictionary<byte, NetworkPlayer>() {
                { 0, new NetworkPlayer(0, 3, PaddleType.Normal) }
            });

            Assert.IsFalse(ArenaFacade.Instance.IsPaused);
        }

        [TestMethod()]
        public void OnArenaObjectCreatedTest()
        {
            PowerUp powerUp = new PowerUp(0, 0, 0, 0, 0);
            ArenaFacade.Instance.OnArenaObjectCreated(powerUp);
            Assert.IsTrue(ArenaFacade.Instance.ArenaObjects.ContainsValue(powerUp));
        }

        [TestMethod()]
        public void OnArenaObjectCreatedTest1()
        {
            Obstacle obs = new Obstacle(0, 0, 0, 0, 0);
            ArenaFacade.Instance.OnArenaObjectCreated(obs);
            Assert.IsTrue(ArenaFacade.Instance.ArenaObjects.ContainsValue(obs));
        }

        [TestMethod()]
        public void OnArenaObjectExpiredTest()
        {
            ArenaFacade.Instance.OnArenaObjectExpired(0);
            Assert.IsTrue(!ArenaFacade.Instance.ArenaObjects.ContainsKey(0));
        }

        [TestMethod()]
        public void BallHasCollectedPowerUpTest()
        {
            PowerUp powerUp = new PowerUp(0, 0, 0, 0, 0);
            powerUp.Init(Color.Transparent, new PoweredUpData());
            IBall ball = Ball.CreateBall(0, BallType.Normal, new Vector2(0, 0), 0, new Vector2(0, 1), 5);

            ArenaFacade.Instance.ArenaObjects.Add(powerUp.Id, powerUp);
            ArenaFacade.Instance.ArenaBalls.Add(ball.GetId(), ball);

            ArenaFacade.Instance.BallHasCollectedPowerUp(powerUp, ball);
            Assert.IsTrue(powerUp.isUsedUp);
        }


        [TestMethod()]
        public void BallHasCollectedPowerUpTest1()
        {
            PowerUp powerUp = new PowerUp(0, 0, 0, 0, 0);
            powerUp.Use();
            IBall ball = Ball.CreateBall(0, BallType.Normal, new Vector2(0, 0), 0, new Vector2(0, 1), 5);



            ArenaFacade.Instance.ArenaObjects.Add(powerUp.Id, powerUp);
            ArenaFacade.Instance.ArenaBalls.Add(ball.GetId(), ball);

            ArenaFacade.Instance.BallHasCollectedPowerUp(powerUp, ball);
            Assert.IsTrue(powerUp.isUsedUp);
        }

        [TestMethod()]
        public void OnReceivedBallPowerUpMessageTest()
        {
            PowerUp powerUp = new PowerUp(0, 0, 0, 0, 0);
            powerUp.Use();
            IBall ball = Ball.CreateBall(0, BallType.Normal, new Vector2(0, 0), 0, new Vector2(0, 1), 5);

            ArenaFacade.Instance.BallHasCollectedPowerUp(powerUp, ball);
            Assert.IsTrue(powerUp.isUsedUp);
        }

        [TestMethod()]
        public void TransferPowerUpToPaddleTest()
        {
            IBall ball = Ball.CreateBall(0, BallType.Normal, new Vector2(0, 0), 0, new Vector2(0, 1), 5);
            ArenaFacade.Instance.ArenaBalls.Add(ball.GetId(), ball);
            ArenaFacade.Instance.TransferPowerUpToPaddle(0, 0, new PoweredUpData() { ChangePaddleSpeed = true});
            Assert.IsTrue(ArenaFacade.Instance.PlayerPaddles[0].PowerUppedData.ChangePaddleSpeed);
        }

        [TestMethod()]
        public void TransferPowerUpToPaddleTest1()
        {
            IBall ball = Ball.CreateBall(0, BallType.Normal, new Vector2(0, 0), 0, new Vector2(0, 1), 5);
            ArenaFacade.Instance.ArenaBalls.Add(ball.GetId(), ball);
            ArenaFacade.Instance.TransferPowerUpToPaddle(0, 0, new PoweredUpData());
            Assert.IsFalse(ArenaFacade.Instance.PlayerPaddles[0].PowerUppedData.ChangePaddleSpeed);
        }

        [TestMethod()]
        public void OnReceivedTransferPowerUpMessageTest()
        {
            IBall ball = Ball.CreateBall(0, BallType.Normal, new Vector2(0, 0), 0, new Vector2(0, 1), 5);
            ArenaFacade.Instance.ArenaBalls.Add(ball.GetId(), ball);
            ArenaFacade.Instance.TransferPowerUpToPaddle(0, 0, new PoweredUpData() { ChangePaddleSpeed = true });
            Assert.IsTrue(ArenaFacade.Instance.PlayerPaddles[0].PowerUppedData.ChangePaddleSpeed);
        }

        [TestMethod()]
        public void DestroyGameLogicTest()
        {
            ArenaFacade.Instance.DestroyGameLogic();
            Assert.IsTrue(ArenaFacade.Instance.PlayerPaddles.Count == 0 && ArenaFacade.Instance.ArenaBalls.Count == 0
                 && ArenaFacade.Instance.ArenaObjects.Count == 0 && !ArenaFacade.Instance.IsInitted);
        }

        [TestMethod()]
        public void ObstacleSpawnedMessageReceivedTest()
        {
            Obstacle obs = new Obstacle(0, 0, 0, 0, 0);
            obs.SetTypeParams(ArenaObjectType.NonPassable);
            ArenaFacade.Instance.ObstacleSpawnedMessageReceived(5, obs);
            Assert.IsTrue(obs.Id == 5 && ArenaFacade.Instance.ArenaObjects.ContainsValue(obs));
        }

        [TestMethod()]
        public void PowerUpSpawnedMessageReceivedTest()
        {
            PowerUp obs = new PowerUp(0, 0, 0, 0, 0);
            obs.SetTypeParams(ArenaObjectType.NonPassable);
            ArenaFacade.Instance.PowerUpSpawnedMessageReceived(5, obs, new PoweredUpData());
            Assert.IsTrue(obs.Id == 5 && ArenaFacade.Instance.ArenaObjects.ContainsValue(obs));
        }
    }
}