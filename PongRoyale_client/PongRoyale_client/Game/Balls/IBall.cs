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
    public interface IBall
    {
        void LocalMove();
        void Render(Graphics g, Brush p);

        void SetPosition(Vector2 pos);
        void SetDirection(Vector2 dir);
        BallType GetBallType();
        Vector2 GetDirection();
        Vector2 GetPosition();
        float GetDiameter();
        Rect2D GetBounds();

        void CheckCollisionWithPaddles(Dictionary<byte, Paddle> paddles);
        void CheckCollisionWithArenaObjects(Dictionary<byte, ArenaObject> objects);
        bool CheckOutOfBounds(float startAngle, Dictionary<byte, Paddle> paddles, out byte paddleId);

        IBall ApplyPowerup(PowerUppedData data);
        byte GetId();
        Color GetColor();
        PowerUppedData GetPoweredUpData();
        void RemovePowerUpData(PowerUppedData data);
    }
}
