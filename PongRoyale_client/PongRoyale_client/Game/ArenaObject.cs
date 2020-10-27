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
            Rect2D bounds = GetBounds();
            Vector2 o = bounds.Origin;
            Vector2 s = bounds.Size;
            Vector2 p = impactPoint;

            bool insideX = Utilities.IsInsideRange(p.X, o.X, o.X + s.X);
            bool insideY = Utilities.IsInsideRange(p.Y, o.Y, o.Y + s.Y);
            bool insideRect = insideX && insideY;

            if (insideRect)
            {
                return impactDirection;
            }

            if (insideX)
            {
                if (p.X < o.X)
                    return Vector2.Left;
                return Vector2.Right;
            }
            if (insideY)
            {
                if (p.Y < o.Y)
                    return Vector2.Down;
                return Vector2.Up;
            }
            return -impactDirection;
        }

        public virtual bool Intersects(Rect2D bounds)
        {
            Rect2D myBounds = GetBounds();
            return myBounds.Intersects(bounds);
        }

        public abstract Rect2D GetBounds();
    }
}
