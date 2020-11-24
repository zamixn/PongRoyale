using PongRoyale_client.Game.ArenaObjects.Powerups;
using PongRoyale_client.Game.Powerups;
using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.Balls.ReboundStrategy
{
    public class PassablePowerupStrategy : IReboundStrategy
    {
        public Vector2 ReboundDirection(Ball b, Vector2 collisionNormal, Paddle p, ArenaObject obj)
        {
            var ballDirection = b.Direction;

            var powerup = (obj as PowerUp);
            ArenaFacade.Instance.BallHasCollectedPowerUp(powerup, b);
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
