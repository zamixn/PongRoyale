using PongRoyale_client.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.Balls.Decorator
{
    public class BallSpeedDecorator : BallDecorator
    {
        public BallSpeedDecorator(IBall ball) : base(ball)
        {
        }

        public override void Render(Graphics g, Brush p)
        {
            base.Render(g, p);
            Pen pen = new Pen(Ball.GetColor(), 2);
            g.DrawVector(pen, GetPosition(), GetDirection(), 15, 15);
            g.DrawVector(pen, GetPosition(), GetDirection(), 20, 15);
            g.DrawVector(pen, GetPosition(), GetDirection(), 25, 15);
            pen.Dispose();
        }
    }
}
