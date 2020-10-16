using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Extensions
{
    public static class MiscExtensions
    {
        public static float Abs(this float f)
        {
            return (float)Math.Abs(f);
        }

        public static float ClampAngle(this float a)
        { 
            return (a + 360) % 360;
        }
    }
}
