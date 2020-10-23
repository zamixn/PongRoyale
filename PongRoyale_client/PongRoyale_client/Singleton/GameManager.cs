using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Singleton
{
    public class GameManager : Singleton<GameManager>
    {
        public enum GameState 
        {
            InMainMenu,
            InGame
        }

        public GameState CurrentGameState { get; private set; }
        public void SetGameState(GameState state)
        {
            CurrentGameState = state;
        }
    }
}
