using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;

namespace time
{


    public partial class Form1 : Form
    {
        private Thread trd;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /**建立接收进程*/
            trd = new Thread(new ThreadStart(this.ThreadTask));
            trd.IsBackground = true;
            trd.Start();

            /* 建立一个线程，显示当前时间 */
        }

        public void ThreadTask()
        {
            while (true)
            {
                DateTime now = DateTime.Now;

                lbTime.Text = now.ToString();

                if  (-1 != now.ToString().IndexOf("17:25:00") ||
                    -1 != now.ToString().IndexOf("20:25:00")
                    )
                {
                    MyBeep.myBeep();
                    MessageBox.Show("快打卡了！要不一天白干了！！！");
                    System.Diagnostics.Process.Start("http://atm.zte.com.cn"); 
                }


            }

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            trd.Abort();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                this.notifyIcon1.Visible = true;
            }
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            this.Visible = true;

            this.WindowState = FormWindowState.Normal;

            this.notifyIcon1.Visible = false;

        }

        private void lbTime_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start("http://atm.zte.com.cn"); 
        }
    }

    public class MyBeep
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool Beep(uint dwFreq, uint dwDuration);

        public static void myBeep()
        {

            Beep(1000, 1000);

        }

    }
}