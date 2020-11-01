using PongRoyale_shared;
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
        public static Paddle CreatePaddle(PaddleType type, byte id)
        {
            switch (type)
            {
                case PaddleType.Normal:
                    return new NormalPaddle(id);
                case PaddleType.Long:
                    return new LongPaddle(id);
                case PaddleType.Short:
                    return new ShortPaddle(id);
                default:
                    return null;
            }
        }
    }
}
