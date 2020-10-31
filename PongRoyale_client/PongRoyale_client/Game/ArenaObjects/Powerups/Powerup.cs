using PongRoyale_client.Extensions;
using PongRoyale_client.Game.ArenaObjects.Powerups;
using PongRoyale_client.Game.Balls.ReboundStrategy;
using PongRoyale_client.Game.Obstacles;
using PongRoyale_client.Singleton;
using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.Powerups
{
    public class Powerup : ArenaObject
    {
        public float Diameter => Width;
        public float Radius => Diameter / 2f;

        public PowerUppedData PowerUppedData { get; private set; }
        public bool isUsedUp { get; private set; }

        public Powerup(float duration, float posX, float posY, float width, float height)
        {
            Duration = duration;
            PosX = posX;
            PosY = posY;
            Width = width;
            Heigth = height;
        }

        public void Init(Color color, PowerUppedData powerUppedData)
        {
            Color = color;
            PowerUppedData = powerUppedData;
        }

        public override void Render(Graphics g, Pen p, Brush b)
        {
            b = new SolidBrush(CurrentColor);
            g.FillNgonAtCenter(b, 8, PosX, PosY, Diameter);
            b.Dispose();
        }

        public override Rect2D GetBounds()
        {
            float diameter = Diameter * .8f;
            float radius = diameter / 2f;
            return new Rect2D(PosX - radius, PosY - radius, diameter, diameter);
        }

        public override IReboundStrategy GetReboundStrategy()
        {
            switch (Type)
            {
                case ArenaObjectType.Passable:
                    return new PassablePowerupStrategy();
                case ArenaObjectType.NonPassable:
                    return new NonPassablePowerupStrategy();
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

            Vector2 normal = p - center;
            normal = normal.Normalize();
            return normal;
        }

        public void Use() 
        {
            isUsedUp = true;
            ForceDestroy();
        }
    }
}
