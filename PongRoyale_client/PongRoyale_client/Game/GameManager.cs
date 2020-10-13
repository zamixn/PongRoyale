using PongRoyale_client.Extensions;
using PongRoyale_client.Game.Balls;
using PongRoyale_client.Game.Paddles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game
{
    public class GameManager : Singleton<GameManager>
    {
        public int PlayerCount { get; private set; }

        public List<Paddle> PlayerPaddles { get; private set; }

        public List<Ball> ArenaBalls { get; private set; }
        public bool IsInitted { get; private set; }

        private Paddle LocalPaddle;
        public GameScreen GameScreen { get; private set; }

        public void InitGame(int playerCount, GameScreen gameScreen)
        {
            GameScreen = gameScreen;
            PlayerCount = playerCount;

            PlayerPaddles = new List<Paddle>();
            ArenaBalls = new List<Ball>();

            float deltaAngle = Utilities.PI * 2 / PlayerCount;
            float angle = (-Utilities.PI + deltaAngle - Utilities.DegToRad(20)) / 2f;

            for (int i = 0; i < PlayerCount; i++)
            {
                PaddleType pType = (PaddleType)RandomNumber.RandomNumb((int)PaddleType.Normal, (int)PaddleType.Short+1);
                Paddle paddle = PaddleFactory.CreatePaddle(pType);
                paddle.SetPosition(Utilities.RadToDeg(angle));
                PlayerPaddles.Add(paddle);
                if (LocalPaddle == null)
                    LocalPaddle = paddle;
                angle += deltaAngle;


            }

            BallType bType = (BallType)RandomNumber.RandomNumb((int)BallType.Normal, (int)BallType.Deadly + 1);
            Ball ball = Ball.CreateBall(bType, GameScreen.GetCenter().ToVector2(), GameSettings.DefaultBallSpeed, Vector2.Right, GameSettings.DefaultBallSize);
            ArenaBalls.Add(ball);

            IsInitted = true;
        }

        public void UpdateGameLoop()
        {
            UpdateGame();
            Render();
        }

        private void UpdateGame()
        {
            LocalPaddle.LocalUpdate();
            foreach (var ball in ArenaBalls)
            {
                ball.LocalUpdate();
                ball.CheckCollision(PlayerPaddles);
            }
        }

        private void Render()
        {
            GameScreen.Refresh();
        }
    }
}
