using PongRoyale_client.Extensions;
using PongRoyale_client.Game.Balls;
using PongRoyale_client.Singleton;
using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PongRoyale_client.Game
{
    public partial class GameplayScreen : Control
    {
        private Color BorderColor;
        private float BorderWidth;

        private Color ArenaColor;
        private float ArenaWidth;
        private float ArenaMargin;

        private Color PlayerColor;
        private float PlayerWidth;

        private bool AreStatsInitted = false;
        private float Diameter;
        private float Radius;
        private PointF Origin;
        private PointF Center;

        public GameplayScreen()
        {
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.UserPaint |
                          ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.ResizeRedraw |
                          ControlStyles.ContainerControl |
                          ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.SupportsTransparentBackColor
                          , true);
            InitializeComponent();

            BorderColor = Color.Black;
            BorderWidth = 2.5f;

            ArenaColor = Color.Gray;
            ArenaWidth = 1;
            ArenaMargin = 20;

            PlayerColor = Color.Black;
            PlayerWidth = 10;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            if (GameplayManager.Instance.IsInitted)
            {
                TryInitGameParamaters();
                Graphics g = pe.Graphics;
                DrawBorder(g);
                DrawArena(g);
                DrawPlayers(g);
                DrawBalls(g);

                // debug stuff
                if (MainMenuSettings.Instance.DebugMode)
                {
                    DrawPaddleNormals(g);
                    DrawBallCollisions(g);
                }
            }
        }

        private void DrawBalls(Graphics g)
        {
            foreach (Ball ball in GameplayManager.Instance.ArenaBalls.Values)
            {
                Brush p = new SolidBrush(Color.Yellow);
                ball.Render(g, p);
                p.Dispose();
            }
        }
        private void DrawPlayers(Graphics g)
        {
            foreach (Paddle paddle in GameplayManager.Instance.PlayerPaddles.Values)
            {
                Pen p = new Pen(PlayerColor, PlayerWidth);
                paddle.Render(g, p, Origin, Diameter);
                p.Dispose();
            }
        }

        private void DrawBorder(Graphics g)
        {
            Pen p = new Pen(BorderColor, BorderWidth);
            g.DrawRectangle(p, BorderWidth, BorderWidth, Width - BorderWidth * 2, Height - BorderWidth * 2);
            p.Dispose();
        }

        private void DrawArena(Graphics g)
        {
            Pen p = new Pen(ArenaColor, ArenaWidth);
            p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            g.DrawEllipse(p, Origin.X, Origin.Y, Diameter, Diameter);

            float angle = (float)(-Math.PI  / 2);
            float angleDelta = (float)(Math.PI * 2 / RoomSettings.Instance.Players.Count);
            for (int i = 0; i < RoomSettings.Instance.Players.Count; i++)
            {
                g.DrawLine(p, Center, Utilities.GetPointOnCircle(Center, Radius, angle));
                angle += angleDelta;
            }
            p.Dispose();
        }

        public PointF GetCenter()
        {
            TryInitGameParamaters();
            return Center;
        }
        public float GetDistanceFromCenter(Vector2 point)
        {
            TryInitGameParamaters();
            return Vector2.Distance(Center.ToVector2(), point);
        }
        public float GetArenaRadius()
        {
            TryInitGameParamaters();
            return Radius;
        }

        private void TryInitGameParamaters()
        {
            if (!AreStatsInitted)
            {
                Diameter = Math.Min(Width, Height) - ArenaMargin * 2;
                Radius = Diameter / 2f;
                Origin = new PointF(ArenaMargin, ArenaMargin);
                Center = new PointF(Origin.X + Radius, Origin.Y + Radius);
                AreStatsInitted = true;
            }
        }

        #region debug stuff
        private void DrawPaddleNormals(Graphics g)
        {
            Pen p = new Pen(Color.Black);
            foreach (Paddle paddle in GameplayManager.Instance.PlayerPaddles.Values)
            {
                float angle = paddle.GetCenterAngle();
                Vector2 paddleCenter = Utilities.GetPointOnCircle(Center.ToVector2(), Radius, angle);
                Vector2 paddleNormal = (Center.ToVector2() - paddleCenter).Normalize();
                g.DrawVector(p, paddleCenter, paddleNormal);
            }
            p.Dispose();
        }

        private void DrawBallCollisions(Graphics g)
        {
            Pen p = new Pen(Color.Blue);
            foreach (Paddle paddle in GameplayManager.Instance.PlayerPaddles.Values)
            {
                float angle = paddle.GetCenterAngle();
                Vector2 paddleCenter = Utilities.GetPointOnCircle(Center.ToVector2(), Radius, angle);
                Vector2 paddleNormal = (Center.ToVector2() - paddleCenter).Normalize();
                foreach (Ball b in GameplayManager.Instance.ArenaBalls.Values)
                {
                    Vector2 ballDir = b.Direction;
                    Vector2 bounceDir = SharedUtilities.GetBounceDirection(paddleNormal, ballDir);
                    g.DrawVector(p, paddleCenter, bounceDir);
                }
            }
            p.Dispose();
        }
        #endregion
    }
}
