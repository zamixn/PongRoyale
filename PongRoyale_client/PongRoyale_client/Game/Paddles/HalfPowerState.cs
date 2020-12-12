using PongRoyale_client.Extensions;
using PongRoyale_client.Game.ArenaObjects.Powerups;
using PongRoyale_client.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.ArenaObjects
{
    class HalfPowerState : IPaddleState
    {
        Paddle obj;
        public HalfPowerState(Paddle obj)
        {
            this.obj = obj;
        }
        public void TransferPowerUp(PoweredUpData data)
        {
            if (data.IsValid())
            {
                if (data.ChangePaddleSpeed)
                {
                    obj.CancellPowerupDisposal();
                    obj.ApplySpeedPowerup();
                }
                if (data.GivePlayerLife)
                {
                    obj.CancellPowerupDisposal();
                    obj.ApplyLifePowerup();
                }
                if (data.UndoPlayerMove)
                {
                    obj.CancellPowerupDisposal();
                    obj.ApplyUndoPowerup();
                }
                obj.ChangeState(new FullPowerState(obj));
            }
        }
    }
}
