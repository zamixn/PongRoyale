using System.Windows.Forms;

namespace PongRoyale_client.Chat
{
    public class ChatControllerProxy : IChat
    {
        RichTextBox output = null;
        TextBox input = null;
        private ChatController controller = null;
        public ChatControllerProxy(RichTextBox output, TextBox input)
        {
            this.output = output;
            this.input = input;
        }

        public bool IsInputSelected()
        {
            if(controller == null)
            {
                controller = new ChatController(output, input);
            }
            return controller.IsInputSelected();
        }

        public void LogChatMessage(byte playerId, string message)
        {
            if (controller == null)
            {
                controller = new ChatController(output, input);
            }
            controller.LogChatMessage(playerId, message);
        }

        public void LogError(string error)
        {
            if (controller == null)
            {
                controller = new ChatController(output, input);
            }
            controller.LogError(error);
        }

        public void LogInfo(string info)
        {
            if (controller == null)
            {
                controller = new ChatController(output, input);
            }
            controller.LogInfo(info);
        }

        public void OnChatInputSubmitted()
        {
            if (controller == null)
            {
                controller = new ChatController(output, input);
            }
            controller.OnChatInputSubmitted();
        }

        public bool ValidateChatError(string error)
        {
            if (controller == null)
            {
                controller = new ChatController(output, input);
            }
            return controller.ValidateChatError(error);
        }

        public bool ValidateChatInfo(string info)
        {
            if (controller == null)
            {
                controller = new ChatController(output, input);
            }
            return controller.ValidateChatInfo(info);
        }

        public bool ValidateChatInput(string input)
        {
            if (controller == null)
            {
                controller = new ChatController(output, this.input);
            }
            return controller.ValidateChatInput(input);
        }
        public void ClearInput()
        {
            if (controller == null)
            {
                controller = new ChatController(output, this.input);
            }
            controller.ClearInput();
        }
        public void ClearChat()
        {
            if (controller == null)
            {
                controller = new ChatController(output, this.input);
            }
            controller.ClearChat();
        }

        public void Undo()
        {
            if (controller == null)
            {
                controller = new ChatController(output, this.input);
            }
            controller.Undo();
        }

        public void OnChatInputChanged()
        {
            if (controller == null)
            {
                controller = new ChatController(output, this.input);
            }
            controller.OnChatInputChanged();
        }
    }
}
