using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DanKeTools.Net;

///<summary>
///脚本名称： RequestTest.cs
///修改时间：
///脚本功能：
///备注：
///</summary>

public class RequestTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Requests.Get("http://httpbin.org/get", ""));
    }
}
