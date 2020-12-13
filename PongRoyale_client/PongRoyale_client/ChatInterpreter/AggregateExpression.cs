using PongRoyale_client.ChatInterpreter.ExpressionVisitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.ChatInterpreter
{
    public class AggregateExpression : IChatExpression
    {
        private List<IChatExpression> Expressions;

        public AggregateExpression(List<IChatExpression> expressions)
        {
            Expressions = expressions;
        }

        public string Interpret(string input, IExpressionVisitor visitor = null)
        {
            foreach (var expr in Expressions)
            {
                input = expr.Interpret(input, visitor);
            }
            return input;
        }
    }
}
