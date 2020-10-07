using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRoyale_client.Extensions
{
    public static class MiscExtensions
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
