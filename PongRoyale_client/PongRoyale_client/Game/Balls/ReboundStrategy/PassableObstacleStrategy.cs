using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.Balls.ReboundStrategy
{
    class PassableObstacleStrategy : IReboundStrategy
    {
        public Vector2 ReboundDirection(Vector2 ballDirection, Vector2 collisionNormal, Paddle p, ArenaObject obj)
        {
            return ballDirection;
        }

        public Vector2 ReboundPosition(Vector2 ballPos, Vector2 ballDirection, Vector2 collisionNormal, Paddle p, ArenaObject obj)
        {
            return ballPos;
        }
    }
}
