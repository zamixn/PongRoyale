using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.Ranking
{
    public class UpdateComposite : UpdateComponent
    {
        protected Dictionary<byte, UpdateComponent> children = new Dictionary<byte, UpdateComponent>();
        public override void Add(byte id, UpdateComponent component)
        {
            children.Add(id, component);
        }
        public override byte GetNextId()
        {
            if (children.Count == 0)
                return 0;

            return (byte)(children.Select((n) => n.Key).Max() + 1);
        }
        public override void Remove(byte id)
        {
            children.Remove(id);
        }
        public override void Clear()
        {
            children.Clear();
        }
        public override UpdateComponent GetChild(byte id)
        {
            return children[id];
        }
        public override void Update()
        {
            foreach(var c in children)
            {
                c.Value.Update();
            }
        }
    }
}
