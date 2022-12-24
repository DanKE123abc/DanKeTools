using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///脚本名称： AutoDestroyObject.cs
///修改时间：2022/12/11
///脚本功能：自动销毁Object
///备注：挂在物体上
///</summary>
[AddComponentMenu("DanKeTools/Auto Destroy")]
public class AutoDestroyObject : MonoBehaviour
{
    // 自动销毁到计时
    public float Delay;
    void Awake()
    {
        StartCoroutine(SelfDestroy(Delay));
    }

    private IEnumerator SelfDestroy(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
