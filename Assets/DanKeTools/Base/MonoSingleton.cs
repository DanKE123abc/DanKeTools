using UnityEngine;
using System;

namespace DanKeTools
{
    ///<summary>
    ///脚本名称： MonoSingleton.cs
    ///修改时间：2023/1/13
    ///脚本功能：继承了Monobehaviour的单例模式
    ///备注：直接继承就可以了。
    ///</summary>
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        [Obsolete("此方法已被淘汰，请使用instance代替")]
        public static T Instance()
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject();
                obj.name = typeof(T).ToString();
                DontDestroyOnLoad(obj); //保证物体过场景不被销毁
                _instance = obj.AddComponent<T>();
            }

            return _instance;
        }
        
        /// <summary>
        /// 单例模式需要实例化
        /// </summary>
        public static T instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).ToString();
                    DontDestroyOnLoad(obj); //保证物体过场景不被销毁
                    _instance = obj.AddComponent<T>();
                }
                return _instance;
            }
        }
        
    }

}