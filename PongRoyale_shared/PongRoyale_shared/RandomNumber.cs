using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_shared
{
    public class RandomNumber
    {
        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
        public static int RandomNumb(int min, int max)
        {
            lock (syncLock)
            { 
                return random.Next(min, max);
            }
        }
        public static double RandomNumb()
        {
            lock (syncLock)
            { 
                return random.NextDouble();
            }
        }

        public static float NextFloat()
        {
            lock (syncLock)
            {
                return (float)random.NextDouble();
            }
        }

        public static byte NextByte(byte min, byte max)
        {
            lock (syncLock)
            {
                return (byte)random.Next(min, max);
            }
        }

        public static T[] GetArray<T>(int count, Func<T> randomGetter)
        {
            T[] a = new T[count];
            for (int i = 0; i < count; i++)
            {
                a[i] = randomGetter.Invoke();
            }
            return a;
        }
    }
}
