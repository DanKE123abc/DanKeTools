using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DanKeTools.Net;
using Unity.VisualScripting;

///<summary>
///脚本名称： NewBehaviourScript.cs
///修改时间：
///脚本功能：
///备注：
///</summary>

public class NewBehaviourScript : MonoBehaviour
{
    void Start()
    {
        Debug.Log(OnlineMT.MT_static("你好，世界",OnlineMT.Language.en));
    }

    void Update()
    {
        
    }
}
