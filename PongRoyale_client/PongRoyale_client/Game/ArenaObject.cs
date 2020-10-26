using PongRoyale_client.Extensions;
using PongRoyale_client.Singleton;
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
        public float Duration { get; set; }
        public float PosX { get; set; }
        public float PosY { get; set; }
        public Color Color { get; set; }
        public Color CurrentColor { get; set; }
        protected float TimeAlive { get; set; } = 0;
        protected bool IsAppearing { get; set; } = true;
        protected bool IsDisappearing { get; set; } = true;


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
            }
            else if (TimeAlive >= Duration)
                IsDisappearing = true;
        }
    }
}
