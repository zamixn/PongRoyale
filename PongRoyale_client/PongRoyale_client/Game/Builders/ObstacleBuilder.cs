using PongRoyale_client.Game.Obstacles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.Builders
{
    class ObstacleBuilder : IArenaObjectBuilder
    {
        public float Duration { get; set; }
        public float PosX { get; set; }
        public float PosY { get; set; }
        public Color Color { get; set; }
        public float Width = 0;
        public float Heigth = 0;

        public void AddWidth(float width)
        {
            Width = width;
        }
        public void AddHeigth(float heigth)
        {
            Heigth = heigth;
        }
        public ArenaObject CreateObject()
        {
            return new Obstacle(PosX, PosY, Duration, Color, Width, Heigth);
        }
    }
}
