using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// add
using System.IO;
using ZLib.DCollection;

namespace ZLib
{
    public class ZFile
    {
        public static byte[] ReadAllBytes(Stream stream1)
        {
            return ReadAllBytes(stream1, 1024 * 1024).ToArray().ZJoin();
        }
        public static List<byte[]> ReadAllBytes(Stream stream1, int iChunkSize)
        {
            List<byte[]> list1 = new List<byte[]>();
            using (BinaryReader reader1 = new BinaryReader(stream1))
            {
                byte[] baChunk;
                do
                {
                    baChunk = reader1.ReadBytes(iChunkSize);
                    if (baChunk.Length > 0)
                        list1.Add(baChunk);
                }
                while (baChunk.Length > 0);
            }
            return list1;
        }

    }
}
