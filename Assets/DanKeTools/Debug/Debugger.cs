using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.Profiling;
using UnityEngine.Serialization;

///<summary>
///脚本名称： Debugger.cs
///修改时间：2023/2/24
///脚本功能：游戏内控制台
///备注：
///</summary>

[AddComponentMenu("DanKeTools/Debugger")]
public struct LogData
{
    public string Time;
    public string Type;
    public string Message;
    public string StackTrace;
}

public enum DebugType
{
    Console,
    Memory,
    System,
    Screen,
    Quality,
    Environment
}

public class Debugger : MonoBehaviour 
{
    /// <summary>
    /// 是否允许调试
    /// </summary>
    [FormerlySerializedAs("开启游戏内控制台")] public bool allowDebugging = true;
    private DebugType debugType = DebugType.Console;
    private List<LogData> logInformations = new List<LogData>();
    private int currentLogIndex = -1;
    private int infoLogCount = 0;
    private int warningLogCount = 0;
    private int errorLogCount = 0;
    private int fatalLogCount = 0;
    private bool showInfoLog = true;
    private bool showWarningLog = true;
    private bool showErrorLog = true;
    private bool showFatalLog = true;
    private Vector2 scrollLogView = Vector2.zero;
    private Vector2 scrollCurrentLogView = Vector2.zero;
    private Vector2 scrollSystemView = Vector2.zero;
    private bool expansion = false;
    private Rect windowRect = new Rect(0, 0, 100, 60);

    private int fps = 0;
    private Color fpsColor = Color.white;
    private int frameNumber = 0;
    private float lastShowFPSTime = 0f;

    private void Start () 
    {
        if (allowDebugging)
        {
            Application.logMessageReceived += LogHandler;
        }
    }
    private void Update()
    {
        if (allowDebugging)
        {
            frameNumber += 1;
            float time = Time.realtimeSinceStartup - lastShowFPSTime;
            if (time >= 1)
            {
                fps = (int)(frameNumber / time);
                frameNumber = 0;
                lastShowFPSTime = Time.realtimeSinceStartup;
            }
        }
    }
    private void OnDestory()
    {
        if (allowDebugging)
        {
            Application.logMessageReceived -= LogHandler;
        }
    }
    private void LogHandler(string condition, string stackTrace, LogType type)
    {
        LogData log = new LogData();
        log.Time = DateTime.Now.ToString("HH:mm:ss");
        log.Message = condition;
        log.StackTrace = stackTrace;

        if (type == LogType.Assert)
        {
            log.Type = "Fatal";
            fatalLogCount += 1;
        }
        else if (type == LogType.Exception || type == LogType.Error)
        {
            log.Type = "Error";
            errorLogCount += 1;
        }
        else if (type == LogType.Warning)
        {
            log.Type = "Warning";
            warningLogCount += 1;
        }
        else if (type == LogType.Log)
        {
            log.Type = "Info";
            infoLogCount += 1;
        }

        logInformations.Add(log);

        if (warningLogCount > 0)
        {
            fpsColor = Color.yellow;
        }
        if (errorLogCount > 0)
        {
            fpsColor = Color.red;
        }
    }

    private void OnGUI()
    {
        if (allowDebugging)
        {
            if (expansion)
            {
                windowRect = GUI.Window(0, windowRect, ExpansionGUIWindow, "控制台");
            }
            else
            {
                windowRect = GUI.Window(0, windowRect, ShrinkGUIWindow, "控制台");
            }
        }
    }
    private void ExpansionGUIWindow(int windowId)
    {
        GUI.DragWindow(new Rect(0, 0, 15000, 20));

        #region title
        GUILayout.BeginHorizontal();
        GUI.contentColor = fpsColor;
        if (GUILayout.Button("帧率:" + fps, GUILayout.Height(30)))
        {
            expansion = false;
            windowRect.width = 100;
            windowRect.height = 60;
        }
        
        GUI.contentColor = (debugType == DebugType.Console ? Color.white : Color.gray);
        if (GUILayout.Button("调试", GUILayout.Height(30)))
        {
            debugType = DebugType.Console;
        }
        
        GUI.contentColor = (debugType == DebugType.Memory ? Color.white : Color.gray);
        if (GUILayout.Button("内存", GUILayout.Height(30)))
        {
            debugType = DebugType.Memory;
        }
        
        GUI.contentColor = (debugType == DebugType.System ? Color.white : Color.gray);
        if (GUILayout.Button("系统", GUILayout.Height(30)))
        {
            debugType = DebugType.System;
        }
        
        GUI.contentColor = (debugType == DebugType.Screen ? Color.white : Color.gray);
        if (GUILayout.Button("屏幕", GUILayout.Height(30)))
        {
            debugType = DebugType.Screen;
        }
        
        GUI.contentColor = (debugType == DebugType.Quality ? Color.white : Color.gray);
        if (GUILayout.Button("质量", GUILayout.Height(30)))
        {
            debugType = DebugType.Quality;
        }
        
        GUI.contentColor = (debugType == DebugType.Environment ? Color.white : Color.gray);
        if (GUILayout.Button("环境", GUILayout.Height(30)))
        {
            debugType = DebugType.Environment;
        }
        GUI.contentColor = Color.white;
        GUILayout.EndHorizontal();
        #endregion

        #region console
        if (debugType == DebugType.Console)
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("清除"))
            {
                logInformations.Clear();
                fatalLogCount = 0;
                warningLogCount = 0;
                errorLogCount = 0;
                infoLogCount = 0;
                currentLogIndex = -1;
                fpsColor = Color.white;
            }
            GUI.contentColor = (showInfoLog ? Color.white : Color.gray);
            showInfoLog = GUILayout.Toggle(showInfoLog, "Info [" + infoLogCount + "]");
            GUI.contentColor = (showWarningLog ? Color.white : Color.gray);
            showWarningLog = GUILayout.Toggle(showWarningLog, "Warning [" + warningLogCount + "]");
            GUI.contentColor = (showErrorLog ? Color.white : Color.gray);
            showErrorLog = GUILayout.Toggle(showErrorLog, "Error [" + errorLogCount + "]");
            GUI.contentColor = (showFatalLog ? Color.white : Color.gray);
            showFatalLog = GUILayout.Toggle(showFatalLog, "Fatal [" + fatalLogCount + "]");
            GUI.contentColor = Color.white;
            GUILayout.EndHorizontal();

            scrollLogView = GUILayout.BeginScrollView(scrollLogView, "Box", GUILayout.Height(165));
            for (int i = 0; i < logInformations.Count; i++)
            {
                bool show = false;
                Color color = Color.white;
                switch (logInformations[i].Type)
                {
                    case "Fatal":
                        show = showFatalLog;
                        color = Color.red;
                        break;
                    case "Error":
                        show = showErrorLog;
                        color = Color.red;
                        break;
                    case "Info":
                        show = showInfoLog;
                        color = Color.white;
                        break;
                    case "Warning":
                        show = showWarningLog;
                        color = Color.yellow;
                        break;
                    default:
                        break;
                }

                if (show)
                {
                    GUILayout.BeginHorizontal();
                    if (GUILayout.Toggle(currentLogIndex == i, ""))
                    {
                        currentLogIndex = i;
                    }
                    GUI.contentColor = color;
                    GUILayout.Label("[" + logInformations[i].Type + "] ");
                    GUILayout.Label("[" + logInformations[i].Time + "] ");
                    GUILayout.Label(logInformations[i].Message);
                    GUILayout.FlexibleSpace();
                    GUI.contentColor = Color.white;
                    GUILayout.EndHorizontal();
                }
            }
            GUILayout.EndScrollView();

            scrollCurrentLogView = GUILayout.BeginScrollView(scrollCurrentLogView, "Box", GUILayout.Height(100));
            if (currentLogIndex != -1)
            {
                GUILayout.Label(logInformations[currentLogIndex].Message + "\r\n\r\n" + logInformations[currentLogIndex].StackTrace);
            }
            GUILayout.EndScrollView();
        }
        #endregion

        #region memory
        else if (debugType == DebugType.Memory)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Memory Information");
            GUILayout.EndHorizontal();

            GUILayout.BeginVertical("Box");
#if UNITY_5
            GUILayout.Label("总内存：" + Profiler.GetTotalReservedMemory() / 1000000 + "MB");
            GUILayout.Label("已占用内存：" + Profiler.GetTotalAllocatedMemory() / 1000000 + "MB");
            GUILayout.Label("空闲中内存：" + Profiler.GetTotalUnusedReservedMemory() / 1000000 + "MB");
            GUILayout.Label("总Mono堆内存：" + Profiler.GetMonoHeapSize() / 1000000 + "MB");
            GUILayout.Label("已占用Mono堆内存：" + Profiler.GetMonoUsedSize() / 1000000 + "MB");
#endif
#if UNITY_7
            GUILayout.Label("总内存：" + Profiler.GetTotalReservedMemoryLong() / 1000000 + "MB");
            GUILayout.Label("已占用内存：" + Profiler.GetTotalAllocatedMemoryLong() / 1000000 + "MB");
            GUILayout.Label("空闲中内存：" + Profiler.GetTotalUnusedReservedMemoryLong() / 1000000 + "MB");
            GUILayout.Label("总Mono堆内存：" + Profiler.GetMonoHeapSizeLong() / 1000000 + "MB");
            GUILayout.Label("已占用Mono堆内存：" + Profiler.GetMonoUsedSizeLong() / 1000000 + "MB");
#endif
            GUILayout.EndVertical();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("卸载未使用的资源"))
            {
                Resources.UnloadUnusedAssets();
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("使用GC垃圾回收"))
            {
                GC.Collect();
            }
            GUILayout.EndHorizontal();
        }
#endregion

        #region system
        else if (debugType == DebugType.System)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("System Information");
            GUILayout.EndHorizontal();

            scrollSystemView = GUILayout.BeginScrollView(scrollSystemView, "Box");
            GUILayout.Label("操作系统：" + SystemInfo.operatingSystem);
            GUILayout.Label("系统内存：" + SystemInfo.systemMemorySize + "MB");
            GUILayout.Label("处理器：" + SystemInfo.processorType);
            GUILayout.Label("处理器数量：" + SystemInfo.processorCount);
            GUILayout.Label("显卡：" + SystemInfo.graphicsDeviceName);
            GUILayout.Label("显卡类型：" + SystemInfo.graphicsDeviceType);
            GUILayout.Label("显存：" + SystemInfo.graphicsMemorySize + "MB");
            GUILayout.Label("显卡标识：" + SystemInfo.graphicsDeviceID);
            GUILayout.Label("显卡供应商：" + SystemInfo.graphicsDeviceVendor);
            GUILayout.Label("显卡供应商标识码：" + SystemInfo.graphicsDeviceVendorID);
            GUILayout.Label("设备模式：" + SystemInfo.deviceModel);
            GUILayout.Label("设备名称：" + SystemInfo.deviceName);
            GUILayout.Label("设备类型：" + SystemInfo.deviceType);
            GUILayout.Label("设备标识：" + SystemInfo.deviceUniqueIdentifier);
            GUILayout.EndScrollView();
        }
        #endregion

        #region screen
        else if (debugType == DebugType.Screen)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Screen Information");
            GUILayout.EndHorizontal();

            GUILayout.BeginVertical("Box");
            GUILayout.Label("DPI：" + Screen.dpi);
            GUILayout.Label("分辨率：" + Screen.currentResolution.ToString());
            GUILayout.EndVertical();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("全屏"))
            {
                Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, !Screen.fullScreen);
            }
            GUILayout.EndHorizontal();
        }
        #endregion

        #region Quality
        else if (debugType == DebugType.Quality)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Quality Information");
            GUILayout.EndHorizontal();

            GUILayout.BeginVertical("Box");
            string value = "";
            if (QualitySettings.GetQualityLevel() == 0)
            {
                value = " [最低]";
            }
            else if (QualitySettings.GetQualityLevel() == QualitySettings.names.Length - 6)
            {
                value = " [较低]";
            }
            else if (QualitySettings.GetQualityLevel() == QualitySettings.names.Length - 5)
            {
                value = " [低]";
            }
            else if (QualitySettings.GetQualityLevel() == QualitySettings.names.Length - 4)
            {
                value = " [中]";
            }
            else if (QualitySettings.GetQualityLevel() == QualitySettings.names.Length - 3)
            {
                value = " [高]";
            }
            else if (QualitySettings.GetQualityLevel() == QualitySettings.names.Length - 2)
            {
                value = " [较高]";
            }
            else if (QualitySettings.GetQualityLevel() == QualitySettings.names.Length - 1)
            {
                value = " [最高]";
            }

            GUILayout.Label("图形质量：" + QualitySettings.names[QualitySettings.GetQualityLevel()] + value);
            GUILayout.EndVertical();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("降低一级图形质量"))
            {
                QualitySettings.DecreaseLevel();
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("提升一级图形质量"))
            {
                QualitySettings.IncreaseLevel();
            }
            GUILayout.EndHorizontal();
        }
        #endregion

        #region Environment
        else if (debugType == DebugType.Environment)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Environment Information");
            GUILayout.EndHorizontal();

            GUILayout.BeginVertical("Box");
            GUILayout.Label("项目名称：" + Application.productName);
#if UNITY_5
            GUILayout.Label("项目ID：" + Application.bundleIdentifier);
#endif
#if UNITY_7
            GUILayout.Label("项目ID：" + Application.identifier);
#endif
            GUILayout.Label("项目版本：" + Application.version);
            GUILayout.Label("Unity版本：" + Application.unityVersion);
            GUILayout.Label("公司名称：" + Application.companyName);
            GUILayout.EndVertical();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("退出程序"))
            {
                Application.Quit();
            }
            GUILayout.EndHorizontal();
        }
#endregion
    }
    private void ShrinkGUIWindow(int windowId)
    {
        GUI.DragWindow(new Rect(0, 0, 10000, 20));

        GUI.contentColor = fpsColor;
        if (GUILayout.Button("FPS:" + fps, GUILayout.Width(80), GUILayout.Height(30)))
        {
            expansion = true;
            windowRect.width = 600;
            windowRect.height = 360;
        }
        GUI.contentColor = Color.white;
    }
}


