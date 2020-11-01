using PongRoyale_client.Extensions;
using PongRoyale_client.Game.ArenaObjects.Powerups;
using PongRoyale_client.Singleton;
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
using static PongRoyale_client.Game.ArenaFacade;
using static PongRoyale_client.Game.GameData;

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
        public byte Id { get; protected set; }

        public PoweredUpData PowerUppedData { get; protected set; }

        public Paddle(PaddleSettings settings, byte id)
        {
            Id = id;
            PowerUppedData = new PoweredUpData();
            AngularSize = settings.Size;
            MaxAngularSpeed = settings.Speed;
            Thickness = settings.Thickness;
            Life = settings.Life;
            CurrentAngularSpeed = 0;
        }

        public void AddClampAngles(float minAngle, float maxAngle)
        {
            MinAngle = minAngle;
            MaxAngle = maxAngle - AngularSize;
        }

        public virtual void Render(Graphics g, Pen p, PointF Origin, float arenaDiameter)
        {
            if (PowerUppedData.ChangePaddleSpeed)
            {
                Pen pp = new Pen(PowerUppedData.RndSpeed > 1 ? Color.Green : Color.Red, 2);
                float centerAngle = GetCenterAngle();
                Vector2 arenaCenter = ArenaFacade.Instance.ArenaDimensions.Center;
                float arenaRadius = arenaDiameter / 2f;
                Vector2 paddleCenter = Utilities.GetPointOnCircle(arenaCenter, arenaRadius, centerAngle);
                Vector2 paddleNormal = (arenaCenter - paddleCenter).Normalize();
                g.DrawVector(pp, paddleCenter, paddleNormal, 10, 10);
                g.DrawVector(pp, paddleCenter, paddleNormal, 15, 10);
                g.DrawVector(pp, paddleCenter, paddleNormal, 20, 10);
                pp.Dispose();
            }
            if (PowerUppedData.GivePlayerLife)
            {
                p.Color = Color.Green;
            }

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
            SetLife(SharedUtilities.Clamp(Life + amount, 0, byte.MaxValue));
        }
        public virtual void SetLife(int life)
        {
            Life = life;
        }

        public virtual bool IsAlive()
        {
            return Life > 0;
        }

        public virtual void OnPosSync(float pos)
        {
            float posChange = pos - AngularPosition;
            AngularPosition = pos;
        }
        public virtual void Move(int direction)
        {
            float multiplier = PowerUppedData.ChangePaddleSpeed ? 
                                (PowerUppedData.RndSpeed > 1 ? 2f : 0.5f) : 1;
            float posChange = MaxAngularSpeed * direction * multiplier;
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

        public virtual void TransferPowerUp(PoweredUpData data)
        {
            if (data.IsValid())
            {
                if (data.ChangePaddleSpeed)
                {
                    PowerUppedData.ChangePaddleSpeed = true;
                    SafeInvoke.Instance.DelayedInvoke(PowerUppedData.GetDurationOnPaddle(), () => PowerUppedData.ChangePaddleSpeed = false);
                }
                if (data.GivePlayerLife)
                {
                    PowerUppedData.GivePlayerLife = true;
                    Life++;
                    SafeInvoke.Instance.DelayedInvoke(PowerUppedData.GetDurationOnPaddle(), () => PowerUppedData.GivePlayerLife = false);
                }
            }
        }
    }
}
