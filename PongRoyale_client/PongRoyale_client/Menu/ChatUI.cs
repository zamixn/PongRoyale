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
using System.Diagnostics;

namespace PongRoyale_client.Menu
{
    public partial class ChatUI : UserControl
    {
        public ChatUI()
        {
            InitializeComponent();
            ChatManager.Instance.Setup(Chat, ChatInput);
        }

        private void ChatUI_Load(object sender, EventArgs e)
        {

        }


        private void ChatInput_Submitted(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ServerConnection.Instance.IsConnected())
                    ChatManager.Instance.Proxy.OnChatInputSubmitted();
                else
                {
                    ChatManager.Instance.Proxy.LogError("Not connected to server!");
                    ChatManager.Instance.Proxy.ClearInput();
                }
            }
        }

        private void ChatInput_TextChanged(object sender, EventArgs e)
        {
            ChatManager.Instance.Proxy.OnChatInputChanged();
        }
    }
}
