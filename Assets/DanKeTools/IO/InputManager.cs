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
    ///脚本功能：输入管理器
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
            if (isStart)
            {
                #region 按键监听列表

                CheckKeyCode(KeyCode.A);
                CheckKeyCode(KeyCode.B);
                CheckKeyCode(KeyCode.C);
                CheckKeyCode(KeyCode.D);
                CheckKeyCode(KeyCode.E);
                CheckKeyCode(KeyCode.F);
                CheckKeyCode(KeyCode.G);
                CheckKeyCode(KeyCode.H);
                CheckKeyCode(KeyCode.I);
                CheckKeyCode(KeyCode.J);
                CheckKeyCode(KeyCode.K);
                CheckKeyCode(KeyCode.L);
                CheckKeyCode(KeyCode.M);
                CheckKeyCode(KeyCode.N);
                CheckKeyCode(KeyCode.O);
                CheckKeyCode(KeyCode.P);
                CheckKeyCode(KeyCode.Q);
                CheckKeyCode(KeyCode.X);
                CheckKeyCode(KeyCode.Y);
                CheckKeyCode(KeyCode.Z);
                CheckKeyCode(KeyCode.U);
                CheckKeyCode(KeyCode.V);
                CheckKeyCode(KeyCode.W);
                CheckKeyCode(KeyCode.X);
                CheckKeyCode(KeyCode.Y);
                CheckKeyCode(KeyCode.Z);
                CheckKeyCode(KeyCode.Alpha0);
                CheckKeyCode(KeyCode.Alpha1);
                CheckKeyCode(KeyCode.Alpha2);
                CheckKeyCode(KeyCode.Alpha3);
                CheckKeyCode(KeyCode.Alpha4);
                CheckKeyCode(KeyCode.Alpha5);
                CheckKeyCode(KeyCode.Alpha6);
                CheckKeyCode(KeyCode.Alpha7);
                CheckKeyCode(KeyCode.Alpha8);
                CheckKeyCode(KeyCode.Alpha9);
                CheckKeyCode(KeyCode.F1);
                CheckKeyCode(KeyCode.F2);
                CheckKeyCode(KeyCode.F3);
                CheckKeyCode(KeyCode.F4);
                CheckKeyCode(KeyCode.F5);
                CheckKeyCode(KeyCode.F6);
                CheckKeyCode(KeyCode.F7);
                CheckKeyCode(KeyCode.F8);
                CheckKeyCode(KeyCode.F9);
                CheckKeyCode(KeyCode.F10);
                CheckKeyCode(KeyCode.F11);
                CheckKeyCode(KeyCode.F12);
                CheckKeyCode(KeyCode.UpArrow);
                CheckKeyCode(KeyCode.DownArrow);
                CheckKeyCode(KeyCode.RightArrow);
                CheckKeyCode(KeyCode.LeftArrow);
                CheckKeyCode(KeyCode.Escape);
                CheckKeyCode(KeyCode.LeftShift);
                CheckKeyCode(KeyCode.RightShift);
                CheckKeyCode(KeyCode.CapsLock);
                CheckKeyCode(KeyCode.Tab);
                CheckKeyCode(KeyCode.KeypadEnter);
                CheckKeyCode(KeyCode.LeftAlt);
                CheckKeyCode(KeyCode.RightAlt);
                CheckKeyCode(KeyCode.LeftControl);
                CheckKeyCode(KeyCode.RightControl);
                CheckKeyCode(KeyCode.LeftCommand);
                CheckKeyCode(KeyCode.RightCommand);
                CheckKeyCode(KeyCode.Delete);
                CheckKeyCode(KeyCode.Backspace);

                #endregion
            }

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
