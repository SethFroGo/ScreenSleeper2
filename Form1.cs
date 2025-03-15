using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using CSCore.CoreAudioAPI;
using System.Reflection;

namespace ScreenSleeper2
{
    public partial class Form1 : Form
    {

        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);
        
        public Form1()
        {
            InitializeComponent(); 
            Point CurrentPosition = Cursor.Position;
            System.Diagnostics.Debug.WriteLine(CurrentPosition);
            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromMinutes(2);

            var timer = new System.Threading.Timer((e) =>
            {
                if (Cursor.Position == CurrentPosition && !IsAudioPlaying(GetDefaultRenderDevice()))
                {
                    TurnOffScreens();
                }
                CurrentPosition = Cursor.Position;
                System.Diagnostics.Debug.WriteLine(CurrentPosition);
            }, null, startTimeSpan, periodTimeSpan);
        }


        public void wait(int milliseconds)
        {
            var timer1 = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0) return;

            // Console.WriteLine("start wait timer");
            timer1.Interval = milliseconds;
            timer1.Enabled = true;
            timer1.Start();

            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                timer1.Stop();
                // Console.WriteLine("stop wait timer");
            };

            while (timer1.Enabled)
            {
                Application.DoEvents();
            }
        }

        public static bool IsAudioPlaying(MMDevice device)
        {
            using (var meter = AudioMeterInformation.FromDevice(device))
            {
                return meter.PeakValue > 0;
            }
        }

        public static MMDevice GetDefaultRenderDevice()
        {
            using (var enumerator = new MMDeviceEnumerator())
            {
                return enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);
            }
        }

        public static void TurnOffScreens()
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

    }
}
