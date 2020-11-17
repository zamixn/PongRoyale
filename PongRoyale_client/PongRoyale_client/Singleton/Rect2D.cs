using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Singleton
{
    public class Rect2D
    {
        public Vector2 Origin;
        public Vector2 Size;

        public Rect2D(Vector2 origin, Vector2 size)
        {
            Origin = origin;
            Size = size;
        }

        public Rect2D(float x, float y, float width, float height)
        {
            Origin = new Vector2(x, y);
            Size = new Vector2(width, height);
        }

        public bool Intersects(Rect2D rect)
        {
            Vector2 l1 = new Vector2(Origin.X, Origin.Y + Size.Y);
            Vector2 r1 = new Vector2(Origin.X + Size.X, Origin.Y);

            Vector2 l2 = new Vector2(rect.Origin.X, rect.Origin.Y + rect.Size.Y);
            Vector2 r2 = new Vector2(rect.Origin.X + rect.Size.X, rect.Origin.Y);

            // If one rectangle is on left side of other  
            if (l1.X >= r2.X || l2.X >= r1.X)
            {
                return false;
            }

            // If one rectangle is above other  
            if (l1.Y <= r2.Y || l2.Y <= r1.Y)
            {
                return false;
            }
            return true;
        }

        public Vector2 GetClosestPointOnBorder(Vector2 point)
        {
            Vector2 bounds = Size * 0.5f;
            Vector2 center = Origin + bounds;
            Vector2 closestPoint = new Vector2(0, 0);
            Vector2 P = point;

            bool insideX = center.X - bounds.X < P.X && P.X < center.X + bounds.X;
            bool insideY = center.Y - bounds.Y < P.Y && P.Y < center.Y + bounds.Y;
            bool pointInsideRectangle = insideX && insideY;

            if (!pointInsideRectangle)
            { //Outside
                closestPoint.X = SharedUtilities.Max(center.X - bounds.X,
                                        SharedUtilities.Min(P.X, center.X + bounds.X));
                closestPoint.Y = SharedUtilities.Max(center.Y - bounds.Y,
                                        SharedUtilities.Min(P.Y, center.Y + bounds.Y));
            }
            else
            { //Inside
                Vector2 distanceToPositiveBounds = center + bounds - P;
                Vector2 distanceToNegativeBounds = -(center - bounds - P);
                float smallestX = SharedUtilities.Min(distanceToPositiveBounds.X, distanceToNegativeBounds.X);
                float smallestY = SharedUtilities.Min(distanceToPositiveBounds.Y, distanceToNegativeBounds.Y);
                float smallestDistance = SharedUtilities.Min(smallestX, smallestY);

                if (smallestDistance == distanceToPositiveBounds.X)
                    closestPoint = new Vector2(center.X + bounds.X, P.Y);
                else if (smallestDistance == distanceToNegativeBounds.X)
                    closestPoint = new Vector2(center.X - bounds.X, P.Y);
                else if (smallestDistance == distanceToPositiveBounds.Y)
                    closestPoint = new Vector2(P.X, center.Y + bounds.Y);
                else
                    closestPoint = new Vector2(P.X, center.Y - bounds.Y);
            }
            return closestPoint + ((point - center).Normalize() * 11);
        }

        public override string ToString()
        {
            return $"(pos={Origin}; size={Size})";
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Rect2D))
                return false;
            else
                return ((Rect2D)obj).Size.Equals(Size) && ((Rect2D)obj).Origin.Equals(Origin);
        }
    }
}
