using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;

namespace DanKeTools
{
    ///<summary>
    ///脚本名称： Loom.cs
    ///修改时间：2023/3/15
    ///脚本功能：Unity多线程执行
    ///备注：
    ///</summary>

    public class Loom : MonoSingleton<Loom>
    {
        #region StructMember
        public struct DelayedQueueItem
        {
            public float time;
            public Action action;
        }
        #endregion

        #region Member
        public static int mMaxThreads = 8;

        private static Loom _current;
        public static Loom Current
        {
            get
            {
                Initialize();
                return _current;
            }
        }

        private static int mNumThreads;
        private static bool initialized;

        private int _count;
        private List<Action> _actions = new List<Action>();
        private List<Action> _currentActions = new List<Action>();
        private List<DelayedQueueItem> _delayed = new List<DelayedQueueItem>();
        private List<DelayedQueueItem> _currentDelayed = new List<DelayedQueueItem>();

        #endregion
        #region MonoLife

        void Awake()
        {
            _current = this;
            initialized = true;
        }

        void OnEnable()
        {
            if (null == _current)
            {
                _current = this;
            }
        }

        void OnDisable()
        {
            if (this == _current)
            {
                _current = null;
            }
        }

        void Update()
        {
            lock (_actions)
            {
                _currentActions.Clear();
                _currentActions.AddRange(_actions);
                _actions.Clear();
            }

            for (int i = 0; i < _currentActions.Count; i++)
            {
                _currentActions[i]();
            }

            lock (_delayed)
            {
                _currentDelayed.Clear();

                for (int i = _delayed.Count - 1; i >= 0; i--)
                {
                    if (_delayed[i].time > Time.time)
                    {
                        continue;
                    }

                    _currentDelayed.Add(_delayed[i]);
                    _delayed.RemoveAt(i);

                }
            }

            for (int i = 0; i < _currentDelayed.Count; i++)
            {
                _currentDelayed[i].action();
            }

        }

        #endregion
        #region BusinessLogic

        private static void Initialize()
        {
            if (initialized == true) return;
            if (Application.isPlaying == false) return;

            initialized = true;
            var g = new GameObject("Loom");
            _current = g.AddComponent<Loom>();

        }

        private static void RunAction(object action)
        {
            try
            {
                ((Action)action)();
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
            finally
            {
                Interlocked.Decrement(ref mNumThreads);
            }
        }

        #endregion
        #region PublicTools
        public static void RunOnMain(Action action, float time = 0.0f)
        {
            if (time != 0)
            {
                lock (Current._delayed)
                {
                    Current._delayed.Add(new DelayedQueueItem { time = Time.time + time, action = action });
                }
            }
            else
            {
                lock (Current._actions)
                {
                    Current._actions.Add(action);
                }
            }
        }

        public static Thread RunOnAsync(Action varAction)
        {
            Initialize();
            while (mNumThreads >= mMaxThreads)
            {
                Thread.Sleep(1);
            }
            Interlocked.Increment(ref mNumThreads);
            ThreadPool.QueueUserWorkItem(RunAction, varAction);
            return null;
        }
        #endregion
    }

}