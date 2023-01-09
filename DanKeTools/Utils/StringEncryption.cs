using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace DanKeTools.Utils.StringEncryption
{

    ///<summary>
    ///脚本名称： StringEncryption.cs
    ///修改时间：2023/1/9
    ///脚本功能：字符串加解密
    ///备注：
    ///</summary>

    public class StringEncryption
    {
        private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public static string DesEncrypt(string encryptString, string encryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream =
                    new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                cStream.Close();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return encryptString;
            }
        }

        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥同样</param>
        /// <returns>解密成功返回解密后的字符串，失败返回源串</returns>
        public static string DesDecrypt(string decryptString, string decryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream =
                    new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                cStream.Close();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return decryptString;
            }
        }

        /// <summary>
        /// 获取密钥对
        /// </summary>
        /// <returns>Encrypt,Decrypt</returns>
        public static KeyValuePair<string, string> GetRsaKeyPair()
        {
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
            string publicKey = RSA.ToXmlString(false);
            string privateKey = RSA.ToXmlString(true);
            return new KeyValuePair<string, string>(publicKey, privateKey);
        }

        /// <summary>
        /// RSA加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">公钥</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public static string RsaEncrypt(string encryptString, string encryptKey)
        {
            try
            {
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(encryptKey);
                UnicodeEncoding ByteConverter = new UnicodeEncoding();
                byte[] DataToEncrypt = ByteConverter.GetBytes(encryptString);
                byte[] resultBytes = rsa.Encrypt(DataToEncrypt, false);
                return Convert.ToBase64String(resultBytes);
            }
            catch
            {
                return encryptString;
            }
        }

        /// <summary>
        /// RSA解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">私钥</param>
        /// <returns>解密成功返回解密后的字符串，失败返回源串</returns>
        public static string RsaDecrypt(string decryptString, string decryptKey)
        {
            try
            {
                byte[] dataToDecrypt = Convert.FromBase64String(decryptString);
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                RSA.FromXmlString(decryptKey);
                byte[] resultBytes = RSA.Decrypt(dataToDecrypt, false);
                UnicodeEncoding ByteConverter = new UnicodeEncoding();
                return ByteConverter.GetString(resultBytes);
            }
            catch
            {
                return decryptString;
            }

        }

    }

}
