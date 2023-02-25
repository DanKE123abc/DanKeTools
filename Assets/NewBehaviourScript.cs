using System.Collections;
using System.Collections.Generic;
using DanKeTools.UI;
using UnityEngine;
using UnityEngine.UI;

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
    }
}
