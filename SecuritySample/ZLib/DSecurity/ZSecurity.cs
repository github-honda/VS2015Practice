using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// add
using System.Security.Cryptography;
using System.Diagnostics;
using System.IO;


namespace ZLib.DSecurity
{
    public static class ZSecurity
    {
        public static string msError { get; private set; }

        /// <summary>
        /// 取得 byte[] 的 MD5 雜湊值. 16 bytes (128 bits).
        /// </summary>
        /// <param name="baPlainText"></param>
        /// <returns></returns>
        public static byte[] GetHashMD5(byte[] baPlainText)
        {
            try
            {
                msError = string.Empty;
                MD5CryptoServiceProvider md5Instance = new MD5CryptoServiceProvider();
                return md5Instance.ComputeHash(baPlainText);
            }
            catch(Exception ex1)
            {
                msError = ex1.Message;
                return null;
            }
        }
        /// <summary>
        /// 取得檔案的 MD5 雜湊值. 16 bytes (128 bits).
        /// </summary>
        /// <param name="sFile"></param>
        /// <returns></returns>
        public static byte[] GetHashMD5(string sFile)
        {
            try
            {
                msError = string.Empty;
                using (MD5 md5Instance = MD5.Create())
                {
                    using (FileStream stream = File.OpenRead(sFile))
                    {
                        return md5Instance.ComputeHash(stream);
                    }
                }
            }
            catch (Exception ex1)
            {
                msError = ex1.Message;
                return null;
            }
        }
        public static Boolean VerifyHashMD5(byte[] baPlainText, byte[] baHash)
        {
            byte[] baHash1 = GetHashMD5(baPlainText);
            if (baHash1 == null)
                return false;
            return ZByte.ZCompare(baHash1, baHash);
        }
        public static Boolean VerifyHashMD5(string sFile, byte[] baHash)
        {

            byte[] baHash1 = GetHashMD5(sFile);
            if (baHash1 == null)
                return false;
            return ZByte.ZCompare(baHash1, baHash);
        }

        /// <summary>
        /// 取得 byte[] 的 SHA1 雜湊值. 20 bytes (160 bits).
        /// </summary>
        /// <param name="baPlainText"></param>
        /// <returns></returns>
        public static byte[] GetHashSHA1(byte[] baPlainText)
        {
            try
            {
                msError = string.Empty;
                SHA1CryptoServiceProvider sha1Instance = new SHA1CryptoServiceProvider();
                return sha1Instance.ComputeHash(baPlainText);
            }
            catch (Exception ex1)
            {
                msError = ex1.Message;
                return null;
            }
        }

        /// <summary>
        /// 取得檔案的 SHA1 雜湊值. 20 bytes (160 bits). 例如: GitHub 使用此雜湊值確認檔案版本.
        /// </summary>
        /// <param name="sFile"></param>
        /// <returns></returns>
        public static byte[] GetHashSHA1(string sFile)
        {
            try
            {
                msError = string.Empty;
                using (SHA1 sha1Instnace = SHA1.Create())
                {
                    using (FileStream stream = File.OpenRead(sFile))
                    {
                        return sha1Instnace.ComputeHash(stream);
                    }
                }
            }
            catch (Exception ex1)
            {
                msError = ex1.Message;
                return null;
            }
        }
        public static Boolean VerifyHashSHA1(byte[] baPlainText, byte[] baHash)
        {
            byte[] baHash1 = GetHashSHA1(baPlainText);
            if (baHash1 == null)
                return false;
            return ZByte.ZCompare(baHash1, baHash);
        }
        public static Boolean VerifyHashSHA1(string sFile, byte[] baHash)
        {

            byte[] baHash1 = GetHashSHA1(sFile);
            if (baHash1 == null)
                return false;
            return ZByte.ZCompare(baHash1, baHash);
        }

    }
}
