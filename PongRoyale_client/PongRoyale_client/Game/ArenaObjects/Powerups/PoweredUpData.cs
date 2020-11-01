using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.ArenaObjects.Powerups
{
    public class PoweredUpData
    {
        public const int BYTE_COUNT = 5;

        public bool MakeBallDeadly;
        public bool ChangeBallSpeed;
        public bool ChangeBallDirection;
        public bool GivePlayerLife;
        public bool ChangePaddleSpeed;

        public Vector2 RndDirection;
        public float RndSpeed;
        public PoweredUpData()
        {
            RndDirection = Vector2.RandomInUnitCircle();
            RndSpeed = RandomNumber.NextFloat(0.01f, 2f);
        }

        public float GetDurationOnBall()
        {
            return (MakeBallDeadly ? 5f : 0) +
                   (ChangeBallSpeed ? 5f : 0) +
                   (ChangeBallDirection ? 1f : 0) +
                   (GivePlayerLife ? 5f : 0) +
                   (ChangePaddleSpeed ? 5f : 0);
        }
        public float GetDurationOnPaddle()
        {
            return (GivePlayerLife ? 1f : 0) +
                   (ChangePaddleSpeed ? 5f : 0);
        }

        public bool[] GetAsArray()
        {
            return new bool[] { MakeBallDeadly, ChangeBallSpeed, ChangeBallDirection, GivePlayerLife, ChangePaddleSpeed };
        }
        public byte[] ToByteArray()
        {
            return GetAsArray().Select(b => b ? (byte)1 : (byte)0).ToArray();
        }

        public static PoweredUpData FromArray(bool[] array)
        {
            int index = 0;
            return new PoweredUpData()
            {
                MakeBallDeadly = array[index++],
                ChangeBallSpeed = array[index++],
                ChangeBallDirection = array[index++],
                GivePlayerLife = array[index++],
                ChangePaddleSpeed = array[index++],
            };
        }
        public static PoweredUpData FromByteArray(byte[] array)
        {
            return FromArray(array.Select(b => b == 1).ToArray());
        }

        public static PoweredUpData RollRandom()
        {
            bool[] array = new bool[BYTE_COUNT];
            array[RandomNumber.NextInt(0, BYTE_COUNT)] = true;
            return FromArray(array);
        }

        public override string ToString()
        {
            var d = ToByteArray();
            var i = 0;
            return $"(MakeBallDeadly: {d[i++]}; ChangeBallSpeed: {d[i++]}; ChangeBallDirection: {d[i++]}; GivePlayerLife: {d[i++]}; ChangePaddleSpeed: {d[i++]})";
        }

        public void Add(PoweredUpData data)
        {
            MakeBallDeadly = MakeBallDeadly || data.MakeBallDeadly;
            ChangeBallSpeed = ChangeBallSpeed || data.ChangeBallSpeed;
            ChangeBallDirection = ChangeBallDirection || data.ChangeBallDirection;
            GivePlayerLife = GivePlayerLife || data.GivePlayerLife;
            ChangePaddleSpeed = ChangePaddleSpeed || data.ChangePaddleSpeed;
        }

        public void Remove(PoweredUpData data)
        {
            MakeBallDeadly = MakeBallDeadly && !data.MakeBallDeadly;
            ChangeBallSpeed = ChangeBallSpeed && !data.ChangeBallSpeed;
            ChangeBallDirection = ChangeBallDirection && !data.ChangeBallDirection;
            GivePlayerLife = GivePlayerLife && !data.GivePlayerLife;
            ChangePaddleSpeed = ChangePaddleSpeed && !data.ChangePaddleSpeed;
        }

        public bool IsValid()
        {
            return MakeBallDeadly || ChangeBallSpeed || ChangeBallDirection || GivePlayerLife || ChangePaddleSpeed;
        }
    }
}
