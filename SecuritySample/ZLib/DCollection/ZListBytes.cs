using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZLib.DCollection
{
    /// <summary>
    /// 繼承 List&lt;byte[]&gt;, 方便常用. 每個item可存放不定長度的byte[].
    /// </summary>
    public class ZListBytes : List<byte[]>
    {
        object mLock = new object();

        public ZListBytes() : base()
        { }

        /// <summary>
        /// 取得本物件對應的Byte個數.
        /// </summary>
        /// <returns></returns>
        public int GetCountBytes()
        {
            int iCount = 0;
            lock (mLock)
            {
                foreach (byte[] ba1 in this)
                    iCount += ba1.Length;
            }
            return iCount;
        }

        /// <summary>
        /// 取得目前的內容轉為byte[].
        /// </summary>
        public byte[] GetBytes()
        {
            lock (mLock)
            {
                int iCountByte = GetCountBytes();
                if (iCountByte < 1)
                    return null;

                byte[] ba1 = new byte[iCountByte];
                int iSeq = 0;
                for (int i = 0; i < Count; i++)
                {
                    this[i].CopyTo(ba1, iSeq);
                    iSeq += this[i].Length;
                }
                return ba1;
            }
        }

    }
}
