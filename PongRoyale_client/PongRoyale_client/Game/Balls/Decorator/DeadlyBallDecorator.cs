using PongRoyale_client.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.Balls.Decorator
{
    public class DeadlyBallDecorator : BallDecorator
    {
        public DeadlyBallDecorator(IBall ball) : base(ball)
        {
        }

        public override void Render(Graphics g, Brush p)
        {
            base.Render(g, p);
            Pen pen = new Pen(Ball.GetColor(), 2);
            g.DrawNgonAtCenter(pen, 8, GetPosition(), GetDiameter() * 1.5f);
            pen.Dispose();
        }
    }
}
