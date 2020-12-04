using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.ChatInterpreter
{
    public class ParserExpression : IChatExpression
    {
        private IChatExpression CommandExpression;
        private IChatExpression TextExpression;

        public ParserExpression(IChatExpression commandExpression, IChatExpression textExpression)
        {
            CommandExpression = commandExpression;
            TextExpression = textExpression;
        }

        public string Interpret(string input)
        {
            if (input.StartsWith("/"))
                return CommandExpression.Interpret(input);
            else
                return TextExpression.Interpret(input);
        }
    }
}
