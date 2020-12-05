using PongRoyale_client.Game.Balls.Decorator;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.RenderChain
{
    class DrawBallsRenderLink : RenderableChainLink
    {
        public override void Render(Graphics g)
        {
            foreach (IBall ball in ArenaFacade.Instance.ArenaBalls.Values)
            {
                Brush p = new SolidBrush(Color.Yellow);
                ball.Render(g, p);
                p.Dispose();

                if (ArenaFacade.Instance.IsPaused)
                    break;
            }

            if (Next != null)
                Next.Render(g);
        }
    }
}
