using System;
using System.Diagnostics;

namespace DZY.WinAPI.Extension
{
    public static class ProcessExtension
    {
        public static void Suspend(this Process process)
        {
            foreach (ProcessThread thread in process.Threads)
            {
                var pOpenThread = Kernel32Wrapper.OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)thread.Id);
                if (pOpenThread == IntPtr.Zero)
                {
                    break;
                }
                Kernel32Wrapper.SuspendThread(pOpenThread);
            }
        }
        public static void Resume(this Process process)
        {
            foreach (ProcessThread thread in process.Threads)
            {
                var pOpenThread = Kernel32Wrapper.OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)thread.Id);
                if (pOpenThread == IntPtr.Zero)
                {
                    break;
                }
                Kernel32Wrapper.ResumeThread(pOpenThread);
            }
        }
    }
}
