using PongRoyale_client.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Observers
{
    class LifeObserver : IObserver
    {
        public void Update()
        {
            ChatController.Instance.LogInfo("Player Lost");
        }
    }
}
