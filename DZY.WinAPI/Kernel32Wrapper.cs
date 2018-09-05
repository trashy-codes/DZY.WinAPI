using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace DZY.WinAPI
{
    public class Kernel32Wrapper
    {
        [DllImport("kernel32.dll")]
        public static extern bool SetProcessWorkingSetSize(IntPtr handle, int minimumWorkingSetSize, int maximumWorkingSetSize);
    }
}
