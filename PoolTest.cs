using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DanKeTools.Pool;

///<summary>
///脚本名称： PoolTest.cs
///修改时间：
///脚本功能：
///备注：
///</summary>

public class PoolTest : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject go = PoolManager.Instance().GetObject("Cube");
            StartCoroutine(Back(go));
        }

        if (Input.GetMouseButtonDown(1))
        {
            GameObject go = PoolManager.Instance().GetObject("Sphere");
            StartCoroutine(Back(go));
        }
    }

    IEnumerator Back(GameObject go)
    {
        yield return new WaitForSeconds(10f);
        PoolManager.Instance().PushObject(go.name, go);
    }
}
