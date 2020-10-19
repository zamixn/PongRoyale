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
    public class GameManager : Singleton<GameManager>
    {
        public int PlayerCount { get; private set; }

        public Dictionary<byte, Paddle> PlayerPaddles { get; private set; }
        public Dictionary<byte, Ball> ArenaBalls { get; private set; }

        public bool IsInitted { get; private set; }

        public Paddle LocalPaddle { get; private set; }
        public GameScreen GameScreen { get; private set; }

        public void InitGame(GameScreen gameScreen)
        {
            GameScreen = gameScreen;

            List<NetworkPlayer> players = RoomSettings.Instance.Players;
            PlayerCount = players.Count;

            PlayerPaddles = new Dictionary<byte, Paddle>();
            ArenaBalls = new Dictionary<byte, Ball>();

            float deltaAngle = SharedUtilities.PI * 2 / PlayerCount;
            float angle = (-SharedUtilities.PI + deltaAngle) / 2f;

            for (int i = 0; i < PlayerCount; i++)
            {
                PaddleType pType = players[i].PaddleType;
                Paddle paddle = PaddleFactory.CreatePaddle(pType);
                paddle.SetPosition(SharedUtilities.RadToDeg(angle));
                PlayerPaddles.Add(players[i].Id, paddle);
                if (Player.Instance.IdMatches(players[i].Id))
                    LocalPaddle = paddle;
                paddle.AddClampAngles(SharedUtilities.RadToDeg(angle - deltaAngle / 2), SharedUtilities.RadToDeg(angle + deltaAngle / 2));
                angle += deltaAngle;
            }

            BallType bType = RoomSettings.Instance.BallType;
            Ball ball = Ball.CreateBall(bType, GameScreen.GetCenter().ToVector2(), GameSettings.DefaultBallSpeed, Vector2.Up, GameSettings.DefaultBallSize);
            ArenaBalls.Add(RoomSettings.Instance.GetNextBallId(), ball);

            IsInitted = true;
        }

        public void PlayerSyncMessageReceived(NetworkMessage message)
        {
            PlayerPaddles[message.SenderId].SetPosition(NetworkMessage.DecodeFloat(message.ByteContents));
        }

        public void BallSyncMessageReceived(NetworkMessage message)
        {
            NetworkMessage.DencodeBallData(message.ByteContents, out byte[] ids, out Vector2[] positions);
            for(int i = 0; i < ids.Length; i++)
            {
                ArenaBalls[ids[i]].SetPosition(positions[i]);
            }
        }

        public void UpdateGameLoop()
        {
            UpdateGame();
            Render();
        }

        private void UpdateGame()
        {
            LocalPaddle.LocalUpdate();

            if(Player.Instance.IsRoomMaster)
                foreach (var ball in ArenaBalls.Values)
                {
                    ball.LocalMove();
                    ball.CheckCollision(PlayerPaddles);
                }
        }

        private void Render()
        {
            GameScreen.Refresh();
        }
    }
}
