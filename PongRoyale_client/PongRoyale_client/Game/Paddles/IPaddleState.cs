using PongRoyale_client.Game.ArenaObjects.Powerups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.ArenaObjects
{
    public interface IPaddleState
    {
        void TransferPowerUp(PoweredUpData data);
    }
}
