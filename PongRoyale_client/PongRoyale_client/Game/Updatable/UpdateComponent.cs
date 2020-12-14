using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.Ranking
{
    public abstract class UpdateComponent
    {
        public abstract void Update();
        public virtual void Add(byte id, UpdateComponent component)
        {
            throw new NotImplementedException();
        }
        public virtual byte GetNextId()
        {
            throw new NotImplementedException();
        }
        public virtual void Remove(byte id)
        {
            throw new NotImplementedException();
        }
        public virtual void Clear()
        {
            throw new NotImplementedException();
        }
        public virtual UpdateComponent GetChild(byte id)
        {
            throw new NotImplementedException();
        }
    }
}
