using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.Balls.ReboundStrategy
{
    public class NonPassableObstacleStrategy : IReboundStrategy
    {
        public Vector2 ReboundDirection(Vector2 ballDirection, Vector2 collisionNormal, Paddle p, ArenaObject obj)
        {
            if (collisionNormal.Equals(-ballDirection))
                return collisionNormal;
            var dir = (collisionNormal + ballDirection).Normalize();
            return dir;
        }

        public Vector2 ReboundPosition(Vector2 ballPos, Vector2 ballDirection, Vector2 collisionNormal, Paddle p, ArenaObject obj)
        {
            var offset = ballDirection * 10;
            return obj.GetBounds().GetClosestPointOnBorder(ballPos) + offset;
        }
    }
}
