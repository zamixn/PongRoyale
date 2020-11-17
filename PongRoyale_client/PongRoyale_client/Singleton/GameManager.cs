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
            Notify();
        }

        public float DeltaTime { get; private set; }
        public void SetTimeSinceLastFrame(float dTime)
        {
            DeltaTime = dTime;
        }

        public static void StartLocalGame()
        {
            byte[] playerIds = new byte[] { 0 };
            PaddleType[] paddleTypes = new PaddleType[] { 
                //(PaddleType)RandomNumber.RandomNumb((int)PaddleType.Normal, (int)PaddleType.Short + 1)
                PaddleType.Normal
            };
            BallType ballType = BallType.Normal;

            RoomSettings.Instance.SetRoomSettings(playerIds, paddleTypes, ballType, playerIds[0]);
            Instance.SetGameState(GameState.InGame);
        }

        #region game state observer
        private void Notify()
        {
            foreach (var observer in GameStateObservers)
            {
                observer.Update();
            }
        }

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
