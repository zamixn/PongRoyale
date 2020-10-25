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
using System.Windows.Forms;

namespace PongRoyale_client.Game.Balls
{
    public abstract class Ball : IBall, IClonable<Ball>
    {
        public BallType bType { get; protected set; }
        public Vector2 Position { get; protected set; }
        public Vector2 Direction { get; protected set; }
        public float Diameter { get; protected set; }
        public float Speed { get; protected set; }
        public byte Id { get; protected set; }

        public void OnCollisionWithPaddle(Paddle coll)
        {
            Vector2 center = GameplayManager.Instance.GameScreen.GetCenter().ToVector2();
            float radius = GameplayManager.Instance.GameScreen.GetArenaRadius();
            float angle = coll.GetCenterAngle();
            Vector2 paddleCenter = Utilities.GetPointOnCircle(center, radius, angle);
            Vector2 paddleNormal = (center - paddleCenter).Normalize();

            IReboundStrategy reboundStrategy;
            switch (bType)
            {
                case BallType.Deadly:
                    reboundStrategy = new BallDeadlyStrategy();
                    break;
                case BallType.Normal:
                    if (coll.CurrentAngularSpeed < 0)
                        reboundStrategy = new PaddleMovingLeft();
                    else if (coll.CurrentAngularSpeed > 0)
                        reboundStrategy = new PaddleMovingRight();
                    else //if (coll.AngularSpeed == 0)
                        reboundStrategy = new PaddleNotMoving();
                    break;
                default:
                    reboundStrategy = null;
                    break;
            }
            Rebound(reboundStrategy, paddleNormal, coll);
        }

        public abstract Ball Clone();

        private void Rebound(IReboundStrategy reboundStrategy, Vector2 surfaceNormal, Paddle p)
        {
            Direction = reboundStrategy.ReboundDirection(Direction, surfaceNormal, p);
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

        public virtual void CheckCollisionWithPaddles(Dictionary<byte, Paddle> paddles)
        {
            Vector2 center = GameplayManager.Instance.GameScreen.GetCenter().ToVector2();
            Vector2 directionFromCenter = (Position - center);
            float angle = Vector2.SignedAngleDeg(Vector2.Right, directionFromCenter);
            float distance = GameplayManager.Instance.GameScreen.GetDistanceFromCenter(Position) + Diameter / 2f;
            float arenaRadius = GameplayManager.Instance.GameScreen.GetArenaRadius();

            foreach (var kvp in paddles)
            {
                var p = kvp.Value;
                float offsetDistance = distance + p.Thickness / 2;
                float pAngle1 = p.AngularPosition;
                float pAngle2 = (p.AngularPosition + p.AngularSize);
                if (offsetDistance > arenaRadius)
                    if (Utilities.IsInsideAngle(angle, pAngle1, pAngle2))
                            OnCollisionWithPaddle(p);
            }
        }

        public virtual bool CheckOutOfBounds(float startAngle, Dictionary<byte, Paddle> paddles, out byte paddleId)
        {
            Vector2 center = GameplayManager.Instance.GameScreen.GetCenter().ToVector2();
            Vector2 directionFromCenter = (Position - center);
            float ballAngle = Vector2.SignedAngleDeg(Vector2.Right, directionFromCenter);
            float distance = GameplayManager.Instance.GameScreen.GetDistanceFromCenter(Position) + Diameter / 2f;
            float arenaRadius = GameplayManager.Instance.GameScreen.GetArenaRadius();

            paddleId = 0;
            int paddleCount = paddles.Count;
            float angleDelta = SharedUtilities.RadToDeg(SharedUtilities.PI * 2 / paddleCount);
            float angle = SharedUtilities.RadToDeg(startAngle);
            foreach (var kvp in paddles)
            {
                paddleId = kvp.Key;
                if (distance >= arenaRadius)
                {
                    float minAngle = angle;
                    float maxAngle = minAngle + angleDelta;
                    if (Utilities.IsInsideAngle(ballAngle, minAngle, maxAngle))
                        return HandleOutOfBounds(true);
                }
                angle += angleDelta;
            }
            return false; 
        }

        protected virtual bool HandleOutOfBounds(bool isOutOfBounds)
        {
            return isOutOfBounds;
        }
    }
}
