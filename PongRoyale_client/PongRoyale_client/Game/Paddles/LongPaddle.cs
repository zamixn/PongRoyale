using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game
{
    class LongPaddle : Paddle
    {
        public LongPaddle() : base(GameSettings.PlayerSizes[typeof(LongPaddle)])
        {
        }

        public override void Render(Graphics g, Pen p, PointF Origin, float Diameter)
        {
            p.Color = Color.Blue;
            base.Render(g, p, Origin, Diameter);
        }
    }
}
