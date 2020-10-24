using System;
using System.Collections.Generic;
using System.Text;

namespace PongRoyale_shared
{
    public class NetworkPlayer
    {
        public byte Id { get; private set; }
        public PaddleType PaddleType { get; private set; }
        public byte Life { get; private set; }
        public void SetLife(int life)
        {
            Life = (byte)life;
        }

        public NetworkPlayer(byte id, int life, PaddleType paddleType)
        {
            Id = id;
            PaddleType = paddleType;
            Life = (byte)life;
        }
    }
}
