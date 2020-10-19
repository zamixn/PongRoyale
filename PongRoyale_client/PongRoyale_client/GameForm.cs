using PongRoyale_client.Game;
using PongRoyale_client.Observers;
using PongRoyale_client.Singleton;
using PongRoyale_shared;
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
        public static GameForm Instance;
        private long FrameCount;
        //private const long FramesToWaitUntilGameStart = 1;
        //EventHandler GameLoopWaitHandler;
        EventHandler GameLoopHandler;

        private LifeObserver LifeObserver;

        public GameForm()
        {
            Instance = this;
            InitializeComponent();

            SafeInvoke.Instance.Setup(this);

            ChatController.Instance.Setup(Chat, ChatInput);
            ConnectToServerButton.Text = Constants.ConnectToServer;

            //GameLoopWaitHandler = new EventHandler(this.GameLoopWait_Tick);
            //GameLoop.Tick += GameLoopWaitHandler;
            //GameLoop.Start();
            LifeObserver = new LifeObserver();
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            if (ServerConnection.Instance.IsConnected())
                ServerConnection.Instance.Disconnect();
        }

        //private void GameLoopWait_Tick(object sender, EventArgs e)
        //{
        //    FrameCountLabel.Text = string.Format(Constants.FrameCount, FrameCount++);
        //    if (FrameCount > FramesToWaitUntilGameStart)
        //    {
        //        StartGame();
        //    }
        //}


        public void StartGame()
        {
            GameManager.Instance.InitGame(GameScreen);
            //GameLoop.Tick -= GameLoopWaitHandler;
            GameLoopHandler = new EventHandler(GameLoop_Tick);
            GameLoop.Tick += GameLoopHandler;
            GameLoop.Start();
            SyncLoop.Tick += new EventHandler(SyncLoop_Tick);
            SyncLoop.Start();
        }
        private void GameLoop_Tick(object sender, EventArgs e)
        {
            FrameCountLabel.Text = string.Format(Constants.FrameCount, FrameCount++);
            GameManager.Instance.UpdateGameLoop();
        }

        private void SyncLoop_Tick(object sender, EventArgs e)
        {
            if (ServerConnection.Instance.IsConnected())
            {
                Player.Instance.SyncWithServer();
            }
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

        private void StartGameButton_Click(object sender, EventArgs e)
        {
            if (ServerConnection.Instance.IsConnected())
            {
                Player.Instance.SendStartGameMessage();
            }
            else
                ChatController.Instance.LogError("Could not start the game");
        }

        private void StartLocalButton_Click(object sender, EventArgs e)
        {
            byte[] playerIds = new byte[] { 0 };
            PaddleType[] paddleTypes = new PaddleType[] { PaddleType.Normal };
            BallType ballType = BallType.Normal;

            RoomSettings.Instance.SetRoomSettings(playerIds, paddleTypes, ballType, playerIds[0]);
            GameForm.Instance.StartGame();
        }
    }
}
