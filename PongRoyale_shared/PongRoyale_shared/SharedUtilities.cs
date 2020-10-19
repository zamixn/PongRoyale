using System;
using System.Collections.Generic;
using System.Text;

namespace PongRoyale_shared
{
    public static class SharedUtilities
    {
        public const float PI = (float)Math.PI;
        private const double DegToRadConst = Math.PI / 180;
        private const double RadToDegConst = 180 / Math.PI;

        public static float DegToRad(float angle)
        {
            return (float)(DegToRadConst * angle);
        }
        public static float RadToDeg(float angle)
        {
            return (float)(RadToDegConst * angle);
        }
    }
}
