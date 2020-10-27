using PongRoyale_client.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game
{
    public abstract class ArenaObjectSpawner
    {
        protected float Time = 0;

        public virtual void Update()
        {
            Time += GameManager.Instance.DeltaTime; ;
        }
    }
}
