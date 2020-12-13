using PongRoyale_client.ChatInterpreter.ExpressionVisitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.ChatInterpreter
{
    public class ChangeColorExpression : IChatExpression
    {
        public string Input { get; private set; }
        public string Interpret(string input, IExpressionVisitor visitor = null)
        {
            Input = input;
            string res = "";
            if (visitor != null)
                res = visitor.Visit(this);
            Input = "";
            return res;
        }
    }
}
