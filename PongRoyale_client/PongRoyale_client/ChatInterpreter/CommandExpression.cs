using PongRoyale_client.ChatInterpreter.ExpressionVisitor;
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

        public string Interpret(string input, IExpressionVisitor visitor = null)
        {

            if (input.Contains("stop"))
            {
                GameManager.Instance.SetGameState(ServerConnection.Instance.IsConnected() ? GameState.InMainMenu_Connected : GameState.InMainMenu_NotConnected);
            }
            else
                ChatManager.Instance.Proxy.LogInfo("Invalid command");
            return "";
        }
    }
}
