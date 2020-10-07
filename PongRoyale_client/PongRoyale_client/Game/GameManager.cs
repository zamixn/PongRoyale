using PongRoyale_client.Extensions;
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
            float angle = (-Utilities.PI + deltaAngle - Utilities.DegToRad(GameSettings.PlayerAngularSize)) / 2f;
            for (int i = 0; i < PlayerCount; i++)
            {
                PlayerPaddles.Add(new Paddle(Utilities.RadToDeg(angle), GameSettings.PlayerAngularSize));
                angle += deltaAngle;
            }
            IsInitted = true;
        }
    }
}
