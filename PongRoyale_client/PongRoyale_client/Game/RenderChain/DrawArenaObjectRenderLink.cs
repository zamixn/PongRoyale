using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.RenderChain
{
    class DrawArenaObjectRenderLink : RenderableChainLink
    {
        public override void Render(Graphics g)
        {
            foreach (ArenaObject obj in ArenaFacade.Instance.ArenaObjects.Values)
            {
                Pen p = new Pen(Color.Magenta);
                Brush b = new SolidBrush(Color.Magenta);
                obj.Render(g, p, b);
                p.Dispose();
                b.Dispose();

                if (ArenaFacade.Instance.IsPaused)
                    break;
            }

            if (Next != null)
                Next.Render(g);
        }
    }
}
