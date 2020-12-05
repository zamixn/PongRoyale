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
    class DebugObjNormalsRenderLink : RenderableChainLink
    {
        public override void Render(Graphics g)
        {
            if (IsDebug)
            {
                Pen p = new Pen(Color.Magenta);
                Brush b = new SolidBrush(Color.Magenta);
                foreach (var ball in ArenaFacade.Instance.ArenaBalls.Values)
                {
                    var Direction = ball.GetDirection();
                    var Position = ball.GetPosition();
                    var offset = (Direction * ball.GetDiameter() * 0.5f);
                    Vector2 impactPos = Position + offset;
                    g.DrawPoint(b, impactPos);
                    foreach (var obj in ArenaFacade.Instance.ArenaObjects.Values)
                    {
                        var collisionNormal = obj.GetCollisionNormal(impactPos, Direction);
                        g.DrawVector(p, impactPos, collisionNormal);

                        if (ArenaFacade.Instance.IsPaused)
                            break;
                    }

                    if (ArenaFacade.Instance.IsPaused)
                        break;
                }
                b.Dispose();
                p.Dispose();
            }

            if (Next != null)
                Next.Render(null);
        }
    }
}
