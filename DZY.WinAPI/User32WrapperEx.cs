using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Interop;
using static DZY.WinAPI.User32Wrapper;

namespace DZY.WinAPI
{
    public static class User32WrapperEx
    {
        public static List<DisplayDevice> Display()
        {
            DisplayDevice d = new DisplayDevice();
            var devices = new List<DisplayDevice>();
            d.cb = Marshal.SizeOf(d);

            for (uint id = 0; EnumDisplayDevices(null, id, ref d, 0); id++)
            {
                if (d.StateFlags.HasFlag(DisplayDeviceStateFlags.AttachedToDesktop))
                {
                    d.cb = Marshal.SizeOf(d);
                    EnumDisplayDevices(d.DeviceName, 0, ref d, 0);
                    Console.WriteLine("{0}, {1}", d.DeviceName, d.DeviceString);
                }

                d.cb = Marshal.SizeOf(d);
                devices.Add(d);
            }
            return devices;
        }

        public static List<MONITORINFO> GetDisplays()
        {
            var tmp = new List<MONITORINFO>();
            MonitorEnumDelegate callback = (IntPtr hDesktop, IntPtr hdc, ref RECT rect, int d) =>
            {
                MONITORINFO info = new MONITORINFO();
                bool isok = GetMonitorInfo(hDesktop, info);
                if (isok)
                    tmp.Add(info);
                return true;
            };

            if (EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, callback, 0))
            {
                return tmp;
            }

            return null;
        }

        /// <summary>
        /// 获取窗口的进程号
        /// </summary>
        /// <param name="hwnd">窗口句柄</param>
        /// <returns>进程号</returns>
        public static int GetProcessId(IntPtr hwnd)
        {
            int processId;
            GetWindowThreadProcessId(hwnd, out processId);
            return processId;
        }

        public static bool SetWindowPosEx(IntPtr targeHandler, RECT rect)
        {
            var ok = SetWindowPos(targeHandler, IntPtr.Zero, rect.Left, rect.Top, rect.Width, rect.Height, 0);
            return ok;
        }

        public static string GetWindowTextEx(IntPtr hWnd)
        {
            int size = GetWindowTextLength(hWnd);
            if (size > 0)
            {
                var builder = new StringBuilder(size + 1);
                GetWindowText(hWnd, builder, builder.Capacity);
                return builder.ToString();
            }

            return string.Empty;
        }

        public static void HideWindowFromSwithcher(Window window)
        {
            WindowInteropHelper wndHelper = new WindowInteropHelper(window);

            int exStyle = (int)User32Wrapper.GetWindowLong(wndHelper.Handle, WindowLongFlags.GWL_EXSTYLE);

            exStyle |= (int)WindowStyles.WS_EX_TOOLWINDOW;
            User32Wrapper.SetWindowLong(wndHelper.Handle, WindowLongFlags.GWL_EXSTYLE, exStyle);
        }

        #region DPI 相关

        public static DPI_AWARENESS SetThreadAwarenessContext(DPI_AWARENESS_CONTEXT newContext)
        {
            var oldContext = SetThreadDpiAwarenessContext(newContext);

            return GetAwarenessFromDpiAwarenessContext(oldContext);
        }

        public static DPI_AWARENESS GetThreadAwarenessContext()
        {
            var context = GetThreadDpiAwarenessContext();

            return GetAwarenessFromDpiAwarenessContext(context);
        }

        public static DPI_AWARENESS GetWindowAwarenessContext(IntPtr handle)
        {
            var context = GetWindowDpiAwarenessContext(handle);

            return GetAwarenessFromDpiAwarenessContext(context);
        }

        #endregion
    }
}
