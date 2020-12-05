using PongRoyale_client.Extensions;
using PongRoyale_client.Singleton;
using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.RenderChain
{
    class DrawArenaRenderLink : RenderableChainLink
    {

        public Color ArenaColor;
        public float ArenaWidth;

        public Color LocalBorderColor;
        public float LocalBorderWidth;


        public DrawArenaRenderLink(Color arenaColor, float arenaWidth, Color localBorderColor, float localBorderWidth)
        {
            ArenaColor = arenaColor;
            ArenaWidth = arenaWidth;
            LocalBorderColor = localBorderColor;
            LocalBorderWidth = localBorderWidth;
        }

        public override void Render(Graphics g)
        {
            Pen p = new Pen(ArenaColor, ArenaWidth);
            Pen pp = new Pen(LocalBorderColor, LocalBorderWidth);
            p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            float angle = (float)(-Math.PI / 2);
            float angleDelta = (float)(Math.PI * 2 / RoomSettings.Instance.Players.Count);
            foreach (var player in RoomSettings.Instance.Players)
            {
                if (ServerConnection.Instance.IdMatches(player.Key))
                    g.DrawArc(pp, Origin.X, Origin.Y, Diameter, Diameter, SharedUtilities.RadToDeg(angle), SharedUtilities.RadToDeg(angleDelta));
                g.DrawLine(p, Center, Utilities.GetPointOnCircle(Center, Radius, angle));
                angle += angleDelta;

                if (ArenaFacade.Instance.IsPaused)
                    break;
            }

            g.DrawEllipse(p, Origin.X, Origin.Y, Diameter, Diameter);
            p.Dispose();
            pp.Dispose();

            if (Next != null)
                Next.Render(g);
        }
    }
}
