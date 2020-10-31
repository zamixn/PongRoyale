using PongRoyale_client.Extensions;
using PongRoyale_client.Game.Balls.ReboundStrategy;
using PongRoyale_client.Singleton;
using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.Obstacles
{
    public class Obstacle : ArenaObject
    {
        public Obstacle(float posX, float posY, float duration, float width, float heigth)
        {
            PosX = posX;
            PosY = posY;
            Duration = duration;
            Width = width;
            Heigth = heigth;
        }

        public void Init(Color color)
        {
            Color = color;
        }

        public override void Render(Graphics g, Pen p, Brush b)
        {
            b = new SolidBrush(CurrentColor);
            g.FillRectangleAtCenter(b, PosX, PosY, Width, Heigth);
            b.Dispose();
        }

        public override Rect2D GetBounds()
        {
            Rect2D bounds = new Rect2D(
                PosX - Width / 2f, PosY - Heigth / 2f,
                Width, Heigth);
            return bounds;
        }

        public override IReboundStrategy GetReboundStrategy()
        {
            switch (Type)
            {
                case ArenaObjectType.Passable:
                    return new PassableObstacleStrategy();
                case ArenaObjectType.NonPassable:
                    return new NonPassableObstacleStrategy();
                default: 
                    return null;
            }
        }

        public override Vector2 GetCollisionNormal(Vector2 impactPoint, Vector2 impactDirection)
        {
            Rect2D rect = GetBounds();
            Vector2 bounds = rect.Size * 0.5f;
            Vector2 center = rect.Origin + bounds;
            Vector2 p = impactPoint;

            int offset = 1;
            bool insideX = center.X - bounds.X + offset < p.X && p.X < center.X + bounds.X - offset;
            bool insideY = center.Y - bounds.Y + offset < p.Y && p.Y < center.Y + bounds.Y - offset;
            bool pointInsideRectangle = insideX && insideY;
            Vector2 normal = -impactDirection;

            if (pointInsideRectangle)
            {
                normal = impactDirection;
            }
            else
            {
                if (insideX)
                {
                    if (p.Y < center.Y)
                        normal = Vector2.Down;
                    else
                        normal = Vector2.Up;
                }
                if (insideY)
                {
                    if (p.X < center.X)
                        normal = Vector2.Left;
                    else
                        normal = Vector2.Right;
                }
            }
            normal = normal.Normalize();
            return normal;
        }
    }
}
