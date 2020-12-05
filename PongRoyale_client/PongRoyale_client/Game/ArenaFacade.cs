﻿
using PongRoyale_client.Game.ArenaObjects.Powerups;
using PongRoyale_client.Game.Balls;
using PongRoyale_client.Game.Balls.Decorator;
using PongRoyale_client.Game.Obstacles;
using PongRoyale_client.Game.Paddles;
using PongRoyale_client.Game.Powerups;
using PongRoyale_client.Game.Renderable;
using PongRoyale_client.Singleton;
using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PongRoyale_client.Game
{
    public class ArenaFacade : Singleton<ArenaFacade>
    {
        public ArenaDimensions ArenaDimensions { get; private set; }
        public void UpdateDimensions(Vector2 size, Vector2 center, Vector2 renderOrigin, float radius)
        {
            ArenaDimensions = new ArenaDimensions(size, center, renderOrigin, radius);
        }

        private readonly Dictionary<ArenaObjectType, AbstractArenaObjectFactory> ArenaObjectFactories = new Dictionary<ArenaObjectType, AbstractArenaObjectFactory>()
        {
            { ArenaObjectType.NonPassable, new NonPassableArenaObjectFactory() },
            { ArenaObjectType.Passable, new PassableArenaObjectFactory() }
        };

        private List<ArenaObjectSpawner> Spawners;

        public int PlayerCount { get; private set; }

        public RenderableComposite RootRenderable;

        public Dictionary<byte, Paddle> PlayerPaddles { get; private set; } = new Dictionary<byte, Paddle>();
        public int StartingAlivePaddleCount { get; private set; }
        public int AlivePaddleCount { get; private set; }
        public Dictionary<byte, IBall> ArenaBalls { get; private set; } = new Dictionary<byte, IBall>();
        public Dictionary<byte, ArenaObject> ArenaObjects { get; private set; } = new Dictionary<byte, ArenaObject>();

        private List<Action> DoAfterGameLoop = new List<Action>();

        public bool IsInitted { get; private set; }

        public Paddle LocalPaddle { get; private set; }
        public GameplayScreen GameScreen { get; private set; }

        public bool IsPaused { get; private set; }

        public void InitGame(Dictionary<byte, NetworkPlayer> players, GameplayScreen gameScreen)
        {
            GameScreen = gameScreen;
            InitLogic(players);
        }

        public void InitLogic(Dictionary<byte, NetworkPlayer> players)
        {
            PlayerCount = players.Count;

            DoAfterGameLoop = new List<Action>();
            PlayerPaddles = new Dictionary<byte, Paddle>();
            ArenaBalls = new Dictionary<byte, IBall>();
            ArenaObjects = new Dictionary<byte, ArenaObject>();

            float deltaAngle = SharedUtilities.PI * 2 / PlayerCount;
            float angle = (-SharedUtilities.PI + deltaAngle) / 2f;

            foreach (var player in players.Values)
            {
                PaddleType pType = player.PaddleType;
                Paddle paddle = PaddleFactory.CreatePaddle(pType, player.Id);
                paddle.SetPosition(SharedUtilities.RadToDeg(angle));
                PlayerPaddles.Add(player.Id, paddle);
                player.SetLife(paddle.Life);
                if (ServerConnection.Instance.IdMatches(player.Id))
                    LocalPaddle = paddle;
                paddle.AddClampAngles(SharedUtilities.RadToDeg(angle - deltaAngle / 2), SharedUtilities.RadToDeg(angle + deltaAngle / 2));
                angle += deltaAngle;
            }
            AlivePaddleCount = PlayerPaddles.Count;
            StartingAlivePaddleCount = AlivePaddleCount;

            BallType bType = RoomSettings.Instance.BallType;
            Ball ball = Ball.CreateBall(0, bType, ArenaDimensions.Center, GameData.DefaultBallSpeed, Vector2.RandomInUnitCircle(), GameData.DefaultBallSize);
            ArenaBalls.Add(0, ball);

            Spawners = new List<ArenaObjectSpawner>();
            Spawners.Add(new ObstacleSpawner(GameData.ObstacleSpawnerParams, ArenaObjectFactories.Values.ToArray()));
            Spawners.Add(new PowerUpSpawner(GameData.PowerUpSpawnerParams, ArenaObjectFactories.Values.ToArray()));

            IsInitted = true;
            PauseGame(false);
        }

        byte idTmp = 0;
        public void OnArenaObjectCreated(ArenaObject obj)
        {
            byte id = idTmp++;
            ArenaObjects.Add(id, obj);
            obj.SetId(id);

            if (ServerConnection.Instance.IsRoomMaster)
            {
                if(obj is Obstacle)
                    ServerConnection.Instance.SendObstacleSpawnedMessage(id, obj as Obstacle);
                else if(obj is PowerUp)
                    ServerConnection.Instance.SendPowerupSpawnedMessage(id, obj as PowerUp);
            }
        }
        public void OnArenaObjectExpired(byte id)
        {
            DoAfterGameLoop.Add(() => ArenaObjects.Remove(id));
        }

        public void BallHasCollectedPowerUp(PowerUp p, IBall b)
        {
            if (!p.isUsedUp)
            {
                var data = p.PowerUppedData;
                if (ServerConnection.Instance.IsRoomMaster)
                    ServerConnection.Instance.SendBallPoweredUpMessage(b.GetId(), p.Id, data);
                OnReceivedBallPowerUpMessage(b.GetId(), p.Id, data);
            }
        }
        public void OnReceivedBallPowerUpMessage(byte ballId, byte powerUpId, PoweredUpData data)
        {
            if (ArenaObjects.TryGetValue(powerUpId, out ArenaObject pwp)) {
                var ball = ArenaBalls[ballId];
                var poweredUpBall = ball.ApplyPowerup(data);
                DoAfterGameLoop.Add(() => {
                    ArenaBalls[ball.GetId()] = poweredUpBall;
                });
                // remove after a duration
                SafeInvoke.Instance.DelayedInvoke(data.GetDurationOnBall(), () =>
                {
                    RemoveBallPowerUp(poweredUpBall, data);
                });
                (pwp as PowerUp).Use();
            }
        }
        private void RemoveBallPowerUp(IBall poweredUpBall, PoweredUpData data)
        {
            DoAfterGameLoop.Add(() => ArenaBalls[poweredUpBall.GetId()] = poweredUpBall.RemovePowerUpData(data));
        }

        public void TransferPowerUpToPaddle(byte paddleId, byte ballId, PoweredUpData poweredUpData)
        {
            if (poweredUpData.IsValid())
            {
                if (ServerConnection.Instance.IsRoomMaster)
                    ServerConnection.Instance.SendTranferPowerUpToPaddle(paddleId, ballId, poweredUpData);
                OnReceivedTransferPowerUpMessage(paddleId, ballId, poweredUpData);
            }
        }
        public void OnReceivedTransferPowerUpMessage(byte paddleId, byte ballId, PoweredUpData poweredUpData)
        {
            if (PlayerPaddles.TryGetValue(paddleId, out Paddle paddle))
            {
                paddle.TransferPowerUp(poweredUpData);
            }
            if (ArenaBalls.TryGetValue(ballId, out IBall ball))
            {
                ball.RemovePowerUpData(poweredUpData);
            }
        }

        public void DestroyGame()
        {
            GameScreen = null;
            DestroyGameLogic();
        }
        public void DestroyGameLogic()
        {
            IsInitted = false;
            PlayerPaddles.Clear();
            ArenaBalls.Clear();
            ArenaObjects.Clear();
            LocalPaddle = null;
            PlayerCount = 0;
        }

        public void ObstacleSpawnedMessageReceived(byte id, Obstacle obstacle)
        {
            obstacle.Init(GameData.ObstacleColors[obstacle.Type]);
            ArenaObjects.Add(id, obstacle);
            obstacle.SetId(id);
        }

        public void PowerUpSpawnedMessageReceived(byte id, PowerUp powerUp, PoweredUpData data)
        {
            powerUp.Init(GameData.PowerupColors[powerUp.Type], data);
            ArenaObjects.Add(id, powerUp);
            powerUp.SetId(id);
        }

        public void PlayerSyncMessageReceived(NetworkMessage message)
        {
            // sytnc message received after game end fix
            if (!IsInitted)
                return;
            float newPos = NetworkDataAdapter.Instance.DecodeFloat(message.ByteContents);
            PlayerPaddles[message.SenderId].OnPosSync(newPos);
        }

        public void BallSyncMessageReceived(NetworkMessage message)
        {
            NetworkDataAdapter.Instance.DecodeBallData(message.ByteContents, out byte[] ids, out Vector2[] positions, out Vector2[] directions);
            for(int i = 0; i < ids.Length; i++)
            {
                ArenaBalls[ids[i]].SetPosition(positions[i]);
                ArenaBalls[ids[i]].SetDirection(directions[i]);
            }
        }

        public void HandleOutOfBounds(byte ballId, byte paddleId)
        {
            Paddle paddle = PlayerPaddles[paddleId];
            PaddleLost(paddleId);
        }

        public void KillPaddle(Paddle p)
        {
            PaddleLost(PlayerPaddles.Where(kvp => kvp.Value == p).First().Key);
        }

        private void PaddleLost(byte paddleId)
        {
            Paddle paddle = PlayerPaddles[paddleId];
            paddle.AddLife(-1);
            RoomSettings.Instance.Players[paddleId].SetLife(paddle.Life);

            if (paddle.IsAlive())
                ResetRound();
            else
            {
                AlivePaddleCount = PlayerPaddles.Count(p => p.Value.IsAlive());
                PauseGame(true);
            }
        }

        private void ResetRound()
        {
            var ballTypes = ArenaBalls.Select(b => b.Value.GetBallType()).ToArray();
            var ballIds = ArenaBalls.Select(b => b.Key).ToArray();
            byte[] playerIds = RoomSettings.Instance.Players.Select(kvp => kvp.Key).ToArray();
            byte[] playerLifes = RoomSettings.Instance.Players.Select(kvp => kvp.Value.Life).ToArray();
            if (ServerConnection.Instance.IsConnected())
            {
                PauseGame(true);
                // fix for round reset not sending due to connection being overfilled
                // delay round reset message so that paddle and ball sync messages have time to clear up
                SafeInvoke.Instance.DelayedInvoke(0.5f, () => 
                    ServerConnection.Instance.SendRoundReset(ballTypes, ballIds)
                );
            }
            else
            {
                PauseGame(true);
                ResetRoundMessageReceived(ballTypes, ballIds, playerIds, playerLifes);
            }
        }

        public void PauseGame(bool pause)
        {
            IsPaused = pause;
        }

        public void ResetRoundMessageReceived(BallType[] newBalls, byte[] ballIds, byte[] playerIds, byte[] playerLifes)
        {
            for (int i = 0; i < playerIds.Length; i++)
            {
                RoomSettings.Instance.Players[playerIds[i]].SetLife(playerLifes[i]);
                PlayerPaddles[playerIds[i]].SetLife(playerLifes[i]);
            }

            ArenaBalls.Clear();
            for (int i = 0; i < newBalls.Length; i++)
            {
                Ball ball = Ball.CreateBall(ballIds[i], newBalls[i], ArenaDimensions.Center, GameData.DefaultBallSpeed, Vector2.RandomInUnitCircle(), GameData.DefaultBallSize);
                ArenaBalls.Add(ballIds[i], ball);
            }
            ArenaObjects.Clear();
            SafeInvoke.Instance.DelayedInvoke(0.5f, 
                () => { PauseGame(false); });
        }

        public void ResetBall(Ball b)
        {
            b.SetPosition(ArenaDimensions.Center);
            b.SetDirection(Vector2.RandomInUnitCircle());
        }

        public void UpdateGameLoop()
        {
            UpdateGame();
            Render();

            if (AlivePaddleCount <= 1 && StartingAlivePaddleCount > AlivePaddleCount)
            {
                ServerConnection.Instance.SendEndGameMessage();
            }
        }

        private void UpdateGame()
        {
            // note: order matters here

            if (IsPaused)
            {
                CleanUp();
                return;
            }

            LocalPaddle.LocalUpdate();

            if (ServerConnection.Instance.IsRoomMaster)
            {
                foreach (var spawner in Spawners)
                {
                    spawner.Update();

                    if (IsPaused)
                        break;
                }
            }

            foreach (var kvp in ArenaObjects)
            {
                kvp.Value.Update();

                if (IsPaused)
                    break;
            }

            if (ServerConnection.Instance.IsRoomMaster)
            {
                foreach (var kvp in ArenaBalls)
                {
                    var ball = kvp.Value;
                    ball.LocalMove();
                    ball.CheckCollisionWithPaddles(PlayerPaddles);
                    ball.CheckCollisionWithArenaObjects(ArenaObjects);
                    if (ball.CheckOutOfBounds((float)(-Math.PI / 2), PlayerPaddles, out byte paddleId))
                        HandleOutOfBounds(kvp.Key, paddleId);

                    if (IsPaused)
                        break;
                }
            }
            CleanUp();
        }

        private void Render()
        {
            GameScreen.Refresh();
        }

        private void CleanUp()
        {
            foreach (var a in DoAfterGameLoop)
            {
                a?.Invoke();
            }
            DoAfterGameLoop.Clear();
        }
    }
}
