using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.Balls.Iterator
{
    abstract class MyIterator
    {
        public abstract object First();
        public abstract object Next();
        public abstract bool HasNext();
        public abstract object CurrentItem();
    }
}
