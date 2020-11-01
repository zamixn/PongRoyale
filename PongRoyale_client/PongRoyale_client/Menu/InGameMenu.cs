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

namespace PongRoyale_client.Menu
{
    public partial class InGameMenu : UserControl
    {
        public InGameMenu()
        {
            InitializeComponent();
            DebugCheckBox.Checked = GameManager.Instance.DebugMode; 
        }

        private void DebugCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            GameManager.Instance.SetDebugMode(DebugCheckBox.Checked);
        }

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            ServerConnection.Instance.Disconnect(
                onConnected: () =>
                {
                    GameManager.Instance.SetGameState(GameState.InMainMenu_NotConnected);
                    ChatController.Instance.LogInfo("Disconnected from server.");
                    GameManager.Instance.SetGameState(GameState.InMainMenu_NotConnected);
                },
                onException: (ex) =>
                {
                    ChatController.Instance.LogError("Failed to disconnect from server.");
                });
        }
    }
}
