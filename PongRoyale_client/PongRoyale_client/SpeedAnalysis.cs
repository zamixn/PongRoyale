using PongRoyale_client.Game;
using PongRoyale_client.Game.Paddles;
using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client
{
    public static class SpeedAnalysis
    {

        public static void DoFlyweightSpeedAnalysis()
        {
            GC.Collect();
            int n = 500000;
            DoWithFlyweight(n);
            //DoWithoutFlyweight(n);
            GC.Collect();
        }
        private static void DoWithFlyweight(int n)
        {
            Stopwatch sw = new Stopwatch();
            long before, after;

            sw.Reset();
            before = GC.GetTotalMemory(false);
            sw.Start();
            for (int i = 0; i < n; i++)
            {
                var type = (PaddleType)(i % 4);
                PaddleFactory.CreatePaddle(type, 0);
            }
            sw.Stop();
            after = GC.GetTotalMemory(false);
            long usedWith = after - before;
            long msWith = sw.ElapsedMilliseconds;
            Debug.WriteLine($"{n}: With Flyweight: {msWith} milliseconds. {usedWith} bytes");
        }

        private static void DoWithoutFlyweight(int n)
        {
            Stopwatch sw = new Stopwatch();
            long before, after;

            sw.Reset();
            before = GC.GetTotalMemory(false);
            sw.Start();
            for (int i = 0; i < n; i++)
            {
                var type = (PaddleType)(i % 4);

                switch (type)
                {
                    case PaddleType.Normal:
                        new NormalPaddle(0, PaddleDataFactory.GetPaddleData(type));
                        break;
                    case PaddleType.Long:
                        new LongPaddle(0, PaddleDataFactory.GetPaddleData(type));
                        break;
                    case PaddleType.Short:
                        new ShortPaddle(0, PaddleDataFactory.GetPaddleData(type));
                        break;
                    default:
                        break;
                }
            }
            sw.Stop();
            after = GC.GetTotalMemory(false);
            long usedWithout = after - before;
            long msWithout = sw.ElapsedMilliseconds;
            Debug.WriteLine($"{n}: Without Flyweight: {msWithout} milliseconds. {usedWithout} bytes");
        }

    }
}
