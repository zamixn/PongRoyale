using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.Renderable
{
    public class RenderableComposite : RenderableComponent
    {
        private List<RenderableComponent> components = new List<RenderableComponent>();

        public override void Add(RenderableComponent component)
        {
            components.Add(component);
        }

        public override void Remove(RenderableComponent component)
        {
            components.Remove(component);
        }

        public override void Render(Graphics g, Pen p, Brush b)
        {
            foreach(RenderableComponent c in components)
                c.Render(g, p, b);
        }
    }
}
