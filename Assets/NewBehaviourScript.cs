using System;
using System.Collections;
using System.Collections.Generic;
using DanKeTools.UI;
using UnityEngine;
using UnityEngine.UI;
using DanKeTools.Log;
using Debug = DanKeTools.Log.Debug;

///<summary>
///脚本名称： NewBehaviourScript.cs
///修改时间：
///脚本功能：
///备注：
///</summary>

public class NewBehaviourScript : UIBasePanel
{
    void Start()
    {
        GetControl<Button>("Button").onClick.AddListener(o);
    }

    void o()
    {
        Debug.Log("hello");
        Debug.LogWarning("warning");
        Debug.LogError("error");
        Debug.LogAssertion("Assertion");
        Debug.LogException(new Exception("exception"));
        Debug.LogFormat("format");
        Debug.LogAssertionFormat("AF");
        Debug.LogErrorFormat("EF");
        Debug.LogWarningFormat("WF");
    }
}
