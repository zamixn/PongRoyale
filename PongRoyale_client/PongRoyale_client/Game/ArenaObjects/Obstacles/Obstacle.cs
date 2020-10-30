using PongRoyale_client.Extensions;
using PongRoyale_client.Singleton;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.Obstacles
{
    public class Obstacle : ArenaObject
    {
        public float Width;
        public float Heigth;
        public Obstacle(float posX, float posY, float duration, float width, float heigth)
        {
            PosX = posX;
            PosY = posY;
            Duration = duration;
            Width = width;
            Heigth = heigth;
        }

        public void Init(Color color)
        {
            Color = color;
        }

        public override void Render(Graphics g, Pen p, Brush b)
        {
            b = new SolidBrush(CurrentColor);
            g.FillRectangleAtCenter(b, PosX, PosY, Width, Heigth);
            b.Dispose();
        }

        public override Rect2D GetBounds()
        {
            Rect2D bounds = new Rect2D(
                PosX - Width / 2f, PosY - Heigth / 2f,
                Width, Heigth);
            return bounds;
        }
    }
}
