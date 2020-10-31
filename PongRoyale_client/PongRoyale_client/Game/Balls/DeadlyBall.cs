using PongRoyale_client.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.Balls
{
    class DeadlyBall : Ball
    {
        public override void LocalMove()
        {
            base.LocalMove();
        }

        public override Color GetColor()
        {
            return Color.OrangeRed;
        }

        public override void Render(Graphics g, Brush b)
        {
            b = new SolidBrush(Color.OrangeRed);
            base.Render(g, b);
        }

        protected override bool HandleOutOfBounds(bool isOutOfBounds)
        {
            ArenaFacade.Instance.ResetBall(this);
            return !isOutOfBounds;
        }
    }
}
