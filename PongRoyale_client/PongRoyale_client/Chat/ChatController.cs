using PongRoyale_client.ChatInterpreter;
using PongRoyale_client.Extensions;
using PongRoyale_client.Singleton;
using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PongRoyale_client.Chat
{
    class ChatController : IChat
    {
        private RichTextBox Output;
        private TextBox Input;
        public void ClearInput() => Input.Clear();
        public void ClearChat() => Output.Clear();

        private Font BoldFont;
        private Font NormalFont;
        private Font ItalicFont;
        private Color NormalColor;
        private Color ErrorColor;
        private Color InfoColor;

        private IChatExpression TextInterpreter;
        public ChatController(RichTextBox output, TextBox input)
        {
            Output = output;
            Input = input;

            BoldFont = new Font(Output.Font, FontStyle.Bold);
            NormalFont = new Font(Output.Font, FontStyle.Regular);
            ItalicFont = new Font(Output.Font, FontStyle.Italic);

            NormalColor = Output.SelectionColor;
            ErrorColor = Color.Red;
            InfoColor = Color.FromArgb(50, 50, 50);


            FixFormatExpression textExpr = new FixFormatExpression(
                new AggregateExpression(new List<IChatExpression>() {
                    new ReplaceExpression(":smile:", "😃"),
                    new ReplaceExpression(":D", "😄"),
                    new ReplaceExpression(":grinning:", "😁"),
                    new ReplaceExpression(":anime:", "😆"),
                    new ReplaceExpression(":embarrased", "😅"),
                    new ReplaceExpression(":rofl:", "🤣"),
                    new ReplaceExpression(":crying:", "😂"),
                    new ReplaceExpression(":)", "🙂"),
                    new ReplaceExpression("(:", "🙃"),
                    new ReplaceExpression(":upsidesmile:", "🙃"),
                    new ReplaceExpression(":wink:", "😉"),
                    new ReplaceExpression(":blush:", "😊"),
                    new ReplaceExpression(":halo:", "😇"),
                })
            );
            CommandExpression cmdExpr = new CommandExpression();

            TextInterpreter = new ParserExpression(cmdExpr, textExpr);
        }
        public void OnChatInputSubmitted()
        {
            string playerName = ServerConnection.Instance.PlayerName;
            string input = Input.Text;

            if (ValidateChatInput(input))
            {
                ServerConnection.Instance.SendChatMessage(input);
                Input.Clear();
            }
        }

        public bool IsInputSelected()
        {
            return Input.Focused;
        }

        public void LogChatMessage(byte playerId, string message)
        {
            if (ValidateChatInput(message))
            {
                message = TextInterpreter.Interpret(message);
                if (!string.IsNullOrEmpty(message))
                {
                    string playerName = ServerConnection.ConstructName(playerId);
                    if (ServerConnection.Instance.IdMatches(playerId))
                    {
                        Output.AppendText("[me] ", InfoColor, NormalFont);
                    }
                    Output.AppendText($"{playerName}", NormalColor, BoldFont);
                    Output.AppendText($": {message.TrimEnd('\0')}\r\n", NormalColor, NormalFont);
                    Debug.WriteLine(message);
                }
            }
        }
        public bool ValidateChatInput(string input)
        {
            return !string.IsNullOrEmpty(input);
        }

        public void LogInfo(string info)
        {
            if (ValidateChatInfo(info))
            {
                Output.AppendText($"Info: {info}\r\n", InfoColor, ItalicFont);
            }
        }
        public bool ValidateChatInfo(string info)
        {
            return !string.IsNullOrEmpty(info);
        }


        public void LogError(string error)
        {
            if (ValidateChatError(error))
            {
                Output.AppendText($"Error: {error}\r\n", ErrorColor, ItalicFont);
            }
        }
        public bool ValidateChatError(string error)
        {
            return !string.IsNullOrEmpty(error);
        }
    }
}
