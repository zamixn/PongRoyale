using PongRoyale_client.Extensions;
using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.RenderChain.Debug
{
    class DebugPaddleNormalsRenderLink : RenderableChainLink
    {
        public override void Render(Graphics g)
        {
            if (IsDebug)
            {
                Pen p = new Pen(Color.Black);
                foreach (Paddle paddle in ArenaFacade.Instance.PlayerPaddles.Values)
                {
                    float angle = paddle.GetCenterAngle();
                    Vector2 paddleCenter = Utilities.GetPointOnCircle(Center, Radius, angle);
                    Vector2 paddleNormal = (Center - paddleCenter).Normalize();
                    g.DrawVector(p, paddleCenter, paddleNormal);

                    if (ArenaFacade.Instance.IsPaused)
                        break;
                }
                p.Dispose();
            }

            if (Next != null)
                Next.Render(g);
        }
    }
}
