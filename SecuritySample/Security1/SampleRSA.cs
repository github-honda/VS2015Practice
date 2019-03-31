using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// add
using System.Security.Cryptography;
using System.Diagnostics;
using ZLib.DSecurity;

namespace Security1
{
    class SampleRSA
    {
        public void Run()
        {
            UTF8Encoding UTF8Converter = new UTF8Encoding();

            Console.WriteLine($"1. 建立金鑰: AB雙方各自產生一組成對的(公鑰及私鑰).");
            string sContainer_A = "A";
            string sContainer_B = "B";
            if (!ZRSA.CreateContainer(sContainer_A))
            {
                Console.WriteLine(ZRSA.msError);
                return;
            }
            if (!ZRSA.CreateContainer(sContainer_B))
            {
                Console.WriteLine(ZRSA.msError);
                return;
            }
            Console.WriteLine($"{sContainer_A} = {ZRSA.ExistingContainer(sContainer_A)}, {ZRSA.GetContainerFileFullName(sContainer_A)}");
            Console.WriteLine($"{sContainer_B} = {ZRSA.ExistingContainer(sContainer_B)}, {ZRSA.GetContainerFileFullName(sContainer_B)}");
            Console.WriteLine();

            Console.WriteLine($"2. 取得公鑰: A取得(B的公鑰), 並確保(B的公鑰)是來自於B.");
            string sPublicKeyXML_B = ZRSA.GetPublicKey(sContainer_B);
            if (string.IsNullOrEmpty(sPublicKeyXML_B))
            {
                Console.WriteLine(ZRSA.msError);
                return;
            }
            Console.WriteLine($"(B的公鑰) = {sPublicKeyXML_B.Length}, {sPublicKeyXML_B}");
            Console.WriteLine();

            Console.WriteLine($"3. 加密原文: A利用(B的公鑰), 加密(原文)後, 產生(加密訊息).");
            string sPlainText = "123, 到台灣, 台灣有個阿里山. ~!@#$%^&*()<>{}[]:;\"'＊％！＃\\/ABCD.";
            byte[] baPlainText = UTF8Converter.GetBytes(sPlainText);
            byte[] baEncrypt = ZRSA.Encrypt(baPlainText, sPublicKeyXML_B);
            if (baEncrypt == null)
            {
                Console.WriteLine(ZRSA.msError);
                return;
            }
            Console.WriteLine($"原文: {UTF8Converter.GetByteCount(sPlainText)}, {sPlainText}");
            Console.WriteLine($"密文: {baEncrypt.Length}, {BitConverter.ToString(baEncrypt)}");
            Console.WriteLine();

            Console.WriteLine($"4. 製作簽章: A利用(A的私鑰), 簽章(原文)後, 產生(簽章訊息).");
            string sPrivateKeyXML_A = ZRSA.GetPrivateKey(sContainer_A);
            byte[] baSignatureUTF8 = ZRSA.SignDataSHA1(baPlainText, sPrivateKeyXML_A);
            if (baSignatureUTF8 == null)
            {
                Console.WriteLine(ZRSA.msError);
                return;
            }
            Console.WriteLine($"簽章: {baSignatureUTF8.Length}, {BitConverter.ToString(baSignatureUTF8)}");
            Console.WriteLine();

            Console.WriteLine($"5. 傳送訊息: B取得(A的公鑰), (加密訊息), (簽章訊息).");
            string sPublicKeyXML_A = ZRSA.GetPublicKey(sContainer_A);
            if (string.IsNullOrEmpty(sPublicKeyXML_A))
            {
                Console.WriteLine(ZRSA.msError);
                return;
            }
            Console.WriteLine($"(A的公鑰) = {sPublicKeyXML_A.Length}, {sPublicKeyXML_A}");
            Console.WriteLine();

            Console.WriteLine($"6. 解密訊息: B利用(B的私鑰), 解密(加密訊息), 取得(原文).");
            string sPrivateKeyXML_B = ZRSA.GetPrivateKey(sContainer_B);
            if (string.IsNullOrEmpty(sPrivateKeyXML_B))
            {
                Console.WriteLine(ZRSA.msError);
                return;
            }
            Console.WriteLine($"(B的私鑰) = {sPrivateKeyXML_B.Length}, {sPrivateKeyXML_B}");

            byte[] baDecrypt = ZRSA.Decrypt(baEncrypt, sPrivateKeyXML_B);
            if (baDecrypt == null)
            {
                Console.WriteLine(ZRSA.msError);
                return;
            }
            string sDecrypt = UTF8Converter.GetString(baDecrypt);
            Console.WriteLine($"解密訊息 = {baDecrypt.Length}, {sDecrypt}");
            Console.WriteLine($"解密結果 = {sDecrypt == sPlainText}.");
            Console.WriteLine();

            Console.WriteLine($"7. 驗證簽章: B取得(A的公鑰), 驗證(簽章訊息), 確定(加密訊息)沒有被竄改..");
            Console.WriteLine($"驗證簽章 = {ZRSA.VerifyDataSHA1(baDecrypt, sPublicKeyXML_A, baSignatureUTF8)}.");
            Console.WriteLine();

            Console.WriteLine($"刪除金鑰: 刪除存放在本機的金鑰.");
            if (ZRSA.DeleteContainer(sContainer_A))
                Console.WriteLine("刪除A");
            if (ZRSA.DeleteContainer(sContainer_B))
                Console.WriteLine("刪除B");

            Console.WriteLine("實際刪除金鑰是由 garbage collection 執行, 不會立刻刪除.");
            Console.WriteLine($"{sContainer_A} = {ZRSA.ExistingContainer(sContainer_A)}, {ZRSA.GetContainerFileFullName(sContainer_A)}");
            Console.WriteLine($"{sContainer_B} = {ZRSA.ExistingContainer(sContainer_B)}, {ZRSA.GetContainerFileFullName(sContainer_B)}");
            Console.WriteLine();

        }

    }
}
