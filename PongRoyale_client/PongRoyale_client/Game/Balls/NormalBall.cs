using PongRoyale_client.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.Balls
{
    public class NormalBall : Ball
    {

        public override Color GetColor()
        {
            return Color.Black;
        }

        public override void Render(Graphics g, Brush b)
        {
            b = new SolidBrush(Color.Black);
            base.Render(g, b);
        }

        public override void LocalMove()
        {
            base.LocalMove();
        }
    }
}
