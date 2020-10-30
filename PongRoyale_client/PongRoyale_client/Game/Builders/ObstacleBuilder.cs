using PongRoyale_client.Game.Obstacles;
using PongRoyale_shared;
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
        public float Width = 0;
        public float Heigth = 0;

        public ObstacleBuilder AddDuration(float duration)
        {
            Duration = duration;
            return this;
        }

        public ObstacleBuilder AddPosX(float posX)
        {
            PosX = posX;
            return this;
        }

        public ObstacleBuilder AddPosY(float posY)
        {
            PosY = posY;
            return this;
        }
        public ObstacleBuilder AddPos(Vector2 pos)
        {
            PosX = pos.X;
            PosY = pos.Y;
            return this;
        }

        public ObstacleBuilder AddWidth(float width)
        {
            Width = width;
            return this;
        }
        public ObstacleBuilder AddHeigth(float heigth)
        {
            Heigth = heigth;
            return this;
        }
        public ArenaObject CreateObject()
        {
            return new Obstacle(PosX, PosY, Duration, Width, Heigth);
        }
    }
}
