using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.ChatInterpreter
{
    public class ReplaceExpression : IChatExpression
    {
        private string Pattern;
        private string Data;

        public ReplaceExpression(string pattern, string replace)
        {
            Pattern = pattern;
            Data = replace;
        }

        public string Interpret(string input)
        {
            return input.Replace(Pattern, Data);
        }
    }
}
