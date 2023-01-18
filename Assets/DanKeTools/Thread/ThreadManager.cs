using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using DanKeTools;
using DanKeTools.Mono;
using DanKeTools.Thread;

namespace DanKeTools.Thread
{


    ///<summary>
    ///脚本名称： ThreadManager.cs
    ///修改时间：2023/1/13
    ///脚本功能：子线程管理器
    ///备注：
    ///</summary>

    public class ThreadManager : Singleton<ThreadManager>
    {

        private System.Threading.Thread tread;

        private Stack<ThreadDelegate.IThreadEvent> treadStack = new Stack<ThreadDelegate.IThreadEvent>();

        public ThreadManager()
        {
            tread = new System.Threading.Thread((ThreadStart)delegate
            {
                BackTread();
            });
            tread.IsBackground = true;
            tread.Start();
        }

        private void BackTread()
        {
            while (true)
            {
                
                if (treadStack.Count != 0)
                {
                    for (int i = 0; i < treadStack.Count; i++)
                    {
                        ThreadDelegate.IThreadEvent threadEvent = treadStack.Pop();
                        threadEvent.Call();
                        threadEvent.CallBack();
                    }
                }
                else
                {
                    System.Threading.Thread.Sleep(3);
                }
            }
        }

        /// <summary>
        /// 添加委托事件
        /// </summary>
        /// <param name="call"></param>
        /// <param name="callback"></param>
        /// <typeparam name="T"></typeparam>
        public void AddDelegate<T>(ThreadDelegate.DelegateType<T>.DelegateEvent call, Action<T> callback)
        {
            treadStack.Push(new ThreadDelegate.DelegateType<T>(call, callback));
        }

        /// <summary>
        /// 添加委托事件
        /// </summary>
        /// <param name="call"></param>
        /// <param name="callback"></param>
        public void AddDelegate(Action call, Action callback)
        {
            treadStack.Push(new ThreadDelegate.DelegateType(call, callback));
        }
        
    }

}
