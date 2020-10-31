using PongRoyale_client.Extensions;
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
        private float Diameter => Width;
        private float Radius => Diameter / 2f;

        public bool MakeBallDeadly;
        public bool MakeBallFaster;
        public bool ChangeBallDirection;
        public bool GivePlayerLife;
        public bool MakePaddleFaster;
        public bool MakePaddleSlower;

        public Powerup(float duration, float posX, float posY, float width, float height,
            bool makeBallDeadly, bool makeBallFaster, bool changeBallDirection, bool givePlayerLife, bool makePaddleFaster, bool makePaddleSlower)
        {
            Duration = duration;
            PosX = posX;
            PosY = posY;
            Width = width;
            Heigth = height;
            MakeBallDeadly = makeBallDeadly;
            MakeBallFaster = makeBallFaster;
            ChangeBallDirection = changeBallDirection;
            GivePlayerLife = givePlayerLife;
            MakePaddleFaster = makePaddleFaster;
            MakePaddleSlower = makePaddleSlower;
        }

        public void Init(Color color)
        {
            Color = color;
        }

        public override void Render(Graphics g, Pen p, Brush b)
        {
            b = new SolidBrush(CurrentColor);
            g.FillNgonAtCenter(b, 8, PosX, PosY, Diameter);
            b.Dispose();
        }

        public override Rect2D GetBounds()
        {
            float offset = Radius;
            return new Rect2D(PosX - offset, PosY - offset, Diameter, Diameter);
        }
    }
}
