using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace PongRoyale_server.Game
{
    public class GameController : Singleton<GameController>
    {
        public Dictionary<Player, ServerSidePaddle> Paddles { get; private set; }


        public void AddPaddle(Player p, ServerSidePaddle paddle)
        {
            Paddles.Add(p, paddle);
        }

        public void UpdatePlayerPos(Player p, float pos)
        {
            Paddles[p].SetPosition(pos);
        }
    }
}
