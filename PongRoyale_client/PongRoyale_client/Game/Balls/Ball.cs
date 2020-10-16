using PongRoyale_client.Extensions;
using PongRoyale_client.Game.Balls.ReboundStrategy;
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
    public abstract class Ball
    {
        public BallType bType;
        public IReboundStrategy reboundStrategy { get; set; }
        public Vector2 Position { get; protected set; }
        public Vector2 Direction { get; private set; }
        public float Diameter { get; private set; }
        public float Speed { get; private set; }
        public int ID { get; protected set; }

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

        public virtual void LocalUpdate()
        {
            Position += Direction * Speed;
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
                // skip collisions with other paddles for now. This will have to be refactored later
                break; 
            }

        }
    }
}
