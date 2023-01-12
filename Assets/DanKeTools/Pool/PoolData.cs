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
        public GameObject fatherObj; // 对象挂载的父节点
        public List<GameObject> poolList;

        public PoolData(GameObject obj, GameObject poolObj)
        {
            fatherObj = new GameObject(obj.name);
            fatherObj.transform.parent = poolObj.transform;
            poolList = new List<GameObject>() { obj };
            PushObj(obj);
        }

        public void PushObj(GameObject obj)
        {
            obj.SetActive(false);
            poolList.Add(obj);
            obj.transform.parent = fatherObj.transform;
        }

        public GameObject GetObj()
        {
            GameObject obj = null;
            obj = poolList[0];
            poolList.RemoveAt(0);
            obj.SetActive(true);
            obj.transform.parent = null;

            return obj;
        }
    }

}