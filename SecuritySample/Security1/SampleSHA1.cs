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
    class SampleSHA1
    {
        public Boolean Run()
        {
            Console.WriteLine($"SHA1 測試:");

            Console.WriteLine($"1. 取得byte[]雜湊值.");
            string sSalt = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            string sPlainText = "123, 到台灣, 台灣有個阿里山. ~!@#$%^&*()<>{}[]:;\"'＊％！＃\\/ABCD." + sSalt;
            byte[] baPlainText = ZByte.GetBytesUTF8(sPlainText);
            byte[] baHash = ZSecurity.GetHashSHA1(baPlainText);
            if (baHash == null)
            {
                Console.WriteLine(ZSecurity.msError);
                return false;
            }
            Console.WriteLine($"原文: {baPlainText.Length}, {sPlainText}");
            Console.WriteLine($"雜湊值: {baHash.Length}, {baHash.ZGetStringHex()}");
            Console.WriteLine($"驗證: {ZSecurity.VerifyHashSHA1(baPlainText, baHash)}");
            Console.WriteLine();

            Console.WriteLine($"2. 取得檔案雜湊值.");
            string sFile = "SHA1.txt";
            File.WriteAllText(sFile, sPlainText);
            baHash = ZSecurity.GetHashSHA1(sFile);
            if (baHash == null)
            {
                Console.WriteLine(ZSecurity.msError);
                return false;
            }
            Console.WriteLine($"雜湊值: {baHash.Length}, {baHash.ZGetStringHex()}");
            Console.WriteLine($"驗證: {ZSecurity.VerifyHashSHA1(sFile, baHash)}");
            Console.WriteLine();
            return true;
        }
    }
}
