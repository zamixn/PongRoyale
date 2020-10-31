using PongRoyale_client.Game.ArenaObjects.Powerups;
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
    class PassableArenaObjectFactory : AbstractArenaObjectFactory
    {
        public override Obstacle CreateObstacle(IArenaObjectBuilder builder)
        {
            Obstacle obstacle = builder.CreateObject() as Obstacle;
            obstacle.SetTypeParams(ArenaObjectType.Passable);
            obstacle.Init(GameData.ObstacleColors[ArenaObjectType.Passable]);
            return obstacle;
        }

        public override Powerup CreatePowerup(IArenaObjectBuilder builder)
        {
            Powerup powerup = builder.CreateObject() as Powerup;
            powerup.SetTypeParams(ArenaObjectType.Passable);
            powerup.Init(GameData.PowerupColors[ArenaObjectType.Passable], PowerUppedData.RollRandom());
            return powerup;
        }
    }
}
