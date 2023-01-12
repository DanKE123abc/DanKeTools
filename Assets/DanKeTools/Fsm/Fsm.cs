using System.Collections;
using System.Collections.Generic;
using DanKeTools.Mono;
using UnityEngine;

namespace DanKeTools.Fsm
{
    
    ///<summary>
    ///脚本名称： Fsm.cs
    ///修改时间：2023/1/5
    ///脚本功能：Fsm有限状态机
    ///备注：
    ///</summary>

    public class Fsm<T>
    {
        private T target;//拥有者
        private FsmState<T> preState;
        public FsmState<T> currentState;

        public Fsm(T target)
        {
            this.target = target;
            preState = null;
            currentState = null;
            MonoManager.Instance().AddUpdateListener(OnUpdate);
        }

        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="current"></param>
        public void SetCurrent(FsmState<T> current)
        {
            this.currentState = current;
            //进入状态
            this.currentState.Enter(target);
        }

        public void ChangeState(FsmState<T> current)
        {
            //退出状态
            this.currentState.Exit(target);
            //记录状态
            this.preState = this.currentState;
            //设置状态
            this.currentState = current;
            //进入状态
            this.currentState.Enter(target);
        }

        public void OnUpdate()
        {
            if (this.currentState != null)
            {
                this.currentState.Execute(target);
            }
        }

    }
    
}
