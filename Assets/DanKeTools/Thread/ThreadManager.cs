using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using DanKeTools;
using DanKeTools.Mono;

namespace DanKeTools.Thread
{


    ///<summary>
    ///脚本名称： ThreadManager.cs
    ///修改时间：2023/1/12
    ///脚本功能：子线程管理器
    ///备注：
    ///</summary>

    public class ThreadManager : Singleton<ThreadManager>
    {
        private System.Threading.Thread thread;
        private List<System.Threading.Thread> multiThreads = new List<System.Threading.Thread>();

        public ThreadManager()
        {
            //初始化
            thread = new System.Threading.Thread(() => BackTread());
            thread.IsBackground = true;
            thread.Start();

            MonoManager.Instance().StartCoroutine(MainTread());
        }

        private interface ThreadEvent
        {
            void Call();
            void CallBack();
        }

        public class Delegate<T> : ThreadEvent
        {
            public delegate T EventDelegate();

            public EventDelegate _events;
            public Action<T> _callback;
            public T _callbackData;

            public Delegate(EventDelegate events, Action<T> callback)
            {
                this._events += events;
                this._callback += callback;
            }

            public void Call()
            {
                _callbackData = _events();
            }

            public void CallBack()
            {
                _callback(_callbackData);
            }
        }
        
        public class Delegate : ThreadEvent
        {
            public Action _events;
            public Action _callback;
            public Delegate(Action events, Action callback)
            {
                this._events += events;
                if (callback != null)
                {
                    this._callback += callback;
                }
                    
            }
            public void Call()
            {
                _events?.Invoke();
            }

            public void CallBack()
            {
                _callback?.Invoke();
            }
        }

        
        private Stack<ThreadEvent> callStack = new Stack<ThreadEvent>();
        private Stack<ThreadEvent> finishStack = new Stack<ThreadEvent>();

        private void BackTread()
        {
            //Action
            List<System.Threading.Thread> remove = new List<System.Threading.Thread>();
            
            while (true)
            {
                foreach (var item in multiThreads)
                {
                    if (!item.IsAlive)
                    {
                        remove.Add(item);
                    }
                }

                foreach (var item in remove)
                {
                    multiThreads.Remove(item);
                }
                
                if (callStack.Count != 0)
                {
                    for (int i = 0; i < callStack.Count; i++)
                    {
                        ThreadEvent wait = callStack.Pop();
                        System.Threading.Thread newTread = new System.Threading.Thread(wait.Call);
                        newTread.IsBackground = true;
                        newTread.Start();
                        multiThreads.Add(newTread);
                        finishStack.Push(wait);
                    }
                }
                else
                {
                    int start = Environment.TickCount;
                    while (Math.Abs(Environment.TickCount - start) < 3000)
                    {
                        continue;
                    }
                }
            }
        }
        
        private IEnumerator MainTread()
        {
            while (true)
            {
                if (finishStack.Count != 0)
                {
                    for (int i = 0; i < finishStack.Count; i++)
                    {
                        finishStack.Pop().Call();
                    }
                        
                }
                yield return 0;
            }
        }

        /// <summary>
        /// 添加后台线程事件
        /// </summary>
        /// <param name="func">委托函数</param>
        /// <param name="callback">回调函数</param>
        public void AddTreadEvent<T>(Delegate<T>.EventDelegate func, Action<T> callback)
        {
            callStack.Push(new Delegate<T>(func, callback));
        }

        /// <summary>
        /// 添加后台线程事件
        /// </summary>
        /// <param name="func">委托</param>
        /// <param name="callback">回调函数</param>
        public void AddTreadEvent(Action func, Action callback)
        {
            callStack.Push(new Delegate(func, callback));
        }

    }

}
