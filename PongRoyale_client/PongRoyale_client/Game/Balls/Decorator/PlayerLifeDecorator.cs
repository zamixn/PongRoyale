using PongRoyale_client.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.Balls.Decorator
{
    public class PlayerLifeDecorator : BallDecorator
    {
        public PlayerLifeDecorator(IBall ball) : base(ball)
        {
        }

        public override void Render(Graphics g, Brush p)
        {
            base.Render(g, p);
            Pen pen = new Pen(Color.Green, 2);
            g.DrawCircleAtCenter(pen, GetPosition(), GetDiameter() * 1.1f);
            g.DrawCircleAtCenter(pen, GetPosition(), GetDiameter() * 1.2f);
            g.DrawCircleAtCenter(pen, GetPosition(), GetDiameter() * 1.3f);
            g.DrawCircleAtCenter(pen, GetPosition(), GetDiameter() * 1.5f);
            pen.Dispose();
        }
    }
}
