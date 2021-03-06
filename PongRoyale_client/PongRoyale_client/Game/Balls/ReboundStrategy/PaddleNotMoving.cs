﻿using PongRoyale_client.Extensions;
using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.Balls.ReboundStrategy
{
    public class PaddleNotMoving : IReboundStrategy
    {

        public Vector2 ReboundDirection(Ball b, Vector2 collisionNormal, Paddle p, ArenaObject obj)
        {
            var ballDirection = b.Direction;
            Vector2 bounceDir = SharedUtilities.GetBounceDirection(collisionNormal, ballDirection);
            ArenaFacade.Instance.TransferPowerUpToPaddle(p.Id, b.Id, b.PoweredUpData);
            return (bounceDir + Vector2.RandomInUnitCircle().Normalize() * 0.2f).Normalize();
        }

        public Vector2 ReboundPosition(Ball b, Vector2 collisionNormal, Paddle p, ArenaObject obj)
        {
            var ballPos = b.Position;
            var ballDirection = b.Direction;
            return ballPos;
        }
    }
}
