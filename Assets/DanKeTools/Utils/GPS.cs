using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace DanKeTools.Utils.GPS
{

    ///<summary>
    ///脚本名称： GPS.cs
    ///修改时间：2022/12/26
    ///脚本功能：GPS工具
    ///备注：
    ///</summary>

    public class GPS : MonoBehaviour
    {

        public Text txt;

        public void GetGPS()
        {
            StartCoroutine(StartGPS());
        }

        IEnumerator StartGPS()
        {
            txt.text = "开始获取GPS信息";

            // 检查位置服务是否可用  
            if (!Input.location.isEnabledByUser)
            {
                txt.text = "位置服务不可用";
                yield break;
            }

            // 查询位置之前先开启位置服务  
            txt.text = "启动位置服务";
            Input.location.Start();

            // 等待服务初始化  
            int maxWait = 20;
            while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
            {
                txt.text = Input.location.status.ToString() + ">>>" + maxWait.ToString();
                yield return new WaitForSeconds(1);
                maxWait--;
            }

            // 服务初始化超时  
            if (maxWait < 1)
            {
                txt.text = "服务初始化超时";
                yield break;
            }

            // 连接失败  
            if (Input.location.status == LocationServiceStatus.Failed)
            {
                txt.text = "无法确定设备位置";
                yield break;
            }
            else
            {
                txt.text = "Location: rn" +
                           "纬度：" + Input.location.lastData.latitude + "rn" +
                           "经度：" + Input.location.lastData.longitude + "rn" +
                           "海拔：" + Input.location.lastData.altitude + "rn" +
                           "水平精度：" + Input.location.lastData.horizontalAccuracy + "rn" +
                           "垂直精度：" + Input.location.lastData.verticalAccuracy + "rn" +
                           "时间戳：" + Input.location.lastData.timestamp;
            }

            Input.location.Stop();

        }
    }

}