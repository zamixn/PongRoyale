using PongRoyale_client.ChatInterpreter.ExpressionVisitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.ChatInterpreter
{
    public class ParserExpression : IChatExpression
    {
        private IChatExpression ColorExpression;
        private IChatExpression CommandExpression;
        private IChatExpression TextExpression;

        public ParserExpression(IChatExpression colorExpression, IChatExpression commandExpression, IChatExpression textExpression)
        {
            ColorExpression = colorExpression;
            CommandExpression = commandExpression;
            TextExpression = textExpression;
        }

        public string Interpret(string input, IExpressionVisitor visitor = null)
        {
            if (input.StartsWith("/"))
                return CommandExpression.Interpret(input, visitor);
            else if (input.StartsWith("!"))
                return ColorExpression.Interpret(input, visitor);
            else
                return TextExpression.Interpret(input, visitor);
        }
    }
}
