using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// add
using System.Diagnostics;
using System.IO;
using ZLib;
using ZLib.DSecurity;


namespace Security1
{
    class SampleDES
    {
        public Boolean Run()
        {
            Console.WriteLine($"DES 測試:");

            Console.WriteLine($"1. 加密.");
            string sPlainTextSalt = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            string sPlainText = "123, 到台灣, 台灣有個阿里山. ~!@#$%^&*()<>{}[]:;\"'＊％！＃\\/ABCD." + sPlainTextSalt;
            string sKey = "12345678";
            string sIV = "12345678";
            byte[] baPlainText = ZByte.GetBytesUTF8(sPlainText);
            byte[] baKey = ZByte.GetBytesUTF8(sKey);
            byte[] baIV = ZByte.GetBytesUTF8(sIV);
            byte[] baEncrypt = ZSecurity.EncryptDES(baPlainText, baKey, baIV);
            if (baEncrypt == null)
            {
                Console.WriteLine(ZSecurity.msError);
                return false;
            }
            Console.WriteLine($"原文: {baPlainText.Length}, {sPlainText}");
            Console.WriteLine($"Key: {baKey.Length}, {baKey.ZGetStringHex()}");
            Console.WriteLine($"IV: {baIV.Length}, {baIV.ZGetStringHex()}");
            Console.WriteLine($"密文: {baEncrypt.Length}, {baEncrypt.ZGetStringHex()}");
            Console.WriteLine();

            Console.WriteLine($"2. 解密.");
            byte[] baDecrypt = ZSecurity.DecryptDES(baEncrypt, baKey, baIV);
            if (baDecrypt == null)
            {
                Console.WriteLine(ZSecurity.msError);
                return false;
            }
            string sDecrypt = ZByte.ZGetStringUTF8(baDecrypt);
            Console.WriteLine($"解密: {baDecrypt.Length}, {sDecrypt}");
            Console.WriteLine($"驗證: {baPlainText.ZEquals(baDecrypt)}");
            Console.WriteLine();

            Console.WriteLine($"3. 加密-金鑰加鹽");
            Console.WriteLine($"以RFC2898 標準 產生 Key 和 IV.");
            string sSalt = "12345678";
            byte[] baSalt = ZByte.GetBytesUTF8(sSalt);
            var vRFC2898 = ZSecurity.CreateRFC2898(baKey, baSalt);
            byte[] baKey_RFC2898 = vRFC2898.GetBytes(8);
            byte[] baIV_RFC2898 = vRFC2898.GetBytes(8);
            baEncrypt = ZSecurity.EncryptDES(baPlainText, baKey_RFC2898, baIV_RFC2898);
            if (baEncrypt == null)
            {
                Console.WriteLine(ZSecurity.msError);
                return false;
            }

            Console.WriteLine($"Key Salted: {baKey.Length}, {baKey_RFC2898.ZGetStringHex()}");
            Console.WriteLine($"IV Salted: {baIV.Length}, {baIV_RFC2898.ZGetStringHex()}");
            Console.WriteLine($"密文: {baEncrypt.Length}, {baEncrypt.ZGetStringHex()}");
            Console.WriteLine();

            Console.WriteLine($"4. 解密-金鑰加鹽.");
            baDecrypt = ZSecurity.DecryptDES(baEncrypt, baKey_RFC2898, baIV_RFC2898);
            if (baDecrypt == null)
            {
                Console.WriteLine(ZSecurity.msError);
                return false;
            }
            sDecrypt = ZByte.ZGetStringUTF8(baDecrypt);
            Console.WriteLine($"解密: {baDecrypt.Length}, {sDecrypt}");
            Console.WriteLine($"驗證: {baPlainText.ZEquals(baDecrypt)}");
            Console.WriteLine();

            Console.WriteLine($"5. 加密-檔案");
            string sFilePlainText = "DESPlainText.dat";
            string sFileEncrypt = "DESEncrypt.dat";
            File.WriteAllBytes(sFilePlainText, baPlainText);
            if (!ZSecurity.EncryptDES(sFilePlainText, sFileEncrypt, baKey_RFC2898, baIV_RFC2898))
            {
                Console.WriteLine(ZSecurity.msError);
                return false;
            }

            Console.WriteLine($"6. 解密-檔案");
            string sFileDecrypt = "DESDecrypt.dat";
            if (!ZSecurity.DecryptDES(sFileEncrypt, sFileDecrypt, baKey_RFC2898, baIV_RFC2898))
            {
                Console.WriteLine(ZSecurity.msError);
                return false;
            }
            baDecrypt = File.ReadAllBytes(sFileDecrypt);
            Console.WriteLine($"驗證: {baPlainText.ZEquals(baDecrypt)}");
            Console.WriteLine();

            return true;
        }

    }
}
