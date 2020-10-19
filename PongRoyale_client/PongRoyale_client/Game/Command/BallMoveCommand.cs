using PongRoyale_client.Game.Balls;
using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.Command
{
    class BallMoveCommand : Command
    {
        private Ball Ball;
        private Vector2 PosOffset;

        public BallMoveCommand(Ball ball, Vector2 posOffset)
        {
            Ball = ball;
            PosOffset = posOffset;
        }

        public override void Execute()
        {
            Ball.Move(PosOffset);
        }

        public override void Undo()
        {
            Ball.Move(-PosOffset);
        }
    }
}
