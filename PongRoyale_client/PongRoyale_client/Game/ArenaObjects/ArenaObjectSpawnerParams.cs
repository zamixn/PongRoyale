using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.ArenaObjects
{
    public class ArenaObjectSpawnerParams
    {
        public float StartDelay;

        public float MinInterval;
        public float MaxInterval;

        public float MinWidth;
        public float MaxWidth;

        public float MinHeight;
        public float MaxHeight;

        public float MinDuration;
        public float MaxDuration;

        public float RollInterval()
        {
            return RandomNumber.RandomFloat(MinInterval, MaxInterval);
        }

        public float RollWidth()
        {
            return RandomNumber.RandomFloat(MinWidth, MaxWidth);
        }

        public float RollHeight()
        {
            return RandomNumber.RandomFloat(MinHeight, MaxHeight);
        }

        public float RollDuration()
        {
            return RandomNumber.RandomFloat(MinDuration, MaxDuration);
        }

        public Vector2 RollPosition()
        {
            var dims = ArenaFacade.Instance.ArenaDimensions;
            var halfSize = new Vector2(dims.Radius, dims.Radius);
            var min = dims.Center - halfSize;
            var max = dims.Center + halfSize;

            return RandomNumber.RandomVector(min, max);
        }
    }
}
