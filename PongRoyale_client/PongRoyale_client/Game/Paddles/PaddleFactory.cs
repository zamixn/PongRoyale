using PongRoyale_shared;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.Paddles
{
    public class PaddleFactory
    {
        public static Paddle CreatePaddle(PaddleType type, byte id)
        {
            switch (type)
            {
                case PaddleType.Normal:
                    return new NormalPaddle(id, PaddleDataFactory.GetPaddleData(type));
                case PaddleType.Long:
                    return new LongPaddle(id, PaddleDataFactory.GetPaddleData(type));
                case PaddleType.Short:
                    return new ShortPaddle(id, PaddleDataFactory.GetPaddleData(type));
                default:
                    return null;
            }
        }
    }
}
