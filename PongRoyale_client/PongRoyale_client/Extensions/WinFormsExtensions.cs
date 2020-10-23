using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PongRoyale_client.Extensions
{
    public static class WinFormsExtensions
    {
        public static void AppendText(this RichTextBox box, string text, Color color, Font font)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.SelectionFont = font;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }

        public static void DrawLine(this Graphics g, Pen p, Vector2 p1, Vector2 p2)
        {
            g.DrawLine(p, p1.X, p1.Y, p2.X, p2.Y);
        }
        public static void DrawVector(this Graphics g, Pen p, Vector2 point, Vector2 dir, float length = 20, float arrowSize = 5)
        {
            Vector2 p1 = point;
            Vector2 p2 = point + (dir * length);
            g.DrawLine(p, p1, p2);

            Vector2 perp = new Vector2(-dir.Y, dir.X) * arrowSize;
            Vector2 arrowOffset = dir * arrowSize;
            Vector2 p3 = p2 + perp - arrowOffset;
            Vector2 p4 = p2 - perp - arrowOffset;
            g.DrawLine(p, p2, p3);
            g.DrawLine(p, p2, p4);
        }
    }
}
