using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game
{
    public class Paddle
    {
        public float AngularPosition { get; private set; }
        public float AngularSize { get; private set; }

        public Paddle(float angularPosition, float angularSize)
        {
            AngularPosition = angularPosition;
            AngularSize = angularSize;
        }
    }
}
