﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.Paddles
{
    class PaddleColorBlack : IPaddleColor
    {
        public Color ApplyColor()
        {
            return Color.Black;
        }
    }
}