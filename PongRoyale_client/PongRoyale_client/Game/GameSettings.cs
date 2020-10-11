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
        public static readonly Dictionary<System.Type, float> PlayerSizes = new Dictionary<System.Type, float>() 
        {
            {typeof(NormalPaddle), 20},
            {typeof(LongPaddle), 40},
            {typeof(ShortPaddle), 10}
        };

    }
}
