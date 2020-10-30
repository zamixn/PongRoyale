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
    public abstract class AbstractArenaObjectFactory
    {
        public abstract Obstacle CreateObstacle(IArenaObjectBuilder builder);
        public abstract Powerup CreatePowerup(IArenaObjectBuilder builder);
    }
}
