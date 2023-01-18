using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using DanKeTools;
using DanKeTools.Mono;

using System.Text;
using System.Xml;
using UnityEngine.U2D;

namespace DanKeTools.IO
{
    
    ///<summary>
    ///脚本名称： FileManager.cs
    ///修改时间：2022/12/25
    ///脚本功能：文件管理器
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
            MonoManager.instance.StartCoroutine(ReallyLoadAsync<T>(name, callback));
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
        
        /// <summary>
        /// 加载Sprite
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Sprite LoadSprite(string path)
        {
            return Resources.Load<Sprite>(path);
        }

        /// <summary>
        /// 加载指定路径图集中的指定名称图片
        /// </summary>
        /// <param name="atlasPath"></param>
        /// <param name="spriteName"></param>
        /// <returns></returns>
        public static Sprite LoadSprite(string atlasPath, string spriteName)
        {
            Sprite tempSprite = null;
            SpriteAtlas tempAtlas = GetSpriteAtlas(atlasPath);
            Debug.Log(atlasPath + ".." + (tempAtlas == null));
            if (tempAtlas != null) tempSprite = tempAtlas.GetSprite(spriteName);
            return tempSprite;
        }
        
        /// <summary>
        /// 已加载图集记录
        /// </summary>
        private static Dictionary<string, SpriteAtlas> _mUISpriteAtlasDic = new Dictionary<string, SpriteAtlas>();
        /// <summary>
        /// 获取一个图集
        /// </summary>
        /// <param name="atlasName"></param>
        /// <returns></returns>
        public static SpriteAtlas GetSpriteAtlas(string atlasName)
        {
            if (_mUISpriteAtlasDic.ContainsKey(atlasName))
            {
                if (_mUISpriteAtlasDic[atlasName] == null)
                    _mUISpriteAtlasDic[atlasName] = Resources.Load<SpriteAtlas>(atlasName);
            }
            else
            {
                _mUISpriteAtlasDic.Add(atlasName, Resources.Load<SpriteAtlas>(atlasName));
            }

            return _mUISpriteAtlasDic[atlasName];
        }

        /// <summary>
        /// 加载材质球
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Material LoadMaterial(string path)
        {
            return Resources.Load<Material>(path);
        }

        /// <summary>
        /// 加载贴图
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Texture LoadTexture(string path)
        {
            return Resources.Load<Texture>(path);
        }

        /// <summary>
        /// 加载XML
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string LoadXML(string path)
        {
            XmlDocument xdoc = new XmlDocument();
            Debug.Log("当前目录是：" + path); //Application.dataPath);
            //加载XML 文件
            xdoc.Load(path + ".xml");

            //获取跟节点
            XmlElement root = xdoc.DocumentElement;
            Debug.Log("根元素是：" + root.Name);

            // data : 下一个子节点的名称 
            //XmlNode dataNode = root.SelectSingleNode("data");  //获取根节点下的子节点
            //Debug.Log("节点名称"+dataNode.Name);

            StringBuilder content = new StringBuilder();

            // 读取到的字符串
            for (int i = 0; i < root.ChildNodes.Count; i++)
            {
                content.Append(root.ChildNodes[i].InnerText + ",");
            }

            return content.ToString();
        }
        
        
    }
    
}