using PongRoyale_client.Game.Obstacles;
using PongRoyale_client.Game.Powerups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game
{
    class PassableFactory : AbstractFactory
    {
        public override Obstacle CreateObstacle(ObstacleType type)
        {
            switch (type)
            {
                case ObstacleType.yes:
                    return null;
                case ObstacleType.no:
                    return null;
                default:
                    return null;
            }
        }

        public override Powerup CreatePowerup(PowerupType type)
        {
            switch (type)
            {
                case PowerupType.yes:
                    return null;
                case PowerupType.no:
                    return null;
                default:
                    return null;
            }
        }
    }
}
