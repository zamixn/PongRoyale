using PongRoyale_client.Game.Obstacles;
using PongRoyale_client.Game.Powerups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game
{
    public abstract class AbstractFactory
    {
        public abstract Obstacle CreateObstacle(ObstacleType type);
        public abstract Powerup CreatePowerup(PowerupType type);
    }
}
