using PongRoyale_client.Game.ArenaObjects.Powerups;
using PongRoyale_client.Singleton;
using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.Balls.Decorator
{
    public class BallDecorator : IBall
    {
        protected readonly IBall Ball;

        public BallDecorator(IBall ball)
        {
            Ball = ball;
        }

        public void CheckCollisionWithArenaObjects(Dictionary<byte, ArenaObject> objects)
        {
            Ball.CheckCollisionWithArenaObjects(objects);
        }

        public void CheckCollisionWithPaddles(Dictionary<byte, Paddle> paddles)
        {
            Ball.CheckCollisionWithPaddles(paddles);
        }

        public bool CheckOutOfBounds(float startAngle, Dictionary<byte, Paddle> paddles, out byte paddleId)
        {
            return Ball.CheckOutOfBounds(startAngle, paddles, out paddleId);
        }

        public BallType GetBallType()
        {
            return Ball.GetBallType();
        }

        public float GetDiameter()
        {
            return Ball.GetDiameter();
        }

        public Vector2 GetDirection()
        {
            return Ball.GetDirection();
        }

        public Vector2 GetPosition()
        {
            return Ball.GetPosition();
        }

        public virtual void Update()
        {
            Ball.Update();
        }

        public virtual void Render(Graphics g, Brush p)
        {
            Ball.Render(g, p);
        }

        public void SetPosition(Vector2 pos)
        {
            Ball.SetPosition(pos);
        }

        public void SetDirection(Vector2 pos)
        {
            Ball.SetDirection(pos);
        }

        public Rect2D GetBounds()
        {
            return Ball.GetBounds();
        }

        public IBall ApplyPowerup(PoweredUpData data)
        {
            return Ball.ApplyPowerup(data);
        }

        public byte GetId()
        {
            return Ball.GetId();
        }

        public Color GetColor()
        {
            return Ball.GetColor();
        }

        public PoweredUpData GetPoweredUpData()
        {
            return Ball.GetPoweredUpData();
        }

        public IBall RemovePowerUpData(PoweredUpData data)
        {
            return Ball.RemovePowerUpData(data);
        }
    }
}
