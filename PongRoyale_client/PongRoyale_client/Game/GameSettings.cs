using PongRoyale_client.Game.Paddles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game
{
    public class GameSettings
    {
        public class PaddleSettings
        {
            public int Size;
            public float Speed;
            public float Thickness;
            public int Life;
        }

        public static readonly Dictionary<System.Type, PaddleSettings> PaddleSettingsDict =
            new Dictionary<System.Type, PaddleSettings>()
        {
            {typeof(NormalPaddle), new PaddleSettings(){
                Size = 20,
                Speed = 1,
                Thickness = 10,
                Life = 3
            }},
            {typeof(LongPaddle), new PaddleSettings(){
                Size = 40,
                Speed = .5f,
                Thickness = 13,
                Life = 4
            }},
            {typeof(ShortPaddle), new PaddleSettings(){
                Size = 12,
                Speed = 2,
                Thickness = 7,
                Life = 2
            }}
        };


        public static readonly float DefaultBallSpeed = 1f;
        public static readonly float DefaultBallSize = 20f;
    }
}
