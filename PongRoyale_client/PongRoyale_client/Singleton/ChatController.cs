﻿using PongRoyale_client.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PongRoyale_client.Singleton
{
    public class ChatController : Singleton<ChatController>
    {
        private RichTextBox Output;
        private TextBox Input;

        private Font BoldFont;
        private Font NormalFont;
        private Font ItalicFont;
        private Color NormalColor;
        private Color ErrorColor;
        private Color InfoColor;
        public void Setup(RichTextBox output, TextBox input)
        {
            Output = output;
            Input = input;

            BoldFont = new Font(Output.Font, FontStyle.Bold);
            NormalFont = new Font(Output.Font, FontStyle.Regular);
            ItalicFont = new Font(Output.Font, FontStyle.Italic);

            NormalColor = Output.SelectionColor;
            ErrorColor = Color.Red;
            InfoColor = Color.FromArgb(50, 50, 50);
        }

        public void OnChatInputSubmitted()
        {
            string playerName = Player.Instance.PlayerName;
            string input = Input.Text;

            if (ValidateChatInput(input))
            {
                Player.Instance.SendChatMessage(input);
                Input.Clear();
            }
        }

        public void LogChatMessage(byte playerId, string message)
        {
            if (ValidateChatInput(message))
            {
                string playerName = Player.ConstructName(playerId);
                if (Player.Instance.IdMatches(playerId))
                {
                    Output.AppendText("[me] ", InfoColor, NormalFont);
                }
                Output.AppendText($"{playerName}", NormalColor, BoldFont);
                Output.AppendText($": {message}\r\n", NormalColor, NormalFont);
            }
        }
        private bool ValidateChatInput(string input)
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
        private bool ValidateChatInfo(string info)
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
        private bool ValidateChatError(string error)
        {
            return !string.IsNullOrEmpty(error);
        }
    }
}
