using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace DanKeTools.Utils.MD5
{

    ///<summary>
    ///脚本名称： MD5.cs
    ///修改时间：2022/12/26
    ///脚本功能：MD5
    ///备注：
    ///</summary>

    public class MD5
    {
        
        /// <summary>
        /// 计算字符串MD5
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string Md5String(string source)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.UTF8.GetBytes(source);
            byte[] md5Data = md5.ComputeHash(data, 0, data.Length);
            md5.Clear();

            string destString = "";
            for (int i = 0; i < md5Data.Length; i++)
            {
                destString += System.Convert.ToString(md5Data[i], 16).PadLeft(2, '0');
            }

            destString = destString.PadLeft(32, '0');
            return destString;

        }

        /// <summary>
        /// 计算文件MD5
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string Md5File(string file)
        {
            try
            {
                FileStream fs = new FileStream(file, FileMode.Open);
                string size = fs.Length / 1024 + "";
                //Debug.Log("当前文件的大小：  " + file + "===>" + (fs.Length / 1024) + "KB");
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(fs);
                fs.Close();

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }

                return sb + "|" + size;
            }
            catch (Exception ex)
            {
                throw new Exception("md5file() fail, error:" + ex.Message);
            }
        }

        /// <summary>
        /// MD5 16位加密
        /// </summary>
        /// <param name="_encryptContent">需要加密的内容</param>
        /// <returns></returns>
        public static string EncryptMD5_16(string _encryptContent)
        {
            var md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(_encryptContent)), 4, 8);
            t2 = t2.Replace("-", "");
            return t2;
        }

        /// <summary>
        /// MD5　32位加密
        /// </summary>
        /// <param name="_encryptContent">需要加密的内容</param>
        /// <returns></returns>
        public static string EncryptMD5_32(string _encryptContent)
        {
            string content_Normal = _encryptContent;
            string content_Encrypt = "";
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();

            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(content_Normal));

            for (int i = 0; i < s.Length; i++)
            {
                content_Encrypt = content_Encrypt + s[i].ToString("X2");
            }

            return content_Encrypt;
        }

        /// <summary>
        /// MD5 64位加密
        /// </summary>
        /// <param name="_encryptContent">需要加密的内容</param>
        /// <returns></returns>
        public static string EncryptMD5_64(string _encryptContent)
        {
            string content = _encryptContent;
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(content));
            return Convert.ToBase64String(s);
        }


    }

}
