using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using System.Security.Cryptography;
using DanKeTools.Utils.Console;
using System;

///<summary>
///脚本名称： FindRepeatAssets.cs
///修改时间：2022/12/6
///脚本功能：利用MD5查找重复的资源
///备注：
///</summary>

public class FindRepeatAssetsGUI : EditorWindow
{
    string path = "Assets/";

    FindRepeatAssetsGUI()
    {
        this.titleContent = new GUIContent("寻找重复资源");
    }

    [MenuItem("DanKeTools/FindRepeatAssets")]
    static void Init()
    {
        GetWindow(typeof(FindRepeatAssetsGUI));
    }

    void OnGUI()
    {
        //绘制标题
        GUILayout.Space(10);
        GUI.skin.label.fontSize = 24;
        GUI.skin.label.alignment = TextAnchor.MiddleCenter;
        GUILayout.Label("寻找重复资源");

        path = EditorGUILayout.TextField(path);
        if(GUILayout.Button("寻找"))
        {
            Debug.Log("寻找目录："+font._color.blue(path));
            FindRepeatAssets.Find(path);
        }
    }
}

public class FindRepeatAssets : MonoBehaviour
{
    
    public static void Find(String path)
    {
        string[] assetsPath = Directory.GetFiles(path,"*.*",SearchOption.AllDirectories);//查询目录
        Dictionary<string, string> MD5Dic = new Dictionary<string, string>();
        string filemd5=null;//文件的MD5

        for (int i = 0; i < assetsPath.Length; i++)
        {
            if(assetsPath[i].EndsWith(".meta"))
            {
                //过滤meta文件
            }
            else if(!assetsPath[i].EndsWith(".meta"))
            {
                //计算资源的md5
                MD5 md5= MD5.Create();
                byte[] md5bytes= md5.ComputeHash(getTextureByte(assetsPath[i]));
                filemd5 = System.BitConverter.ToString(md5bytes).Replace("-", "").ToLower();

                //存入字典,检验重复资源
                if (MD5Dic.ContainsKey(filemd5))
                {
                      Debug.LogError(font._color.red("【发现重复资源！】")+"\n MD5："+ font._color.yellow(filemd5) + "\n 文件地址：" + font._color.green(assetsPath[i]) + "\n 文件地址：" + font._color.green(MD5Dic[filemd5]) + "\n");
                }
                else
                {
                      MD5Dic.Add(filemd5, assetsPath[i]);
                }
            }   
        }
    }

    static byte[] getTextureByte(string assetsPath)
    {
        FileStream file = new FileStream(assetsPath, FileMode.Open);
        byte[] txByte = new byte[file.Length];
        file.Read(txByte, 0, txByte.Length);
        file.Close();
        return txByte;
    }
}
