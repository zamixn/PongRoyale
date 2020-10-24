using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PongRoyale_client.Observers;
using PongRoyale_client.Singleton;
using System.Diagnostics;

namespace PongRoyale_client.Menu
{
    public partial class GameEnd : UserControl, IObserverReceiver<GameStateObserver>
    {
        public GameEnd()
        {
            InitializeComponent();
            new GameStateObserver(GameManager.Instance, this);
        }

        public void ObserverNotify(GameStateObserver observer)
        {
            var state = GameManager.Instance.CurrentGameState;
            switch (state)
            {
                case GameManager.GameState.GameEnded:
                    PlayerNameLabel.Text = RoomSettings.Instance.GetPlayerWonName();
                    break;
            }
        }

        private void GameEnd_Load(object sender, EventArgs e)
        {

        }
    }
}
