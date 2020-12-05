using PongRoyale_client.Extensions;
using PongRoyale_client.Game.Balls.Decorator;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.RenderChain.Debug
{
    class DebugRect2DsRenderLink : RenderableChainLink
    {
        public override void Render(Graphics g)
        {
            if (IsDebug)
            {
                Pen p = new Pen(Color.Magenta);
                foreach (var obj in ArenaFacade.Instance.ArenaObjects.Values)
                {
                    g.DrawRect2D(p, obj.GetBounds());

                    if (ArenaFacade.Instance.IsPaused)
                        break;
                }
                foreach (IBall ball in ArenaFacade.Instance.ArenaBalls.Values)
                {
                    g.DrawRect2D(p, ball.GetBounds());

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
