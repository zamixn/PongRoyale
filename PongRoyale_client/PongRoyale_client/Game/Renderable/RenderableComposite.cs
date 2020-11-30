using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.Renderable
{
    class RenderableComposite : RenderableComponent
    {
        private List<RenderableComponent> components = new List<RenderableComponent>();
        public override void Add(RenderableComponent component)
        {
            throw new NotImplementedException();
        }

        public override void Remove(RenderableComponent component)
        {
            throw new NotImplementedException();
        }

        public override void Render()
        {
            throw new NotImplementedException();
        }
    }
}
