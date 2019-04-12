using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// add
using System.Security.Cryptography;
using System.Diagnostics;
using System.IO;
using ZLib.DCollection;


namespace ZLib.DSecurity
{
    /// <summary>
    /// 計算雜湊值(MD5, SHA1), 私鑰加解密(AES), 及加鹽功能(RFC2898). 
    /// </summary>
    public static class ZSecurity
    {
        public static string msError { get; private set; }

        #region MD5

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
            catch (Exception ex1)
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
            return baHash1.ZEquals(baHash);
        }
        public static Boolean VerifyHashMD5(string sFile, byte[] baHash)
        {

            byte[] baHash1 = GetHashMD5(sFile);
            if (baHash1 == null)
                return false;
            return baHash1.ZEquals(baHash);
        }
        #endregion

        #region SHA1
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
            return baHash1.ZEquals(baHash);
        }
        public static Boolean VerifyHashSHA1(string sFile, byte[] baHash)
        {

            byte[] baHash1 = GetHashSHA1(sFile);
            if (baHash1 == null)
                return false;
            return baHash1.ZEquals(baHash);
        }

        #endregion

        #region SymmetricAlgorithm
        /*
        SymmetricAlgorithm Represents the abstract base class from which all implementations of symmetric algorithms must inherit
        Derived:
          System.Security.Cryptography.Aes
          System.Security.Cryptography.DES
          System.Security.Cryptography.RC2
          System.Security.Cryptography.Rijndael
          System.Security.Cryptography.TripleDES 
        */

        /// <summary>
        /// 通用 SymmetricAlgorithm 加密函數. 可指定 (AES, DES, RC2, Rijndael, TripleDES) 演算法加密.
        /// </summary>
        /// <param name="symAlg"></param>
        /// <param name="baPlainText"></param>
        /// <returns></returns>
        public static byte[] Encrypt(SymmetricAlgorithm symAlg, byte[] baPlainText)
        {
            // usage:
            //AesCryptoServiceProvider aesCSP = new AesCryptoServiceProvider();
            //aesCSP.GenerateKey();
            //aesCSP.GenerateIV();
            //byte[] baEncrypt1 = Encrypt(aesCSP, baPlainText);
            //byte[] baDecrypt1 = Decrypt(aesCSP, baPlainText);
            try
            {
                ICryptoTransform transform1 = symAlg.CreateEncryptor();
                byte[] baEncrypt = transform1.TransformFinalBlock(baPlainText, 0, baPlainText.Length);
                symAlg.Clear();
                return baEncrypt;
            }
            catch (Exception e1)
            {
                msError = e1.Message;
                return null;
            }
        }

        /// <summary>
        /// 通用 SymmetricAlgorithm 解密函數. 可指定 (AES, DES, RC2, Rijndael, TripleDES) 演算法解密.
        /// </summary>
        /// <param name="symAlg"></param>
        /// <param name="baEncrypt"></param>
        /// <returns></returns>
        public static byte[] Decrypt(SymmetricAlgorithm symAlg, byte[] baEncrypt)
        {
            // usage:
            //AesCryptoServiceProvider aesCSP = new AesCryptoServiceProvider();
            //aesCSP.GenerateKey();
            //aesCSP.GenerateIV();
            //byte[] baEncrypt1 = Encrypt(aesCSP, baPlainText);
            //byte[] baDecrypt1 = Decrypt(aesCSP, baPlainText);
            try
            {
                ICryptoTransform transform1 = symAlg.CreateDecryptor();
                byte[] baDecrypt = transform1.TransformFinalBlock(baEncrypt, 0, baEncrypt.Length);
                symAlg.Clear();
                return baDecrypt;
            }
            catch (Exception e1)
            {
                msError = e1.Message;
                return null;
            }
        }

        /// <summary>
        /// 取得 RFC2898 標準加鹽物件, 可用來產生 Key 跟 IV. 參數 baSalt 至少為 8 bytes.
        /// </summary>
        /// <param name="baPassword"></param>
        /// <param name="baSalt"></param>
        /// <param name="iIterations"></param>
        /// <returns></returns>
        public static Rfc2898DeriveBytes CreateRFC2898(byte[] baPassword, byte[] baSalt, int iIteration = 10000)
        {
            // 使用 Rfc2898DeriveBytes 的好處, 若有其他方法可補足這些好處也可自行產生:
            // 1. 可產生符合AES加密所需的指定長度的Key或IV.
            // 2. 可經由不同的(baSalt, iIteration), 產生不同的密碼雜湊值.
            // 3. 產生的雜湊值, 會用滿整個byte. 避免若密碼僅為ascii字元, 則不會用到最高bit, 造成弱點, 易受攻擊.

            // Rfc2898DeriveBytes 備忘:
            // 1. baSalt 至少為 8bytes. 
            // 2. 預設 Iteration=1000, 建議為10000次.
            // 3. 若輸入參數(baSalt, iIteration)不變, 則依序呼叫 GetBytes() 產生的結果也完全相同. 因此稱為假亂數 pseudo-random.
            // 4. 每次呼叫 GetBytes(N) 可取得只定長度的雜湊值. 此雜湊值是從建立 Rfc2898DeriveBytes() 物件後就固定的雜湊表依序取出. 
            // 注意: 假亂數 pseudo-random 特性. 若輸入參數(baSalt, iIteration)不變, 則依序呼叫 GetBytes(N) 產生的結果也完全相同. 

            // Rfc2898DeriveBytes usage:
            //Rfc2898DeriveBytes rfc2898 = new Rfc2898DeriveBytes(baPassword, baSalt, iIterations);
            //return rfc2898.GetBytes(32); 取得 32 bytes, 256 bits 雜湊值
            //return rfc2898.GetBytes(16); 取得 16 bytes, 128 bits 雜湊值
            //return new Rfc2898DeriveBytes(baPassword, baSalt, iIterations);
            try
            {
                return new Rfc2898DeriveBytes(baPassword, baSalt, iIteration);
            }
            catch (Exception e1)
            {
                msError = e1.Message;
                return null;
            }
        }

        #endregion

        #region AES
        /// <summary>
        /// 以AES演算法加密. AES同Rijndael. Key應為(16 or 32) bytes, baIV應為16 bytes.
        /// </summary>
        /// <param name="baPlainText"></param>
        /// <param name="baKey"></param>
        /// <param name="baIV"></param>
        /// <returns></returns>
        public static byte[] EncryptAES(byte[] baPlainText, byte[] baKey, byte[] baIV)
        {
            try
            {
                if (baPlainText == null || baPlainText.Length < 1)
                    throw new ArgumentNullException(nameof(baPlainText));
                byte[] baEncrypt = null;
                using (AesCryptoServiceProvider aes1 = new AesCryptoServiceProvider())
                {
                    //default aesAlg.Mode = CipherMode.CBC; // CBC (Cipher-Block Chaining): CBC是一種串鏈的加密方式, 參考: http://svc.luckstar.com.tw/ShareAll/KM/What/加密方法比較表.html
                    //default aesAlg.Padding = PaddingMode.PKCS7;
                    //default aesAlg.KeySize = 256;    Key的長度 可以是128, 192或256
                    //default aesAlg.BlockSize = 128;  IV的長度
                    aes1.Key = baKey;
                    aes1.IV = baIV;

                    ICryptoTransform transform1 = aes1.CreateEncryptor(aes1.Key, aes1.IV);
                    using (MemoryStream ms1 = new MemoryStream())
                    {
                        using (CryptoStream streamEncrypt = new CryptoStream(ms1, transform1, CryptoStreamMode.Write))
                        {
                            using (BinaryWriter writer1 = new BinaryWriter(streamEncrypt))
                            {
                                writer1.Write(baPlainText);
                            }
                        }
                        baEncrypt = ms1.ToArray();
                    }
                    aes1.Clear();
                }
                return baEncrypt;
            }
            catch (Exception e1)
            {
                msError = e1.Message;
                return null;
            }
        }


        /// <summary>
        /// 以AES演算法解密. AES同Rijndael. Key應為(16 or 32) bytes, baIV應為16 bytes.
        /// </summary>
        /// <param name="baCipher"></param>
        /// <param name="baKey"></param>
        /// <param name="baIV"></param>
        /// <returns></returns>
        public static byte[] DecryptAES(byte[] baCipher, byte[] baKey, byte[] baIV)
        {
            try
            {
                if (baCipher == null || baCipher.Length < 1)
                    throw new ArgumentNullException(nameof(baCipher));
                using (AesCryptoServiceProvider aes1 = new AesCryptoServiceProvider())
                {
                    //default aesAlg.Mode = CipherMode.CBC; // CBC (Cipher-Block Chaining): CBC是一種串鏈的加密方式, 參考: http://svc.luckstar.com.tw/ShareAll/KM/What/加密方法比較表.html
                    //default aesAlg.Padding = PaddingMode.PKCS7;
                    //default aesAlg.KeySize = 256;    Key的長度 可以是128, 192或256
                    //default aesAlg.BlockSize = 128;  IV的長度
                    aes1.Key = baKey;
                    aes1.IV = baIV;
                    ICryptoTransform transform1 = aes1.CreateDecryptor(aes1.Key, aes1.IV);
                    using (MemoryStream ms1 = new MemoryStream(baCipher))
                    {
                        using (CryptoStream streamDecrypt = new CryptoStream(ms1, transform1, CryptoStreamMode.Read))
                        {
                            // CryptoStream 不支援 length: 
                            // exception: 資料留不支援搜尋
                            return ZFile.ReadAllBytes(streamDecrypt);
                        }

                    }
                }
            }
            catch (Exception e1)
            {
                msError = e1.Message;
                return null;
            }
        }
        #endregion
    }
}
