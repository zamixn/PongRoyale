using PongRoyale_client.Extensions;
using PongRoyale_client.Game.Balls;
using PongRoyale_client.Game.Balls.Decorator;
using PongRoyale_client.Game.Balls.ReboundStrategy;
using PongRoyale_client.Game.Obstacles;
using PongRoyale_client.Game.RenderChain;
using PongRoyale_client.Game.RenderChain.Debug;
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

        private RenderableChainLink RenderChain;

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

            RenderableChainLink borderRenderLink = new BorderRenderLink(BorderColor, BorderWidth);
            RenderableChainLink arenaRenderLink = new DrawArenaRenderLink(ArenaColor, ArenaWidth, LocalBorderColor, LocalBorderWidth);
            RenderableChainLink arenaObjectsLink = new DrawArenaObjectRenderLink();
            RenderableChainLink drawPlayersLink = new DrawPlayersRenderLink(PlayerColor, PlayerWidth, LifeRadiusOffset, LifeFont, LifeBrush, LifeStringFormat);
            RenderableChainLink drawBallsLink = new DrawBallsRenderLink();
            RenderableChainLink debugPaddles = new DebugPaddleNormalsRenderLink();
            RenderableChainLink debugBalls = new DebugBallCollisionsRenderLink();
            RenderableChainLink debugRects = new DebugRect2DsRenderLink();
            RenderableChainLink debugObjects = new DebugObjNormalsRenderLink();

            borderRenderLink.SetNext(arenaRenderLink);
            arenaRenderLink.SetNext(arenaObjectsLink);
            arenaObjectsLink.SetNext(drawPlayersLink);
            drawPlayersLink.SetNext(drawBallsLink);
            drawBallsLink.SetNext(debugPaddles);
            debugPaddles.SetNext(debugBalls);
            debugBalls.SetNext(debugRects);
            debugRects.SetNext(debugObjects);
            RenderChain = borderRenderLink;

            SafeInvoke.Instance.DelayedInvoke(0.5f, () => TryInitGameParamaters());
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            if (ArenaFacade.Instance.IsInitted)
            {
                TryInitGameParamaters();
                Graphics g = pe.Graphics;
                RenderChain.Render(g);
            }
        }

        private void TryInitGameParamaters()
        {
            if (!AreStatsInitted)
            {
                Diameter = Math.Min(Width, Height) - ArenaMargin * 2;
                Radius = Diameter / 2f;
                Origin = new PointF(ArenaMargin, ArenaMargin);
                Center = new PointF(Origin.X + Radius, Origin.Y + Radius);
                ArenaFacade.Instance.UpdateDimensions(new Vector2(Width, Height), Center.ToVector2(), Origin.ToVector2(), Radius);
                AreStatsInitted = true;
            }
        }
    }
}
