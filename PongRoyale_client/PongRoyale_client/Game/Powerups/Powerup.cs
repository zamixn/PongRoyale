using PongRoyale_client.Singleton;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.Powerups
{
    public class Powerup : ArenaObject
    {
        public bool MakeBallDeadly;
        public bool MakeBallFaster;
        public bool ChangeBallDirection;
        public bool GivePlayerLife;
        public bool MakePaddleFaster;
        public bool MakePaddleSlower;

        public Powerup(float duration, float posX, float posY, Color color,
            bool makeBallDeadly, bool makeBallFaster, bool changeBallDirection, bool givePlayerLife, bool makePaddleFaster, bool makePaddleSlower)
        {
            Duration = duration;
            PosX = posX;
            PosY = posY;
            Color = color;
            MakeBallDeadly = makeBallDeadly;
            MakeBallFaster = makeBallFaster;
            ChangeBallDirection = changeBallDirection;
            GivePlayerLife = givePlayerLife;
            MakePaddleFaster = makePaddleFaster;
            MakePaddleSlower = makePaddleSlower;
        }

        public override void Render(Graphics g, Pen p, Brush b)
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }

        public override Rect2D GetBounds()
        {
            throw new NotImplementedException();
        }
    }
}
