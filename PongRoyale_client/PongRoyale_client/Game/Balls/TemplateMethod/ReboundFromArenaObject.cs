using PongRoyale_client.Game.Balls.ReboundStrategy;
using PongRoyale_client.Game.Obstacles;
using PongRoyale_client.Game.Powerups;
using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.Balls.TemplateMethod
{
    class ReboundFromArenaObject : ReboundTemplate
    {
        public ReboundFromArenaObject(float paddleSpeed, BallType bType, ArenaObject arObject, ArenaObjectType arObjectType) : base(paddleSpeed, bType, arObject, arObjectType)
        {
        }

        public sealed override void IsReboundingFromArenaObject()
        {
            ReboundFromArenaObject = true;
        }

        public sealed override void IsReboundingFromPaddle()
        {
            ReboundFromPaddle = false;
        }
    }
}
