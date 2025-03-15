using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using CSCore.CoreAudioAPI;
using System.Runtime.CompilerServices;

namespace ScreenSleeper2
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        /// 
        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);


        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            System.Diagnostics.Debug.WriteLine("1==================");
            ApplicationConfiguration.Initialize();
            Form1 f = new Form1();
            Application.Run();
        }


    }

}