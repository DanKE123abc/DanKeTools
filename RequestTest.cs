using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DanKeTools.Net;
using DanKeTools.Fsm;

///<summary>
///脚本名称： RequestTest.cs
///修改时间：
///脚本功能：
///备注：
///</summary>

public class RequestTest : MonoBehaviour
{
    private Fsm<RequestTest> fsm;
    // Start is called before the first frame update
    void Start()
    {
        fsm = new Fsm<RequestTest>(this);
    }
}
