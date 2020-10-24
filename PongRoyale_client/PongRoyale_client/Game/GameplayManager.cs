using PongRoyale_client.Extensions;
using PongRoyale_client.Game.Balls;
using PongRoyale_client.Game.Paddles;
using PongRoyale_client.Singleton;
using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game
{
    public class GameplayManager : Singleton<GameplayManager>
    {
        public int PlayerCount { get; private set; }

        public Dictionary<byte, Paddle> PlayerPaddles { get; private set; }
        public int StartingAlivePaddleCount { get; private set; }
        public int AlivePaddleCount { get; private set; }
        public Dictionary<byte, Ball> ArenaBalls { get; private set; }

        public bool IsInitted { get; private set; }

        public Paddle LocalPaddle { get; private set; }
        public GameplayScreen GameScreen { get; private set; }

        public void InitGame(GameplayScreen gameScreen)
        {
            GameScreen = gameScreen;

            var players = RoomSettings.Instance.Players;
            PlayerCount = players.Count;

            PlayerPaddles = new Dictionary<byte, Paddle>();
            ArenaBalls = new Dictionary<byte, Ball>();

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
            Ball ball = Ball.CreateBall(bType, GameScreen.GetCenter().ToVector2(), GameSettings.DefaultBallSpeed, Vector2.Up, GameSettings.DefaultBallSize);
            ArenaBalls.Add(RoomSettings.Instance.GetNextBallId(), ball);

            IsInitted = true;
        }

        public void DestroyGame()
        {
            IsInitted = false;
            GameScreen = null;
            PlayerPaddles.Clear();
            ArenaBalls.Clear();
            LocalPaddle = null;
            PlayerCount = 0;
        }

        public void PlayerSyncMessageReceived(NetworkMessage message)
        {
            float newPos = NetworkMessage.DecodeFloat(message.ByteContents);
            PlayerPaddles[message.SenderId].OnPosSync(newPos);
        }

        public void BallSyncMessageReceived(NetworkMessage message)
        {
            NetworkMessage.DencodeBallData(message.ByteContents, out byte[] ids, out Vector2[] positions);
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
            paddle.AddLife(-1);
            RoomSettings.Instance.Players[paddleId].SetLife(paddle.Life);
            Player.Instance.SendPlayerLostLifeMessage(paddleId, RoomSettings.Instance.Players[paddleId].Life);
            if (paddle.IsAlive())
                foreach (var ball in ArenaBalls)
                {
                    ball.Value.SetPosition(GameScreen.GetCenter().ToVector2());
                }
            else
            {
                AlivePaddleCount = PlayerPaddles.Count(p => p.Value.IsAlive());
            }
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
            LocalPaddle.LocalUpdate();

            if(Player.Instance.IsRoomMaster)
                foreach (var kvp in ArenaBalls)
                {
                    var ball = kvp.Value;
                    ball.LocalMove();
                    ball.CheckCollisionWithPaddles(PlayerPaddles);
                    if (ball.CheckOutOfBounds((float)(-Math.PI / 2), PlayerPaddles, out byte paddleId))
                        OutOfBounds(kvp.Key, paddleId);
                }
        }

        private void Render()
        {
            GameScreen.Refresh();
        }
    }
}
