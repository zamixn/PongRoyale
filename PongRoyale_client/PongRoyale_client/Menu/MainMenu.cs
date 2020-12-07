using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PongRoyale_client.Singleton;
using PongRoyale_shared;
using PongRoyale_client.Observers;

namespace PongRoyale_client.Menu
{
    public partial class MainMenu : UserControl, IObserverReceiver<GameStateObserver>
    {
        public MainMenu()
        {
            new GameStateObserver(GameManager.Instance, this);
            InitializeComponent();
            DebugCheckBox.Checked = GameManager.Instance.DebugMode;
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {

        }

        private void StartGameButton_Click(object sender, EventArgs e)
        {
            if (ServerConnection.Instance.IsConnected())
            {
                ServerConnection.Instance.SendStartGameMessage();
            }
            else
                ChatManager.Instance.Proxy.LogError("Could not start the game");
        }

        private void StartLocalButton_Click(object sender, EventArgs e)
        {
            GameManager.StartLocalGame();
        }

        
        private void ConnectToServerButton_Click(object sender, EventArgs e)
        {

            if (!ServerConnection.Instance.IsConnected())
            {
                ServerConnection.Instance.Connect(connectionString: IPTextBox.Text,
                    onConnected: () =>
                    {
                        GameManager.Instance.SetGameState(GameState.InMainMenu_Connected);
                        ChatManager.Instance.Proxy.ClearChat();
                        ChatManager.Instance.Proxy.ClearInput();
                        ChatManager.Instance.Proxy.LogInfo("Connected to server.");
                        ChatManager.Instance.Proxy.LogInfo($"Welcome to the chat: {ServerConnection.Instance.PlayerName}!");

                    },
                    onException: (ex) =>
                    {
                        ChatManager.Instance.Proxy.LogError("Failed to connect to server.");
                    });
            }
            else
            {
                ServerConnection.Instance.Disconnect(
                    onConnected: () =>
                    {
                        GameManager.Instance.SetGameState(GameState.InMainMenu_NotConnected);
                        ChatManager.Instance.Proxy.LogInfo("Disconnected from server.");
                    },
                    onException: (ex) =>
                    {
                        ChatManager.Instance.Proxy.LogError("Failed to disconnect from server.");
                    });

            }
        }

        private void DebugCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            GameManager.Instance.SetDebugMode(DebugCheckBox.Checked);
        }

        public bool IsInputSelected()
        {
            return IPTextBox.Focused;
        }

        public void ObserverNotify(GameStateObserver observer)
        {
            DebugCheckBox.Checked = GameManager.Instance.DebugMode;
            var state = GameManager.Instance.CurrentGameState;
            switch (state)
            {
                case GameState.InMainMenu_NotConnected:
                    ConnectToServerButton.Text = Constants.ConnectToServer;
                    break;
                case GameState.InMainMenu_Connected:
                    ConnectToServerButton.Text = Constants.DisconnectFromServer;
                    break;
            }
        }
    }
}
