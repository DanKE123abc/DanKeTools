using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///脚本名称： DontDestroyObject.cs
///修改时间：2022/12/11
///脚本功能：不销毁该Object
///备注：挂在物体上
///</summary>

public class DontDestroyObject : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
