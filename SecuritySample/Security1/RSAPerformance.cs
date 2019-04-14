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
    class RSAPerformance
    {
        public Boolean Run()
        {
            Console.WriteLine($"RSA performance test :");
            Console.WriteLine($"1. 建立金鑰: AB雙方各自產生一組成對的(公鑰及私鑰).");
            string sContainer_A = "A";
            string sContainer_B = "B";
            if (!ZRSA.CreateContainer(sContainer_A))
            {
                Console.WriteLine(ZRSA.msError);
                return false;
            }
            if (!ZRSA.CreateContainer(sContainer_B))
            {
                Console.WriteLine(ZRSA.msError);
                return false;
            }
            Console.WriteLine($"{sContainer_A} = {ZRSA.ExistingContainer(sContainer_A)}, {ZRSA.GetContainerFileFullName(sContainer_A)}");
            Console.WriteLine($"{sContainer_B} = {ZRSA.ExistingContainer(sContainer_B)}, {ZRSA.GetContainerFileFullName(sContainer_B)}");
            Console.WriteLine();

            Console.WriteLine($"2. 取得公鑰: A取得(B的公鑰), 並確保(B的公鑰)是來自於B.");
            string sPublicKeyXML_B = ZRSA.GetPublicKey(sContainer_B);
            if (string.IsNullOrEmpty(sPublicKeyXML_B))
            {
                Console.WriteLine(ZRSA.msError);
                return false;
            }
            Console.WriteLine($"(B的公鑰) = {sPublicKeyXML_B.Length}, {sPublicKeyXML_B}");
            Console.WriteLine();

            Console.WriteLine($"3. 加密原文: A利用(B的公鑰), 加密(原文)後, 產生(加密訊息).");
            string sPlainText;
            byte[] baPlainText;
            byte[] baEncrypt;
            string sPrivateKeyXML_A = ZRSA.GetPrivateKey(sContainer_A);
            byte[] baSignatureUTF8;
            string sPublicKeyXML_A = ZRSA.GetPublicKey(sContainer_A);
            string sPrivateKeyXML_B = ZRSA.GetPrivateKey(sContainer_B);
            byte[] baDecrypt;

            int iTestCount = 300;
            Stopwatch swEncryptRSA = new Stopwatch();
            Stopwatch swDecryptRSA = new Stopwatch();
            Stopwatch swSignRSA = new Stopwatch();
            Stopwatch swVerifySignRSA = new Stopwatch();
            for (int i = 0; i < iTestCount; i++)
            {
                swEncryptRSA.Start();
                sPlainText = new string('H', i+1);
                baPlainText = ZByte.GetBytesUTF8(sPlainText);

                // 原文長度最多 117 bytes.
                baEncrypt = ZRSA.Encrypt(baPlainText, sPublicKeyXML_B);
                if (baEncrypt == null)
                {
                    Console.WriteLine("Encrypt " + ZRSA.msError);
                    break;
                }
                swEncryptRSA.Stop();

                swSignRSA.Start();
                baSignatureUTF8 = ZRSA.SignDataSHA1(baPlainText, sPrivateKeyXML_A);
                if (baSignatureUTF8 == null)
                {
                    Console.WriteLine("Sign " + ZRSA.msError);
                    break;
                }
                swSignRSA.Stop();

                swDecryptRSA.Start();
                baDecrypt = ZRSA.Decrypt(baEncrypt, sPrivateKeyXML_B);
                if (baDecrypt == null)
                {
                    Console.WriteLine("Decrypt " + ZRSA.msError);
                    break;
                }
                swDecryptRSA.Stop();

                swVerifySignRSA.Start();
                //Console.WriteLine($"驗證簽章 = {ZRSA.VerifyDataSHA1(baDecrypt, sPublicKeyXML_A, baSignatureUTF8)}.");
                if (!ZRSA.VerifyDataSHA1(baDecrypt, sPublicKeyXML_A, baSignatureUTF8))
                {
                    Console.WriteLine("Verify " + ZRSA.msError);
                    return false;
                }
                swVerifySignRSA.Stop();

                Console.WriteLine("{0}, Encrypt={1}, Sign={2}, Decrypt={3}, Verify={4}, EncryptLen={5}, DecryptLen={6}.",
                    i+1, 
                    swEncryptRSA.ElapsedMilliseconds,
                    swDecryptRSA.ElapsedMilliseconds,
                    swSignRSA.ElapsedMilliseconds,
                    swVerifySignRSA.ElapsedMilliseconds,
                    baEncrypt.Length,
                    baDecrypt.Length);

                /*

                1, Encrypt=1, Sign=6, Decrypt=9, Verify=0, EncryptLen=128, DecryptLen=1.
                2, Encrypt=1, Sign=11, Decrypt=14, Verify=0, EncryptLen=128, DecryptLen=2.
                3, Encrypt=1, Sign=15, Decrypt=17, Verify=1, EncryptLen=128, DecryptLen=3.
                4, Encrypt=1, Sign=22, Decrypt=22, Verify=1, EncryptLen=128, DecryptLen=4.
                5, Encrypt=1, Sign=28, Decrypt=25, Verify=1, EncryptLen=128, DecryptLen=5.
                6, Encrypt=2, Sign=31, Decrypt=30, Verify=1, EncryptLen=128, DecryptLen=6.
                7, Encrypt=2, Sign=36, Decrypt=34, Verify=2, EncryptLen=128, DecryptLen=7.
                8, Encrypt=2, Sign=41, Decrypt=41, Verify=2, EncryptLen=128, DecryptLen=8.
                9, Encrypt=2, Sign=46, Decrypt=46, Verify=2, EncryptLen=128, DecryptLen=9.
                10, Encrypt=3, Sign=51, Decrypt=54, Verify=2, EncryptLen=128, DecryptLen=10.
                20, Encrypt=6, Sign=106, Decrypt=115, Verify=5, EncryptLen=128, DecryptLen=20.
                30, Encrypt=10, Sign=155, Decrypt=180, Verify=8, EncryptLen=128, DecryptLen=30.
                40, Encrypt=29, Sign=206, Decrypt=260, Verify=11, EncryptLen=128, DecryptLen=40.
                50, Encrypt=33, Sign=249, Decrypt=323, Verify=15, EncryptLen=128, DecryptLen=50.
                60, Encrypt=37, Sign=298, Decrypt=394, Verify=18, EncryptLen=128, DecryptLen=60.
                70, Encrypt=43, Sign=353, Decrypt=474, Verify=22, EncryptLen=128, DecryptLen=70.
                80, Encrypt=47, Sign=411, Decrypt=557, Verify=26, EncryptLen=128, DecryptLen=80.
                90, Encrypt=54, Sign=456, Decrypt=619, Verify=28, EncryptLen=128, DecryptLen=90.
                100, Encrypt=57, Sign=495, Decrypt=669, Verify=30, EncryptLen=128, DecryptLen=100.
                117, Encrypt=62, Sign=571, Decrypt=762, Verify=34, EncryptLen=128, DecryptLen=117.
                Encrypt 長度錯誤。

                */

            }

            return true;
        }

    }
}
