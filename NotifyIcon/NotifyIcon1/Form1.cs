using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotifyIcon1
{
    public partial class Form1 : Form
    {
        ContextMenu contextMenu1;
        MenuItem menuRun;
        MenuItem menuStop;
        MenuItem menuExit;
        Boolean StatusRunning;
        Icon IconRun;
        Icon IconStop;
        public Form1()
        {
            InitializeComponent();
            contextMenu1 = new ContextMenu();
            menuExit = new MenuItem();
            menuRun = new MenuItem();
            menuStop = new MenuItem();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StatusRunning = true;

            Text = Application.ProductName + ", " + Application.ProductVersion;
            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;

            contextMenu1.MenuItems.AddRange(new MenuItem[] { menuRun, menuStop, menuExit });
            menuRun.Index = 0;
            menuRun.Text = "&Run";
            menuRun.Click += MenuRun_Click;
            menuStop.Index = 1;
            menuStop.Text = "&Stop";
            menuStop.Click += MenuStop_Click;
            menuExit.Index = 2;
            menuExit.Text = "&Exit";
            menuExit.Click += MenuExit_Click;

            Bitmap BitmapRun = new Bitmap(@"Resources\StatusOK_32x.png");
            Bitmap BitmapStop = new Bitmap(@"Resources\StatusStop_32x.png");
            IntPtr HIconRun = BitmapRun.GetHicon();
            IntPtr HIconStop = BitmapStop.GetHicon();
            IconRun = Icon.FromHandle(HIconRun);
            IconStop = Icon.FromHandle(HIconStop);

            NotifyIcon1.ContextMenu = contextMenu1; // mouse right click.
            NotifyIcon1.Visible = true;
            //notifyIcon1.DoubleClick += NotifyIcon1_DoubleClick;
            NotifyIcon1.MouseClick += NotifyIcon1_MouseClick;
            ShowNotifyStatus();
        }

        private void NotifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MessageBox.Show("Please click with mouse right button.");
            }
        }

        void ShowNotifyStatus()
        {
            if (StatusRunning)
            {
                NotifyIcon1.Icon = IconRun;
                NotifyIcon1.Text = "Running";
                menuRun.Enabled = false;
                menuStop.Enabled = true;
            }
            else
            {
                NotifyIcon1.Icon = IconStop;
                NotifyIcon1.Text = "Stop";
                menuRun.Enabled = true;
                menuStop.Enabled = false;
            }
        }

        private void MenuStop_Click(object sender, EventArgs e)
        {
            StatusRunning = false;
            ShowNotifyStatus();
        }


        private void MenuRun_Click(object sender, EventArgs e)
        {
            StatusRunning = true;
            ShowNotifyStatus();
        }

        private void NotifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
                WindowState = FormWindowState.Normal;
            Activate();
        }

        private void MenuExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
