using PongRoyale_client.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.Balls
{
    class DeadlyBall : Ball
    {
        public override void Render(Graphics g, Brush b)
        {
            b = new SolidBrush(Color.Black);
            base.Render(g, b);
        }
    }
}
