﻿using PongRoyale_client.Game;
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
    public partial class MainForm : Form, IObserverReceiver<GameStateObserver>
    { 
        public static MainForm Instance;
        private long FrameCount;

        public MainForm()
        {
            Instance = this;
            new GameStateObserver(GameManager.Instance, this);

            InitializeComponent();
            GameLoop.Tick += new EventHandler(GameLoop_Tick);
            SyncLoop.Tick += new EventHandler(SyncLoop_Tick);

            SafeInvoke.Instance.Setup(this);
            MainMenu.ConnectToServerButton.Text = Constants.ConnectToServer;
            GameManager.Instance.SetGameState(GameManager.GameState.InMainMenu_NotConnected);

            //tests            
            //UnitTests.NetworkMessageUnitTests.TestBallSyncEncodingAndDecoding();
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            if (ServerConnection.Instance.IsConnected())
                ServerConnection.Instance.Disconnect();
        }


        public void StartGame()
        {
            ArenaManager.Instance.InitGame(GameScreen);
            GameLoop.Start();
            SyncLoop.Start();
        }

        public void EndGame()
        {
            ArenaManager.Instance.DestroyGame();
            GameLoop.Stop();
            SyncLoop.Stop();
        }

        private void GameLoop_Tick(object sender, EventArgs e)
        {
            FrameCountLabel.Text = string.Format(Constants.FrameCount, FrameCount++);
            ArenaManager.Instance.UpdateGameLoop();
        }

        private void SyncLoop_Tick(object sender, EventArgs e)
        {
            if (ServerConnection.Instance.IsConnected())
            {
                Player.Instance.SyncWithServer();
            }
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
            return ChatController.Instance.IsInputSelected() || MainMenu.IsInputSelected();
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            InputManager.Instance.OnKeyUp(e.KeyCode);
            if(!IsTextBoxSelected())
                e.SuppressKeyPress = true;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        // Game state observer
        public void ObserverNotify(GameStateObserver observer)
        {
            var state = GameManager.Instance.CurrentGameState;

            switch (state)
            {
                case GameManager.GameState.InMainMenu_NotConnected:
                    break;
                case GameManager.GameState.InMainMenu_Connected:
                    break;
                case GameManager.GameState.GameEnded:
                    EndGame();
                    break;
                case GameManager.GameState.InGame:
                    StartGame();
                    break;
                default:
                    break;
            }

            ChatUI.Visible = state != GameManager.GameState.InMainMenu_NotConnected;
            GameScreen.Visible = state == GameManager.GameState.InGame;
            MainMenu.Visible = state != GameManager.GameState.InGame;
            InGameMenu.Visible = state == GameManager.GameState.InGame;
            GameEndMenu.Visible = state == GameManager.GameState.GameEnded;

        }
    }
}