using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DanKeTools;
using DanKeTools.UI;
using DanKeTools.IO;
using UnityEngine.Events;

namespace DanKeTools.UI
{


	///<summary>
	///脚本名称： UIManager.cs
	///修改时间：2022/12/26
	///脚本功能：UI层级管理器（需搭配预制UI使用）
	///备注：
	///</summary>

//UI层级枚举
	public enum E_UI_Layer
	{
		Bot,
		Mit,
		Top
	}


	public class UIManager : Singleton<UIManager>
	{
		public Dictionary<string, UIBasePanel> panelDic = new Dictionary<string, UIBasePanel>();

		//这是几个UI面板
		private Transform bot;
		private Transform mid;
		private Transform top;

		public UIManager() //初始化面板
		{
			GameObject obj = FileManager.Instance().Load<GameObject>("DanKeTools/UI/Canvas");
			Transform canvas = obj.transform;
			GameObject.DontDestroyOnLoad(obj);

			bot = canvas.Find("Bot");
			mid = canvas.Find("Mid");
			top = canvas.Find("Top");

			obj = FileManager.Instance().Load<GameObject>("DanKeTools/UI/EventSystem");
			GameObject.DontDestroyOnLoad(obj);
		}
	
		/// <summary>
		/// 显示面板
		/// </summary>
		/// <param name="panelName"></param>
		/// <param name="layer"></param>
		/// <param name="callback"></param>
		/// <typeparam name="T"></typeparam>
		public void ShowPanel<T>(string panelName, E_UI_Layer layer = E_UI_Layer.Top, UnityAction<T> callback = null)
			where T : UIBasePanel
		{
			//已经显示了此面板
			if (panelDic.ContainsKey(panelName))
			{
				//调用重写方法，具体内容自己添加
				panelDic[panelName].ShowMe();
				if (callback != null)
					callback(panelDic[panelName] as T);
				return;
			}

			FileManager.Instance().LoadAsync<GameObject>("DanKeTools/UI/" + panelName, (obj) =>
			{
				//把它作为Canvas的子对象
				//并且设置它的相对位置
				//找到父对象
				Transform father = bot;
				switch (layer)
				{
					case E_UI_Layer.Mit:
						father = mid;
						break;
					case E_UI_Layer.Top:
						father = top;
						break;
				}

				//设置父对象
				obj.transform.SetParent(father);

				//设置相对位置和大小
				obj.transform.localPosition = Vector3.zero;
				obj.transform.localScale = Vector3.one;

				(obj.transform as RectTransform).offsetMax = Vector2.zero;
				(obj.transform as RectTransform).offsetMin = Vector2.zero;

				//得到预设体身上的脚本（继承自BasePanel）
				T panel = obj.GetComponent<T>();

				//执行外面想要做的事情
				if (callback != null)
				{
					callback(panel);
				}

				//在字典中添加此面板
				panelDic.Add(panelName, panel);
			});
		}

		/// <summary>
		/// 隐藏面板
		/// </summary>
		/// <param name="panelName"></param>
		public void HidePanel(string panelName)
		{
			if (panelDic.ContainsKey(panelName))
			{
				//调用重写方法，具体内容自己添加
				panelDic[panelName].HideMe();
				GameObject.Destroy(panelDic[panelName].gameObject);
				panelDic.Remove(panelName);
			}
		}

	}

}