using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.RenderChain
{
    class BorderRenderLink : RenderableChainLink
    {
        private Color BorderColor;
        private float BorderWidth;

        public BorderRenderLink(Color borderColor, float borderWidth)
        {
            BorderColor = borderColor;
            BorderWidth = borderWidth;
        }

        public override void Render(Graphics g)
        {
            Pen p = new Pen(BorderColor, BorderWidth);
            g.DrawRectangle(p, BorderWidth, BorderWidth, Width - BorderWidth * 2, Height - BorderWidth * 2);
            p.Dispose();

            if (Next != null)
                Next.Render(g);
        }
    }
}
