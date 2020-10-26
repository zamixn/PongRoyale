using PongRoyale_client.Extensions;
using PongRoyale_client.Game.Balls;
using PongRoyale_client.Game.Builders;
using PongRoyale_client.Game.Paddles;
using PongRoyale_client.Singleton;
using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game
{
    public class ArenaFacade : Singleton<ArenaFacade>
    {
        private readonly NetworkDataConverterAdapter Converter = NetworkDataConverterAdapter.Instance;
        public int PlayerCount { get; private set; }

        public Dictionary<byte, Paddle> PlayerPaddles { get; private set; }
        public int StartingAlivePaddleCount { get; private set; }
        public int AlivePaddleCount { get; private set; }
        public Dictionary<byte, Ball> ArenaBalls { get; private set; }
        public Dictionary<byte, ArenaObject> ArenaObjects { get; private set; }

        public bool IsInitted { get; private set; }

        public Paddle LocalPaddle { get; private set; }
        public GameplayScreen GameScreen { get; private set; }

        public bool IsPaused { get; private set; }

        public void InitGame(GameplayScreen gameScreen)
        {
            GameScreen = gameScreen;

            var players = RoomSettings.Instance.Players;
            PlayerCount = players.Count;

            PlayerPaddles = new Dictionary<byte, Paddle>();
            ArenaBalls = new Dictionary<byte, Ball>();
            ArenaObjects = new Dictionary<byte, ArenaObject>();

            float deltaAngle = SharedUtilities.PI * 2 / PlayerCount;
            float angle = (-SharedUtilities.PI + deltaAngle) / 2f;

            foreach (var player in players.Values)
            {
                PaddleType pType = player.PaddleType;
                Paddle paddle = PaddleFactory.CreatePaddle(pType);
                paddle.SetPosition(SharedUtilities.RadToDeg(angle));
                PlayerPaddles.Add(player.Id, paddle);
                player.SetLife(paddle.Life);
                if (Player.Instance.IdMatches(player.Id))
                    LocalPaddle = paddle;
                paddle.AddClampAngles(SharedUtilities.RadToDeg(angle - deltaAngle / 2), SharedUtilities.RadToDeg(angle + deltaAngle / 2));
                angle += deltaAngle;
            }
            AlivePaddleCount = PlayerPaddles.Count;
            StartingAlivePaddleCount = AlivePaddleCount;

            BallType bType = RoomSettings.Instance.BallType;
            Ball ball = Ball.CreateBall(bType, GameScreen.GetCenter().ToVector2(), GameSettings.DefaultBallSpeed, Vector2.RandomInUnitCircle(), GameSettings.DefaultBallSize);
            ArenaBalls.Add(0, ball);

            var obstacle = new ObstacleBuilder().AddHeigth(50).AddWidth(50).AddColor(Color.Red).AddDuration(1f).AddPos(GameScreen.GetCenter().ToVector2()).CreateObject();
            ArenaObjects.Add(0, obstacle);

            IsInitted = true;
            PauseGame(false);
        }

        public void DestroyGame()
        {
            IsInitted = false;
            GameScreen = null;
            PlayerPaddles.Clear();
            ArenaBalls.Clear();
            ArenaObjects.Clear();
            LocalPaddle = null;
            PlayerCount = 0;
        }

        public void PlayerSyncMessageReceived(NetworkMessage message)
        {
            float newPos = Converter.DecodeFloat(message.ByteContents);
            PlayerPaddles[message.SenderId].OnPosSync(newPos);
        }

        public void BallSyncMessageReceived(NetworkMessage message)
        {
            Converter.DecodeBallData(message.ByteContents, out byte[] ids, out Vector2[] positions);
            Debug.WriteLine(ids.Select(a => a.ToString()).Aggregate((b, c) => $"{b}, {c}"));
            for(int i = 0; i < ids.Length; i++)
            {
                ArenaBalls[ids[i]].SetPosition(positions[i]);
            }
        }
        public void PLayerLostLifeMessageReceived(NetworkMessage message)
        {
            byte playerId = message.ByteContents[0];
            byte life = message.ByteContents[1];
            PlayerPaddles[playerId].SetLife(life);
            RoomSettings.Instance.Players[playerId].SetLife(life);
        }

        public void OutOfBounds(byte ballId, byte paddleId)
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
            var ballTypes = ArenaBalls.Select(b => b.Value.bType).ToArray();
            var ballIds = ArenaBalls.Select(b => b.Key).ToArray();
            byte[] playerIds = RoomSettings.Instance.Players.Select(kvp => kvp.Key).ToArray();
            byte[] playerLifes = RoomSettings.Instance.Players.Select(kvp => kvp.Value.Life).ToArray();
            if (ServerConnection.Instance.IsConnected())
            {
                PauseGame(true);
                Player.Instance.SendRoundReset(ballTypes, ballIds);
            }
            else
                ResetRoundMessageReceived(ballTypes, ballIds, playerIds, playerLifes);
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
                Ball ball = Ball.CreateBall(newBalls[i], GameScreen.GetCenter().ToVector2(), GameSettings.DefaultBallSpeed, Vector2.RandomInUnitCircle(), GameSettings.DefaultBallSize);
                ArenaBalls.Add(ballIds[i], ball);
            }
            ArenaObjects.Clear();
            PauseGame(false);
        }

        public void ResetBall(Ball b)
        {
            b.SetPosition(GameScreen.GetCenter().ToVector2());
            b.SetDirection(Vector2.RandomInUnitCircle());
        }

        public void UpdateGameLoop()
        {
            UpdateGame();
            Render();

            if (AlivePaddleCount <= 1 && StartingAlivePaddleCount > AlivePaddleCount)
            {
                Player.Instance.SendEndGameMessage();
            }
        }

        private void UpdateGame()
        {
            if (IsPaused)
                return;

            LocalPaddle.LocalUpdate();

            if (Player.Instance.IsRoomMaster)
            {
                foreach (var kvp in ArenaObjects)
                {
                    kvp.Value.Update();
                }

                foreach (var kvp in ArenaBalls)
                {

                    var ball = kvp.Value;
                    ball.LocalMove();
                    ball.CheckCollisionWithPaddles(PlayerPaddles);
                    if (ball.CheckOutOfBounds((float)(-Math.PI / 2), PlayerPaddles, out byte paddleId))
                        OutOfBounds(kvp.Key, paddleId);

                    if (IsPaused)
                        break;
                }
            }
        }

        private void Render()
        {
            GameScreen.Refresh();
        }
    }
}
