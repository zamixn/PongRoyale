using PongRoyale_client.Extensions;
using PongRoyale_client.Game.ArenaObjects.Powerups;
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
    public abstract class Ball : IBall
    {
        public byte Id { get; protected set; }
        public BallType bType { get; protected set; }
        public Vector2 Position { get; protected set; }
        public Vector2 Direction { get; protected set; }
        public float Diameter { get; protected set; }
        public float Speed { get; protected set; }
        public PowerUppedData PoweredUpData { get; protected set; }

        public Vector2 GetDirection() => Direction;
        public Vector2 GetPosition() => Position;
        public float GetDiameter() => Diameter;
        public PowerUppedData GetPoweredUpData() => PoweredUpData;

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
        public static Ball CreateBall(byte id, BallType type, Vector2 position, float speed, Vector2 direction, float diameter)
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
            ball.PoweredUpData = new PowerUppedData();

            return ball;
        }

        public virtual void LocalMove()
        {
            bool pressed = false;
            if (InputManager.Instance.IsKeyDown(Keys.Left))
            {
                Direction = pressed ? Direction + Vector2.Left : Vector2.Left;
                Direction = Direction.Normalize();
                pressed = true;
            }
            if (InputManager.Instance.IsKeyDown(Keys.Right))
            {
                Direction = pressed ? Direction + Vector2.Right : Vector2.Right;
                Direction = Direction.Normalize();
                pressed = true;
            }
            if (InputManager.Instance.IsKeyDown(Keys.Down))
            {
                Direction = pressed ? Direction + Vector2.Up : Vector2.Up;
                Direction = Direction.Normalize();
                pressed = true;
            }
            if (InputManager.Instance.IsKeyDown(Keys.Up))
            {
                Direction = pressed ? Direction + Vector2.Down : Vector2.Down;
                Direction = Direction.Normalize();
                pressed = true;
            }

            float actualSpeed = Speed;
            if (PoweredUpData.ChangeBallDirection)
                Direction = Vector2.Lerp(Direction, PoweredUpData.RndDirection, GameManager.Instance.DeltaTime * 5);
            if (PoweredUpData.ChangeBallSpeed)
                actualSpeed = Speed * 2f;
            if (PoweredUpData.ChangePaddleSpeed)
                actualSpeed = Speed * 1.1f;
            if (PoweredUpData.GivePlayerLife)
                actualSpeed = Speed * 1.1f;
            if (PoweredUpData.MakeBallDeadly)
                actualSpeed = Speed * 1.1f;


            Move(Direction * actualSpeed);
        }
        public void SetPosition(Vector2 pos)
        {
            Position = pos;
        }

        public BallType GetBallType()
        {
            return bType;
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

        public IBall ApplyPowerup(PowerUppedData data)
        {
            IBall result = this;
            PoweredUpData.Add(data);
            if (data.ChangeBallDirection)
                result = new BallDirectionDecorator(this);
            if (data.ChangeBallSpeed)
                result = new BallSpeedDecorator(this);
            if (data.ChangePaddleSpeed)
                result = new PaddleSpeedDecorator(this);
            if (data.GivePlayerLife)
                result = new PlayerLifeDecorator(this);
            if (data.MakeBallDeadly)
                result = new DeadlyBallDecorator(this);
            return result;
        }
        public void RemovePowerUpData(PowerUppedData data)
        {
            PoweredUpData.Remove(data);
        }

        public byte GetId()
        {
            return Id;
        }

        public virtual Color GetColor()
        {
            return Color.Magenta;
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
                }
            }
            else if (obj != null)// collision with an arena object
            {
                reboundStrategy = obj.GetReboundStrategy();
                var offset = (Direction * Diameter * 0.5f);
                Vector2 impactPos = Position + offset;
                collisionNormal = obj.GetCollisionNormal(impactPos, Direction);
            }
            Rebound(reboundStrategy, collisionNormal, coll, obj);
        }

        private void Rebound(IReboundStrategy reboundStrategy, Vector2 surfaceNormal, Paddle p, ArenaObject obj)
        {
            var dir = reboundStrategy.ReboundDirection(this, surfaceNormal, p, obj);
            //var pos = reboundStrategy.ReboundPosition(Position, Direction, surfaceNormal, p, obj);
            Direction = dir;
            //SetPosition(pos);
        }
        #endregion
    }
}
