using PongRoyale_client.Game.Powerups;
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
        public Color Color { get; set; }

        public bool MakeBallDeadly = false;
        public bool MakeBallFaster = false;
        public bool ChangeBallDirection = false;
        public bool GivePlayerLife = false;
        public bool MultiplyBalls = false;
        public bool MakePaddleFaster = false;
        public bool MakePaddleSlower = false;

        public void AddDeadlyBallPw()
        {
            MakeBallDeadly = true;
        }
        public void AddFasterBallPw()
        {
            MakeBallFaster = true;
        }
        public void AddBallDirPw()
        {
            ChangeBallDirection = true;
        }
        public void AddLifePw()
        {
            GivePlayerLife = true;
        }
        public void AddMultiplyBallsPw()
        {
            MultiplyBalls = true;
        }
        public void AddFasterPaddlePw()
        {
            MakePaddleFaster = true;
        }
        public void AddSlowerPaddlePw()
        {
            MakePaddleSlower = true;
        }
        public ArenaObject CreateObject()
        {
            return new Powerup(Duration, PosX, PosY, Color, MakeBallDeadly, MakeBallFaster, ChangeBallDirection, GivePlayerLife, MultiplyBalls, MakePaddleFaster, MakePaddleSlower);
        }
    }
}
