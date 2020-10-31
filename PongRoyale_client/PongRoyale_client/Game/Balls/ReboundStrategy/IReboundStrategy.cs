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
    public interface IReboundStrategy
    {
        Vector2 ReboundDirection(Ball b, Vector2 collisionNormal, Paddle p, ArenaObject obj);
        Vector2 ReboundPosition(Ball b, Vector2 collisionNormal, Paddle p, ArenaObject obj);
    }
}
