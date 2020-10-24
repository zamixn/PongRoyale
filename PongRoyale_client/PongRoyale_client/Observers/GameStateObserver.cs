using PongRoyale_client.Singleton;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Observers
{
    public class GameStateObserver : IObserver
    {
        private GameManager Publisher;
        private IObserverReceiver<GameStateObserver> Receiver;

        public GameStateObserver(GameManager publisher, IObserverReceiver<GameStateObserver> receiver)
        {
            Publisher = publisher;
            Receiver = receiver;
            publisher.RegisterObserver(this);
        }

        public void Update()
        {
            Receiver.ObserverNotify(this);
        }
    }
}
