using PongRoyale_client.Game.ArenaObjects.Powerups;
using PongRoyale_client.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.ArenaObjects
{
    class NoPowerState : IPaddleState
    {
        Paddle obj;
        public NoPowerState(Paddle obj)
        {
            this.obj = obj;
        }
        public void TransferPowerUp(PoweredUpData data)
        {
            if (data.IsValid())
            {
                if (data.ChangePaddleSpeed)
                {
                    obj.ApplySpeedPowerup();
                }
                if (data.GivePlayerLife)
                {
                    obj.ApplyLifePowerup();
                }
                if(data.UndoPlayerMove)
                {
                    obj.ApplyUndoPowerup();
                }
                obj.ChangeState(new HalfPowerState(obj));
            }
        }
    }
}
