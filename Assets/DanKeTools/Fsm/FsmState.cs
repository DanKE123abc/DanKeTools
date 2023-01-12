using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DanKeTools.Fsm
{

    ///<summary>
    ///脚本名称： FsmState.cs
    ///修改时间：2023/1/5
    ///脚本功能：Fsm状态基类
    ///备注：
    ///</summary>

    public abstract class FsmState<T>
    {
        public abstract void Enter(T target);
        public abstract void Execute(T target);
        public abstract void Exit(T target);
    }

}