using PongRoyale_client.Extensions;
using PongRoyale_client.Game.ArenaObjects;
using PongRoyale_client.Game.ArenaObjects.Powerups;
using PongRoyale_client.Game.Paddles;
using PongRoyale_client.Game.Ranking;
using PongRoyale_client.Singleton;
using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PongRoyale_client.Game.ArenaFacade;
using static PongRoyale_client.Game.GameData;

namespace PongRoyale_client.Game
{
    public abstract class Paddle : UpdateLeaf
    {
        public float MinAngle { private set; get; }
        public float MaxAngle { private set; get; }

        private float _angPos;
        public float AngularPosition {
            get { return _angPos; }
            protected set
            {
                float posChange = value - _angPos;
                CurrentAngularSpeed = posChange.Clamp(-MaxAngularSpeed, MaxAngularSpeed);
                _angPos = value;
            } }
        public float AngularSize => Settings.Size;
        public float MaxAngularSpeed => Settings.Speed;
        public float CurrentAngularSpeed { get; protected set; }
        public float Thickness => Settings.Thickness;
        public int Life { get; protected set; }
        public byte Id { get; protected set; }
        public IPaddleColor PaddleColor => Settings.PaddleColor;
        public PaddleType PType => Settings.PType;
        public PaddleSettings Settings { get; protected set; }

        public PoweredUpData PowerUppedData { get; protected set; }
        public IPaddleState state;
        private List<CancellationTokenSource> tokens;

        public Paddle(PaddleSettings settings, byte id)
        {
            Id = id;
            PowerUppedData = new PoweredUpData();
            Life = settings.Life;
            CurrentAngularSpeed = 0;
            Settings = settings;
            state = new NoPowerState(this);
            tokens = new List<CancellationTokenSource>();
        }
        public void ChangeState(IPaddleState state)
        {
            this.state = state;
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

        public override void Update()
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
            state.TransferPowerUp(data);
        }
        public void ApplySpeedPowerup()
        {
            PowerUppedData.ChangePaddleSpeed = true;
            tokens.Add(SafeInvoke.Instance.DelayedCancellableToken(PowerUppedData.GetDurationOnPaddle(), () => ClearPowerups()));
            
        }
        public void ApplyLifePowerup()
        {
            PowerUppedData.GivePlayerLife = true;
            Life++;
            tokens.Add(SafeInvoke.Instance.DelayedCancellableToken(PowerUppedData.GetDurationOnPaddle(), () => ClearPowerups()));
        }
        public void ApplyUndoPowerup()
        {
            PowerUppedData.UndoPlayerMove = true;
            InputManager.Instance.UndoLastInput();
            tokens.Add(SafeInvoke.Instance.DelayedCancellableToken(PowerUppedData.GetDurationOnPaddle(), () => ClearPowerups()));
        }
        public void CancellPowerupDisposal()
        {
            foreach (CancellationTokenSource t in tokens)
            {
                if (t != null)
                {
                    t.Cancel();
                    t.Dispose();
                }
            }
            tokens.Clear();
        }
        public void ClearPowerups()
        {
            PowerUppedData.ChangePaddleSpeed = false;
            PowerUppedData.GivePlayerLife = false;
            PowerUppedData.UndoPlayerMove = false;
            ChangeState(new NoPowerState(this));
        }
        public void ExtendPowerups()
        {
            tokens.Add(SafeInvoke.Instance.DelayedCancellableToken(PowerUppedData.GetDurationOnPaddle(), () => ClearPowerups()));
        }
    }
}
