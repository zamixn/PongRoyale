using PongRoyale_client.Extensions;
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

        private float _angPos;
        public float AngularPosition { 
            get { return _angPos; } 
            protected set 
            {
                float posChange = value - _angPos;
                CurrentAngularSpeed = posChange.Clamp(-MaxAngularSpeed, MaxAngularSpeed);
                _angPos = value;
            } }
        public float AngularSize { get; protected set; }
        public float MaxAngularSpeed { get; protected set; }
        public float CurrentAngularSpeed { get; protected set; }
        public float Thickness { get; private set; }
        public int Life { get; protected set; }

        public Paddle(PaddleSettings settings)
        {
            AngularSize = settings.Size;
            MaxAngularSpeed = settings.Speed;
            Thickness = settings.Thickness;
            CurrentAngularSpeed = 0;
        }

        public void AddClampAngles(float minAngle, float maxAngle)
        {
            MinAngle = minAngle;
            MaxAngle = maxAngle - AngularSize;
        }

        public virtual void Render(Graphics g, Pen p, PointF Origin, float arenaDiameter)
        {
            p.Width = Thickness;
            g.DrawArc(p, Origin.X, Origin.Y, arenaDiameter, arenaDiameter, AngularPosition, AngularSize);
        }
        public float GetCenterAngle()
        {
            return SharedUtilities.DegToRad(AngularPosition + AngularSize / 2f);
        }


        public virtual void SetPosition(float position)
        {
            AngularPosition = position;
        }

        public virtual void AddLife(int amount)
        {
            Life += amount;
        }

        public virtual void OnPosSync(float pos)
        {
            float posChange = pos - AngularPosition;
            AngularPosition = pos;
        }
        public virtual void Move(int direction)
        {
            float posChange = MaxAngularSpeed * direction;
            AngularPosition = SharedUtilities.Clamp(AngularPosition + posChange, MinAngle, MaxAngle);
        }

        public virtual void LocalUpdate()
        {
            if (InputManager.Instance.IsKeyDown(Keys.Left))
                Move(-1);
            else if (InputManager.Instance.IsKeyDown(Keys.Right))
                Move(1);
            else
                Move(0);
        }
    }
}
