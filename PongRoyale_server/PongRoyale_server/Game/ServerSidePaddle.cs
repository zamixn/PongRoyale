using System;
using System.Collections.Generic;
using System.Text;

namespace PongRoyale_server.Game
{
    public class ServerSidePaddle
    {
        public float AngularPosition { get; protected set; }
        public float AngularSize { get; protected set; }
        public float AngularSpeed { get; protected set; }
        public float Thickness { get; private set; }

        public ServerSidePaddle(float angularSize, float angularSpeed, float thickness)
        {
            AngularSize = angularSize;
            AngularSpeed = angularSpeed;
            Thickness = thickness;
        }

        public void SetPosition(float pos)
        { 
            AngularPosition = pos;
        }


    }
}
