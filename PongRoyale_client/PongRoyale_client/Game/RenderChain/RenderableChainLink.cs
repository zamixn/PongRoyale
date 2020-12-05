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
    public abstract class RenderableChainLink
    {
        protected RenderableChainLink Next;

        protected ArenaDimensions Dimensions => ArenaFacade.Instance.ArenaDimensions;
        protected Vector2 Origin => Dimensions.RenderOrigin;
        protected Vector2 Center => Dimensions.Center;

        protected bool IsDebug => GameManager.Instance.DebugMode;

        protected float Diameter => Dimensions.Radius * 2f;
        protected float Radius => Dimensions.Radius;

        protected float Width => Dimensions.Size.X;
        protected float Height => Dimensions.Size.Y;


        public abstract void Render(Graphics g);

        public virtual void SetNext(RenderableChainLink next)
        {
            Next = next;
        }
    }
}
