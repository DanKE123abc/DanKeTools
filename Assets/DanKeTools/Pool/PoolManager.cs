using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DanKeTools;
using DanKeTools.Pool;

namespace DanKeTools.Pool
{

    ///<summary>
    ///脚本名称： PoolManager.cs
    ///修改时间：2022/12/19
    ///脚本功能：缓存池管理器
    ///备注：
    ///</summary>

    public class PoolManager : Singleton<PoolManager>
    {
        public Dictionary<string, PoolData> PoolDic = new Dictionary<string, PoolData>();

        private GameObject poolObj;

        /// <summary>
        /// 取出物体
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public GameObject GetObject(string name)
        {
            GameObject obj = null;
            if (PoolDic.ContainsKey(name) && PoolDic[name].PoolList.Count > 0)
            {
                obj = PoolDic[name].GetObj();
            }
            else
            {
                obj = GameObject.Instantiate(Resources.Load<GameObject>(name));
                obj.name = name;
            }

            return obj;
        }

        /// <summary>
        /// 存入物体
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="obj"></param>
        public void PushObject(string name, GameObject obj)
        {
            if (poolObj == null) poolObj = new GameObject("Pool");

            if (PoolDic.ContainsKey(name))
            {
                PoolDic[name].PushObj(obj);
            }
            else
            {
                PoolDic.Add(name, new PoolData(obj, poolObj));
            }
        }

        /// <summary>
        /// 清空缓存池
        /// </summary>
        public void Clear()
        {
            PoolDic.Clear();
            poolObj = null;
            //Debug.Log("[缓存池] 缓存池被清空！");
        }
    }

}
