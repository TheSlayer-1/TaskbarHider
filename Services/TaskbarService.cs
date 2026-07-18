using System.Runtime.InteropServices;
using System.Windows;

namespace TaskbarHider.Services;

public class TaskbarService
{
    [DllImport("user32.dll")]
    private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

    [DllImport("user32.dll")]
    private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    private const int SW_HIDE = 0;
    private const int SW_SHOW = 5;
    private const uint SWP_NOSIZE = 0x0001;
    private const uint SWP_NOMOVE = 0x0002;

    private IntPtr _taskbarHandle;
    private bool _isHidden;

    public TaskbarService()
    {
        _taskbarHandle = FindWindow("Shell_traywnd", null);
        _isHidden = false;
    }

    public void HideTaskbar()
    {
        if (_taskbarHandle == IntPtr.Zero || _isHidden) return;

        try
        {
            ShowWindow(_taskbarHandle, SW_HIDE);
            _isHidden = true;
        }
        catch { }
    }

    public void ShowTaskbar()
    {
        if (_taskbarHandle == IntPtr.Zero || !_isHidden) return;

        try
        {
            ShowWindow(_taskbarHandle, SW_SHOW);
            _isHidden = false;
        }
        catch { }
    }

    public bool IsTaskbarHidden => _isHidden;

    public static int GetTaskbarHeight()
    {
        IntPtr taskbarHandle = FindWindow("Shell_traywnd", null);
        if (taskbarHandle == IntPtr.Zero) return 0;

        GetWindowRect(taskbarHandle, out RECT rect);
        return Math.Abs(rect.Bottom - rect.Top);
    }

    public static List<RECT> GetAllMonitorBounds()
    {
        var bounds = new List<RECT>();
        foreach (var screen in System.Windows.Forms.Screen.AllScreens)
        {
            bounds.Add(new RECT
            {
                Left = screen.Bounds.Left,
                Top = screen.Bounds.Top,
                Right = screen.Bounds.Right,
                Bottom = screen.Bounds.Bottom
            });
        }
        return bounds;
    }
}