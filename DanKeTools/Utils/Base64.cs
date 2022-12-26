using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace DanKeTools.Utils.Base64
{


    ///<summary>
    ///脚本名称： Base64.cs
    ///修改时间：2022/12/26
    ///脚本功能：
    ///备注：
    ///</summary>

    public class Base64
    {
        // Base64编码
        public static string Encode(string message)
        {
            byte[] bytes = Encoding.GetEncoding("utf-8").GetBytes(message);
            return Convert.ToBase64String(bytes);
        }

        // Base64解码
        public static string Decode(string message)
        {
            byte[] bytes = Convert.FromBase64String(message);
            return Encoding.GetEncoding("utf-8").GetString(bytes);
        }
    }

}