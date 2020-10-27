using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.Balls.ReboundStrategy
{
    public class ObstacleStrategy : IReboundStrategy
    {
        public Vector2 ReboundDirection(Vector2 ballDirection, Vector2 collisionNormal, Paddle p, ArenaObject obj)
        {
            return (collisionNormal + ballDirection).Normalize();
        }

        public Vector2 ReboundPosition(Vector2 ballPos, Vector2 ballDirection, Vector2 collisionNormal, Paddle p, ArenaObject obj)
        {
            return obj.GetBounds().GetClosestPointOnBorder(ballPos);
        }
    }
}
