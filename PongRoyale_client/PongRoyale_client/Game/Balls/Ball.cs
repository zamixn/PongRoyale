using PongRoyale_client.Extensions;
using PongRoyale_client.Game.Balls.Decorator;
using PongRoyale_client.Game.Balls.ReboundStrategy;
using PongRoyale_client.Game.Command;
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

namespace PongRoyale_client.Game.Balls
{
    public abstract class Ball : IBall, IClonable<Ball>
    {
        public BallType bType { get; protected set; }
        public Vector2 Position { get; protected set; }
        public Vector2 Direction { get; protected set; }
        public float Diameter { get; protected set; }
        public float Speed { get; protected set; }

        public abstract Ball Clone();

        public virtual void Render(Graphics g, Brush p)
        {
            try
            {
                float offset = Diameter / 2;
                g.FillEllipse(p, Position.X - offset, Position.Y - offset, Diameter, Diameter);
            }
            catch
            {
                Debug.WriteLine("Error while drawing ball");
            }
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

        public void SetDirection(Vector2 dir)
        {
            Direction = dir;
        }

        public void Move(Vector2 posOffset)
        {
            Position += posOffset;
        }

        public virtual void CheckCollisionWithPaddles(Dictionary<byte, Paddle> paddles)
        {
            Vector2 center = ArenaFacade.Instance.ArenaDimensions.Center;
            Vector2 directionFromCenter = (Position - center);
            float angle = Vector2.SignedAngleDeg(Vector2.Right, directionFromCenter);
            float distance = ArenaFacade.Instance.ArenaDimensions.GetDistanceFromCenter(Position) + Diameter / 2f;
            float arenaRadius = ArenaFacade.Instance.ArenaDimensions.Radius;

            foreach (var kvp in paddles)
            {
                var p = kvp.Value;
                float offsetDistance = distance + p.Thickness / 2;
                float pAngle1 = p.AngularPosition;
                float pAngle2 = (p.AngularPosition + p.AngularSize);
                if (offsetDistance > arenaRadius)
                    if (Utilities.IsInsideAngle(angle, pAngle1, pAngle2))
                        OnCollision(p, null);
            }
        }

        public virtual void CheckCollisionWithArenaObjects(Dictionary<byte, ArenaObject> objects)
        {

            foreach (var obj in objects.Values)
            {
                if (obj.Intersects(GetBounds()))
                    OnCollision(null, obj);

                if (ArenaFacade.Instance.IsPaused)
                    break;
            }
        }

        public Rect2D GetBounds()
        {
            float offset = Diameter / 2f;
            return new Rect2D(Position.X - offset, Position.Y - offset, Diameter, Diameter);
        }

        public virtual bool CheckOutOfBounds(float startAngle, Dictionary<byte, Paddle> paddles, out byte paddleId)
        {
            Vector2 center = ArenaFacade.Instance.ArenaDimensions.Center;
            Vector2 directionFromCenter = (Position - center);
            float ballAngle = Vector2.SignedAngleDeg(Vector2.Right, directionFromCenter);
            float distance = ArenaFacade.Instance.ArenaDimensions.GetDistanceFromCenter(Position) + Diameter / 2f;
            float arenaRadius = ArenaFacade.Instance.ArenaDimensions.Radius;

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



        #region collisions
        public void OnCollision(Paddle coll, ArenaObject obj)
        {
            Vector2 center = ArenaFacade.Instance.ArenaDimensions.Center;
            float radius = ArenaFacade.Instance.ArenaDimensions.Radius;
            Vector2 collisionNormal = null;

            IReboundStrategy reboundStrategy = null;
            if (coll != null)// collision with a paddle
            {
                float paddleAngle = coll.GetCenterAngle();
                Vector2 paddleCenter = Utilities.GetPointOnCircle(center, radius, paddleAngle);
                collisionNormal = (center - paddleCenter).Normalize();

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
            }
            else if (obj != null)// collision with an arena object
            {
                float offset = Diameter / 2f;
                Vector2 impactPos = new Vector2(Position.X + offset, Position.Y + offset);
                collisionNormal = obj.GetCollisionNormal(impactPos, Direction);
                reboundStrategy = new ObstacleStrategy();
            }
            Rebound(reboundStrategy, collisionNormal, coll, obj);
        }

        private void Rebound(IReboundStrategy reboundStrategy, Vector2 surfaceNormal, Paddle p, ArenaObject obj)
        {
            var dir = reboundStrategy.ReboundDirection(Direction, surfaceNormal, p, obj);
            var pos = reboundStrategy.ReboundPosition(Position, Direction, surfaceNormal, p, obj);
            Direction = dir;
            SetPosition(pos);
        }
        #endregion
    }
}
