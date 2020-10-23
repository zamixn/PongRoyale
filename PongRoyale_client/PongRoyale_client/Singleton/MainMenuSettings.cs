using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Singleton
{
    public class MainMenuSettings : Singleton<MainMenuSettings>
    {
        public bool DebugMode { get; set; }
    }
}
