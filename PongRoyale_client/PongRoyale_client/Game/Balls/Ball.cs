using PongRoyale_client.Game.Balls.ReboundStrategy;
using System;
using System.Collections.Generic;
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
        public float PositionX { get; protected set; }
        public float PositionY { get; protected set; }
        public float BallSpeedX { get; protected set; }
        public float BallSpeedY { get; protected set; }
        public int ID { get; protected set; }

        public void OnCollisionWithPaddle(Paddle coll)
        {
            switch (bType)
            {
                case BallType.Deadly:
                    reboundStrategy = new BallDeadlyStrategy();
                    break;
                case BallType.Normal:
                    if (coll.Speed < 0)
                        reboundStrategy = new PaddleMovingLeft();
                    else if (coll.Speed == 0)
                        reboundStrategy = new PaddleNotMoving();
                    else if (coll.Speed > 0)
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
            float[] speeds = reboundStrategy.ReboundDirection(BallSpeedX, BallSpeedY);
            BallSpeedX = speeds[0];
            BallSpeedY = speeds[1];
        }

        public virtual void Render(Graphics g, Brush p, PointF Origin, float Diameter)
        {
            g.FillEllipse(p, Origin.X, Origin.Y, Diameter, Diameter);
        }
        public virtual void SetPosition(float positionX, float positionY)
        {
            PositionX = positionX;
            PositionY = positionY;
        }
        public static Ball CreateBall()
        {
            double spawnRadius = 100;
            Ball ball;
            BallType bType =(BallType)RandomNumber.RandomNumb((int)BallType.Normal, (int)BallType.Deadly + 1);

            switch (bType)
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

            double a = RandomNumber.RandomNumb() * 2 * Math.PI;
            double r = spawnRadius * Math.Sqrt(RandomNumber.RandomNumb());
            // 225.5 approx middle of screen
            double x = r * Math.Cos(a) + 225.5;
            double y = r * Math.Sin(a) + 225.5;

            ball.SetPosition((float)x, (float)y);

            return ball;
        }
    }
}
