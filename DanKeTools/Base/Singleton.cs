
///<summary>
///脚本名称： Singleton.cs
///修改时间：2022/11/17
///脚本功能：单例模式
///备注：直接继承就可以了。
///</summary>
namespace DanKeTools
{

public class Singleton<T> where T:new(){
    private static T instance;
    //静态函数,建T的实体instance
    public static T Instance(){
        if (instance == null){
            instance = new T();
        }
        return instance;
    }
    //返回这个类的实体instance
}

}