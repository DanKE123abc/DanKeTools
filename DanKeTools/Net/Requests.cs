using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace DanKeTools.Net
{

    ///<summary>
    ///脚本名称： Requests.cs
    ///修改时间：2022/12/15
    ///脚本功能：网络请求
    ///备注：
    ///</summary>

    public class Requests
    {

        public static string _UserAgent = "DanKeToolsRequests/1.0";
        
        /// <summary>
        /// 向指定URL发送GET方法的请求
        /// </summary>
        /// <param name="url">发送请求的URL</param>
        /// <param name="param">请求参数，请求参数应该是 name1=value1&name2=value2 的形式。</param>
        /// <returns>所代表远程资源的响应结果</returns>
        public static string Get(string url, string param)
        {
            string result = String.Empty;
            StreamReader reader = null;
            try
            {
                string urlNameString = url + "?" + param;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlNameString);
                request.Method = "GET";
                request.ContentType = "text/html;charset=UTF-8";
                request.Accept = "*/*";
                request.UserAgent = Requests._UserAgent;

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                result = reader.ReadToEnd();

                reader.Close();
                responseStream.Close();
                response.Close();
                reader = null;
                responseStream = null;
                response = null;
            }
            catch (Exception ex)
            {
                Debug.Log("发送GET请求出现异常：" + ex.Message);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// 向指定URL发送GET方法的请求
        /// </summary>
        /// <param name="url">发送请求的URL</param>
        /// <param name="param">请求参数，请求参数应该是 name1=value1&name2=value2 的形式。</param>
        /// <param name="encoding">设置响应信息的编码格式，如utf-8</param>
        /// <returns>所代表远程资源的响应结果</returns>
        public static string Get(string url, string param, string encoding)
        {
            string result = String.Empty;
            StreamReader reader = null;
            try
            {
                string urlNameString = url + "?" + param;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlNameString);
                request.Method = "GET";
                request.ContentType = "text/html;charset=" + encoding;
                request.Accept = "*/*";
                request.UserAgent =  Requests._UserAgent;

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                reader = new StreamReader(responseStream, Encoding.GetEncoding(encoding));
                result = reader.ReadToEnd();

                reader.Close();
                responseStream.Close();
                response.Close();
                reader = null;
                responseStream = null;
                response = null;
            }
            catch (Exception ex)
            {
                Debug.Log("发送GET请求出现异常：" + ex.Message);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// 向指定 URL 发送POST方法的请求
        /// </summary>
        /// <param name="url">发送请求的 URL</param>
        /// <param name="jsonData">请求参数，请求参数应该是Json格式字符串的形式。</param>
        /// <returns>所代表远程资源的响应结果</returns>
        public static string Post(string url, string jsonData)
        {
            string result = String.Empty;
            try
            {
                CookieContainer cookie = new CookieContainer();

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.Headers.Add("x-requested-with", "XMLHttpRequest");
                request.ServicePoint.Expect100Continue = false;
                request.ContentType = "application/x-www-form-urlencoded";
                request.Accept = "*/*";
                request.UserAgent =  Requests._UserAgent;
                request.ContentLength = Encoding.UTF8.GetByteCount(jsonData);
                request.CookieContainer = cookie;
                using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
                {

                    writer.Write(jsonData);
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                response.Cookies = cookie.GetCookies(response.ResponseUri);
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(responseStream))
                    {
                        result = reader.ReadToEnd();

                        reader.Close();
                    }
                    responseStream.Close();
                }
                response.Close();
                response = null;
                request = null;
            }
            catch (Exception ex)
            {
                Debug.Log("发送GET请求出现异常：" + ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 向指定 URL 发送POST方法的请求
        /// </summary>
        /// <param name="url">发送请求的 URL</param>
        /// <param name="jsonData">请求参数，请求参数应该是Json格式字符串的形式。</param>
        /// <param name="encoding">设置响应信息的编码格式，如utf-8</param>
        /// <returns>所代表远程资源的响应结果</returns>
        public static string Post(string url, string jsonData, string encoding)
        {
            string result = String.Empty;
            try
            {
                CookieContainer cookie = new CookieContainer();

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.Headers.Add("x-requested-with", "XMLHttpRequest");
                request.ServicePoint.Expect100Continue = false;
                request.ContentType = "application/x-www-form-urlencoded";
                request.Accept = "*/*";
                request.UserAgent =  Requests._UserAgent;
                request.ContentLength = Encoding.UTF8.GetByteCount(jsonData);
                request.CookieContainer = cookie;
                using (StreamWriter writer = new StreamWriter(request.GetRequestStream(), Encoding.GetEncoding(encoding)))
                {

                    writer.Write(jsonData);
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                response.Cookies = cookie.GetCookies(response.ResponseUri);
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding(encoding)))
                    {
                        result = reader.ReadToEnd();

                        reader.Close();
                    }
                    responseStream.Close();
                }
                response.Close();
                response = null;
                request = null;
            }
            catch (Exception ex)
            {
                Debug.Log("发送GET请求出现异常：" + ex.Message);
                
                
            }
            return result;
        }
        
    }

}
