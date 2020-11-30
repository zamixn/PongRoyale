using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.Balls.Iterator
{
    class PaddleArrayList : Aggregator
    {
        private ArrayList items = new ArrayList();
        public override MyIterator CreateIterator()
        {
            return new PaddleIterator(this);
        }
        public void AddPaddles(Paddle[] paddles)
        {
            List<Paddle> unsortedPaddles = new List<Paddle>();
            unsortedPaddles.AddRange(paddles);
            SortPaddles(unsortedPaddles);
        }
        public void SortPaddles(List<Paddle> unsortedPaddles)
        {
            //1 - Long Paddle, 0 - Normal Paddle, 2 - Short Paddle
            int[] map = new[] { 1, 0, 2 };
            items.AddRange(unsortedPaddles.OrderBy(x => map[(int)(x.PType-1)]).ToArray());
        }
        public int Count
        {
            get { return items.Count; }
        }
        public object this[int index]
        {
            get { return items[index]; }
            set { items.Insert(index, value); }
        }
    }
}
