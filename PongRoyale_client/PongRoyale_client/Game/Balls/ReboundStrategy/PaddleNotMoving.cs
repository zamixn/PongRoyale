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
    public class PaddleNotMoving : IReboundStrategy
    {

        public Vector2 ReboundDirection(Vector2 ballDirection, Vector2 collisionNormal)
        {
            Vector2 bounceDir = SharedUtilities.GetBounceDirection(collisionNormal, ballDirection);
            return (bounceDir + Vector2.RandomInUnitCircle().Normalize() * 0.2f).Normalize();
        }
    } 
}
