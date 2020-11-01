using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Singleton
{
    public enum GameState
    {
        InMainMenu_NotConnected,
        InMainMenu_Connected,
        GameEnded,
        InGame
    }
}
