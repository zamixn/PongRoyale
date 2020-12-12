using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Chat
{
    public interface IChat
    {
        void ClearInput();
        void ClearChat();
        void OnChatInputSubmitted();
        void OnChatInputChanged();
        bool IsInputSelected();
        void LogChatMessage(byte playerId, string message);
        bool ValidateChatInput(string input);
        void LogInfo(string info);
        bool ValidateChatInfo(string info);
        void LogError(string error);
        bool ValidateChatError(string error);
        void Undo();
    }
}
