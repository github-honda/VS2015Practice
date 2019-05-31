using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// add
using System.IO;

namespace ZLib
{
    public class ZFile
    {
        public const int ciBufferSize = 4096;
        public static byte[] ReadAllBytes(Stream stream1)
        {
            return ReadAllBytes(stream1, ciBufferSize).ToArray().ZJoin();
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
        public static void WriteBytes(Stream stream1, byte[] ba1)
        {
            stream1.Write(ba1, 0, ba1.Length);
        }



    }
}
