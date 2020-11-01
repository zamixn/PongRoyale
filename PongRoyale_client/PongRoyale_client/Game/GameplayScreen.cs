using PongRoyale_client.Extensions;
using PongRoyale_client.Game.Balls;
using PongRoyale_client.Game.Balls.Decorator;
using PongRoyale_client.Game.Balls.ReboundStrategy;
using PongRoyale_client.Game.Obstacles;
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
        private Color LocalBorderColor;
        private float LocalBorderWidth;

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

        private Font LifeFont;
        private Brush LifeBrush;
        private StringFormat LifeStringFormat;
        private float LifeRadiusOffset;

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
            LocalBorderColor = Color.Green;
            LocalBorderWidth = BorderWidth = 3;

            ArenaColor = Color.Gray;
            ArenaWidth = 1;
            ArenaMargin = 25;

            PlayerColor = Color.Black;
            PlayerWidth = 10;

            LifeFont = new Font(new FontFamily("Arial"), 16, FontStyle.Bold, GraphicsUnit.Pixel);
            LifeBrush = Brushes.Black;
            LifeStringFormat = new StringFormat()
            { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            LifeRadiusOffset = 14;


            SafeInvoke.Instance.DelayedInvoke(0.5f, () => TryInitGameParamaters());
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            if (ArenaFacade.Instance.IsInitted)
            {
                TryInitGameParamaters();
                Graphics g = pe.Graphics;
                DrawBorder(g);
                DrawArena(g);
                DrawArenaObjects(g);
                DrawPlayers(g);
                DrawBalls(g);

                // debug stuff
                if (GameManager.Instance.DebugMode)
                {
                    DrawPaddleNormals(g);
                    DrawBallCollisions(g);
                    DrawRect2DBounds(g);
                    DrawArenaObjectNormals(g);
                }
            }
        }

        private void DrawArenaObjects(Graphics g)
        {
            foreach (ArenaObject obj in ArenaFacade.Instance.ArenaObjects.Values)
            {
                Pen p = new Pen(Color.Magenta);
                Brush b = new SolidBrush(Color.Magenta);
                obj.Render(g, p, b);
                p.Dispose();
                b.Dispose();

                if (ArenaFacade.Instance.IsPaused)
                    break;
            }
        }
        private void DrawBalls(Graphics g)
        {
            foreach (IBall ball in ArenaFacade.Instance.ArenaBalls.Values)
            {
                Brush p = new SolidBrush(Color.Yellow);
                ball.Render(g, p);
                p.Dispose();

                if (ArenaFacade.Instance.IsPaused)
                    break;
            }
        }
        private void DrawPlayers(Graphics g)
        {
            foreach (Paddle paddle in ArenaFacade.Instance.PlayerPaddles.Values)
            {
                Pen p = new Pen(PlayerColor, PlayerWidth);
                paddle.Render(g, p, Origin, Diameter);
                p.Dispose();

                PointF lifePos = Utilities.GetPointOnCircle(Center, Radius + LifeRadiusOffset, paddle.GetCenterAngle());
                g.DrawString(paddle.Life.ToString(), LifeFont, LifeBrush, lifePos, LifeStringFormat);

                if (ArenaFacade.Instance.IsPaused)
                    break;
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
            Pen pp = new Pen(LocalBorderColor, LocalBorderWidth);
            p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            float angle = (float)(-Math.PI / 2);
            float angleDelta = (float)(Math.PI * 2 / RoomSettings.Instance.Players.Count);
            foreach (var player in RoomSettings.Instance.Players)
            {
                if (ServerConnection.Instance.IdMatches(player.Key))
                    g.DrawArc(pp, Origin.X, Origin.Y, Diameter, Diameter, SharedUtilities.RadToDeg(angle), SharedUtilities.RadToDeg(angleDelta));
                g.DrawLine(p, Center, Utilities.GetPointOnCircle(Center, Radius, angle));
                angle += angleDelta;

                if (ArenaFacade.Instance.IsPaused)
                    break;
            }

            g.DrawEllipse(p, Origin.X, Origin.Y, Diameter, Diameter);
            p.Dispose();
            pp.Dispose();
        }

        private void TryInitGameParamaters()
        {
            if (!AreStatsInitted)
            {
                Diameter = Math.Min(Width, Height) - ArenaMargin * 2;
                Radius = Diameter / 2f;
                Origin = new PointF(ArenaMargin, ArenaMargin);
                Center = new PointF(Origin.X + Radius, Origin.Y + Radius);
                ArenaFacade.Instance.UpdateDimensions(new Vector2(Width, Height), Center.ToVector2(), Radius);
                AreStatsInitted = true;
            }
        }

        #region debug stuff
        private void DrawPaddleNormals(Graphics g)
        {
            Pen p = new Pen(Color.Black);
            foreach (Paddle paddle in ArenaFacade.Instance.PlayerPaddles.Values)
            {
                float angle = paddle.GetCenterAngle();
                Vector2 paddleCenter = Utilities.GetPointOnCircle(Center.ToVector2(), Radius, angle);
                Vector2 paddleNormal = (Center.ToVector2() - paddleCenter).Normalize();
                g.DrawVector(p, paddleCenter, paddleNormal);

                if (ArenaFacade.Instance.IsPaused)
                    break;
            }
            p.Dispose();
        }

        private void DrawBallCollisions(Graphics g)
        {
            Pen p = new Pen(Color.Blue);
            foreach (Paddle paddle in ArenaFacade.Instance.PlayerPaddles.Values)
            {
                float angle = paddle.GetCenterAngle();
                Vector2 paddleCenter = Utilities.GetPointOnCircle(Center.ToVector2(), Radius, angle);
                Vector2 paddleNormal = (Center.ToVector2() - paddleCenter).Normalize();
                foreach (IBall b in ArenaFacade.Instance.ArenaBalls.Values)
                {
                    Vector2 ballDir = b.GetDirection();
                    Vector2 bounceDir = SharedUtilities.GetBounceDirection(paddleNormal, ballDir);
                    g.DrawVector(p, paddleCenter, bounceDir);

                    if (ArenaFacade.Instance.IsPaused)
                        break;
                }

                if (ArenaFacade.Instance.IsPaused)
                    break;
            }
            p.Dispose();
        }

        private void DrawRect2DBounds(Graphics g)
        {
            Pen p = new Pen(Color.Magenta);
            foreach (var obj in ArenaFacade.Instance.ArenaObjects.Values)
            {
                g.DrawRect2D(p, obj.GetBounds());

                if (ArenaFacade.Instance.IsPaused)
                    break;
            }
            foreach (IBall ball in ArenaFacade.Instance.ArenaBalls.Values)
            {
                g.DrawRect2D(p, ball.GetBounds());

                if (ArenaFacade.Instance.IsPaused)
                    break;
            }
            p.Dispose();
        }

        private void DrawArenaObjectNormals(Graphics g)
        {
            Pen p = new Pen(Color.Magenta);
            Brush b = new SolidBrush(Color.Magenta);
            foreach (var ball in ArenaFacade.Instance.ArenaBalls.Values)
            {
                var Direction = ball.GetDirection();
                var Position = ball.GetPosition();
                var offset = (Direction * ball.GetDiameter() * 0.5f);
                Vector2 impactPos = Position + offset;
                g.DrawPoint(b, impactPos);
                foreach (var obj in ArenaFacade.Instance.ArenaObjects.Values)
                {
                    var collisionNormal = obj.GetCollisionNormal(impactPos, Direction);
                    g.DrawVector(p, impactPos, collisionNormal);

                    if (ArenaFacade.Instance.IsPaused)
                        break;
                }

                if (ArenaFacade.Instance.IsPaused)
                    break;
            }
            b.Dispose();
            p.Dispose();
        }
        #endregion
    }
}
