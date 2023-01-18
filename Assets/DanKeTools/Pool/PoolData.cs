using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DanKeTools.Pool
{
    ///<summary>
    ///脚本名称： PoolData.cs
    ///修改时间：2022/12/19
    ///脚本功能：缓存池数据管理
    ///备注：
    ///</summary>

    public class PoolData
    {
        public GameObject FatherObj; // 对象挂载的父节点
        public List<GameObject> PoolList;

        public PoolData(GameObject obj, GameObject poolObj)
        {
            FatherObj = new GameObject(obj.name);
            FatherObj.transform.parent = poolObj.transform;
            PoolList = new List<GameObject>() { obj };
            PushObj(obj);
        }

        public void PushObj(GameObject obj)
        {
            obj.SetActive(false);
            PoolList.Add(obj);
            obj.transform.parent = FatherObj.transform;
        }

        public GameObject GetObj()
        {
            GameObject obj = null;
            obj = PoolList[0];
            PoolList.RemoveAt(0);
            obj.SetActive(true);
            obj.transform.parent = null;

            return obj;
        }
    }

}