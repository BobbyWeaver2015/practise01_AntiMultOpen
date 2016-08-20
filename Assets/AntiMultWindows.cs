using System;
using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using Debug = UnityEngine.Debug;

public class AntiMultWindows : MonoBehaviour
{
    private delegate bool WNDENUMPROC(IntPtr hWnd, uint IParam);

    [DllImport("user32.dll")]
    private static extern bool EnumWindows(WNDENUMPROC lpEnumFunc, uint IParam);

    [DllImport("user32.dll")]
    private static extern int GetWindowTextW(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder lpString, int nMaxCount);

    [DllImport("user32.dll")]
    private static extern int GetClassNameW(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder lpString, int nMaxCount);

    [DllImport("user32.dll")]
    private static extern bool IsWindowVisible(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern bool IsWindowEnabled(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern IntPtr GetPropW(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder lpString);

    [DllImport("user32.dll")]
    private static extern IntPtr SetPropW(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder lpString, IntPtr hData);

    [DllImport("user32.dll")]
    private static extern IntPtr GetParent(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern uint GetWindowThreadProcessId(IntPtr hWnd, ref uint lpdwProcessId);

    struct WindowInfo
    {
        public IntPtr hWnd;
        public string szWindowName;
        public string szClassName;
    }
	void Start ()
	{
        //List<WindowInfo> wndList = new List<WindowInfo>();

	    StringBuilder propName = new StringBuilder("Prop20160817");
	    IntPtr hValue = new IntPtr(1);

	    IntPtr ptrWnd = IntPtr.Zero;    //当前启动窗口句柄
	    uint pid = (uint) Process.GetCurrentProcess().Id;   //当前进程ID

	    bool createNew = EnumWindows(delegate(IntPtr hWnd, uint lParam)
	    {
	        uint id = 0;
	        if (GetParent(hWnd) == IntPtr.Zero)
	        {
	            GetWindowThreadProcessId(hWnd, ref id);
	            if (id == lParam)
	            {
	                ptrWnd = hWnd;
	            }
	        }

	        IntPtr h = GetPropW(hWnd, propName);
	        if (h.Equals(hValue))
	        {
                Debug.Log("XYZ ====Find Same : " + h.ToString());
	            return false;
	        }

            //WindowInfo wnd = new WindowInfo();
            //StringBuilder sb = new StringBuilder(256);

            //wnd.hWnd = hWnd;

            //GetWindowTextW(hWnd, sb, sb.Capacity);
            //wnd.szWindowName = sb.ToString();

            //GetClassNameW(hWnd, sb, sb.Capacity);
            //wnd.szClassName = sb.ToString();

	        return true;
	    }, pid);
	    if (createNew)
	    {
            Debug.Log("XYZ ======== create New !!!!!");
            SetPropW(ptrWnd, propName, hValue);
	    }
	    else
	    {
            Debug.Log("XYZ ======== ERROR !!!!!");
	        Application.Quit();
	    }
	}
}
