using PongRoyale_client.Extensions;
using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.Balls.ReboundStrategy
{
    public class BallDeadlyStrategy : IReboundStrategy
    {
        public Vector2 ReboundDirection(Ball b, Vector2 collisionNormal, Paddle p, ArenaObject obj)
        {
            var ballDirection = b.Direction;
            if (p != null)
                ArenaFacade.Instance.KillPaddle(p);
            return ballDirection;
        }

        public Vector2 ReboundPosition(Ball b, Vector2 collisionNormal, Paddle p, ArenaObject obj)
        {
            var ballPos = b.Position;
            var ballDirection = b.Direction;
            return ballPos;
        }
    }
}
