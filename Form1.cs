using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using CSCore.CoreAudioAPI;
using System.Reflection;
using Microsoft.Win32;
using System.Diagnostics;
using System.Configuration;

namespace ScreenSleeper2
{
    public partial class Form1 : Form
    {
        public Form1 Instance;

        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        RegistryKey screenSaverKey = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop");

        Boolean saverMode;



        public Form1()
        {
            Instance = this;
            InitializeComponent();
            Point CurrentPosition = Cursor.Position;
            System.Diagnostics.Debug.WriteLine(CurrentPosition);
            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromMinutes(3);
            var screenSaved = false;
            this.saverMode = true;
            notifyIcon1.ContextMenuStrip.ShowCheckMargin = true;

            var timer = new System.Threading.Timer((e) =>
            {

                if (saverMode)
                {
                    if (Cursor.Position == CurrentPosition && !IsAudioPlaying(GetDefaultRenderDevice()) && !screenSaved)
                    {
                        if (screenSaverKey != null)
                        {
                            string screenSaverFilePath = screenSaverKey.GetValue("SCRNSAVE.EXE", string.Empty).ToString();
                            if (!string.IsNullOrEmpty(screenSaverFilePath) && File.Exists(screenSaverFilePath))
                            {
                                Process screenSaverProcess = Process.Start(new ProcessStartInfo(screenSaverFilePath, "/s"));
                                screenSaved = true;
                                screenSaverProcess.WaitForExit();
                            }
                        }
                    }
                    else if (Cursor.Position != CurrentPosition || IsAudioPlaying(GetDefaultRenderDevice()))
                    {
                        screenSaved = false;
                    }
                } else
                {
                    if (Cursor.Position == CurrentPosition && !IsAudioPlaying(GetDefaultRenderDevice()))
                    {
                        TurnOffScreens();
                    }
                }
                CurrentPosition = Cursor.Position;
                System.Diagnostics.Debug.WriteLine(CurrentPosition);
            }, null, startTimeSpan, periodTimeSpan);
        }


        public void wait(int milliseconds)
        {
            var timer1 = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0) return;

            
            timer1.Interval = milliseconds;
            timer1.Enabled = true;
            timer1.Start();

            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                timer1.Stop();
            };

            while (timer1.Enabled)
            {
                Application.DoEvents();
            }
        }

        public bool IsAudioPlaying(MMDevice device)
        {
            using (var meter = AudioMeterInformation.FromDevice(device))
            {
                return meter.PeakValue > 0;
            }
        }

        public MMDevice GetDefaultRenderDevice()
        {
            using (var enumerator = new MMDeviceEnumerator())
            {
                return enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);
            }
        }

        public void TurnOffScreens()
        {
            SendMessage(new Form().Handle, 0x0112, 0xF170, 2);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (IsAudioPlaying(GetDefaultRenderDevice()))
            {
                label1.Text = "true";
            } else
            {
                label1.Text = "false";
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            label1.Text = "OK";
            TurnOffScreens();

        }

        public void Form1_Load(object sender, EventArgs e)
        {
            this.ShowInTaskbar = true;
            this.Hide();
        }

        public void Toggle_Mode(object? sender, System.EventArgs e)
        {
            var item = (ToolStripMenuItem)notifyIcon1.ContextMenuStrip.Items[0];
            item.Checked = !item.Checked;
            this.saverMode = !this.saverMode;
        }

    }
}
