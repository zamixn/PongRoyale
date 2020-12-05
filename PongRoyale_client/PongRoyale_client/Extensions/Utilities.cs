using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Extensions
{
    public static class Utilities
    {
        public static PointF GetPointOnCircle(PointF origin, float radius, float angle)
        {
            PointF point = new PointF(origin.X + radius * (float)Math.Cos(angle), origin.Y + radius * (float)Math.Sin(angle));
            return point;
        }
        public static Vector2 GetPointOnCircle(Vector2 origin, float radius, float angle)
        {
            Vector2 point = new Vector2(origin.X + radius * Math.Cos(angle), origin.Y + radius * Math.Sin(angle));
            return point;
        }
        public static Vector2 ToVector2(this PointF p)
        {
            return new Vector2(p.X, p.Y);
        }
        public static PointF ToPointF(this Vector2 p)
        {
            return new PointF(p.X, p.Y);
        }

        public static bool IsInsideAngle(float a, float min, float max)
        {
            a = a.ClampAngle().Abs(); ;
            min = min.ClampAngle().Abs();
            max = max.ClampAngle().Abs();
            if (min < max)
                return (a >= min && a <= max);
            else
            {
                if (a > min)
                    return true;
                if (a < max)
                    return true;
            }
            return false;
        }

        public static bool IsInsideRange(float f, float min, float max)
        {
            return f > min && f < max;
        }

        public static Color Lerp(Color a, Color b, float t)
        {
            if (t <= 0)
                return a;
            if (t >= 1)
                return b;

            return Color.FromArgb(
                SharedUtilities.Lerp(a.A, b.A, t),
                SharedUtilities.Lerp(a.R, b.R, t),
                SharedUtilities.Lerp(a.G, b.G, t),
                SharedUtilities.Lerp(a.B, b.B, t)
                );
        }
    }
}
