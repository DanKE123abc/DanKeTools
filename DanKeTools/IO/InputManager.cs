using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DanKeTools;
using DanKeTools.Event;
using DanKeTools.Mono;
using UnityEngine.PlayerLoop;

namespace DanKeTools.IO
{

    ///<summary>
    ///脚本名称： InputManager.cs
    ///修改时间：2022/12/24
    ///脚本功能：
    ///备注：
    ///</summary>

    public class InputManager : Singleton<InputManager>
    {
        private bool isStart = false;

        public void InputCheck(bool isOpen)
        {
            isStart = isOpen;
        }

        public InputManager()
        {
            MonoManager.Instance().AddUpdateListener(Update);
        }

        private void Update()
        {
            if (!isStart)
            {
                return;
            }

            CheckKeyCode(KeyCode.A);

        }

        private void CheckKeyCode(KeyCode key)
        {
            if (Input.GetKeyDown(key))
            {
                //事件中心模块，分发按下抬起事件（把哪个按键也发送出去）
                EventCenter.Instance().EventTrigger("KeyisDown", key);
            }

            if (Input.GetKeyUp(key))
            {
                EventCenter.Instance().EventTrigger("KeyisUp", key);
            }
        }


    }

}
