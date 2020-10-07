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
    public partial class Form1 : Form
    {
        private long FrameCount;

        public Form1()
        {
            InitializeComponent();

            SafeInvoke.Instance.Setup(this);

            ChatController.Instance.Setup(Chat, ChatInput);
            ConnectToServerButton.Text = Constants.ConnectToServer;
            GameLoop.Start();
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            Debug.WriteLine("Closing");
            if (ServerConnection.Instance.IsConnected())
                ServerConnection.Instance.Disconnect();
        }

        private void GameLoop_Tick(object sender, EventArgs e)
        {
            FrameCountLabel.Text = string.Format(Constants.FrameCount, FrameCount++);
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
                        ConnectToServerButton.Text = Constants.ConnectToServer;
                    },
                    onException: (ex) => 
                    {
                        ChatController.Instance.LogError("Failed to disconnect from server.");
                    });

            }
        }

        private void SendDataToServer_Click(object sender, EventArgs e)
        {
            //ServerConnection.Instance.SendDataToServer("Ping: " + DataToServerTextBox.Text, waitForResponse: true,
            //    onResponse: (response) =>
            //    {
            //        ChatController.Instance.LogInfo(response);
            //    });
        }

        private void ChatInput_Submitted(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ChatController.Instance.OnChatInputSubmitted();
            }
        }
    }
}
