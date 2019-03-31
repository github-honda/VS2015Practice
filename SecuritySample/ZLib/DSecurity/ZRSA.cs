// ZRSA.cs
// 20190326, Honda, Copy from ZLib.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// add
using System.Security.Cryptography;
//using System.Diagnostics;
using System.IO;

namespace ZLib.DSecurity
{
    public static class ZRSA
    {
        public static string msError { get; private set; }
        private static string myGetInfo(RSACryptoServiceProvider rsa1)
        {
            StringBuilder sb1 = new StringBuilder(null);

            sb1.AppendLine($".KeyExchangeAlgorithm={rsa1.KeyExchangeAlgorithm}.");
            sb1.AppendLine($".KeySize={rsa1.KeySize}.");  // 1024 bits or 128 bytes.
            sb1.AppendLine($".PersistKeyInCsp={rsa1.PersistKeyInCsp}");
            sb1.AppendLine($".PublicOnly={rsa1.PublicOnly}.");
            sb1.AppendLine($".SignatureAlgorithm={rsa1.SignatureAlgorithm}.");
            sb1.AppendLine($".ToXmlString(false)={rsa1.ToXmlString(false)}");
            sb1.AppendLine($".ToXmlString(true)={rsa1.ToXmlString(true)}");
            // registry key is not exist.
            //sb1.AppendLine($".CspKeyContainerInfo.Exportable={rsa1.CspKeyContainerInfo.Exportable}");
            sb1.AppendLine($".CspKeyContainerInfo.HardwareDevice={rsa1.CspKeyContainerInfo.HardwareDevice}");
            sb1.AppendLine($".CspKeyContainerInfo.KeyContainerName={rsa1.CspKeyContainerInfo.KeyContainerName}");
            sb1.AppendLine($".CspKeyContainerInfo.MachineKeyStore={rsa1.CspKeyContainerInfo.MachineKeyStore}");
            sb1.AppendLine($".CspKeyContainerInfo.Protected={rsa1.CspKeyContainerInfo.Protected}");

            // ProviderNamd and ProviderType
            // registry: HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Cryptography\Defaults (registry key Provider Type)
            // 24=Microsoft Enhanced RSA and AES Cryptographic Provider, 1=default="Microsoft Strong Cryptographic Provider", 1=舊版="Microsoft Base Cryptographic Provider"
            sb1.AppendLine($".CspKeyContainerInfo.ProviderName={rsa1.CspKeyContainerInfo.ProviderName}"); // https://docs.microsoft.com/en-us/windows/desktop/seccrypto/microsoft-cryptographic-service-providers
            // Default=24=PROV_RSA_AES, 1=PROV_RSA_FULL, defined in WinCrypt.h.
            sb1.AppendLine($".CspKeyContainerInfo.ProviderType={rsa1.CspKeyContainerInfo.ProviderType}"); // https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.cspparameters.providertype

            sb1.AppendLine($".CspKeyContainerInfo.RandomlyGenerated={rsa1.CspKeyContainerInfo.RandomlyGenerated}");
            sb1.AppendLine($".CspKeyContainerInfo.Removable={rsa1.CspKeyContainerInfo.Removable}");
            sb1.AppendLine($".CspKeyContainerInfo.UniqueKeyContainerName={rsa1.CspKeyContainerInfo.UniqueKeyContainerName}");
            //RSAParameters paraPublic = rsa1.ExportParameters(false);
            //RSAParameters paraAll = rsa1.ExportParameters(true);
            return sb1.ToString();
            // Samples:
            // Default: RSACryptoServiceProvider RSA = new RSACryptoServiceProvider().
            // .KeyExchangeAlgorithm=RSA-PKCS1-KeyEx.
            // .KeySize=1024.
            // .PersistKeyInCsp=False
            // .PublicOnly=False.
            // .SignatureAlgorithm=http://www.w3.org/2000/09/xmldsig#rsa-sha1.
            // .ToXmlString(false)=<RSAKeyValue><Modulus>pv5uDlbVkbfQLEUJDjqabrEJ0OdMWv+IaaP3TjoCLU8Ok8ki4jWSz5Xs6FdjeFFJVeFOIAZeLy2/iqCDAYcwyLAXA9TT0x6Mr73GMnfe8Xu2qfOesD8Zj6fL6HeMDuYCb1pStIRQhOMWb1akF/F3WkdzRKl5j7IFUmcgEy/d/+0=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>
            // .ToXmlString(true)=<RSAKeyValue><Modulus>pv5uDlbVkbfQLEUJDjqabrEJ0OdMWv+IaaP3TjoCLU8Ok8ki4jWSz5Xs6FdjeFFJVeFOIAZeLy2/iqCDAYcwyLAXA9TT0x6Mr73GMnfe8Xu2qfOesD8Zj6fL6HeMDuYCb1pStIRQhOMWb1akF/F3WkdzRKl5j7IFUmcgEy/d/+0=</Modulus><Exponent>AQAB</Exponent><P>2/RQcRPST3Ei6osvfLiNooacGK1/upoqz2yRLBWm0TTmrGVgwuG6GoMX41BSlJe2lRmdeWawz/nb5jW0J9tcTw==</P><Q>wlxHV0R6wp0P1PA6qmK/cuXFMkgqwrqBfbmKxH0V05M9zkl7f+/m8CkAY9wt4IyyvOuVe2ieI70qsa8rGSOlAw==</Q><DP>F+A7Yyrtp2X2jlMxgm5eIYhPJXaf6NeXUhDM6WYoR4lgiNIwYLc0BnC4hRpQ/IRDc4fmSilVhB3xOtoVhkYPjQ==</DP><DQ>rW3IHt+1JtkdWmXuA0HQEYdjueVZep8Pkw8v+vqeskctnFhHgjagfGYpufGrYjZJ+3e/z/nfOoa+hQSIUYtT3w==</DQ><InverseQ>ggn6j/R3zXr0YtQHaqwl01+MpBOxBaVnQwyFgkbzxDjRTRqTIx3cvbEITsIUqfa7V/TeVlU1fvnzXNlID+/lKw==</InverseQ><D>F57phlG2B9SON5TUQRT8/yc/kjMFU+HNM9QjiQcfg7UpBLJworqdN4unZP3nbf9hdipWWKVN8TJxY3Rs02yB88aJ4IKrfjyKL7fwcAEsizaEz147+jp8VQRfa1y0K3RagHi9vjsVbbkQ4ZuoeN73BGpS1WJOqrXAcWywnRnupOk=</D></RSAKeyValue>
            // .CspKeyContainerInfo.HardwareDevice=False
            // .CspKeyContainerInfo.KeyContainerName=
            // .CspKeyContainerInfo.MachineKeyStore=False
            // .CspKeyContainerInfo.Protected=False
            // .CspKeyContainerInfo.ProviderName=Microsoft Enhanced RSA and AES Cryptographic Provider
            // .CspKeyContainerInfo.ProviderType=24
            // .CspKeyContainerInfo.RandomlyGenerated=True
            // .CspKeyContainerInfo.Removable=False
            // .CspKeyContainerInfo.UniqueKeyContainerName=

            // Default: RSACryptoServiceProvider RSA = new RSACryptoServiceProvider().
            // .KeyExchangeAlgorithm=RSA-PKCS1-KeyEx.
            // .KeySize=1024.
            // .PersistKeyInCsp=False
            // .PublicOnly=False.
            // .SignatureAlgorithm=http://www.w3.org/2000/09/xmldsig#rsa-sha1.
            // .ToXmlString(false)=<RSAKeyValue><Modulus>uW5nX6hrDzcg+PUlPPFwwZauyxcUVKqSBnuEgDI/Jy3htFn00e4oSVuVsKXjJp0Y5wDj+w1zy0rL6lEA14cBDVNqUtNaKBL++A9j96OV9b9+FNq0NP3Zm0C+ahBuWKP7iJv+vDdwvIo9E6DRXw9bPPAFO3tf1Jj8P0V4AVYMcJ0=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>
            // .ToXmlString(true)=<RSAKeyValue><Modulus>uW5nX6hrDzcg+PUlPPFwwZauyxcUVKqSBnuEgDI/Jy3htFn00e4oSVuVsKXjJp0Y5wDj+w1zy0rL6lEA14cBDVNqUtNaKBL++A9j96OV9b9+FNq0NP3Zm0C+ahBuWKP7iJv+vDdwvIo9E6DRXw9bPPAFO3tf1Jj8P0V4AVYMcJ0=</Modulus><Exponent>AQAB</Exponent><P>9IrjGLOS3GWWKPdwLclpbQNuqGAyp2QV0JsEsjCgcQnj0Zw8jxcebcYWxqPg4VRCoSsnpp2BcWizJKDTOvZv8w==</P><Q>wh6E852PrNNGZi1c/Tl3sXfAKhiCExVBVVX7hbFtL5XB/PYm4CmKc/0wwpRNhuhsrKbqD6CGGtg2BPnRi5ZRLw==</Q><DP>F2NhLO+f0Cx3HBvuQrLMvnxhjQEGVzQfUgBBhrlX3vfPLjD/KcBQTLdxXjY2P9bLOJbkB+9wfeRBnfMzwKPpOQ==</DP><DQ>Mzl2gArd9Q8zjpb96NyXVz9weCtLd9muCeTMoLT3SQ1NyYDzNUgA/yabDV2xL555vv202jbT7JWNe7aosxsRhQ==</DQ><InverseQ>qZIGbV132YUfvkk+ZW/BPGtYVga8oxrcEfpKUcLlxoLuaOe4vEbN4QtEmca/vHmeWElIrJUTxI7r0ybzShSx4g==</InverseQ><D>t8Am4gzz4kUAAJR0zTp4QO3/0jLRq8DHKXNW7u5b1IhL5DtMNoepFmCSKlWIjegZGDiyskI3X9mDznNswScAKzCvnzeSwhEv5mqkrYKERWn0hFzeEZEyoOpqaZv0EhWHBW4JEsV1ltVZtJwh6quAxFtaFedKQnZ+YasFJcbxgcU=</D></RSAKeyValue>
            // .CspKeyContainerInfo.HardwareDevice=False
            // .CspKeyContainerInfo.KeyContainerName=
            // .CspKeyContainerInfo.MachineKeyStore=False
            // .CspKeyContainerInfo.Protected=False
            // .CspKeyContainerInfo.ProviderName=Microsoft Enhanced RSA and AES Cryptographic Provider
            // .CspKeyContainerInfo.ProviderType=24
            // .CspKeyContainerInfo.RandomlyGenerated=True
            // .CspKeyContainerInfo.Removable=False
            // .CspKeyContainerInfo.UniqueKeyContainerName=
        }
        private static RSACryptoServiceProvider myGenerateDefaultKey()
        {
            return new RSACryptoServiceProvider();
            // .KeyExchangeAlgorithm=RSA-PKCS1-KeyEx.
            // .KeySize=1024.
            // .PersistKeyInCsp=False
            // .PublicOnly=False.
            // .SignatureAlgorithm=http://www.w3.org/2000/09/xmldsig#rsa-sha1.
            // .CspKeyContainerInfo.HardwareDevice=False
            // .CspKeyContainerInfo.KeyContainerName=
            // .CspKeyContainerInfo.MachineKeyStore=False
            // .CspKeyContainerInfo.Protected=False
            // .CspKeyContainerInfo.ProviderName=Microsoft Enhanced RSA and AES Cryptographic Provider
            // .CspKeyContainerInfo.ProviderType=24
            // .CspKeyContainerInfo.RandomlyGenerated=True
            // .CspKeyContainerInfo.Removable=False
            // .CspKeyContainerInfo.UniqueKeyContainerName=
        }

        /// <summary>
        /// 從 MachineKeyStore 容器中, 取得已存在的 KeyContainer.
        /// </summary>
        /// <param name="sKeyContainerName"></param>
        /// <returns></returns>
        private static RSACryptoServiceProvider myUseExistingKey(string sKeyContainerName)
        {
            CspParameters para1 = new CspParameters(24);
            para1.KeyContainerName = sKeyContainerName;
            para1.Flags = CspProviderFlags.UseMachineKeyStore & CspProviderFlags.UseExistingKey;
            return new RSACryptoServiceProvider(para1);
            //// registry: HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Cryptography\Defaults (ProviderName, ProviderType)
            //para1.ProviderName = "Microsoft Enhanced RSA and AES Cryptographic Provider";
            //para1.ProviderType = 24;
        }

        /// <summary>
        /// 在 MachineKeyStore 容器中建立 KeyContainer. 
        /// </summary>
        /// <param name="sKeyContainerName"></param>
        /// <returns></returns>
        public static Boolean CreateContainer(string sKeyContainerName)
        {
            msError = string.Empty;
            try
            {
                CspParameters para1 = new CspParameters(24);
                para1.KeyContainerName = sKeyContainerName;
                para1.Flags = CspProviderFlags.UseMachineKeyStore;
                using (RSACryptoServiceProvider rsa1 = new RSACryptoServiceProvider(para1))
                {
                    //Debug.Print(myGetInfo(rsa1));
                    return true;
                }
                // diff. from default (new RSACryptoServiceProvider())
                // .KeyExchangeAlgorithm=RSA-PKCS1-KeyEx.
                // .KeySize=1024.
                // .PersistKeyInCsp=False  ----> True 
                // .PublicOnly=False.
                // .SignatureAlgorithm=http://www.w3.org/2000/09/xmldsig#rsa-sha1.
                // .CspKeyContainerInfo.HardwareDevice=False
                // .CspKeyContainerInfo.KeyContainerName=     ----> A
                // .CspKeyContainerInfo.MachineKeyStore=False
                // .CspKeyContainerInfo.Protected=False
                // .CspKeyContainerInfo.ProviderName=Microsoft Enhanced RSA and AES Cryptographic Provider
                // .CspKeyContainerInfo.ProviderType=24
                // .CspKeyContainerInfo.RandomlyGenerated=True   ----> False
                // .CspKeyContainerInfo.Removable=False
                // .CspKeyContainerInfo.UniqueKeyContainerName=   ----> long string
            }
            catch (CryptographicException ex)
            {
                msError = ex.Message;
                return false;
            }
            catch (Exception ex)
            {
                msError = ex.Message;
                return false;
            }
        }


        /// <summary>
        /// 在 MachineKeyStore 中檢查 KeyContainer 存在否 ? 注意若在 garbage collection 未清理前呼叫, 則仍可能會找到已經不存在的 KeyContainer.
        /// </summary>
        /// <param name="sKeyContainerName"></param>
        /// <returns></returns>
        public static Boolean ExistingContainer(string sKeyContainerName)
        {
            msError = string.Empty;
            try
            {
                using (RSACryptoServiceProvider rsa1 = myUseExistingKey(sKeyContainerName))
                {
                    return true;
                }
            }
            catch (CryptographicException ex)
            {
                msError = ex.Message;
                return false;
            }
            catch (Exception ex)
            {
                msError = ex.Message;
                return false;
            }

        }

        /// <summary>
        /// 在 MachineKeyStore 中刪除 KeyContainer. 注意不是立刻刪除, 實際是由 garbage collected.
        /// </summary>
        /// <param name="sKeyContainerName"></param>
        /// <returns></returns>
        public static Boolean DeleteContainer(string sKeyContainerName)
        {
            msError = string.Empty;
            try
            {
                RSACryptoServiceProvider rsa2 = myUseExistingKey(sKeyContainerName);
                rsa2.PersistKeyInCsp = false;
                rsa2.Clear();
                return true;
                // Set PersistKeyInCsp = false will cause the key container to be deleted when RSA instance is released or garbage collected. 
            }
            catch (CryptographicException ex)
            {
                msError = ex.Message;
                return false;
            }
            catch (Exception ex)
            {
                msError = ex.Message;
                return false;
            }

        }

        /// <summary>
        /// 從 MachineKeyStore 容器中, 取得已存在的公鑰.
        /// </summary>
        /// <param name="sKeyContainerName"></param>
        /// <returns></returns>
        public static string GetPublicKey(string sKeyContainerName)
        {
            msError = string.Empty;
            try
            {
                using (RSACryptoServiceProvider rsa1 = myUseExistingKey(sKeyContainerName))
                {
                    return rsa1.ToXmlString(false);
                }
            }
            catch (CryptographicException ex)
            {
                msError = ex.Message;
                return null;
            }
            catch (Exception ex)
            {
                msError = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 從 MachineKeyStore 容器中, 取得已存在的私鑰.
        /// </summary>
        /// <param name="sKeyContainerName"></param>
        /// <returns></returns>
        public static string GetPrivateKey(string sKeyContainerName)
        {
            msError = string.Empty;
            try
            {
                using (RSACryptoServiceProvider rsa1 = myUseExistingKey(sKeyContainerName))
                {
                    return rsa1.ToXmlString(true);
                }
            }
            catch (CryptographicException ex)
            {
                msError = ex.Message;
                return null;
            }
            catch (Exception ex)
            {
                msError = ex.Message;
                return null;
            }
        }


        /// <summary>
        /// 簽章原文.
        /// </summary>
        /// <param name="baToSign"></param>
        /// <param name="sPrivateKeyXML"></param>
        /// <returns></returns>
        public static byte[] SignDataSHA1(byte[] baToSign, string sPrivateKeyXML)
        {
            msError = string.Empty;
            try
            {
                using (RSACryptoServiceProvider rsa1 = new RSACryptoServiceProvider())
                {
                    rsa1.FromXmlString(sPrivateKeyXML);
                    RSAParameters para1 = rsa1.ExportParameters(true);
                    using (RSACryptoServiceProvider rsa2 = new RSACryptoServiceProvider())
                    {
                        rsa2.ImportParameters(para1);
                        // return rsa2.SignData(baToSign, new SHA1CryptoServiceProvider());
                        byte[] baDebug = rsa2.SignData(baToSign, new SHA1CryptoServiceProvider());
                        //Debug.Print($"2={baDebug.Length}.");
                        return baDebug;
                    }
                }
            }
            catch (CryptographicException ex)
            {
                msError = ex.Message;
                return null;
            }
            catch (Exception ex)
            {
                msError = ex.Message;
                return null;
            }
        }


        /// <summary>
        /// 驗證簽章. 驗證Hash方法為SHA1.
        /// </summary>
        /// <param name="baDecript"></param>
        /// <param name="sPublicKeyXML"></param>
        /// <param name="baSignature"></param>
        /// <returns></returns>
        public static Boolean VerifyDataSHA1(byte[] baDecript, string sPublicKeyXML, byte[] baSignature)
        {
            msError = string.Empty;
            try
            {
                using (RSACryptoServiceProvider rsa1 = new RSACryptoServiceProvider())
                {
                    rsa1.FromXmlString(sPublicKeyXML);
                    RSAParameters para1 = rsa1.ExportParameters(false);
                    using (RSACryptoServiceProvider rsa2 = new RSACryptoServiceProvider())
                    {
                        rsa2.ImportParameters(para1);
                        Boolean b1 = rsa2.VerifyData(baDecript, new SHA1CryptoServiceProvider(), baSignature);
                        return b1;
                    }
                }
            }
            catch (CryptographicException ex)
            {
                msError = ex.Message;
                return false;
            }
            catch (Exception ex)
            {
                msError = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 加密原文.
        /// </summary>
        /// <param name="baToEncrypt"></param>
        /// <param name="sPublicKeyXML"></param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] baToEncrypt, string sPublicKeyXML)
        {
            msError = string.Empty;
            try
            {
                using (RSACryptoServiceProvider rsa1 = new RSACryptoServiceProvider())
                {
                    rsa1.FromXmlString(sPublicKeyXML);
                    RSAParameters para1 = rsa1.ExportParameters(false);
                    using (RSACryptoServiceProvider rsa2 = new RSACryptoServiceProvider())
                    {
                        rsa2.ImportParameters(para1);
                        // OAEP padding: true to perform direct RSA encryption using OAEP padding (only available on a computer running Windows XP or later); otherwise, false to use PKCS#1 v1.5 padding.
                        return rsa2.Encrypt(baToEncrypt, false);
                    }
                }
            }
            catch (CryptographicException ex)
            {
                msError = ex.Message;
                return null;
            }
            catch (Exception ex)
            {
                msError = ex.Message;
                return null;
            }
        }




        /// <summary>
        /// 解密密文.
        /// </summary>
        /// <param name="baToDecrypt"></param>
        /// <param name="sPriviateKeyXML"></param>
        /// <returns></returns>
        public static byte[] Decrypt(byte[] baToDecrypt, string sPriviateKeyXML)
        {
            msError = string.Empty;
            try
            {
                using (RSACryptoServiceProvider rsa1 = new RSACryptoServiceProvider())
                {
                    rsa1.FromXmlString(sPriviateKeyXML);
                    RSAParameters para1 = rsa1.ExportParameters(true);
                    using (RSACryptoServiceProvider rsa2 = new RSACryptoServiceProvider())
                    {
                        rsa2.ImportParameters(para1);
                        // OAEP padding: true to perform direct RSA encryption using OAEP padding (only available on a computer running Windows XP or later); otherwise, false to use PKCS#1 v1.5 padding.
                        return rsa2.Decrypt(baToDecrypt, false);
                    }
                }
            }
            catch (CryptographicException ex)
            {
                msError = ex.Message;
                return null;
            }
            catch (Exception ex)
            {
                msError = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 取得 MachineKeyStore 的路徑.
        /// </summary>
        /// <returns></returns>
        public static string GetContainerFolder()
        {
            // example: C:\ProgramData\Microsoft\Crypto\RSA\MachineKeys\95e144418ae76df473da2336114fd064_a4dad12f-a58e-4644-ab87-15d2a1050eda
            string sFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), @"Microsoft\Crypto\RSA\MachineKeys");
            if (!Directory.Exists(sFolder))
                return null;
            return sFolder;
        }

        /// <summary>
        /// 取得 Container 的檔案名稱.
        /// </summary>
        /// <param name="sKeyContainerName"></param>
        /// <returns></returns>
        public static string GetContainerFileName(string sKeyContainerName)
        {
            CspParameters para1 = new CspParameters();
            para1.KeyContainerName = sKeyContainerName;
            para1.Flags = CspProviderFlags.UseMachineKeyStore & CspProviderFlags.UseExistingKey;

            CspKeyContainerInfo info = new CspKeyContainerInfo(para1);
            return info.UniqueKeyContainerName;
        }

        /// <summary>
        /// 取得 Container 的檔案全名.
        /// </summary>
        /// <param name="sKeyContainerName"></param>
        /// <returns></returns>
        public static string GetContainerFileFullName(string sKeyContainerName)
        {
            string sFile = Path.Combine(GetContainerFolder(), GetContainerFileName(sKeyContainerName));
            if (!File.Exists(sFile))
                return null;
            return sFile;
        }
    }
}
