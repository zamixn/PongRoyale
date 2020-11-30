using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.Balls.Iterator
{
    class ArenaObjectArray : Aggregator
    {
        private ArenaObject [] items = new ArenaObject[100];
        public override MyIterator CreateIterator()
        {
            return new ArenaObjectIterator(this);
        }
        public void AddAObjects(ArenaObject[] AObjects)
        {
            ArenaObject[] unsortedAObjects = AObjects;
            SortAObject(unsortedAObjects);
        }
        public void SortAObject(ArenaObject[] unsortedAObjects)
        {
            //1 - NonPassable, 0 - Passable, 2 - Short Paddle
            int[] map = new[] { 1, 0 };
            items = unsortedAObjects.OrderBy(x => map[(int)(x.Type - 1)]).ToArray();
        }
        public int Count
        {
            get { return items.Length; }
        }
        public object this[int index]
        {
            get { return items[index]; }
            set { items[index] = (ArenaObject)value; }
        }
    }
}
