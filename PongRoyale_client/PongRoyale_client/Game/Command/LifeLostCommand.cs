using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.Command
{
    class LifeLostCommand : Command
    {
        private Paddle Paddle;
        private int LifeLost;

        public LifeLostCommand(Paddle paddle, int lifeLost)
        {
            Paddle = paddle;
            LifeLost = lifeLost;
        }

        public override void Execute()
        {
            Paddle.AddLife(LifeLost);
        }

        public override void Undo()
        {
            Paddle.AddLife(-LifeLost);
        }
    }
}
