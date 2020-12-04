using PongRoyale_client.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.ChatInterpreter
{
    public class CommandExpression : IChatExpression
    {
        public CommandExpression()
        {
        }

        public string Interpret(string input)
        {
            if (input.Contains("stop"))
            {
                GameManager.Instance.SetGameState(ServerConnection.Instance.IsConnected() ? GameState.InMainMenu_Connected : GameState.InMainMenu_NotConnected);
            }
            else
                ChatController.Instance.LogInfo("Invalid command");
            return "";
        }
    }
}
