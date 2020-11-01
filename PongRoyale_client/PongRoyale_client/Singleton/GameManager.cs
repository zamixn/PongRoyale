using PongRoyale_client.Observers;
using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Singleton
{
    public class GameManager : Singleton<GameManager>, IObserverPublisher<GameStateObserver>
    {
        public bool DebugMode { get; private set; } = false;
        public void SetDebugMode(bool value)
        {
            DebugMode = value;
        }
        public GameState CurrentGameState { get; private set; }
        public void SetGameState(GameState state)
        {
            CurrentGameState = state;

            foreach (var observer in GameStateObservers)
            {
                observer.Update();
            }
        }

        public float DeltaTime { get; private set; }
        public void SetTimeSinceLastFrame(float dTime)
        {
            DeltaTime = dTime;
        }


        #region game state observer
        private List<GameStateObserver> GameStateObservers = new List<GameStateObserver>();
        public void RegisterObserver(GameStateObserver observer)
        {
            GameStateObservers.Add(observer);
        }

        public void UnregisterObserver(GameStateObserver observer)
        {
            GameStateObservers.Remove(observer);
        }
        #endregion
    }
}
