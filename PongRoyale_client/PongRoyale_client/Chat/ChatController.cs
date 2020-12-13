using PongRoyale_client.Chat.Memento;
using PongRoyale_client.ChatInterpreter;
using PongRoyale_client.ChatInterpreter.ExpressionVisitor;
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

        private ChatOriginator Originator;
        private ChatCaretaker Caretaker;
        private bool dontSaveMemento;

        private RedExpressionVisitor RedVisitor;
        private BlueExpressionVisitor BlueVisitor;
        private GreenExpressionVisitor GreenVisitor;
        private YellowExpressionVisitor YellowVisitor;
        private PurpleExpressionVisitor PurpleVisitor;

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

            RedVisitor = new RedExpressionVisitor(output);
            BlueVisitor = new BlueExpressionVisitor(output);
            GreenVisitor = new GreenExpressionVisitor(output);
            YellowVisitor = new YellowExpressionVisitor(output);
            PurpleVisitor = new PurpleExpressionVisitor(output);

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
            ChangeColorExpression clrExpr = new ChangeColorExpression();

            TextInterpreter = new ParserExpression(clrExpr, cmdExpr, textExpr);

            Originator = new ChatOriginator();
            Caretaker = new ChatCaretaker();
        }
        public void OnChatInputSubmitted()
        {
            string playerName = ServerConnection.Instance.PlayerName;
            string input = Input.Text;

            if (ValidateChatInput(input))
            {
                ServerConnection.Instance.SendChatMessage(input);
                Input.Clear();
                Caretaker.ClearMementos();
            }
        }

        public void OnChatInputChanged()
        {
            if (!dontSaveMemento)
            {
                Debug.WriteLine(Input.Text);
                Originator.SetState(Input.Text);
                Caretaker.SaveMemento(Originator.CreateMemento());
            }
            dontSaveMemento = false;
        }

        public bool IsInputSelected()
        {
            return Input.Focused;
        }

        public void LogChatMessage(byte playerId, string message)
        {
            if (ValidateChatInput(message))
            {
                IExpressionVisitor visitor = GetVisitor(message);
                message = TextInterpreter.Interpret(message, visitor);
                Debug.WriteLine(message);
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

        public void Undo()
        {
            if (Caretaker.HasMementos())
            {
                ChatMemento memento = Caretaker.GetMemento();
                Originator.RestoreState(memento);
                dontSaveMemento = true;
                Input.Text = Originator.GetState();
            }
        }

        private IExpressionVisitor GetVisitor(string message)
        {
            IExpressionVisitor visitor = null;
            if (message.Contains("!red"))
                visitor = RedVisitor;
            if (message.Contains("!blue"))
                visitor = BlueVisitor;
            if (message.Contains("!green"))
                visitor = GreenVisitor;
            if (message.Contains("!yellow"))
                visitor = YellowVisitor;
            if (message.Contains("!purple"))
                visitor = PurpleVisitor;
            return visitor;
        }
    }
}
