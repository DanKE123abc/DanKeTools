using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using UnityEngine;
using UnityEngine.U2D;


namespace DanKeTools.IO
{
    ///<summary>
    ///脚本名称： LoadAssets.cs
    ///修改时间：2022/12/14
    ///脚本功能：加载各种资源
    ///备注：
    ///</summary>
    public static class Assets
    {
        #region 加载资源

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
        /// 已加载图集记录
        /// </summary>
        private static Dictionary<string, SpriteAtlas> m_UISpriteAtlasDic = new Dictionary<string, SpriteAtlas>();

        /// <summary>
        /// 加载指定路径图集中的指定名称图片
        /// </summary>
        /// <param name="_atlasPath"></param>
        /// <param name="_spriteName"></param>
        /// <returns></returns>
        public static Sprite LoadSprite(string _atlasPath, string _spriteName)
        {
            Sprite tempSprite = null;
            SpriteAtlas tempAtlas = GetSpriteAtlas(_atlasPath);
            Debug.Log(_atlasPath + ".." + (tempAtlas == null));
            if (tempAtlas != null) tempSprite = tempAtlas.GetSprite(_spriteName);
            return tempSprite;
        }

        /// <summary>
        /// 获取一个图集
        /// </summary>
        /// <param name="atlasName"></param>
        /// <returns></returns>
        static SpriteAtlas GetSpriteAtlas(string atlasName)
        {
            if (m_UISpriteAtlasDic.ContainsKey(atlasName))
            {
                if (m_UISpriteAtlasDic[atlasName] == null)
                    m_UISpriteAtlasDic[atlasName] = Resources.Load<SpriteAtlas>(atlasName);
            }
            else
            {
                m_UISpriteAtlasDic.Add(atlasName, Resources.Load<SpriteAtlas>(atlasName));
            }

            return m_UISpriteAtlasDic[atlasName];
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
            XmlDocument Xdoc = new XmlDocument();
            Debug.Log("当前目录是：" + path); //Application.dataPath);
            //加载XML 文件
            Xdoc.Load(path + ".xml");

            //获取跟节点
            XmlElement root = Xdoc.DocumentElement;
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

        #endregion

    }

}