using UnityEngine;

///<summary>
///脚本名称： MonoSingleton.cs
///修改时间：2022/11/18
///脚本功能：继承了Monobehaviour的单例模式
///备注：直接继承就可以了。
///</summary>

namespace DanKeTools
{

public class MonoSingleton<T> : MonoBehaviour where T: MonoBehaviour{
    private static T instance;
    //静态函数,建T的实体instance
    public static T Instance(){
        if(instance == null){
            GameObject obj = new GameObject();
            obj.name = typeof(T).ToString();
            DontDestroyOnLoad(obj);//保证物体过场景不被销毁
            instance = obj.AddComponent<T>();
        }
        return instance;   
    }
    //返回这个类的实体instance
}

}