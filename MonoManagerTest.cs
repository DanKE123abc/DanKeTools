using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DanKeTools.Mono;
using DanKeTools.Net;
using DanKeTools.Utils.StringEncryption;

///<summary>
///脚本名称： MonoManagerTest.cs
///修改时间：
///脚本功能：
///备注：
///</summary>

public class Test
{
    public void Update()
    {
        //Debug.Log("来自Test的Update");
    }

    public Test()
    {
        //开启协程
        MonoManager.Instance().StartCoroutine(这是协程());
    }
    IEnumerator 这是协程()
    {
        yield return new WaitForSeconds(1f);
        Debug.LogError("协程：等了1秒");
    }

}
public class MonoManagerTest : MonoBehaviour
{
    void Start()
    {
        Test t = new Test();
        MonoManager.Instance().AddUpdateListener(t.Update);
    }

}
