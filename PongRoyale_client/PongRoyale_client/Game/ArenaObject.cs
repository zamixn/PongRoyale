using PongRoyale_client.Extensions;
using PongRoyale_client.Singleton;
using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game
{
    public abstract class ArenaObject
    {
        public byte Id { get; private set; }
        public void SetId(byte id)
        {
            Id = id;
        }
        public float Duration { get; set; }
        public float PosX { get; set; }
        public float PosY { get; set; }
        public Color Color { get; set; }
        public Color CurrentColor { get; set; }
        protected float TimeAlive { get; set; } = 0;
        protected bool IsAppearing { get; set; } = true;
        protected bool IsDisappearing { get; set; } = true;
        protected bool IsBeingDestroyed { get; set; } = false;


        public ArenaObject()
        {
            CurrentColor = Color.Transparent;
        }

        public abstract void Render(Graphics g, Pen p, Brush b);

        public virtual void Update()
        {
            TimeAlive += GameManager.Instance.DeltaTime;
            if (IsAppearing)
            {
                CurrentColor = Utilities.Lerp(CurrentColor, Color, TimeAlive);
                if (CurrentColor == Color)
                    IsAppearing = false;
            }
            else if (IsDisappearing)
            {
                CurrentColor = Utilities.Lerp(CurrentColor, Color.Transparent, TimeAlive - Duration);
                if (CurrentColor == Color.Transparent)
                    IsBeingDestroyed = false;
            }
            else if (TimeAlive >= Duration)
                IsDisappearing = true;


            if (IsBeingDestroyed)
                ArenaFacade.Instance.DestroyArenaObject(Id);
        }

        public virtual Vector2 GetCollisionNormal(Vector2 impactPoint, Vector2 impactDirection)
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
            Debug.WriteLine(pointInsideRectangle + ", " + impactDirection);
            return normal;
        }

        public virtual bool Intersects(Rect2D bounds)
        {
            Rect2D myBounds = GetBounds();
            return myBounds.Intersects(bounds);
        }

        public abstract Rect2D GetBounds();
    }
}
