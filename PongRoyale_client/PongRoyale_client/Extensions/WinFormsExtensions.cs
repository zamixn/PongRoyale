using PongRoyale_client.Singleton;
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

        public static void DrawCircleAtCenter(this Graphics g, Pen p, Vector2 pos, float diameter)
        {
            g.DrawCircleAtCenter(p, pos.X, pos.Y, diameter);
        }

        public static void DrawCircleAtCenter(this Graphics g, Pen p, float x, float y, float diameter)
        {
            float radius = diameter / 2f;
            g.DrawEllipse(p, x - radius, y - radius, diameter, diameter);
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

        public static void DrawPoint(this Graphics g, Brush b, Vector2 point, float size = 5)
        {
            g.FillEllipse(b, point.X - size / 2f, point.Y - size / 2f, size, size);
        }

        public static void FillRectangleAtCenter(this Graphics g, Brush b, float x, float y, float width, float height)
        {
            g.FillRectangle(b, x - width / 2f, y - height / 2f, width, height);
        }

        public static void FillCricleAtCenter(this Graphics g, Brush b, Vector2 point, float diameter = 10)
        {
            g.FillCricleAtCenter(b, point.X - diameter / 2f, point.Y - diameter / 2f, diameter);
        }

        public static void FillCricleAtCenter(this Graphics g, Brush b, float x, float y, float diameter = 10)
        {
            g.FillEllipse(b, x - diameter / 2f, y - diameter / 2f, diameter, diameter);
        }

        public static void FillNgonAtCenter(this Graphics g, Brush b, int n, Vector2 pos, float diameter = 10, float phase = 0)
        {
            g.FillNgonAtCenter(b, n, pos.X, pos.Y, diameter, phase);
        }
        public static void FillNgonAtCenter(this Graphics g, Brush b, int n, float x, float y, float diameter = 10, float phase = 0)
        {
            float radius = diameter / 2f;
            PointF[] points = new PointF[n];
            float angle = SharedUtilities.DegToRad(phase);
            float deltaAngle = SharedUtilities.PI * 2f / n;
            for (int i = 0; i < n; i++)
            {
                points[i] = new PointF(
                    x + radius * SharedUtilities.CosF(angle), 
                    y + radius * SharedUtilities.SinF(angle));
                angle += deltaAngle;
            }
            g.FillPolygon(b, points);
        }

        public static void DrawNgonAtCenter(this Graphics g, Pen p, int n, Vector2 pos, float diameter = 10, float phase = 0)
        {
            g.DrawNgonAtCenter(p, n, pos.X, pos.Y, diameter, phase);
        }
        public static void DrawNgonAtCenter(this Graphics g, Pen p, int n, float x, float y, float diameter = 10, float phase = 0)
        {
            float radius = diameter / 2f;
            PointF[] points = new PointF[n];
            float angle = SharedUtilities.DegToRad(phase);
            float deltaAngle = SharedUtilities.PI * 2f / n;
            for (int i = 0; i < n; i++)
            {
                points[i] = new PointF(
                    x + radius * SharedUtilities.CosF(angle),
                    y + radius * SharedUtilities.SinF(angle));
                angle += deltaAngle;
            }
            g.DrawPolygon(p, points);
        }

        public static void DrawRect2D(this Graphics g, Pen p, Rect2D rect)
        {
            g.DrawRectangle(p, rect.Origin.X, rect.Origin.Y, rect.Size.X, rect.Size.Y);
        }
    }
}
