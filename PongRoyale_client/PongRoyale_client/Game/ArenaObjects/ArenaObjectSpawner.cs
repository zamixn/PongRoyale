using PongRoyale_client.Game.ArenaObjects;
using PongRoyale_client.Game.Ranking;
using PongRoyale_client.Singleton;
using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game
{
    public abstract class ArenaObjectSpawner : UpdateLeaf
    {
        protected float SpawnInterval;
        protected float LastSpawnTime;
        public float Time { get; protected set; } = 0;
        protected ArenaObjectSpawnerParams Params;
        protected AbstractArenaObjectFactory[] Factories;

        protected ArenaObjectSpawner(ArenaObjectSpawnerParams _params, AbstractArenaObjectFactory[] factories)
        {
            Params = _params;
            Factories = factories;
            SpawnInterval = Params.RollInterval();
            LastSpawnTime = -SpawnInterval + Params.StartDelay;
        }

        public override void Update()
        {
            if (ServerConnection.Instance.IsRoomMaster)
            {
                Time += GameManager.Instance.DeltaTime; ;

                if (CanSpawn())
                {
                    Spawn();
                }
            }
        }

        protected bool CanSpawn()
        {
            return LastSpawnTime + SpawnInterval < Time;
        }

        protected abstract void Spawn();
    }
}
