using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.ChatInterpreter
{
    public interface IChatExpression
    {
        string Interpret(string input);
    }
}
