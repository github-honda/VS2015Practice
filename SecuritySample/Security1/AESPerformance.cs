using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// add
using System.Security.Cryptography;
using System.Diagnostics;
using ZLib;
using ZLib.DSecurity;


namespace Security1
{
    class AESPerformance
    {
        public Boolean Run()
        {
            Console.WriteLine($"AES performance test:");
            Console.WriteLine($"1. 加密.");

            string sPlainText;
            byte[] baPlainText;
            byte[] baEncrypt;
            byte[] baDecrypt;
            string sKey = "12345678901234567890123456789012";
            //string sKey = "1234567890123456";
            string sIV = "1234567890123456";
            byte[] baKey = ZByte.GetBytesUTF8(sKey);
            byte[] baIV = ZByte.GetBytesUTF8(sIV);

            int iTestCount = 3002;
            Stopwatch swEncrypt = new Stopwatch();
            Stopwatch swDecrypt = new Stopwatch();
            for (int i = 0; i < iTestCount; i+=100)
            {
                swEncrypt.Start();
                sPlainText = new string('H', i + 1);
                baPlainText = ZByte.GetBytesUTF8(sPlainText);

                baEncrypt = ZSecurity.EncryptAES(baPlainText, baKey, baIV);
                if (baEncrypt == null)
                {
                    Console.WriteLine("Encrypt " + ZRSA.msError);
                    break;
                }
                swEncrypt.Stop();

                swDecrypt.Start();
                baDecrypt = ZSecurity.DecryptAES(baEncrypt, baKey, baIV);
                if (baDecrypt == null)
                {
                    Console.WriteLine("Decrypt " + ZRSA.msError);
                    break;
                }
                swDecrypt.Stop();

                if (baEncrypt.ZEquals(baDecrypt))
                {
                    Console.WriteLine("驗證錯誤");
                    return false;
                }

                Console.WriteLine("{0}, Encrypt={1}, Decrypt={2}, EncryptLen={3}, DecryptLen={4}.",
                    i+1,
                    swEncrypt.ElapsedMilliseconds,
                    swDecrypt.ElapsedMilliseconds,
                    baEncrypt.Length,
                    baDecrypt.Length);

                /*
                 
1, Encrypt=3, Decrypt=10, EncryptLen=16, DecryptLen=1.
101, Encrypt=5, Decrypt=15, EncryptLen=112, DecryptLen=101.
201, Encrypt=5, Decrypt=19, EncryptLen=208, DecryptLen=201.
301, Encrypt=6, Decrypt=22, EncryptLen=304, DecryptLen=301.
401, Encrypt=6, Decrypt=25, EncryptLen=416, DecryptLen=401.
501, Encrypt=7, Decrypt=29, EncryptLen=512, DecryptLen=501.
601, Encrypt=7, Decrypt=32, EncryptLen=608, DecryptLen=601.
701, Encrypt=8, Decrypt=34, EncryptLen=704, DecryptLen=701.
801, Encrypt=8, Decrypt=37, EncryptLen=816, DecryptLen=801.
901, Encrypt=9, Decrypt=39, EncryptLen=912, DecryptLen=901.
1001, Encrypt=9, Decrypt=42, EncryptLen=1008, DecryptLen=1001.
1101, Encrypt=9, Decrypt=43, EncryptLen=1104, DecryptLen=1101.
1201, Encrypt=10, Decrypt=45, EncryptLen=1216, DecryptLen=1201.
1301, Encrypt=10, Decrypt=48, EncryptLen=1312, DecryptLen=1301.
1401, Encrypt=12, Decrypt=50, EncryptLen=1408, DecryptLen=1401.
1501, Encrypt=12, Decrypt=52, EncryptLen=1504, DecryptLen=1501.
1601, Encrypt=12, Decrypt=55, EncryptLen=1616, DecryptLen=1601.
1701, Encrypt=13, Decrypt=57, EncryptLen=1712, DecryptLen=1701.
1801, Encrypt=13, Decrypt=59, EncryptLen=1808, DecryptLen=1801.
1901, Encrypt=14, Decrypt=62, EncryptLen=1904, DecryptLen=1901.
2001, Encrypt=14, Decrypt=65, EncryptLen=2016, DecryptLen=2001.
2101, Encrypt=15, Decrypt=67, EncryptLen=2112, DecryptLen=2101.
2201, Encrypt=15, Decrypt=69, EncryptLen=2208, DecryptLen=2201.
2301, Encrypt=15, Decrypt=72, EncryptLen=2304, DecryptLen=2301.
2401, Encrypt=16, Decrypt=74, EncryptLen=2416, DecryptLen=2401.
2501, Encrypt=16, Decrypt=77, EncryptLen=2512, DecryptLen=2501.
2601, Encrypt=17, Decrypt=79, EncryptLen=2608, DecryptLen=2601.
2701, Encrypt=17, Decrypt=83, EncryptLen=2704, DecryptLen=2701.
2801, Encrypt=18, Decrypt=85, EncryptLen=2816, DecryptLen=2801.
2901, Encrypt=19, Decrypt=88, EncryptLen=2912, DecryptLen=2901.
3001, Encrypt=21, Decrypt=90, EncryptLen=3008, DecryptLen=3001.



                 */

            }

            return true;
        }

    }
}
