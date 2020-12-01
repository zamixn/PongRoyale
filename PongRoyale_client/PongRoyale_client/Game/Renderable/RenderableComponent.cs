using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.Renderable
{
    public abstract class RenderableComponent
    {
        public abstract void Render(Graphics g, Pen p, Brush b);

        public virtual void Add(RenderableComponent component) 
        {
            throw new NotImplementedException();
        }

        public virtual void Remove(RenderableComponent component) 
        {
            throw new NotImplementedException();
        }
    }
}
