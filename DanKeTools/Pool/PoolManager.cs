using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DanKeTools;
using DanKeTools.Pool;

namespace DanKeTools.Pool
{

    ///<summary>
    ///脚本名称： PoolManager.cs
    ///修改时间：
    ///脚本功能：
    ///备注：
    ///</summary>

    public class PoolManager : Singleton<PoolManager>
    {
        public Dictionary<string, PoolData> poolDic = new Dictionary<string, PoolData>();

        private GameObject poolObj;

        public GameObject GetObj(string name)
        {
            GameObject obj = null;
            if (poolDic.ContainsKey(name) && poolDic[name].poolList.Count > 0)
            {
                obj = poolDic[name].GetObj();
            }
            else
            {
                obj = GameObject.Instantiate(Resources.Load<GameObject>(name));
                obj.name = name;
            }

            return obj;
        }

        public void PushObj(string name, GameObject obj)
        {
            if (poolObj == null) poolObj = new GameObject("Pool");

            if (poolDic.ContainsKey(name))
            {
                poolDic[name].PushObj(obj);
            }
            else
            {
                poolDic.Add(name, new PoolData(obj, poolObj));
            }
        }

        // 清空缓存池的方法，主要用于场景切换
        public void Clear()
        {
            poolDic.Clear();
            poolObj = null;
        }
    }

}
