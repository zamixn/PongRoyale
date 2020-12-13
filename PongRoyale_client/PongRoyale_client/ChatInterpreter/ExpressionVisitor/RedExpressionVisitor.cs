using PongRoyale_client.Extensions;
using PongRoyale_client.Singleton;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PongRoyale_client.ChatInterpreter.ExpressionVisitor
{
    public class RedExpressionVisitor : IExpressionVisitor
    {
        private RichTextBox RichTextBox;
        public RedExpressionVisitor(RichTextBox rtb)
        {
            RichTextBox = rtb;
        }

        public string Visit(ChangeColorExpression e)
        {
            string input = e.Input;
            RichTextBox.AppendText($"{e.Input.Substring(4, e.Input.Length - 4).TrimEnd('\0')}\r\n", Color.Red);
            return "";
        }
    }
}
