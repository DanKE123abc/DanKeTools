using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using DanKeTools;
using DanKeTools.Mono;

namespace DanKeTools.IO
{
    
    ///<summary>
    ///脚本名称： FileManager.cs
    ///修改时间：2022/12/25
    ///脚本功能：
    ///备注：
    ///</summary>

    public class FileManager : Singleton<FileManager>
    {
        /// <summary>
        /// 写入文本文件
        /// </summary>
        /// <param name="pathName">文件</param>
        /// <param name="info">内容</param>
        public static void Write(string pathName, string info)
        {
            StreamWriter sw;
            FileInfo fi = new FileInfo(pathName);
            sw = fi.CreateText();
            sw.WriteLine(info);
            sw.Close();
            sw.Dispose();
        }
        
        /// <summary>
        /// 读取文本文件
        /// </summary>
        /// <param name="pathName">文件</param>
        /// <returns></returns>
        public static string Read(string pathName)
        {
            StreamReader sr;
            FileInfo fi = new FileInfo(pathName);
            sr = fi.OpenText();
            string info = sr.ReadToEnd();
            sr.Close();
            sr.Dispose();
            return info;
        }
        
        /// <summary>
        /// 同步加载资源
        /// </summary>
        /// <param name="name"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Load<T>(string name) where T : Object
        {
            T res = Resources.Load<T>(name);
            //如果对象是一个GameObject类型的，我把它实例化后，再返回出去直接使用。
            if (res is GameObject)
            {
                return GameObject.Instantiate(res);
            }
            else //else情况示例：TextAsset、AudioClip
            {
                return res;
            }
        }

        /// <summary>
        /// 异步加载资源
        /// </summary>
        /// <param name="name"></param>
        /// <param name="callback"></param>
        /// <typeparam name="T"></typeparam>
        public void LoadAsync<T>(string name, UnityAction<T> callback) where T : Object
        {
            //开启异步加载的协程
            MonoManager.Instance().StartCoroutine(ReallyLoadAsync<T>(name, callback));
        }
        private IEnumerator ReallyLoadAsync<T>(string name, UnityAction<T> callback) where T : Object
        {
            ResourceRequest r = Resources.LoadAsync<T>(name);
            yield return r;

            if (r.asset is GameObject)
            {
                //实例化一下再传给方法
                callback(GameObject.Instantiate(r.asset) as T);
            }
            else
            {
                //直接传给方法
                callback(r.asset as T);
            }
        }


    }
    
    

}