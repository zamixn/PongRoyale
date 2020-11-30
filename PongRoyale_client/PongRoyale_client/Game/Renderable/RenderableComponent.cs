using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.Renderable
{
    abstract class RenderableComponent
    {
        public abstract void Render();

        public abstract void Add(RenderableComponent component);

        public abstract void Remove(RenderableComponent component);
    }
}
