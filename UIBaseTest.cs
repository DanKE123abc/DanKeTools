using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DanKeTools.UI;
using UnityEngine.UI;

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
        });

    }


}
