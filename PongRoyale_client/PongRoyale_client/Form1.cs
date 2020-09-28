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
        public static Form1 Instance;
        private long FrameCount;
        private Player player;

        public Form1()
        {
            Instance = this;
            InitializeComponent();

            player = new Player();
            ConnectToServerButton.Text = Constants.ConnectToServer;
            GameLoop.Start();
        }

        private void GameLoop_Tick(object sender, EventArgs e)
        {
            FrameCountLabel.Text = string.Format(Constants.FrameCount, FrameCount++);
        }

        private void ConnectToServerButton_Click(object sender, EventArgs e)
        {
            if (!player.IsConnected())
            {
                player.Connect(
                    onConnected: () =>
                    {
                        LogToServerInfo("Connected to server.");
                        ConnectToServerButton.Text = Constants.DisconnectFromServer;

                    },
                    onException: OnException);
            }
            else
            {
                player.Disconnect(
                    onConnected: () =>
                    {
                        LogToServerInfo("Disconnected from server.");
                        ConnectToServerButton.Text = Constants.ConnectToServer;
                    },
                    onException: OnException);

            }
        }

        private void SendDataToServer_Click(object sender, EventArgs e)
        {
            player.SendDataToServer("Ping: " + DataToServerTextBox.Text, waitForResponse: true,
                onResponse: (response) => { LogToServerInfo("Received: " + response); },
                onException: OnException);
        }

        public void LogToServerInfo(string message)
        {
            ServerResponseLabel.Text += "\n" + message;
        }

        public void OnException(Exception ex)
        {
            Debug.WriteLine(string.Format("Exception occured: {0}", ex.Message ?? "null"));
        }
    }
}
