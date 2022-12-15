using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


namespace DanKeTools.Utils.ToJson
{
    ///<summary>
    ///脚本名称： ToJson.cs
    ///修改时间：2022/12/14
    ///脚本功能：转JSON
    ///备注：
    ///</summary>
    public static class ToJson
    {
        #region List，Dictionary 转字符串

        //需要转换什么类型，转换为什么格式自己重载就可以了
        //建议不要写成泛型的方式，为避免装箱拆箱。

        public static string ListToJson(int[] data)
        {
            StringBuilder content = new StringBuilder();
            content.Append("[");
            for (int i = 0; i < data.Length; i++)
            {
                if (i == data.Length - 1)
                    content.Append(data[i] + "]");
                else
                    content.Append(data[i] + ",");
            }

            return content.ToString();
        }

        public static string ListToJson(List<string> list)
        {
            StringBuilder content = new StringBuilder();
            content.Append("[");
            for (int i = 0; i < list.Count; i++)
            {
                content.Append(i + ":" + list[i]);
            }

            content.Append("]");
            return content.ToString();
        }

        public static string DicToJson(Dictionary<string, string> dictionary)
        {
            StringBuilder content = new StringBuilder();
            content.Append("{");
            foreach (var key in dictionary.Keys)
            {
                content.Append(key + ":" + dictionary[key] + ",");
            }

            content.Append("}");
            return content.ToString();
        }

        #endregion
    }

}
