using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


namespace DanKeTools.Utils.Console
{
    
///<summary>
///脚本名称： Console.cs
///修改时间：2022/12/8
///脚本功能：改变控制台颜色和大小
///备注：
///</summary>

public class font
{
    /// <summary>
    /// 自定义字体大小
    /// </summary>
    /// <param name="size">字体大小</param>
    /// <returns></returns>
    public static string size(int size,string msg)
    {
        msg = "<size=" + size + ">" + msg + "</size>";
        return msg;
    }


    /// <summary>
    /// 自定义颜色
    /// </summary>
    /// <param name="color">十六进制颜色</param>
    /// <returns></returns>
    public static string color(string color,string msg){
        msg = "<color=" + color + ">" + msg + "</color>";
        return msg;
    }
    
    /// <summary>
    /// 内置颜色
    /// </summary>
    public class _color{
        public static string black(string msg){
            msg = "<color=#FF000000>" + msg + "</color>";
            return msg;
        }
        public static string white(string msg){
            msg = "<color=#FFFFFFFF>" + msg + "</color>";
            return msg;
        }
        public static string red(string msg){
            msg = "<color=#FF0000>" + msg + "</color>";
            return msg;
        }
        public static string orange(string msg){
            msg = "<color=#FF7F00>" + msg + "</color>";
            return msg;
        }
        public static string yellow(string msg){
            msg = "<color=#FFFF00>" + msg + "</color>";
            return msg;
        }
        public static string green(string msg){
            msg = "<color=#00FF00>" + msg + "</color>";
           return msg;
        }
        public static string cyan(string msg){
            msg = "<color=#00FFFF>" + msg + "</color>";
            return msg;
        }
        public static string blue(string msg){
            msg = "<color=#0000FF>" + msg + "</color>";
            return msg;
        }
        public static string purple(string msg){
            msg = "<color=#8B00FF>" + msg + "</color>";
            return msg;
        }
    }

}

}