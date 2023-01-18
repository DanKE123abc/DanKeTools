using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DanKeTools.Thread
{

    ///<summary>
    ///脚本名称： ThreadDelegate.cs
    ///修改时间：2022/1/13
    ///脚本功能：委托类型
    ///备注：
    ///</summary>

    public class ThreadDelegate
    {
        public interface IThreadEvent
        {
            void Call();

            void CallBack();
        }

        public class DelegateType<T> : IThreadEvent
        {
            public delegate T DelegateEvent();

            public DelegateEvent _Event;

            public Action<T> _CallBack;

            public T _CallBackData;

            public DelegateType(DelegateEvent call, Action<T> callback)
            {
                this._Event = (DelegateEvent)Delegate.Combine(this._Event, call);
                this._CallBack = (Action<T>)Delegate.Combine(this._CallBack, callback);
            }

            public void Call()
            {
                _CallBackData = _Event();
            }

            public void CallBack()
            {
                _CallBack(_CallBackData);
            }
        }

        public class DelegateType : IThreadEvent
        {
            public Action _Event;

            public Action _CallBack;

            public DelegateType(Action call, Action callback)
            {
                this._Event = (Action)Delegate.Combine(this._Event, call);
                if (_CallBack != null)
                {
                    this._CallBack = (Action)Delegate.Combine(this._CallBack, callback);
                }
            }

            public void Call()
            {
                Action action = _Event;
                if (action != null)
                {
                    action();
                }
            }

            public void CallBack()
            {
                Action action = _CallBack;
                if (action != null)
                {
                    action();
                }
            }
            
        }
        
    }
}
