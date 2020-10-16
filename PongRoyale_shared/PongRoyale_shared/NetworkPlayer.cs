using System;
using System.Collections.Generic;
using System.Text;

namespace PongRoyale_shared
{
    public class NetworkPlayer
    {
        public byte Id { get; private set; }
        public PaddleType PaddleType { get; private set; }

        public NetworkPlayer(byte id, PaddleType paddleType)
        {
            Id = id;
            PaddleType = paddleType;
        }
    }
}
