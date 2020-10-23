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

namespace PongRoyale_client.Menu
{
    public partial class MainMenu : UserControl
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void MainMenu_Load(object sender, EventArgs e)
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
            PaddleType[] paddleTypes = new PaddleType[] { 
                //(PaddleType)RandomNumber.RandomNumb((int)PaddleType.Normal, (int)PaddleType.Short + 1)
                PaddleType.Normal
            };
            BallType ballType = BallType.Normal;

            RoomSettings.Instance.SetRoomSettings(playerIds, paddleTypes, ballType, playerIds[0]);
            GameForm.Instance.StartGame();
        }

        private void ConnectToServerButton_Click(object sender, EventArgs e)
        {

            if (!ServerConnection.Instance.IsConnected())
            {
                ServerConnection.Instance.Connect(connectionString: IPTextBox.Text,
                    onConnected: () =>
                    {
                        ChatController.Instance.LogInfo("Connected to server.");
                        ChatController.Instance.LogInfo($"Welcome to the chat: {Player.Instance.PlayerName}!");
                        GameForm.Instance.OnConnectedToServer();
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
                        GameForm.Instance.OnDisconnectedToServer();
                        ConnectToServerButton.Text = Constants.ConnectToServer;
                    },
                    onException: (ex) =>
                    {
                        ChatController.Instance.LogError("Failed to disconnect from server.");
                    });

            }
        }

        private void DebugCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            MainMenuSettings.Instance.DebugMode = DebugCheckBox.Checked;
        }
    }
}
