using UnityEngine;
using UnityEditor;
using System.Collections;
[CanEditMultipleObjects]//支持选中多对象编辑
//[ExecuteInEditMode] //在非播放游戏的情况下执行
//[AddComponentMenu("My/ttttttt")]//在component的菜单下添加功能，点击此选项就添加此脚本组件
//[RequireComponent(typeof(Rigidbody))] //表示在添加此脚本类型时，必须要附带添加Rigidbody组件
public class test : MonoBehaviour {

    public int test_1;

    [System.Serializable]//用于显示声明的内部类，结构体，并序列化
    public class test_shuju
    {
        public int a, b, c;
        public string d, e, f;
    }
    public test_shuju ts;

    //[SerializeField] //显示非public的变量并序列化
    //private int ii = 1;

    //[HideInInspector] //隐藏public的变量
    //public int deHide = 0;

    [System.Serializable]
    public struct struct_shuju
    {
        public int x, y, z;
    }
    public struct_shuju ss;

    public enum enum_shuju { 一, 二, 三 };
    public enum_shuju es;

    void Start()
    {
        ts.a = 10;
    }

    [ContextMenu("+a")]//添加以下方法在inspector的小三角菜单中，点击可执行
    void app()
    {
        ts.a += 1; 
    }

    [MenuItem("Window/ttttttt")]//添加如下方法到任意菜单路径，点击后执行，需要usingeditor，需要方法为static
    static void tttttt()
    {
        Debug.Log("tttttttttttt");
    }

    public int i
    {
        get
        {
            return i;
        }
        set
        {
            i = i;
        }
    }

}
