using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace DZY.WinAPI
{
    #region 结构体

    /// <summary>
    /// Process information from the create process call
    /// Represents PROCESS_INFORMATION in win32
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PROCESS_INFORMATION
    {
        /// <summary>
        /// A handle to the newly created process. The handle is used to specify the process in all functions that perform operations on the process object.
        /// </summary>
        public IntPtr hProcess;

        /// <summary>
        /// A handle to the primary thread of the newly created process. The handle is used to specify the thread in all functions that perform operations on the thread object
        /// </summary>
        public IntPtr hThread;

        /// <summary>
        /// A value that can be used to identify a process.
        /// The value is valid from the time the process is created until all handles to the process are closed and 
        /// the process object is freed; at this point, the identifier may be reused.
        /// </summary>
        public int dwProcessId;

        /// <summary>
        /// A value that can be used to identify a thread. The value is valid from the time the thread is created until all handles to the thread are closed and the thread object is freed; at this point, the identifier may be reused.
        /// </summary>
        public int dwThreadId;
    }
    /// <summary>
    /// Structure that contains the startupinfo
    /// Represents STARTUP_INFO in win32
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct STARTUP_INFO
    {
        /// <summary>
        /// The size of the structure, in bytes.
        /// </summary>
        public Int32 cb;

        /// <summary>
        /// Reserved; must be NULL
        /// </summary>
        public string lpReserved;

        /// <summary>
        /// The name of the desktop, or the name of both the desktop and window station for this process.
        /// A backslash in the string indicates that the string includes both the desktop and window station names
        /// </summary>
        public string lpDesktop;

        /// <summary>
        /// For console processes, this is the title displayed in the title bar if a new console window is created. 
        /// If NULL, the name of the executable file is used as the window title instead. 
        /// This parameter must be NULL for GUI or console processes that do not create a new console window
        /// </summary>
        public string lpTitle;

        /// <summary>
        /// If dwFlags specifies STARTF_USEPOSITION, this member is the x offset of the upper left corner of a window if a new window is created, in pixels. Otherwise, this member is ignored
        /// </summary>
        public Int32 dwX;

        /// <summary>
        /// If dwFlags specifies STARTF_USEPOSITION, this member is the y offset of the upper left corner of a window if a new window is created, in pixels. Otherwise, this member is ignored.
        /// </summary>
        public Int32 dwY;

        /// <summary>
        /// If dwFlags specifies STARTF_USESIZE, this member is the width of the window if a new window is created, in pixels. Otherwise, this member is ignored. 
        /// </summary>
        public Int32 dwXSize;

        /// <summary>
        /// If dwFlags specifies STARTF_USESIZE, this member is the height of the window if a new window is created, in pixels. Otherwise, this member is ignored.
        /// </summary>
        public Int32 dwYSize;

        /// <summary>
        /// If dwFlags specifies STARTF_USECOUNTCHARS, if a new console window is created in a console process, this member specifies the screen buffer width, in character columns. Otherwise, this member is ignored.
        /// </summary>
        public Int32 dwXCountChars;

        /// <summary>
        /// If dwFlags specifies STARTF_USECOUNTCHARS, if a new console window is created in a console process, this member specifies the screen buffer height, in character rows. Otherwise, this member is ignored.dwFillAttribute 
        /// </summary>
        public Int32 dwYCountChars;

        /// <summary>
        /// If dwFlags specifies STARTF_USEFILLATTRIBUTE, this member is the initial text and background colors if a new console window is created in a console application. Otherwise, this member is ignored. 
        /// </summary>
        public Int32 dwFillAttribute;

        /// <summary>
        /// A bit field that determines whether certain STARTUPINFO members are used when the process creates a window
        /// </summary>
        public Int32 dwFlags;

        /// <summary>
        /// If dwFlags specifies STARTF_USESHOWWINDOW, this member can be any of the SW_ constants defined in Winuser.h. Otherwise, this member is ignored.
        /// </summary>
        public Int16 wShowWindow;

        /// <summary>
        /// Reserved for use by the C Run-time; must be zero.
        /// </summary>
        public Int16 cbReserved2;

        /// <summary>
        /// Reserved for use by the C Run-time; must be NULL.
        /// </summary>
        public IntPtr lpReserved2;

        /// <summary>
        /// If dwFlags specifies STARTF_USESTDHANDLES, this member is the standard input handle for the process. Otherwise, this member is ignored and the default for standard input is the keyboard buffer.
        /// </summary>
        public IntPtr hStdInput;

        /// <summary>
        /// If dwFlags specifies STARTF_USESTDHANDLES, this member is the standard output handle for the process. Otherwise, this member is ignored and the default for standard output is the console window's buffer.
        /// </summary>
        public IntPtr hStdOutput;

        /// <summary>
        /// If dwFlags specifies STARTF_USESTDHANDLES, this member is the standard error handle for the process. Otherwise, this member is ignored and the default for standard error is the console window's buffer.
        /// </summary>
        public IntPtr hStdError;
    }
    /// <summary>
    /// Structure to contain security attributes from the create process call represents
    /// SECURITY_ATTRIBUTE in win32
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SECURITY_ATTRIBUTES
    {
        /// <summary>
        /// The size, in bytes, of this structure. Set this value to the size of the SECURITY_ATTRIBUTES structure
        /// </summary>
        public int nLength;

        /// <summary>
        /// A pointer to a security descriptor for the object that controls the sharing of it.
        /// If NULL is specified for this member, the object is assigned the default security descriptor of the calling process. 
        /// This is not the same as granting access to everyone by assigning a NULL discretionary access control list (DACL). 
        /// The default security descriptor is based on the default DACL of the access token belonging to the calling process.
        /// By default, the default DACL in the access token of a process allows access only to the user represented by the access token. 
        /// If other users must access the object, you can either create a security descriptor with the appropriate access, 
        /// or add ACEs to the DACL that grants access to a group of users. 
        /// </summary>
        public IntPtr lpSecurityDescriptor;

        /// <summary>
        /// A Boolean value that specifies whether the returned handle is inherited when a new process is created.
        /// If this member is TRUE, the new process inherits the handle.
        /// </summary>
        public int bInheritHandle;
    }

    #endregion
    public class Kernel32Wrapper
    {
        [DllImport("kernel32.dll")]
        public static extern bool SetProcessWorkingSetSize(IntPtr handle, int minimumWorkingSetSize, int maximumWorkingSetSize);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetConsoleWindow();
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);
        /// <summary>
        /// Create a new process
        /// </summary>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CreateProcess
        (
            string lpApplicationName,
            string lpCommandLine,
            ref SECURITY_ATTRIBUTES lpProcessAttributes,
            ref SECURITY_ATTRIBUTES lpThreadAttributes,
            [In, MarshalAs(UnmanagedType.Bool)]
            bool bInheritHandles,
            uint dwCreationFlags,
            IntPtr lpEnvironment,
            string lpCurrentDirectory,
            [In] ref STARTUP_INFO lpStartupInfo,
            out PROCESS_INFORMATION lpProcessInformation
        );
    }
}
