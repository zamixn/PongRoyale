using System;
using System.Collections.Generic;
using System.Text;

namespace PongRoyale_server
{
    public static class Extensions
    {
        public static byte[] PrependBytes(this byte[] bArray, byte[] newBytes)
        {
            byte[] newArray = new byte[bArray.Length + newBytes.Length];
            bArray.CopyTo(newArray, newBytes.Length);
            for (int i = 0; i < newBytes.Length; i++)
            {
                newArray[i] = newBytes[i];
            }
            return newArray;
        }
    }
}
