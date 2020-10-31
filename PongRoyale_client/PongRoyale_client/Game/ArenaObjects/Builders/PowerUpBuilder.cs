using PongRoyale_client.Game.Powerups;
using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.Builders
{
    public class PowerUpBuilder : IArenaObjectBuilder
    {
        public float Duration { get; set; }
        public float PosX { get; set; }
        public float PosY { get; set; }
        public float Diameter { get; set; }

        public PowerUpBuilder AddDuration(float duration)
        {
            Duration = duration;
            return this;
        }

        public PowerUpBuilder AddPosX(float posX)
        {
            PosX = posX;
            return this;
        }

        public PowerUpBuilder AddPosY(float posY)
        {
            PosY = posY;
            return this;
        }

        public PowerUpBuilder AddDiameter(float diameter)
        {
            Diameter = diameter;
            return this;
        }

        public PowerUpBuilder AddPos(Vector2 pos)
        {
            PosX = pos.X;
            PosY = pos.Y;
            return this;
        }

        public ArenaObject CreateObject()
        {
            return new Powerup(Duration, PosX, PosY, Diameter, Diameter);
        }
    }
}
