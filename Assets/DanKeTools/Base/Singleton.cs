using System;

namespace DanKeTools
{


    ///<summary>
    ///脚本名称： Singleton.cs
    ///修改时间：2023/1/13
    ///脚本功能：单例模式
    ///备注：直接继承就可以了。
    ///</summary>
    public class Singleton<T> where T : new()
    {
        private static T _instance;

        /// <summary>
        /// 单例模式需要实例化
        /// </summary>
        public static T instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new T();
                }
                return _instance;
            }
        }

    }

}