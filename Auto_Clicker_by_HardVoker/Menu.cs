using Auto_Clicker_by_HardVoker;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace AutoClicker
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }
        [DllImport("user32.dll", SetLastError = true)]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);

        private const uint MOUSEEVENTF_LEFTDOWN = 0x02;
        private const uint MOUSEEVENTF_LEFTUP = 0x04;

        private readonly (uint X, uint Y) point1 = (500, 300);
        private readonly (uint X, uint Y) point2 = (1200, 300);

        private const int clicksPerPoint = 700;
        private bool isClickerRunning = false;



        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Interval = 750000;
            timer1.Tick += Timer1_Tick;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {

            if (isClickerRunning)
            {
                PerformClicks(point1);
                PerformClicks(point2);
            }
        }

        private void PerformClicks((uint X, uint Y) point)
        {
            for (int i = 0; i < clicksPerPoint; i++)
            {
                if (!isClickerRunning) break;
                PerformClick(point);
                Thread.Sleep(100);
            }
        }

        private void PerformClick((uint X, uint Y) point)
        {
            Cursor.Position = new System.Drawing.Point((int)point.X, (int)point.Y);
            mouse_event(MOUSEEVENTF_LEFTDOWN, point.X, point.Y, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, point.X, point.Y, 0, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            isClickerRunning = true;
            timer1.Start();
            Timer1_Tick(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.ShowDialog();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Space)
            {
                if (isClickerRunning)
                {
                    button2_Click(this, new EventArgs());
                }
                return true;
            }
            else if (keyData == Keys.Escape)
            {
                if (isClickerRunning)
                {
                    button2_Click(this, new EventArgs());
                }
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Settings form1 = new Settings();
            form1.ShowDialog();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}