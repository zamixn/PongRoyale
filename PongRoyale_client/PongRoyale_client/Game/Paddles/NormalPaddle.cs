﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.Paddles
{
    class NormalPaddle : Paddle
    {
        public NormalPaddle() : base(GameData.PaddleSettingsDict[typeof(NormalPaddle)])
        {
        }

        public override void Render(Graphics g, Pen p, PointF Origin, float Diameter)
        {
            p.Color = Color.Black;
            base.Render(g, p, Origin, Diameter);
        }
    }
}
