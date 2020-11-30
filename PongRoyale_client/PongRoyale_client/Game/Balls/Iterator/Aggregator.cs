using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.Balls.Iterator
{
    abstract class Aggregator
    {
        public abstract MyIterator CreateIterator();
    }
}
