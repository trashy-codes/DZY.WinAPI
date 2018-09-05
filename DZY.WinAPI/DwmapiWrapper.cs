using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace DZY.WinAPI
{
    [Flags]
    public enum BbFlags : byte //Blur Behind Flags
    {
        DwmBbEnable = 1,
        DwmBbBlurregion = 2,
        DwmBbTransitiononmaximized = 4,
    };
    [StructLayout(LayoutKind.Sequential)]
    public struct BbStruct //Blur Behind Structure
    {
        public BbFlags Flags;
        public bool Enable;
        public IntPtr Region;
        public bool TransitionOnMaximized;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct Margins
    {
        public int cxLeftWidth;      // width of left border that retains its size
        public int cxRightWidth;     // width of right border that retains its size
        public int cyTopHeight;      // height of top border that retains its size
        public int cyBottomHeight;   // height of bottom border that retains its size
    };
    [StructLayout(LayoutKind.Sequential)]
    public struct DWM_COLORIZATION_PARAMS
    {
        public int clrColor;
        public int clrAfterGlow;
        public int nIntensity;
        public int clrAfterGlowBalance;
        public int clrBlurBalance;
        public int clrGlassReflectionIntensity;
        public bool fOpaque;
    }
    public class DwmapiWrapper
    {

        [DllImport("dwmapi.dll")]
        public static extern int DwmEnableBlurBehindWindow(IntPtr hWnd, ref BbStruct blurBehind);

        [DllImport("DwmApi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hwnd, ref Margins pMarInset);

        [DllImport("dwmapi.dll", EntryPoint = "#127", PreserveSig = false)]
        public static extern void DwmGetColorizationParameters(out DWM_COLORIZATION_PARAMS parameters);

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern bool DwmIsCompositionEnabled();

        [DllImport("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled(out bool enabled);

        [DllImport("dwmapi.dll", EntryPoint = "#131", PreserveSig = false)]
        public static extern void DwmSetColorizationParameters(ref DWM_COLORIZATION_PARAMS parameters, long uUnknown);

        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

    }
}
