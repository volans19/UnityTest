using UnityEngine;
using System.Collections;

public class ShowGameInfo : MonoBehaviour
{
    private static int count = 0;//用于控制帧率的显示速度的count
    private static float milliSecond = 0;//毫秒数
    private static float fps = 0;//帧率值
    private static float deltaTime = 0.0f;//用于显示帧率的deltaTime
    string systemInfoLabel;

    bool bool_showInfo = true;
    bool bool_toggle = false;
    void OnGUI()
    {
        //左上方帧数显示
        if (++count > 10)
        {
            count = 0;
            milliSecond = deltaTime * 1000.0f;
            fps = 1.0f / deltaTime;
        }
        
        if (bool_showInfo)
        {
            string text = string.Format(" 当前每帧渲染间隔：{0:0.0} ms ({1:0.} 帧每秒)", milliSecond, fps);
            GUI.Label(new Rect(0, 15, 300, 300), text);
            GUI.Label(new Rect(0, 30, 500, 500), systemInfoLabel);
        }

        bool_toggle = GUI.Toggle(new Rect(0, 0, 100, 100), bool_toggle, "开启信息");
        if (bool_toggle)
        {
            bool_showInfo = true;
            return;
        }
        else
        {
            bool_showInfo = false;
            return;
        }
        
    }

    void Update()
    {
        //帧数显示的计时delataTime
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        systemInfoLabel = " CPU型号：" + SystemInfo.processorType + "\n (" + SystemInfo.processorCount +
            " cores核心数, " + SystemInfo.systemMemorySize + "MB RAM内存)\n " + "\n 显卡型号：" + SystemInfo.graphicsDeviceName + "\n " +
            Screen.width + "x" + Screen.height + " @" + Screen.currentResolution.refreshRate +
            " (" + SystemInfo.graphicsMemorySize + "MB VRAM显存)";
    }

}

