using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using DanKeTools.Utils.MD5;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

namespace DanKeTools.Net
{

    ///<summary>
    ///脚本名称： OnlineMT.cs
    ///修改时间：2023/1/27
    ///脚本功能：利用百度api在线机器翻译
    ///备注：
    ///</summary>

    [System.Serializable]
    public class Trans_result
    {
        public string src;

        public string dst;
    }

    [System.Serializable]
    public class Root
    {
        public string from;

        public string to;
        public List<Trans_result> trans_result;
    }


    public class OnlineMT
    {
        /// <summary>
        /// 语言列表
        /// </summary>
        public enum Language
        {
            auto, //自动检测
            zh, //中文
            en, //英文
            wyw, //文言文
            jp, //日文
            kor, //韩语
            fra, //法语
            spa, //西班牙语
            ru, //俄语
            yue, //粤语
            th, //泰语
            ara, //阿拉伯语
            pt, //葡萄牙语
            de, //德语
            it, //意大利语
            el, //希腊语
            nl, //荷兰语
            pl, //波兰语
            cht, //繁体中文
            vie //越南语
        }

        public string _appid;
        public string _key;
        public string _randomKey;
        public Random _random;

        public OnlineMT()
        {
            _appid = "20201230000659149";
            _key = "18puhOFvCCqQnbHnfgR8";
            _random = new Random();
        }
        
        public OnlineMT(string key, string appid)
        {
            this._appid = appid;
            this._key = key;
            _random = new Random();
        }

        private WebClient client = new WebClient();

        private string sign(string text)
        {
            string data = _appid + text + _randomKey + _key;
            return MD5.Md5String(data);
        }

        /// <summary>
        /// 翻译
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="original">原语言</param>
        /// <param name="target">目标语言</param>
        /// <returns></returns>
        public string MT(string text, Language original, Language target)
        {
            if (original == target)
            {
                return text;
            }
            else
            {
                try
                {
                    _randomKey = _random.Next(100000, 999999).ToString();
                    string address =
                        string.Format(
                            "https://fanyi-api.baidu.com/api/trans/vip/translate?q={0}&from={4}&to={5}&appid={3}&salt={1}&sign={2}",
                            text, _randomKey, sign(text), _appid, original, target);

                    byte[] bytes = client.DownloadData(address);
                    string @string = Encoding.UTF8.GetString(bytes);
                    var root = JsonUtility.FromJson<Root>(@string);
                    return root.trans_result[0].dst;
                }
                catch (Exception exception)
                {
                    Debug.LogError("翻译'" + text + "'出错！正在重试\n错误报告：" + exception);
                    try
                    {
                        _randomKey = _random.Next(100000, 999999).ToString();
                        string address =
                            string.Format(
                                "https://fanyi-api.baidu.com/api/trans/vip/translate?q={0}&from={4}&to={5}&appid={3}&salt={1}&sign={2}",
                                text, _randomKey, sign(text), _appid, original, target);

                        byte[] bytes = client.DownloadData(address);
                        string @string = Encoding.UTF8.GetString(bytes);
                        var root = JsonUtility.FromJson<Root>(@string);
                        return root.trans_result[0].dst;
                    }
                    catch (Exception e)
                    {
                        Debug.LogError("无法翻译'" + text + "'\n错误报告：" + e);
                        return text;
                    }
                }

            }
        }
        
        /// <summary>
        /// 翻译
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="target">目标语言</param>
        /// <returns></returns>
        public string MT(string text, Language target)
        {
            return MT(text, Language.auto, target);
        }
        
        /// <summary>
        /// 翻译
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="original">原语言</param>
        /// <param name="target">目标语言</param>
        /// <returns></returns>
        public static string MT_static(string text, Language original, Language target)
        {
            var onlineMT = new OnlineMT();
            return onlineMT.MT(text, original, target);
        }
        
        /// <summary>
        /// 翻译
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="target">目标语言</param>
        /// <returns></returns>
        public static string MT_static(string text, Language target)
        {
            var onlineMT = new OnlineMT();
            return onlineMT.MT(text, Language.auto, target);
        }
    }

}