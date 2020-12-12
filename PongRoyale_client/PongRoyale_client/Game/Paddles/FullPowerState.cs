using PongRoyale_client.Extensions;
using PongRoyale_client.Game.ArenaObjects.Powerups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.ArenaObjects
{
    class FullPowerState : IPaddleState
    {
        Paddle obj;
        public FullPowerState(Paddle obj)
        {
            this.obj = obj;
        }
        public void TransferPowerUp(PoweredUpData data)
        {
            if (data.IsValid())
            {
                obj.CancellPowerupDisposal();
                obj.ExtendPowerups();
            }
        }
    }
}
