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
        EventHandler GameLoopHandler;

        private LifeObserver LifeObserver;

        public GameForm()
        {
            Instance = this;
            InitializeComponent();

            SafeInvoke.Instance.Setup(this);
            MainMenu.ConnectToServerButton.Text = Constants.ConnectToServer;

            LifeObserver = new LifeObserver();
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            if (ServerConnection.Instance.IsConnected())
                ServerConnection.Instance.Disconnect();
        }


        public void StartGame()
        {
            GameplayManager.Instance.InitGame(GameScreen);
            GameLoopHandler = new EventHandler(GameLoop_Tick);
            GameLoop.Tick += GameLoopHandler;
            GameLoop.Start();
            SyncLoop.Tick += new EventHandler(SyncLoop_Tick);
            SyncLoop.Start();
        }
        private void GameLoop_Tick(object sender, EventArgs e)
        {
            FrameCountLabel.Text = string.Format(Constants.FrameCount, FrameCount++);
            GameplayManager.Instance.UpdateGameLoop();
        }

        private void SyncLoop_Tick(object sender, EventArgs e)
        {
            if (ServerConnection.Instance.IsConnected())
            {
                Player.Instance.SyncWithServer();
            }
        }

        public void OnConnectedToServer()
        {
            Player.Instance.Register(LifeObserver);
        }
        public void OnDisconnectedToServer()
        {
            Player.Instance.Unregister(LifeObserver);
        }

        // this is an on key down event
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            InputManager.Instance.OnKeyDown(keyData);
            if (!IsTextBoxSelected())
                return true;
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private bool IsTextBoxSelected()
        {
            return ChatController.Instance.IsInputSelected() || MainMenu.IPTextBox.Focused;
        }

        private void GameForm_KeyUp(object sender, KeyEventArgs e)
        {
            InputManager.Instance.OnKeyUp(e.KeyCode);
            if(!IsTextBoxSelected())
                e.SuppressKeyPress = true;
        }

        private void GameForm_Load(object sender, EventArgs e)
        {

        }
    }
}
