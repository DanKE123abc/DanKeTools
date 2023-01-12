using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DanKeTools;
using DanKeTools.Mono;
using UnityEngine.Events;
using System.ComponentModel;
using System;


namespace DanKeTools.Mono
{
    ///<summary>
    ///脚本名称： MonoManager.cs
    ///修改时间：2023/1/12
    ///脚本功能：Mono事件管理器
    ///备注：
    ///</summary>
    public class MonoManager : Singleton<MonoManager>
    {
        private MonoController controller;

        public MonoManager()
        {
            GameObject obj = new GameObject("MonoController");
            controller = obj.AddComponent<MonoController>();
        }

        /// <summary>
        /// 添加帧更新事件
        /// </summary>
        /// <param name="function"></param>
        public void AddUpdateListener(UnityAction function)
        {
            controller.AddUpdateListener(function);
        }

        /// <summary>
        /// 移除帧更新事件
        /// </summary>
        /// <param name="function"></param>
        public void RemoveUpdateListener(UnityAction function)
        {
            controller.RemoveUpdateListener(function);
        }


        #region 封装 协程接口

        /// <summary>
        /// 开启协程
        /// </summary>
        public Coroutine StartCoroutine(string methodName) //开启协程
        {
            return controller.StartCoroutine(methodName);
        }
        /// <summary>
        /// 开启协程
        /// </summary>
        public Coroutine StartCoroutine(IEnumerator routine)
        {
            return controller.StartCoroutine(routine);
        }
        /// <summary>
        /// 开启协程
        /// </summary>
        public Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value)
        {
            return controller.StartCoroutine(methodName, value);
        }

        /// <summary>
        /// 停止协程
        /// </summary>
        public void StopCoroutine(IEnumerator routine) //停止协程
        {
            controller.StopCoroutine(routine);
        }
        /// <summary>
        /// 停止协程
        /// </summary>
        public void StopCoroutine(Coroutine routine)
        {
            controller.StopCoroutine(routine);
        }
        /// <summary>
        /// 停止协程
        /// </summary>
        public void StopCoroutine(string methodName)
        {
            controller.StopCoroutine(methodName);
        }

        /// <summary>
        /// 停止所有协程
        /// </summary>
        public void StopAllCoroutines() //停止所有协程
        {
            controller.StopAllCoroutines();
        }

        internal void StartCoroutine(IEnumerable enumerable)
        {
            throw new NotImplementedException();
        }

        #endregion


    }

}