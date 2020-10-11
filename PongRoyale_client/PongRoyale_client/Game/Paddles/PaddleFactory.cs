using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.Paddles
{
    class PaddleFactory
    {
        public static Paddle CreatePaddle(PaddleType type)
        {
            switch (type)
            {
                case PaddleType.Normal:
                    return new NormalPaddle();
                case PaddleType.Long:
                    return new LongPaddle();
                case PaddleType.Short:
                    return new ShortPaddle();
                default:
                    return null;
            }
        }
    }
}
