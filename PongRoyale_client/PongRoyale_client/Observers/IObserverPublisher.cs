using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PongRoyale_client.Observers
{
    public interface IObserverPublisher<T> where T : IObserver
    {
        void RegisterObserver(T observer);
        void UnregisterObserver(T observer);
    }
}
