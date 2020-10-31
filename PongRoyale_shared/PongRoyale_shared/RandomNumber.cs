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
        public static int NextInt(int min, int max)
        {
            lock (syncLock)
            { 
                return random.Next(min, max);
            }
        }
        public static double NextDouble()
        {
            lock (syncLock)
            { 
                return random.NextDouble();
            }
        }

        public static float NextFloat(float min, float max)
        {
            lock (syncLock)
            {
                var rnd = (float)random.NextDouble();
                return min + (max - min) * rnd;
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

        public static Vector2 RandomVector(Vector2 min, Vector2 max)
        {
            return new Vector2(NextFloat(min.X, max.X), NextFloat(min.Y, max.Y));
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
