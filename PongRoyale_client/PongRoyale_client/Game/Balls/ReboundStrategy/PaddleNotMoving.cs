﻿using PongRoyale_client.Extensions;
using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.Balls.ReboundStrategy
{
    public class PaddleNotMoving : IReboundStrategy
    {

        public Vector2 ReboundDirection(Vector2 ballDirection)
        {
            throw new NotImplementedException();
        }
    } 
}