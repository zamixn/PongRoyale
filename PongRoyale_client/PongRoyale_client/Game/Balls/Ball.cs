using PongRoyale_client.Extensions;
using PongRoyale_client.Game.Balls.Decorator;
using PongRoyale_client.Game.Balls.ReboundStrategy;
using PongRoyale_client.Game.Command;
using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.Balls
{
    public abstract class Ball : IBall, IClonable<Ball>
    {
        public BallType bType { get; protected set; }
        public IReboundStrategy reboundStrategy { get; protected set; }
        public Vector2 Position { get; protected set; }
        public Vector2 Direction { get; protected set; }
        public float Diameter { get; protected set; }
        public float Speed { get; protected set; }
        public byte Id { get; protected set; }

        public void OnCollisionWithPaddle(Paddle coll)
        {
            switch (bType)
            {
                case BallType.Deadly:
                    reboundStrategy = new BallDeadlyStrategy();
                    break;
                case BallType.Normal:
                    if (coll.AngularSpeed < 0)
                        reboundStrategy = new PaddleMovingLeft();
                    else if (coll.AngularSpeed == 0)
                        reboundStrategy = new PaddleNotMoving();
                    else if (coll.AngularSpeed > 0)
                        reboundStrategy = new PaddleMovingRight();
                    break;
                default:
                    reboundStrategy = null;
                    break;
            }
            Rebound(reboundStrategy);
        }

        public abstract Ball Clone();

        private void Rebound(IReboundStrategy reboundStrategy)
        {
            Direction = reboundStrategy.ReboundDirection(Direction);
        }

        public virtual void Render(Graphics g, Brush p)
        {
            float offset = Diameter / 2;
            g.FillEllipse(p, Position.X - offset, Position.Y - offset, Diameter, Diameter);
        }
        public static Ball CreateBall(BallType type, Vector2 position, float speed, Vector2 direction, float diameter)
        {
            Ball ball;
            switch (type)
            {
                case BallType.Normal:
                    ball = new NormalBall();
                    ball.bType = BallType.Normal;
                    break;
                case BallType.Deadly:
                    ball = new DeadlyBall();
                    ball.bType = BallType.Deadly;
                    break;
                default:
                    ball = null;
                    break;
            }

            ball.Position = position;
            ball.Speed = speed;
            ball.Direction = direction;
            ball.Diameter = diameter;

            return ball;
        }

        public override void LocalMove()
        {
            Move(Direction * Speed);
        }
        public void SetPosition(Vector2 pos)
        {
            Position = pos;
        }

        public void Move(Vector2 posOffset)
        {
            Position += posOffset;
        }

        public virtual void CheckCollision(Dictionary<byte, Paddle> paddles)
        {
            Vector2 center = GameManager.Instance.GameScreen.GetCenter().ToVector2();
            Vector2 directionFromCenter = (Position - center);
            float angle = Vector2.SignedAngleDeg(Vector2.Right, directionFromCenter);
            float distance = GameManager.Instance.GameScreen.GetDistanceFromCenter(Position) + Diameter / 2f;

            foreach (var p in paddles.Values)
            {
                float offsetDistance = distance + p.Thickness / 2;
                float pAngle1 = p.AngularPosition;
                float pAngle2 = (p.AngularPosition + p.AngularSize);
                if (offsetDistance > GameManager.Instance.GameScreen.GetArenaRadius())
                {
                    if (Utilities.IsInsideAngle(angle, pAngle1, pAngle2))
                    {
                        Direction = -Direction + Vector2.RandomInUnitCircle().Normalize() * 0.1f;
                    }
                }
            }

        }
    }
}
