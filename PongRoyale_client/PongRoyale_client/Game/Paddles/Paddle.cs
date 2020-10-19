using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PongRoyale_client.Game.GameManager;
using static PongRoyale_client.Game.GameSettings;

namespace PongRoyale_client.Game
{
    public abstract class Paddle
    {
        private float MinAngle, MaxAngle;

        public float AngularPosition { get; protected set; }
        public float AngularSize { get; protected set; }
        public float AngularSpeed { get; protected set; }
        public float Thickness { get; private set; }

        public Paddle(PaddleSettings settings)
        {
            AngularSize = settings.Size;
            AngularSpeed = settings.Speed;
            Thickness = settings.Thickness;
        }

        public void AddClampAngles(float minAngle, float maxAngle)
        {
            MinAngle = minAngle + AngularSize / 2;
            MaxAngle = maxAngle - AngularSize / 2;
        }

        public virtual void Render(Graphics g, Pen p, PointF Origin, float arenaDiameter)
        {
            p.Width = Thickness;
            g.DrawArc(p, Origin.X, Origin.Y, arenaDiameter, arenaDiameter, AngularPosition, AngularSize);
        }
        public virtual void SetPosition(float position)
        {
            AngularPosition = position;
        }

        public virtual void Move(int direction)
        {
            float posChange = AngularSpeed * direction;
            AngularPosition = SharedUtilities.Clamp(AngularPosition + posChange, MinAngle, MaxAngle);
        }

        public virtual void LocalUpdate()
        {
            if (InputManager.Instance.IsKeyDown(Keys.Left))
                Move(-1);
            else if (InputManager.Instance.IsKeyDown(Keys.Right))
                Move(1);
        }
    }
}
