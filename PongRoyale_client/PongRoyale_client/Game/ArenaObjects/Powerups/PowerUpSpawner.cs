using PongRoyale_client.Game.Builders;
using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.ArenaObjects.Powerups
{
    public class PowerUpSpawner : ArenaObjectSpawner
    {
        public PowerUpSpawner(ArenaObjectSpawnerParams _params, AbstractArenaObjectFactory[] factories)
            : base(_params, factories)
        {
        }

        protected override void Spawn()
        {
            var diameter = Params.RollWidth();
            var duration = Params.RollDuration();
            var pos = Params.RollPosition();

            var powerupBuilder = new PowerUpBuilder().AddDiameter(diameter).AddDuration(duration).AddPos(pos);
            var powerup = Factories[RandomNumber.RandomNumb(0, Factories.Length)].CreatePowerup(powerupBuilder);
            ArenaFacade.Instance.OnArenaObjectCreated(powerup);

            SpawnInterval = Params.RollInterval();
            LastSpawnTime = Time;
        }
    }
}
