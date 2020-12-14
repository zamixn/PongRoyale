using PongRoyale_client.Game.Balls.Decorator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.Mediator
{
    public interface IAbstractMediator
    {
        void Notify(string message, object data);
        bool GetBool(string message, object data);
        Paddle PaddleGetter(string message, object data);
        Dictionary<byte, IBall> BallGetter(string message, object data);
    }
}
