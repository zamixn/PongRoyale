using PongRoyale_client.ChatInterpreter.ExpressionVisitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.ChatInterpreter
{
    public class FixFormatExpression : IChatExpression
    {
        private IChatExpression Expression;

        private readonly char[] Punctuations = new char[] { '.', '?', '!' };

        public string Input { get; private set; }

        public FixFormatExpression(IChatExpression expression)
        {
            Expression = expression;
        }

        public string Interpret(string input, IExpressionVisitor visitor = null)
        {
            if (input.Length > 0)
            {
                input = new string(input.ToCharArray().TakeWhile(c => c != '\0').ToArray());
                input = char.ToUpper(input[0]) + input.Substring(1, input.Length - 1);
            }

            if (!Punctuations.Any(c => input.EndsWith(c.ToString())))
            {
                input += ".";
            }

            return Expression.Interpret(input, visitor);
        }
    }
}
