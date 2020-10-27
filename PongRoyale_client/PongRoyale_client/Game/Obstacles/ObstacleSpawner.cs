using PongRoyale_client.Game.Builders;
using PongRoyale_client.Singleton;
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
        private ArenaDimensions ArenaDimensions() => ArenaFacade.Instance.ArenaDimensions;

        private float SpawnInterval = 5f;
        private float LastSpawnTime = -5f;

        private bool CanSpawn()
        {
            return LastSpawnTime + SpawnInterval < Time;
        }

        private void Spawn()
        {

            var obstacle = new ObstacleBuilder().AddHeigth(150).AddWidth(150).AddColor(Color.Red).AddDuration(5f).AddPos(ArenaDimensions().Center).CreateObject();
            SafeInvoke.Instance.DelayedInvoke(1f, () => ArenaFacade.Instance.CreateArenaObject(obstacle));

            LastSpawnTime = Time;
        }

        public override void Update()
        {
            base.Update();

            if(CanSpawn())
            {
                Spawn();
            }
        }
    }
}
