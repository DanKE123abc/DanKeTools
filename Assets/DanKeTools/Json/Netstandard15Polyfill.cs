#if NETSTANDARD1_5
using System;
using System.Reflection;
namespace DanKeTools.Json
{
    ///<summary>
    /// Developed on the basis of LitJson
    ///脚本名称： Netstandard15Polyfill.cs
    ///修改时间：2023/2/18
    ///脚本功能：
    ///备注：
    ///</summary>


    internal static class Netstandard15Polyfill
    {
        internal static Type GetInterface(this Type type, string name)
        {
            return type.GetTypeInfo().GetInterface(name); 
        }

        internal static bool IsClass(this Type type)
        {
            return type.GetTypeInfo().IsClass;
        }

        internal static bool IsEnum(this Type type)
        {
            return type.GetTypeInfo().IsEnum;
        }
    }
}
#endif