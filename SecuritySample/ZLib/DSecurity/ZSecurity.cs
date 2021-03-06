﻿using System;
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
        /// 通用 SymmetricAlgorithm 加密檔案函數. 可指定 (AES, DES, RC2, Rijndael, TripleDES) 演算法加密.
        /// </summary>
        /// <param name="symAlg"></param>
        /// <param name="sFileInput"></param>
        /// <param name="sFileEncrypt"></param>
        /// <returns></returns>
        public static Boolean Encrypt(SymmetricAlgorithm symAlg, string sFileInput, string sFileEncrypt)
        {
            try
            {
                using (FileStream streamInput = new FileStream(sFileInput, FileMode.Open, FileAccess.Read))
                using (FileStream streamEncrypt = new FileStream(sFileEncrypt, FileMode.Create, FileAccess.Write))
                using (CryptoStream streamCrypto = new CryptoStream(streamEncrypt, symAlg.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    byte[] baBuffer = new byte[ZFile.ciBufferSize];
                    int iReadBytes = 0;
                    do
                    {
                        iReadBytes = streamInput.Read(baBuffer, 0, baBuffer.Length);
                        if (iReadBytes > 0)
                            streamCrypto.Write(baBuffer, 0, iReadBytes);
                    }
                    while (iReadBytes > 0);
                    streamCrypto.FlushFinalBlock();
                    return true;
                }
            }
            catch (Exception e1)
            {
                msError = e1.Message;
                return false;
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
        /// 通用 SymmetricAlgorithm 解密檔案函數. 可指定 (AES, DES, RC2, Rijndael, TripleDES) 演算法解密.
        /// </summary>
        /// <param name="symAlg"></param>
        /// <param name="sFileEncrypt"></param>
        /// <param name="sFileDecrypt"></param>
        /// <returns></returns>
        public static Boolean Decrypt(SymmetricAlgorithm symAlg, string sFileEncrypt, string sFileDecrypt)
        {
            try
            {
                using (FileStream streamEncrypt = new FileStream(sFileEncrypt, FileMode.Open, FileAccess.Read))
                using (FileStream streamDecrypt = new FileStream(sFileDecrypt, FileMode.Create, FileAccess.Write))
                using (CryptoStream streamCrypto = new CryptoStream(streamDecrypt, symAlg.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    byte[] baBuffer = new byte[ZFile.ciBufferSize];
                    int iReadBytes = 0;
                    do
                    {
                        iReadBytes = streamEncrypt.Read(baBuffer, 0, baBuffer.Length);
                        if (iReadBytes > 0)
                            streamCrypto.Write(baBuffer, 0, iReadBytes);
                    }
                    while (iReadBytes > 0);
                    streamCrypto.FlushFinalBlock();
                    return true;
                }
            }
            catch (Exception e1)
            {
                msError = e1.Message;
                return false;
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

        #region DES
        /// <summary>
        /// 以 DES 演算法加密 byte[]. baKey 應為 8 bytes, baIV 應為 8 bytes.
        /// </summary>
        /// <param name="baPlainText"></param>
        /// <param name="baKey"></param>
        /// <param name="baIV"></param>
        /// <returns></returns>
        public static byte[] EncryptDES(byte[] baPlainText, byte[] baKey, byte[] baIV)
        {
            try
            {
                byte[] baEncrypt = null;
                using (DESCryptoServiceProvider des1 = new DESCryptoServiceProvider())
                {
                    des1.Key = baKey;
                    des1.IV = baIV;

                    baEncrypt = Encrypt(des1, baPlainText);
                    if (baEncrypt == null)
                        return null;
                    des1.Clear();
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
        /// 以 DES 演算法加密檔案. baKey 應為 8 bytes, baIV 應為 8 bytes.
        /// </summary>
        /// <param name="sFileInput"></param>
        /// <param name="sFileEncrypted"></param>
        /// <param name="baKey"></param>
        /// <param name="baIV"></param>
        /// <returns></returns>
        public static Boolean EncryptDES(string sFileInput, string sFileEncrypted, byte[] baKey, byte[] baIV)
        {
            try
            {
                using (DESCryptoServiceProvider des1 = new DESCryptoServiceProvider())
                {
                    des1.Key = baKey;
                    des1.IV = baIV;
                    if (!Encrypt(des1, sFileInput, sFileEncrypted))
                        return false;
                    des1.Clear();
                    return true;
                }
            }
            catch (Exception e1)
            {
                msError = e1.Message;
                return false;
            }
        }

        /// <summary>
        /// 以 DES 演算法解密 byte[]. baKey 應為 8 bytes, baIV 應為 8 bytes.
        /// </summary>
        /// <param name="baCipher"></param>
        /// <param name="baKey"></param>
        /// <param name="baIV"></param>
        /// <returns></returns>
        public static byte[] DecryptDES(byte[] baCipher, byte[] baKey, byte[] baIV)
        {
            try
            {
                using (DESCryptoServiceProvider des1 = new DESCryptoServiceProvider())
                {
                    des1.Key = baKey;
                    des1.IV = baIV;
                    byte[] baDecrypt = Decrypt(des1, baCipher);
                    if (baDecrypt == null)
                        return null;
                    des1.Clear();
                    return baDecrypt;
                }
            }
            catch (Exception e1)
            {
                msError = e1.Message;
                return null;
            }
        }

        /// <summary>
        /// 以 DES 演算法解密檔案. baKey 應為 8 bytes, baIV 應為 8 bytes.
        /// </summary>
        /// <param name="sFileEncrypt"></param>
        /// <param name="sFileDecrypt"></param>
        /// <param name="baKey"></param>
        /// <param name="baIV"></param>
        /// <returns></returns>
        public static Boolean DecryptDES(string sFileEncrypt, string sFileDecrypt, byte[] baKey, byte[] baIV)
        {
            try
            {
                using (DESCryptoServiceProvider des1 = new DESCryptoServiceProvider())
                {
                    des1.Key = baKey;
                    des1.IV = baIV;
                    Decrypt(des1, sFileEncrypt, sFileDecrypt);
                    des1.Clear();
                    return true;
                }
            }
            catch (Exception e1)
            {
                msError = e1.Message;
                return false;
            }
        }

        #endregion

        #region AES
        /// <summary>
        /// 以 AES 演算法加密 byte[]. AES 同 Rijndael. baKey 應為(16 or 32) bytes, baIV 應為 16 bytes.
        /// </summary>
        /// <param name="baPlainText"></param>
        /// <param name="baKey"></param>
        /// <param name="baIV"></param>
        /// <returns></returns>
        public static byte[] EncryptAES(byte[] baPlainText, byte[] baKey, byte[] baIV)
        {
            try
            {
                byte[] baEncrypt = null;
                using (AesCryptoServiceProvider aes1 = new AesCryptoServiceProvider())
                {
                    //default aesAlg.Mode = CipherMode.CBC; // CBC (Cipher-Block Chaining): CBC是一種串鏈的加密方式, 參考: http://svc.luckstar.com.tw/ShareAll/KM/What/加密方法比較表.html
                    //default aesAlg.Padding = PaddingMode.PKCS7;
                    //default aesAlg.KeySize = 256;    Key的長度 可以是128, 192或256
                    //default aesAlg.BlockSize = 128;  IV的長度
                    aes1.Key = baKey;
                    aes1.IV = baIV;
                    baEncrypt = Encrypt(aes1, baPlainText);
                    if (baEncrypt == null)
                        return null;
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
        /// 以 AES 演算法加密檔案. AES 同 Rijndael. baKey 應為(16 or 32) bytes, baIV 應為 16 bytes.
        /// </summary>
        /// <param name="sFileInput"></param>
        /// <param name="sFileEncrypted"></param>
        /// <param name="baKey"></param>
        /// <param name="baIV"></param>
        /// <returns></returns>
        public static Boolean EncryptAES(string sFileInput, string sFileEncrypted, byte[] baKey, byte[] baIV)
        {
            try
            {
                using (AesCryptoServiceProvider aes1 = new AesCryptoServiceProvider())
                {
                    aes1.Key = baKey;
                    aes1.IV = baIV;
                    if (!Encrypt(aes1, sFileInput, sFileEncrypted))
                        return false;
                    aes1.Clear();
                    return true;
                }
            }
            catch (Exception e1)
            {
                msError = e1.Message;
                return false;
            }
        }


        /// <summary>
        /// 以 AES 演算法解密 byte[]. AES 同Rijndael. baKey 應為(16 or 32) bytes, baIV 應為 16 bytes.
        /// </summary>
        /// <param name="baCipher"></param>
        /// <param name="baKey"></param>
        /// <param name="baIV"></param>
        /// <returns></returns>
        public static byte[] DecryptAES(byte[] baCipher, byte[] baKey, byte[] baIV)
        {
            try
            {
                using (AesCryptoServiceProvider aes1 = new AesCryptoServiceProvider())
                {
                    //default aesAlg.Mode = CipherMode.CBC; // CBC (Cipher-Block Chaining): CBC是一種串鏈的加密方式, 參考: http://svc.luckstar.com.tw/ShareAll/KM/What/加密方法比較表.html
                    //default aesAlg.Padding = PaddingMode.PKCS7;
                    //default aesAlg.KeySize = 256;    Key的長度 可以是128, 192或256
                    //default aesAlg.BlockSize = 128;  IV的長度
                    aes1.Key = baKey;
                    aes1.IV = baIV;
                    byte[] baDecrypt = Decrypt(aes1, baCipher);
                    if (baDecrypt == null)
                        return null;
                    aes1.Clear();
                    return baDecrypt;
                }
            }
            catch (Exception e1)
            {
                msError = e1.Message;
                return null;
            }
        }

        /// <summary>
        /// 以 AES 演算法解密檔案. AES 同 Rijndael. baKey 應為(16 or 32) bytes, baIV 應為 16 bytes.
        /// </summary>
        /// <param name="sFileEncrypt"></param>
        /// <param name="sFileDecrypt"></param>
        /// <param name="baKey"></param>
        /// <param name="baIV"></param>
        /// <returns></returns>
        public static Boolean DecryptAES(string sFileEncrypt, string sFileDecrypt, byte[] baKey, byte[] baIV)
        {
            try
            {
                using (AesCryptoServiceProvider aes1 = new AesCryptoServiceProvider())
                {
                    aes1.Key = baKey;
                    aes1.IV = baIV;
                    Decrypt(aes1, sFileEncrypt, sFileDecrypt);
                    aes1.Clear();
                    return true;
                }
            }
            catch (Exception e1)
            {
                msError = e1.Message;
                return false;
            }
        }

        #endregion
    }
}
