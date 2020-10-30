using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.Paddles
{
    class ShortPaddle : Paddle
    {
        public ShortPaddle() : base(GameData.PaddleSettingsDict[typeof(ShortPaddle)])
        {
        }

        public override void Render(Graphics g, Pen p, PointF Origin, float Diameter)
        {
            p.Color = Color.Red;
            base.Render(g, p, Origin, Diameter);
        }
    }
}
