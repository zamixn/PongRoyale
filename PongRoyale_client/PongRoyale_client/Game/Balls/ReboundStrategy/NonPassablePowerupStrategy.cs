﻿using PongRoyale_client.Game.ArenaObjects.Powerups;
using PongRoyale_client.Game.Powerups;
using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.Balls.ReboundStrategy
{
    public class NonPassablePowerupStrategy : IReboundStrategy
    {
        public Vector2 ReboundDirection(Ball b, Vector2 collisionNormal, Paddle p, ArenaObject obj)
        {
            var ballDirection = b.Direction;
            if (collisionNormal.Equals(-ballDirection))
                return collisionNormal;
            var dir = (collisionNormal + ballDirection).Normalize();

            var powerup = (obj as PowerUp);
            ArenaFacade.Instance.BallHasCollectedPowerUp(powerup, b);
            return dir;
        }

        public Vector2 ReboundPosition(Ball b, Vector2 collisionNormal, Paddle p, ArenaObject obj)
        {
            var ballPos = b.Position;
            var ballDirection = b.Direction;

            var offset = ballDirection * 10;
            return obj.GetBounds().GetClosestPointOnBorder(ballPos) + offset;
        }
    }
}
