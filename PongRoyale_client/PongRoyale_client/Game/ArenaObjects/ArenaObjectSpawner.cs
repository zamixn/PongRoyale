using PongRoyale_client.Game.ArenaObjects;
using PongRoyale_client.Singleton;
using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game
{
    public abstract class ArenaObjectSpawner
    {
        protected float SpawnInterval;
        protected float LastSpawnTime;
        protected float Time = 0;
        protected ArenaObjectSpawnerParams Params;
        protected AbstractArenaObjectFactory[] Factories;

        protected ArenaObjectSpawner(ArenaObjectSpawnerParams _params)
        {
            Params = _params;
            Factories = new AbstractArenaObjectFactory[] {
                new PassableArenaObjectFactory(),
                new NonPassableArenaObjectFactory()
            };
            SpawnInterval = Params.RollInterval();
            LastSpawnTime = -SpawnInterval + Params.StartDelay;
        }

        public virtual void Update()
        {
            Time += GameManager.Instance.DeltaTime; ;

            if (CanSpawn())
            {
                Spawn();
            }
        }

        protected bool CanSpawn()
        {
            return LastSpawnTime + SpawnInterval < Time;
        }

        protected abstract void Spawn();
    }
}
