using PongRoyale_client.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.Balls
{
    class NormalBall : Ball
    {
        public override void Render(Graphics g, Brush b)
        {
            b = new SolidBrush(Color.OrangeRed);
            base.Render(g, b);
        }
    }
}
