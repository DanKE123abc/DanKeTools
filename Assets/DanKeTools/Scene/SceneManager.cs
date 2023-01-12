using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DanKeTools;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using DanKeTools.Mono;
using DanKeTools.Event;

namespace DanKeTools.Scene
{
    
    ///<summary>
    ///脚本名称： SceneManager.cs
    ///修改时间：2023/1/12
    ///脚本功能：场景加载管理器
    ///备注：
    ///</summary>

    public class SceneManager : Singleton<SceneManager>
    {
        /// <summary>
        /// 加载场景
        /// </summary>
        /// <param name="name">场景名称</param>
        /// <param name="func">回调函数</param>
        public void LoadScene(string name, UnityAction func = null)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(name);
            func();
        }

        /// <summary>
        /// 异步加载场景
        /// </summary>
        /// <param name="name"></param>
        /// <param name="func"></param>
        public void LoadSceneAsyn(string name, UnityAction func = null)
        {
            MonoManager.Instance().StartCoroutine(ReallyLoadSceneAsyn(name,func));
            
        }

        private IEnumerator ReallyLoadSceneAsyn(string name, UnityAction func)
        {
            AsyncOperation asyncOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(name);
            while (!asyncOperation.isDone)
            {
                EventCenter.Instance().EventTrigger("SceneLoading",asyncOperation.progress);
                yield return asyncOperation.progress;
            }
            func();
        }
        
        
        
    }

}