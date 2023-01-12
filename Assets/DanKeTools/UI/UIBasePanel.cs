using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DanKeTools.UI
{

    ///<summary>
    ///脚本名称： UIBasePanel.cs
    ///修改时间：2022/12/6
    ///脚本功能：UI基类面板
    ///备注：
    ///</summary>

    public class UIBasePanel : MonoBehaviour
    {
        //通过里式转换原则，来存储所有的UI控件
        private Dictionary<string, List<UIBehaviour>> controlDic = new Dictionary<string, List<UIBehaviour>>();

        private void Awake()
        {
            //控件并保存在字典中，可根据自己UI搭载的控件更改
            FinChildControl<Button>();
            FinChildControl<Image>();
            FinChildControl<Scrollbar>();
            FinChildControl<Text>();
        }

        private void FinChildControl<T>() where T : UIBehaviour
        {
            T[] controls = this.GetComponentsInChildren<T>(); //注意用的是GetComents,返回的是一组
            string objName; //使用名字对控件按物体分类
            for (int i = 0; i < controls.Length; i++)
            {
                objName = controls[i].gameObject.name;
                if (controlDic.ContainsKey(objName))
                {
                    controlDic[objName]
                        .Add(controls[i]); //注意controlDic中存储的类型为<string, List<UIBehaviour>,所以我们的同一个UI的所有控件都在这个链表中
                }
                else
                {
                    controlDic.Add(objName, new List<UIBehaviour>() { controls[i] }); //添加UI到字典中
                }
            }
        }


        protected T GetControl<T>(string controlName) where T : UIBehaviour
        {
            if (controlDic.ContainsKey(controlName))
            {
                for (int i = 0; i < controlDic[controlName].Count; i++)
                {
                    if (controlDic[controlName][i] is T) //使用is判断是否为T控件
                        return controlDic[controlName][i] as T;
                }
            }

            return null;
        }

        //虚函数，可在继承类中自己定义
        public virtual void ShowMe()
        {

        }

        //同上
        public virtual void HideMe()
        {

        }


    }

}