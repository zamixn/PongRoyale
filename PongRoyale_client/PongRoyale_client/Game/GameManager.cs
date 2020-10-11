using PongRoyale_client.Extensions;
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

        public bool IsInitted { get; private set; }

        public void InitGame(int playerCount)
        {
            PlayerCount = playerCount;

            PlayerPaddles = new List<Paddle>();

            float deltaAngle = Utilities.PI * 2 / PlayerCount;
            float angle = (-Utilities.PI + deltaAngle - Utilities.DegToRad(20)) / 2f;
            Random random = new Random();

            for (int i = 0; i < PlayerCount; i++)
            {
                PaddleType type = (PaddleType) random.Next((int)PaddleType.Normal, (int)PaddleType.Short+1);
                Paddle paddle = PaddleFactory.CreatePaddle(type);
                paddle.SetPosition(Utilities.RadToDeg(angle));
                PlayerPaddles.Add(paddle);
                angle += deltaAngle;
            }
            IsInitted = true;
        }
    }
}
