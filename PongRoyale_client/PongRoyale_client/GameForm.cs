using PongRoyale_client.Game;
using PongRoyale_client.Observers;
using PongRoyale_client.Singleton;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PongRoyale_client
{
    public partial class GameForm : Form
    {
        private long FrameCount;
        private const long FramesToWaitUntilGameStart = 1;
        EventHandler GameLoopWaitHandler;
        EventHandler GameLoopHandler;

        private LifeObserver LifeObserver;

        public GameForm()
        {
            InitializeComponent();

            SafeInvoke.Instance.Setup(this);

            ChatController.Instance.Setup(Chat, ChatInput);
            ConnectToServerButton.Text = Constants.ConnectToServer;

            GameLoopWaitHandler = new EventHandler(this.GameLoopWait_Tick);
            GameLoop.Tick += GameLoopWaitHandler;
            GameLoop.Start();
            LifeObserver = new LifeObserver();
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            if (ServerConnection.Instance.IsConnected())
                ServerConnection.Instance.Disconnect();
        }

        private void GameLoopWait_Tick(object sender, EventArgs e)
        {
            FrameCountLabel.Text = string.Format(Constants.FrameCount, FrameCount++);
            if (FrameCount > FramesToWaitUntilGameStart)
            {
                StartGame();
            }
        }


        private void StartGame()
        {
            GameManager.Instance.InitGame(RoomSettings.Instance.PlayerCount, GameScreen);
            GameLoop.Tick -= GameLoopWaitHandler;
            GameLoopHandler = new EventHandler(GameLoop_Tick);
            GameLoop.Tick += GameLoopHandler;
        }
        private void GameLoop_Tick(object sender, EventArgs e)
        {
            FrameCountLabel.Text = string.Format(Constants.FrameCount, FrameCount++);

            GameManager.Instance.UpdateGameLoop();
        }

        private void ConnectToServerButton_Click(object sender, EventArgs e)
        {
            if (!ServerConnection.Instance.IsConnected())
            {
                ServerConnection.Instance.Connect(
                    onConnected: () =>
                    {
                        ChatController.Instance.LogInfo("Connected to server.");
                        ChatController.Instance.LogInfo($"Welcome to the chat: {Player.Instance.PlayerName}!");
                        Player.Instance.Register(LifeObserver);
                        ConnectToServerButton.Text = Constants.DisconnectFromServer;

                    },
                    onException: (ex) =>
                    {
                        ChatController.Instance.LogError("Failed to connect to server.");
                    });
            }
            else
            {
                ServerConnection.Instance.Disconnect(
                    onConnected: () =>
                    {
                        ChatController.Instance.LogInfo("Disconnected from server.");
                        Player.Instance.Unregister(LifeObserver);
                        ConnectToServerButton.Text = Constants.ConnectToServer;
                    },
                    onException: (ex) => 
                    {
                        ChatController.Instance.LogError("Failed to disconnect from server.");
                    });

            }
        }

        private void ChatInput_Submitted(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ChatController.Instance.OnChatInputSubmitted();
            }
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            InputManager.Instance.OnKeyDown(e.KeyCode);
            if (!ChatController.Instance.IsInputSelected())
                e.SuppressKeyPress = true;
        }

        private void GameForm_KeyUp(object sender, KeyEventArgs e)
        {
            InputManager.Instance.OnKeyUp(e.KeyCode);
            if(!ChatController.Instance.IsInputSelected())
                e.SuppressKeyPress = true;
        }

        private void GameForm_Load(object sender, EventArgs e)
        {

        }
    }
}
