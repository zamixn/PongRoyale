﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.Balls.ReboundStrategy
{
    public interface IReboundStrategy
    {
        float[] ReboundDirection(float ballSpeedX, float ballSpeedY);
    }
}
