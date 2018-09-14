using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace DZY.WinAPI
{
    public class PsapiWrapper
    {
        [DllImport("psapi.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern uint GetModuleFileNameEx(IntPtr hProcess, IntPtr hModule, StringBuilder lpFilename, int nSize);

        public static string GetModuleFileNameEx(IntPtr hProcess, IntPtr hModule, int capacity = 260)
        {
            StringBuilder buffer;
            do
            {
                buffer = new StringBuilder(capacity);
                GetModuleFileNameEx(hProcess, hModule, buffer, buffer.Capacity);
                capacity *= 2;
            } while (Marshal.GetLastWin32Error() == 0x7A);
            return buffer.ToString();
        }
    }
}
