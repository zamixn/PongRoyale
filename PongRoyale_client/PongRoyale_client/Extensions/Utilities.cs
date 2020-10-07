using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Extensions
{
    public static class Utilities
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

        public static PointF GetPointOnCircle(PointF origin, float radius, float angle)
        {
            PointF point = new PointF(origin.X + radius * (float)Math.Cos(angle), origin.Y + radius * (float)Math.Sin(angle));
            return point;
        }
    }
}
