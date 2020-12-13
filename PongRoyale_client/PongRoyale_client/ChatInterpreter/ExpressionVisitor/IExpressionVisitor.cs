using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.ChatInterpreter.ExpressionVisitor
{
    public interface IExpressionVisitor
    {
        string Visit(ChangeColorExpression e);
    }
}
