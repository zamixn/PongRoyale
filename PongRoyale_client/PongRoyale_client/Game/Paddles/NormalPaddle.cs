﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.Paddles
{
    public class NormalPaddle : Paddle
    {
        public NormalPaddle(byte id, PaddleSettings settings) : base(settings, id)
        {
        }

        public override void Render(Graphics g, Pen p, PointF Origin, float Diameter)
        {
            p.Color = PaddleColor.ApplyColor();
            base.Render(g, p, Origin, Diameter);
        }
    }
}
