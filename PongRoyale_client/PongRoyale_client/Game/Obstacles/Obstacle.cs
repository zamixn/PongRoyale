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
        public Obstacle(float posX, float posY, float duration, Color color, float width, float heigth)
        {
            PosX = posX;
            PosY = posY;
            Duration = duration;
            Color = color;
            Width = width;
            Heigth = heigth;
        }
    }
}
