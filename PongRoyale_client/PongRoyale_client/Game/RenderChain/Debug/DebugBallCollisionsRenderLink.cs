using PongRoyale_client.Extensions;
using PongRoyale_client.Game.Balls.Decorator;
using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.RenderChain.Debug
{
    class DebugBallCollisionsRenderLink : RenderableChainLink
    {
        public override void Render(Graphics g)
        {
            if (IsDebug)
            {
                Pen p = new Pen(Color.Blue);
                foreach (Paddle paddle in ArenaFacade.Instance.PlayerPaddles.Values)
                {
                    float angle = paddle.GetCenterAngle();
                    Vector2 paddleCenter = Utilities.GetPointOnCircle(Center, Radius, angle);
                    Vector2 paddleNormal = (Center - paddleCenter).Normalize();
                    foreach (IBall b in ArenaFacade.Instance.ArenaBalls.Values)
                    {
                        Vector2 ballDir = b.GetDirection();
                        Vector2 bounceDir = SharedUtilities.GetBounceDirection(paddleNormal, ballDir);
                        g.DrawVector(p, paddleCenter, bounceDir);

                        if (ArenaFacade.Instance.IsPaused)
                            break;
                    }

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
