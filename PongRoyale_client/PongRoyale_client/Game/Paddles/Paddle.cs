using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game
{
    public abstract class Paddle
    {
        public float AngularPosition { get; protected set; }
        public float AngularSize { get; protected set; }
        public float Speed { get; protected set; }

        public Paddle(float angularSize)
        {
            AngularSize = angularSize;
        }
        public virtual void Render(Graphics g, Pen p, PointF Origin, float Diameter)
        {
            g.DrawArc(p, Origin.X, Origin.Y, Diameter, Diameter, AngularPosition, AngularSize);
        }
        public virtual void SetPosition(float position)
        {
            AngularPosition = position;
        }
    }
}
