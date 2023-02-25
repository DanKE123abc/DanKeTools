using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DanKeTools.Log
{

    ///<summary>
    ///脚本名称： LogManager.cs
    ///修改时间：2023/2/25
    ///脚本功能：打印炫酷的Log信息
    ///备注：
    ///</summary>
    
    public class Debug
    {
        public static bool EnableLog = true;

        public static void DrawLine(Vector3 start, Vector3 end, Color color, float duration)
        {
            if (EnableLog)
            {
                UnityEngine.Debug.DrawLine(start, end, color, duration);
            }
        }

        public static void DrawLine(Vector3 start, Vector3 end)
        {
            if (EnableLog)
            {
                UnityEngine.Debug.DrawLine(start, end);
            }
        }

        public static void DrawLine(Vector3 start, Vector3 end, Color color)
        {
            if (EnableLog)
            {
                UnityEngine.Debug.DrawLine(start, end, color);
            }
        }

        public static void Log(object message, UnityEngine.Object context)
        {
            if (EnableLog)
            {
                UnityEngine.Debug.Log(string.Format("<b><color={0}><size={1}>{2}</size></color></b>", "#DDDDDD", 13, message), context);
            }
        }

        public static void Log(object message)
        {
            if (EnableLog)
            {
                UnityEngine.Debug.Log(string.Format("<b><color={0}><size={1}>{2}</size></color></b>", "#DDDDDD", 13, message));
            }
        }

        public static void LogAssertion(object message)
        {
            if (EnableLog)
            {
                UnityEngine.Debug.LogAssertion(string.Format("<b><color={0}><size={1}>{2}</size></color></b>", "red", 13,
                    message));
            }
        }

        public static void LogAssertionFormat(string format, params object[] args)
        {
            if (EnableLog)
            {
                UnityEngine.Debug.LogAssertionFormat(string.Format("<b><color={0}><size={1}>{2}</size></color></b>", "red", 13,
                    format), args);
            }
        }

        public static void LogError(object message, UnityEngine.Object context)
        {
            if (EnableLog)
            {
                UnityEngine.Debug.LogError(string.Format("<b><color={0}><size={1}>{2}</size></color></b>", "red", 13,
                    message), context);
            }
        }

        public static void LogError(object message)
        {
            if (EnableLog)
            {
                UnityEngine.Debug.LogError(string.Format("<b><color={0}><size={1}>{2}</size></color></b>", "red", 13,
                    message));
            }
        }

        public static void LogErrorFormat(string format, params object[] args)
        {
            if (EnableLog)
            {
                UnityEngine.Debug.LogErrorFormat(string.Format("<b><color={0}><size={1}>{2}</size></color></b>", "red", 13,
                    format), args);
            }
        }

        public static void LogException(Exception exception)
        {
            if (EnableLog)
            {
                UnityEngine.Debug.LogException(new Exception(string.Format("<b><color={0}><size={1}>{2}</size></color></b>", "red", 12,
                    exception)));
            }
        }

        public static void LogFormat(string format, params object[] args)
        {
            if (EnableLog)
            {
                UnityEngine.Debug.LogFormat(string.Format("<b><color={0}><size={1}>{2}</size></color></b>", "#DDDDDD", 13, format), args);
            }
        }

        public static void LogWarning(object message)
        {
            if (EnableLog)
            {
                UnityEngine.Debug.LogWarning(string.Format("<b><color={0}><size={1}>{2}</size></color></b>", "yellow", 13,
                    message));
            }
        }

        public static void LogWarning(object message, UnityEngine.Object context)
        {
            if (EnableLog)
            {
                UnityEngine.Debug.LogWarning(string.Format("<b><color={0}><size={1}>{2}</size></color></b>", "yellow", 13,
                    message), context);
            }
        }

        public static void LogWarningFormat(string format, params object[] args)
        {
            if (EnableLog)
            {
                UnityEngine.Debug.LogWarningFormat(string.Format("<b><color={0}><size={1}>{2}</size></color></b>", "yellow", 13,
                    format), args);
            }
        }
    }
}
