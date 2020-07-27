using DZY.WinAPI;
using DZY.WinAPI.Desktop.API;
using DZY.WinAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var cp = Process.GetCurrentProcess();
            new DZY.WinAPI.Helpers.OtherProgramChecker(cp.Id).CheckMaximized(out List<Screen> fullscreens);
            System.Windows.Forms.MessageBox.Show(fullscreens.Count.ToString());
        }

        private void BtnTest_Click(object sender, RoutedEventArgs e)
        {
            var test0 = System.Windows.Forms.Screen.AllScreens;
            var test = User32WrapperEx.GetDisplays();
            var test2 = User32WrapperEx.Display();

            var desktopFactory = DesktopWallpaperFactory.Create();
            desktopFactory.GetSlideshowOptions(out DesktopSlideshowOptions options, out uint slide);
            desktopFactory.SetSlideshowOptions(DesktopSlideshowOptions.DSO_SHUFFLEIMAGES, 1000 * 60);
            desktopFactory.SetSlideshowOptions(DesktopSlideshowOptions.DSO_SHUFFLEIMAGES, 1000 * 60 * 60 * 24);
            desktopFactory.GetSlideshowOptions(out DesktopSlideshowOptions option1s, out uint sli1de);

        }

        private void BtnWindowsSettingTest_Click(object sender, RoutedEventArgs e)
        {
            _ = User32Wrapper.SystemParametersInfo(User32Wrapper.SPI_SETCLIENTAREAANIMATION, 0, true, User32Wrapper.SPIF_UPDATEINIFILE | User32Wrapper.SPIF_SENDWININICHANGE);
        }

        private void BtnCreateProcess_Click(object sender, RoutedEventArgs e)
        {
            //const uint NORMAL_PRIORITY_CLASS = 0x0020;

            //bool retValue;
            //string Application = Environment.GetEnvironmentVariable("windir") + @"\Notepad.exe";
            //Application = @"D:\github-categorized\dotnet\LiveWallpaperEngine\LiveWallpaperEngine.Samples.NetCore.Test\WallpaperSamples\game\sheep.exe";
            //string CommandLine = @" c:\boot.ini";
            //PROCESS_INFORMATION pInfo = new PROCESS_INFORMATION();
            //STARTUP_INFO sInfo = new STARTUP_INFO();
            //SECURITY_ATTRIBUTES pSec = new SECURITY_ATTRIBUTES();
            //SECURITY_ATTRIBUTES tSec = new SECURITY_ATTRIBUTES();
            //pSec.nLength = Marshal.SizeOf(pSec);
            //tSec.nLength = Marshal.SizeOf(tSec);

            //const int STARTF_USEPOSITION = 0x00000004;
            //sInfo.dwFlags = STARTF_USEPOSITION;
            //sInfo.dwX = 11111115;
            //sInfo.dwY = 111115;
            //sInfo.cb = Marshal.SizeOf(sInfo);

            ////Open Notepad
            //retValue = Kernel32Wrapper.CreateProcess(Application, null,
            //ref pSec, ref tSec, false, NORMAL_PRIORITY_CLASS,
            //IntPtr.Zero, null, ref sInfo, out pInfo);

            //Console.WriteLine("Process ID (PID): " + pInfo.dwProcessId);
            //Console.WriteLine("Process Handle : " + pInfo.hProcess);
        }

        ProcessJobTracker pj = null;
        private void BtnProcessJobTracker_Click(object sender, RoutedEventArgs e)
        {
            var p = Process.Start("notepad.exe");
            if (pj == null)
                pj = new ProcessJobTracker();
            pj.AddProcess(p);
        }
    }
}