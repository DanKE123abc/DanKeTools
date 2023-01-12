using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DanKeTools;
using Unity.VisualScripting;

namespace DanKeTools.UI
{

    ///<summary>
    ///脚本名称： UITriggerManager.cs
    ///修改时间：2022/12/26
    ///脚本功能：
    ///备注：
    ///</summary>


    public enum UIEventTriggerType
    {
        PointerEnter,
        PointerExit,
        PointerDown,
        PointerUp,
        PointerClick,
        Drag,
        Drop,
        Scroll,
        UpdateSelected,
        Select,
        Deselect,
        Move,
        InitializePotentialDrag,
        BeginDrag,
        EndDrag,
        Submit,
        Cancel,
    }

    public class UITriggerManager : Singleton<UITriggerManager>
    {

        [HideInInspector]
        public delegate void MyMehod(BaseEventData baseEventData); //方法     

        /// <summary>
        /// 调用该函数即可添加各类方法
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="eventTriggerType"></param>
        /// <param name="myMehod"></param>
        public void AddTriggersListener(GameObject obj, UIEventTriggerType eventTriggerType, MyMehod myMehod)
        {
            EventTrigger ET = obj.GetComponent<EventTrigger>();
            if (ET == null)
            {
                ET = obj.AddComponent<EventTrigger>();
            } //1, 给需要的物体添加事件的组件EventerTrigger

            if (ET.triggers.Count == 0)
            {
                ET.triggers = new List<EventTrigger.Entry>(); //2.初始化EventTrigger.Entry容器
            }

            //实例化一个EventTrigger.Entry对象
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = (EventTriggerType)eventTriggerType; //3.指定事件触发的类型
            UnityAction<BaseEventData> callBack = new UnityAction<BaseEventData>(myMehod); //4.指定事件触发的方法
            entry.callback.AddListener(callBack); //事件加入到entry
            ET.triggers.Add(entry); //entry加入到EventTrigger
        }
        
        /// <summary>
        /// 调用该函数即可添加各类方法
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="eventTriggerType"></param>
        /// <param name="myMehod"></param>
        public void AddTriggersListener(UIBehaviour obj, UIEventTriggerType eventTriggerType, MyMehod myMehod)
        {
            EventTrigger ET = obj.GetComponent<EventTrigger>();
            if (ET == null)
            {
                ET = obj.AddComponent<EventTrigger>();
            } //1, 给需要的物体添加事件的组件EventerTrigger

            if (ET.triggers.Count == 0)
            {
                ET.triggers = new List<EventTrigger.Entry>(); //2.初始化EventTrigger.Entry容器
            }

            //实例化一个EventTrigger.Entry对象
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = (EventTriggerType)eventTriggerType; //3.指定事件触发的类型
            UnityAction<BaseEventData> callBack = new UnityAction<BaseEventData>(myMehod); //4.指定事件触发的方法
            entry.callback.AddListener(callBack); //事件加入到entry
            ET.triggers.Add(entry); //entry加入到EventTrigger
        }
             
        
    }

}