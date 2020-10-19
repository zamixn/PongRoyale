using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.Balls.Decorator
{
    public class BallDecorator : IBall
    {
        protected readonly IBall Ball;

        public BallDecorator(IBall ball)
        {
            Ball = ball;
        }

        public override void LocalMove()
        {
            Ball.LocalMove();
        }
    }
}
