using PongRoyale_client.Chat;
using PongRoyale_client.ChatInterpreter;
using PongRoyale_client.Extensions;
using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PongRoyale_client.Singleton
{
    public class ChatManager : Singleton<ChatManager>
    {
        public ChatControllerProxy Proxy { get; private set; }
        public void Setup(RichTextBox output, TextBox input)
        {
            Proxy = new ChatControllerProxy(output, input);
        }
    }
}
