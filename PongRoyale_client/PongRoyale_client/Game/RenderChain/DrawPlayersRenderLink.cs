using PongRoyale_client.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.RenderChain
{
    class DrawPlayersRenderLink : RenderableChainLink
    {
        public Color PlayerColor;
        public float PlayerWidth;

        public float LifeRadiusOffset;

        public Font LifeFont;
        public Brush LifeBrush;

        public StringFormat LifeStringFormat;

        public DrawPlayersRenderLink(Color playerColor, float playerWidth, float lifeRadiusOffset, Font lifeFont, Brush lifeBrush, StringFormat lifeStringFormat)
        {
            PlayerColor = playerColor;
            PlayerWidth = playerWidth;
            LifeRadiusOffset = lifeRadiusOffset;
            LifeFont = lifeFont;
            LifeBrush = lifeBrush;
            LifeStringFormat = lifeStringFormat;
        }

        public override void Render(Graphics g)
        {
            foreach (Paddle paddle in ArenaFacade.Instance.PlayerPaddles.Values)
            {
                Pen p = new Pen(PlayerColor, PlayerWidth);
                paddle.Render(g, p, Origin.ToPointF(), Diameter);
                p.Dispose();

                PointF lifePos = Utilities.GetPointOnCircle(Center.ToPointF(), Radius + LifeRadiusOffset, paddle.GetCenterAngle());
                g.DrawString(paddle.Life.ToString(), LifeFont, LifeBrush, lifePos, LifeStringFormat);

                if (ArenaFacade.Instance.IsPaused)
                    break;
            }

            if (Next != null)
                Next.Render(g);
        }
    }
}
