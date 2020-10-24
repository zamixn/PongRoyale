using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Observers
{
    public interface IObserverReceiver<T> where T : IObserver
    {
        void ObserverNotify(T observer);
    }
}
