//https://stackoverflow.com/questions/3342941/kill-child-process-when-parent-process-is-killed
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DZY.WinAPI.Helpers
{
    public class ProcessJobTracker : IDisposable
    {
        private readonly IntPtr _jobHandle;

        public ProcessJobTracker()
        {
            string jobName = nameof(ProcessJobTracker) + Process.GetCurrentProcess().Id;
            _jobHandle = Kernel32Wrapper.CreateJobObject(IntPtr.Zero, jobName);

            var extendedInfo = new JOBOBJECT_EXTENDED_LIMIT_INFORMATION
            {
                BasicLimitInformation = new JOBOBJECT_BASIC_LIMIT_INFORMATION
                {
                    LimitFlags = JOBOBJECTLIMIT.JOB_OBJECT_LIMIT_KILL_ON_JOB_CLOSE,
                },
            };

            int length = Marshal.SizeOf(extendedInfo);
            IntPtr pExtendedInfo = Marshal.AllocHGlobal(length);
            try
            {
                Marshal.StructureToPtr(extendedInfo, pExtendedInfo, false);
                try
                {
                    if (!Kernel32Wrapper.SetInformationJobObject(_jobHandle, JobObjectInfoType.ExtendedLimitInformation, pExtendedInfo, (uint)length))
                    {
                        throw new Win32Exception();
                    }
                }
                finally
                {
                    Marshal.DestroyStructure<JOBOBJECT_EXTENDED_LIMIT_INFORMATION>(pExtendedInfo);
                }
            }
            finally
            {
                Marshal.FreeHGlobal(pExtendedInfo);
            }
        }

        /// <summary>
        /// 当前进程关闭时，关闭目标进程
        /// </summary>
        /// <param name="process">The process whose lifetime should never exceed the lifetime of the current process.</param>
        public void AddProcess(Process process)
        {
            if (process == null)
                return;

            bool success = Kernel32Wrapper.AssignProcessToJobObject(_jobHandle, process.Handle);
            if (!success && !process.HasExited)
            {
                throw new Win32Exception();
            }
        }

        public void Dispose()
        {
            Kernel32Wrapper.CloseHandle(_jobHandle);
        }
    }
}
