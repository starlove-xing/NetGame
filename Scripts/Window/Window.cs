using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;

public class Window : MonoBehaviour
{
    /// <summary>  
    /// 窗口宽度  
    /// </summary>  
    public int winWidth;
    /// <summary>  
    /// 窗口高度  
    /// </summary>  
    public int winHeight;
    /// <summary>  
    /// 窗口左上角x  
    /// </summary>  
    public int winPosX;
    /// <summary>  
    /// 窗口左上角y  
    /// </summary>  
    public int winPosY;

    [DllImport("user32.dll")]
    static extern IntPtr SetWindowLong(IntPtr hwnd, int _nIndex, int dwNewLong);
    [DllImport("user32.dll")]
    static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
    [DllImport("user32.dll")]
    static extern IntPtr GetForegroundWindow();

    const uint SWP_SHOWWINDOW = 0x0040;
    const int GWL_STYLE = -16;
    const int WS_BORDER = 1;
    const int WS_POPUP = 0x800000;

    // Use this for initialization  
    void Start()
    {
        winWidth = 800;
        winHeight = 600;
        //显示器支持的所有分辨率  
        int i = Screen.resolutions.Length;

        int resWidth = Screen.resolutions[i - 1].width;
        int resHeight = Screen.resolutions[i - 1].height;

        winPosX = resWidth / 2 - winWidth / 2;
        winPosY = resHeight / 2 - winHeight / 2;

        SetWindowLong(GetForegroundWindow(), GWL_STYLE, WS_POPUP);
        bool result = SetWindowPos(GetForegroundWindow(), 0, winPosX, winPosY, winWidth, winHeight, SWP_SHOWWINDOW);

    }
}