using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.Paddles
{
    public class PaddleColorRed : IPaddleColor
    {
        public Color ApplyColor()
        {
            return Color.Red;
        }
    }
}
