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
        public override void Render(Graphics g, Brush b, PointF Origin, float Diameter)
        {
            b = new SolidBrush(Color.Black);
            base.Render(g, b, Origin, 25);
        }
    }
}
