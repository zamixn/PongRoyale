using PongRoyale_client.Extensions;
using PongRoyale_client.Game.Balls.ReboundStrategy;
using PongRoyale_client.Game.Obstacles;
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
        public float Width { get; set; }
        public float Heigth { get; set; }
        public Color Color { get; set; }
        public Color CurrentColor { get; set; }
        public ArenaObjectType Type { get; private set; }
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
                if (CurrentColor.A < 10)
                    IsBeingDestroyed = true;
            }
            else if (TimeAlive >= Duration)
                IsDisappearing = true;


            if (IsBeingDestroyed)
                ArenaFacade.Instance.OnArenaObjectExpire(Id);
        }

        protected void ForceDestroy()
        {
            TimeAlive = Duration;
        }

        public abstract Vector2 GetCollisionNormal(Vector2 impactPoint, Vector2 impactDirection);

        public virtual bool Intersects(Rect2D bounds)
        {
            Rect2D myBounds = GetBounds();
            return myBounds.Intersects(bounds);
        }

        public abstract Rect2D GetBounds();

        public virtual void SetTypeParams(ArenaObjectType type)
        {
            Type = type;
        }


        public abstract IReboundStrategy GetReboundStrategy();
    }
}
