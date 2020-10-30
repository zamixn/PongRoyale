using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_shared
{
    public class Vector2
    {
        public const int ByteSize = 8;

        public static Vector2 Right => new Vector2(1, 0);
        public static Vector2 Left => new Vector2(-1, 0);
        public static Vector2 Up => new Vector2(0, 1);
        public static Vector2 Down => new Vector2(0, -1);
        public static Vector2 Zero => new Vector2(0, 0);
        public static Vector2 One => new Vector2(1, 1);

        public float X;
        public float Y;

        public Vector2() { }

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public Vector2(double x, double y)
        {
            X = (float)x;
            Y = (float)y;
        }

        public static float Distance(Vector2 a, Vector2 b)
        {
            return (float)Math.Pow(Math.Pow(b.X - a.X, 2) + Math.Pow(b.Y - a.Y, 2), 0.5f);
        }

        public static float Angle(Vector2 a, Vector2 b)
        {
            a = a.Normalize();
            b = b.Normalize();
            return (float)Math.Acos((a * b) / (a.Magnitude() * b.Magnitude()));
        }
        public static float SignedAngle(Vector2 a, Vector2 b)
        {
            return (float)(Math.Atan2(b.Y, b.X) - Math.Atan2(a.Y, a.X));
        }
        public static float SignedAngleDeg(Vector2 a, Vector2 b)
        {
            float angle = SignedAngle(a, b);
            return SharedUtilities.RadToDeg(angle);
        }
        public static float AngleDeg(Vector2 a, Vector2 b)
        {
            float angle = Angle(a, b);
            return SharedUtilities.RadToDeg(angle);
        }

        public float Magnitude()
        {
            return (float)Math.Pow(Math.Pow(X, 2) + Math.Pow(Y, 2), 0.5f);
        }
        public Vector2 Normalize()
        {
            float magnitude = this.Magnitude();
            return new Vector2(X / magnitude, Y / magnitude);
        }

        public static Vector2 RandomInUnitCircle()
        {
            float theta = (2.0f * SharedUtilities.PI) * RandomNumber.NextFloat();
            return new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta));
        }

        public override string ToString()
        {
            return $"{X}, {Y}";
        }

        public static Vector2 operator * (Vector2 a, float b)
        {
            return new Vector2(a.X * b, a.Y * b);
        }
        public static Vector2 operator *(float b, Vector2 a)
        {
            return new Vector2(a.X * b, a.Y * b);
        }

        public static Vector2 operator + (Vector2 a, Vector2 b)
        {
            return new Vector2(a.X + b.X, a.Y + b.Y);
        }

        public static Vector2 operator - (Vector2 a, Vector2 b)
        {
            return new Vector2(a.X - b.X, a.Y - b.Y);
        }

        public static float operator *(Vector2 a, Vector2 b)
        {
            return a.X * b.X + a.Y * b.Y;
        }
        public static Vector2 operator -(Vector2 a)
        {
            return new Vector2(-a.X, -a.Y);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Vector2))
                return false;

            Vector2 v = obj as Vector2;
            return (v.X == X && v.Y == Y);
        }
    }
}
