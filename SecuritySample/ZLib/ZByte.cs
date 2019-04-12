// ZBytes.
// 20190408, Honda, Copy from ZLib.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// add
using System.Data.Linq; // Binary

namespace ZLib
{
    public static class ZByte
    {
        public static Boolean ZEquals(this byte[] ba1, byte[] ba2)
        {
            return Equals(ba1, ba2);
        }

        public static Boolean Equals(Binary b1, Binary b2)
        {
            return b1.Equals(b2);
        }
        public static string ZGetStringHex(this byte[] ba, string sSeparator = "-")
        {
            if (sSeparator == "-")
                return BitConverter.ToString(ba);
            else
                return BitConverter.ToString(ba).Replace("-", sSeparator);

        }

        public static string ZGetStringUTF8(this byte[] baUTF8)
        {
            return Encoding.UTF8.GetString(baUTF8); 
        }
        public static byte[] GetBytesUTF8(string sUTF8)
        {
            return Encoding.UTF8.GetBytes(sUTF8);
        }
        public static int ZGetBytesCount(this byte[][] baInput)
        {
            int iCount = 0;
            foreach (byte[] ba1 in baInput)
                iCount += ba1.Length;
            return iCount;
        }

        public static byte[] ZJoin(this byte[][] baInput)
        {
            int iCount = ZGetBytesCount(baInput);
            byte[] baOutput = new byte[iCount];
            int iSeq = 0;
            for (int i = 0; i < baInput.Length; i++)
            {
                baInput[i].CopyTo(baOutput, iSeq);
                iSeq += baInput[i].Length;
            }
            return baOutput;
        }

    }
}
