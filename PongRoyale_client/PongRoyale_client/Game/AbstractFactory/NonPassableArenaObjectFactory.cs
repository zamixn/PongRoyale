using PongRoyale_client.Game.Builders;
using PongRoyale_client.Game.Obstacles;
using PongRoyale_client.Game.Powerups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game
{
    class NonPassableArenaObjectFactory : AbstractArenaObjectFactory
    {
        public override Obstacle CreateObstacle(IArenaObjectBuilder builder)
        {
            Obstacle obstacle = builder.CreateObject() as Obstacle;
            obstacle.SetTypeParams(ArenaObjectType.NonPassable);
            obstacle.Init(GameData.ObstacleColors[ArenaObjectType.NonPassable]);
            return obstacle;
        }

        public override Powerup CreatePowerup(IArenaObjectBuilder builder)
        {
            Powerup powerup = builder.CreateObject() as Powerup;
            powerup.SetTypeParams(ArenaObjectType.NonPassable);
            return powerup;
        }
    }
}
