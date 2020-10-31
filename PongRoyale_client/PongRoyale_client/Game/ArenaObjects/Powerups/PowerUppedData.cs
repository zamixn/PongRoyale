using PongRoyale_shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Game.ArenaObjects.Powerups
{
    public class PowerUppedData
    {
        public const int BYTE_COUNT = 5;

        public bool MakeBallDeadly;
        public bool ChangeBallSpeed;
        public bool ChangeBallDirection;
        public bool GivePlayerLife;
        public bool ChangePaddleSpeed;

        public bool[] GetAsArray()
        {
            return new bool[] { MakeBallDeadly, ChangeBallSpeed, ChangeBallDirection, GivePlayerLife, ChangePaddleSpeed };
        }
        public byte[] ToByteArray()
        {
            return GetAsArray().Select(b => b ? (byte)1 : (byte)0).ToArray();
        }

        public static PowerUppedData FromArray(bool[] array)
        {
            int index = 0;
            return new PowerUppedData()
            {
                MakeBallDeadly = array[index++],
                ChangeBallSpeed = array[index++],
                ChangeBallDirection = array[index++],
                GivePlayerLife = array[index++],
                ChangePaddleSpeed = array[index++],
            };
        }
        public static PowerUppedData FromByteArray(byte[] array)
        {
            return FromArray(array.Select(b => b == 1).ToArray());
        }

        public static PowerUppedData RollRandom()
        {
            bool[] array = new bool[BYTE_COUNT];
            array[RandomNumber.NextInt(0, BYTE_COUNT)] = true;
            return FromArray(array);
        }
    }
}
