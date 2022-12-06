using UnityEngine;
using UnityEngine.Events;

///<summary>
///脚本名称： MonoController.cs
///修改时间：
///脚本功能：
///备注：
///</summary>

namespace DanKeTools.MonoManger
{

public class MonoController : MonoBehaviour
{
    private event UnityAction updateEvent;
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);   
    }

     void Update()
    {
        if (updateEvent != null)
        {
            updateEvent();
        }
    }

    /// <summary>
    /// 给外部提供的 添加帧更新事件的函数
    /// </summary>
    /// <param name="function"></param>
    public void AddUpdateListener(UnityAction function)
    {
        updateEvent += function;
    }

    /// <summary>
    /// 提供给外部用于移除帧更新事件函数
    /// </summary>
    /// <param name="function"></param>
    public void RemoveUpdateListener(UnityAction function)
    {
        updateEvent -= function;
    }

}

}