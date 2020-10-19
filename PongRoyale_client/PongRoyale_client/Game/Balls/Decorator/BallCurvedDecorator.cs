using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.Balls.Decorator
{
    public class BallCurvedDecorator : BallDecorator
    {
        private readonly float Curvature;
        public BallCurvedDecorator(IBall ball, float curvature) : base(ball)
        {
            Curvature = curvature;
        }

        public override void LocalMove()
        {
            // TODO: override movement, so that it is curved
            base.LocalMove();
        }
    }
}
