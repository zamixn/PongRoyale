using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.Builders
{
    public interface IArenaObjectBuilder
    {
        float Duration { get; set; }
        float PosX { get; set; }
        float PosY { get; set; }
        ArenaObject CreateObject();
    }
}
