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
    ///修改时间：2022/12/23
    ///脚本功能：
    ///备注：
    ///</summary>

    public class SceneManager : Singleton<SceneManager>
    {
        public void LoadScene(string name, UnityAction func)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(name);
            func();
        }

        
        public void LoadSceneAsyn(string name, UnityAction func)
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