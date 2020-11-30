using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.Balls.Iterator
{
    class PaddleIterator : MyIterator
    {
        private PaddleArrayList aggregate;
        private int current = 0;
        public PaddleIterator(PaddleArrayList aggregate)
        {
            this.aggregate = aggregate;
        }
        public override object First()
        {
            return aggregate[0];
        }
        public override object Next()
        {
            ++current;
            object ret = null;
            if (current < aggregate.Count)
            {
                ret = aggregate[current];
            }

            return ret;
        }
        public override object CurrentItem()
        {
            return aggregate[current];
        }
        public override bool HasNext()
        {
            return current < aggregate.Count;
        }
    }
}
