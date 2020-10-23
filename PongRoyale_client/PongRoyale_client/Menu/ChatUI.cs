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
    public partial class ChatUI : UserControl
    {
        public ChatUI()
        {
            InitializeComponent();
            ChatController.Instance.Setup(Chat, ChatInput);
        }

        private void ChatUI_Load(object sender, EventArgs e)
        {

        }


        private void ChatInput_Submitted(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ServerConnection.Instance.IsConnected())
                    ChatController.Instance.OnChatInputSubmitted();
                else
                {
                    ChatController.Instance.LogError("Not connected to server!");
                    ChatController.Instance.ClearInput();
                }
            }
        }
    }
}
