using PongRoyale_client.Game.ArenaObjects;
using PongRoyale_client.Game.Builders;
using PongRoyale_client.Singleton;
using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.Obstacles
{
    public class ObstacleSpawner : ArenaObjectSpawner
    {
        public ObstacleSpawner(ArenaObjectSpawnerParams _params, AbstractArenaObjectFactory[] factories) 
            : base(_params, factories)
        {            
        }

        protected override void Spawn()
        {
            var width = Params.RollWidth();
            var height = Params.RollHeight();
            var duration = Params.RollDuration();
            var pos = Params.RollPosition();

            var obstacleBuilder = new ObstacleBuilder().AddHeigth(width).AddWidth(height).AddDuration(duration).AddPos(pos);
            var obstacle = Factories[RandomNumber.NextInt(0, Factories.Length)].CreateObstacle(obstacleBuilder);
            ArenaFacade.Instance.OnArenaObjectCreated(obstacle);

            SpawnInterval = Params.RollInterval();
            LastSpawnTime = Time;
        }
    }
}
