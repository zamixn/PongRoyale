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
    abstract class ReboundTemplate
    {
        public ReboundTemplate(float paddleSpeed, BallType bType, ArenaObject arObject, ArenaObjectType arObjectType)
        {
            PaddleSpeed = paddleSpeed;
            BType = bType;
            ArObject = arObject;
            ArObjectType = arObjectType;
        }

        protected float PaddleSpeed { get; set; }
        protected BallType BType { get; set; }
        protected ArenaObject ArObject { get; set; }
        protected ArenaObjectType ArObjectType { get; set; }
        protected bool ReboundFromPaddle { get; set; }
        protected bool ReboundFromArenaObject { get; set; }

        public virtual IReboundStrategy ChooseStrategy()
        {
            IsReboundingFromPaddle();
            IsReboundingFromArenaObject();
            if (ReboundFromPaddle)
            {
                switch (BType)
                {
                    case BallType.Deadly:
                        return new BallDeadlyStrategy();
                    case BallType.Normal:
                        if (PaddleSpeed < 0)
                            return new PaddleMovingLeft();
                        else if (PaddleSpeed > 0)
                            return new PaddleMovingRight();
                        else //if PaddleSpeed == 0
                            return new PaddleNotMoving();
                }
            }
            else if (ReboundFromArenaObject)
            {
                if (ArObject.GetType() == typeof(Obstacle))
                {
                    switch (ArObjectType)
                    {
                        case ArenaObjectType.Passable:
                            return new PassableObstacleStrategy();
                        case ArenaObjectType.NonPassable:
                            return new NonPassableObstacleStrategy();
                        default:
                            return null;
                    }
                }
                else if (ArObject.GetType() == typeof(PowerUp))
                {
                    switch (ArObjectType)
                    {
                        case ArenaObjectType.Passable:
                            return new PassablePowerupStrategy();
                        case ArenaObjectType.NonPassable:
                            return new NonPassablePowerupStrategy();
                        default:
                            return null;
                    }
                }
            }
            return null;
        }

        public virtual void IsReboundingFromPaddle()
        {
            ReboundFromPaddle = false;
        }
        public virtual void IsReboundingFromArenaObject()
        {
            ReboundFromArenaObject = false;
        }
    }
}
