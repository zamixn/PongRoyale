using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Singleton
{
    public class RoomSettings : Singleton<RoomSettings>
    {
        public int PlayerCount { get; private set; } = 8;
    }
}
