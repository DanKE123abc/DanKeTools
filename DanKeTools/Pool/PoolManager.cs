using System.Collections.Generic;
using UnityEngine;
using DanKeTools;

namespace DanKeTools.Pool
{
    ///<summary>
    ///脚本名称： PoolManager.cs
    ///修改时间：2022/12/14
    ///脚本功能：缓存暂时不需要的物体，用内存换cpu占用
    ///备注：
    ///</summary>

    public class PoolData
    {
        //抽屉中，对象挂载的父节点
        public GameObject fatherObj;

        //对象的容器
        public List<GameObject> poolList;

        public PoolData(GameObject obj, GameObject poolObj)
        {
            //根据obj创建一个同名父类空物体，它的父物体为总Pool空物体
            fatherObj = new GameObject(obj.name);
            fatherObj.transform.parent = poolObj.transform;

            poolList = new List<GameObject>() { };
            PushObj(obj);
        }

        //像抽屉里面压东西并且设置好父对象
        public void PushObj(GameObject obj)
        {
            //存起来
            poolList.Add(obj);
            //设置父对象
            obj.transform.parent = fatherObj.transform;
            //失活，让其隐藏
            obj.SetActive(false);
        }

        //像抽屉中取东西
        public GameObject GetObj()
        {
            GameObject obj = null;
            //取出第一个
            obj = poolList[0];
            poolList.RemoveAt(0);
            //激活，让其展示
            obj.SetActive(true);
            //断开父子关系
            obj.transform.parent = null;
            return obj;
        }

    }

    public class PoolManager : Singleton<PoolManager>
    {
        /// 缓冲池容器
        public Dictionary<string, PoolData> poolDic = new Dictionary<string, PoolData>();

        private GameObject poolObj;

        /// <summary>
        /// 获得物体
        /// </summary>
        public GameObject GetObject(string name)
        {
            GameObject obj = null;
            if (poolDic.ContainsKey(name) && poolDic[name].poolList.Count > 0)
            {
                obj = poolDic[name].GetObj();
            }
            else
            {
                obj = GameObject.Instantiate(Resources.Load<GameObject>(name));
                obj.name = name; //改名
            }
            return obj;
        }

        /// <summary>
        /// 存入物体
        /// </summary>
        public void PushObject(string name, GameObject obj)
        {
            if (poolObj = null)
            {
                poolObj = new GameObject("Pool");
            }

            if (poolDic.ContainsKey(name))
            {
                poolDic[name].PushObj(obj);
            }
            else
            {
                poolDic.Add(name, new PoolData(obj,poolObj){});

            }
        }
        
        /// <summary>
        /// 清空缓存池
        /// </summary>
        public void Clear() { 
            poolDic.Clear(); 
            poolObj = null;
            Debug.Log("[缓存池] 缓存池被清空！");
        }

    }

}