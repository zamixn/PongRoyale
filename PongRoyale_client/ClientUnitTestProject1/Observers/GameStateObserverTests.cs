using Microsoft.VisualStudio.TestTools.UnitTesting;
using PongRoyale_client.Observers;
using PongRoyale_client.Singleton;
using System;
using System.Collections.Generic;
using System.Text;

namespace PongRoyale_client.Observers.Tests
{
    class SimpleObserverReceiver : IObserverReceiver<GameStateObserver>
    {
        public GameState state;

        public SimpleObserverReceiver()
        {
            new GameStateObserver(GameManager.Instance, this);
        }

        public void ObserverNotify(GameStateObserver observer)
        {
            state = GameManager.Instance.CurrentGameState;
        }
    }

    [TestClass()]
    public class GameStateObserverTests
    {
        [TestMethod()]
        public void GameStateObserverTest()
        {
            SimpleObserverReceiver receiver = new SimpleObserverReceiver();
            GameManager.Instance.SetGameState(GameState.GameEnded);
            Assert.AreEqual(GameState.GameEnded, receiver.state);
        }
    }
}