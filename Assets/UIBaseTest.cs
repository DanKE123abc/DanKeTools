using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DanKeTools.UI;
using UnityEngine.UI;
using DanKeTools.Scene;

///<summary>
///脚本名称： UIBaseTest.cs
///修改时间：
///脚本功能：
///备注：
///</summary>

public class UIBaseTest : UIBasePanel
{
    void Start()
    {
        GetControl<Button>("B1").onClick.AddListener(() =>
	    {
	        Debug.Log("B1按下");
	    });
        GetControl<Button>("B2").onClick.AddListener(() =>
        {
            Debug.Log("B2按下");
            SceneManager.Instance().LoadScene("scene1",load);
        });

    }

    void load()
    {
        Debug.Log("加载成功");
    }


}
