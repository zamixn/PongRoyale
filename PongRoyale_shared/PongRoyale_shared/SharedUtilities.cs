﻿using System;
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

        public static float Clamp(float f, float min, float max)
        {
            if (f < min)
                return min;
            if (f > max)
                return max;
            return f;
        }

        public static Vector2 GetBounceDirection(Vector2 surfaceNormal, Vector2 impactDirection)
        {
            Vector2 tmp = (-2 * (surfaceNormal * impactDirection)) * surfaceNormal;
            return (tmp + impactDirection).Normalize();
        }

        public static Vector2 GetSurfaceNormalOfLine(Vector2 p1, Vector2 p2)
        {
            Vector2 dir = p2 - p1;
            return new Vector2(-dir.Y, dir.X).Normalize();
        }
    }
}
