using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// add
using System.Diagnostics;
using System.IO;
using ZLib.DSecurity;


namespace Security1
{

    class SampleMD5
    {
        public void Run()
        {
            UTF8Encoding UTF8Converter = new UTF8Encoding();

            Console.WriteLine($"MD5 測試:");

            Console.WriteLine($"1. 取得byte[]雜湊值.");
            string sSalt = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            string sPlainText = "123, 到台灣, 台灣有個阿里山. ~!@#$%^&*()<>{}[]:;\"'＊％！＃\\/ABCD." + sSalt;
            byte[] baPlainText = UTF8Converter.GetBytes(sPlainText);
            byte[] baHash = ZSecurity.GetHashMD5(baPlainText);
            if (baHash == null)
            {
                Console.WriteLine(ZSecurity.msError);
                return;
            }
            Console.WriteLine($"原文: {UTF8Converter.GetByteCount(sPlainText)}, {sPlainText}");
            Console.WriteLine($"雜湊值: {baHash.Length}, {BitConverter.ToString(baHash)}");
            Console.WriteLine($"驗證: {ZSecurity.VerifyHashMD5(baPlainText, baHash)}");
            Console.WriteLine();

            Console.WriteLine($"2. 取得檔案雜湊值.");
            string sFile = "MD5.txt";
            File.WriteAllText(sFile, sPlainText);
            baHash = ZSecurity.GetHashMD5(sFile);
            if (baHash == null)
            {
                Console.WriteLine(ZSecurity.msError);
                return;
            }
            Console.WriteLine($"雜湊值: {baHash.Length}, {BitConverter.ToString(baHash)}");
            Console.WriteLine($"驗證: {ZSecurity.VerifyHashMD5(sFile, baHash)}");
            Console.WriteLine();
        }
    }
}
