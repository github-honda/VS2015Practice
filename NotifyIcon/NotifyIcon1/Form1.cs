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
        // 20200410, Follow .NET coding convention naming rules.
        ContextMenu ContextMenu_;
        MenuItem MenuRun_;
        MenuItem MenuStop_;
        MenuItem MenuExit_;
        Boolean StatusRunning_;
        Icon IconRun_;
        Icon IconStop_;
        public Form1()
        {
            InitializeComponent();
            ContextMenu_ = new ContextMenu();
            MenuExit_ = new MenuItem();
            MenuRun_ = new MenuItem();
            MenuStop_ = new MenuItem();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StatusRunning_ = true;

            Text = Application.ProductName + ", " + Application.ProductVersion;
            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;

            ContextMenu_.MenuItems.AddRange(new MenuItem[] { MenuRun_, MenuStop_, MenuExit_ });
            MenuRun_.Index = 0;
            MenuRun_.Text = "&Run";
            MenuRun_.Click += MenuRun_Click;
            MenuStop_.Index = 1;
            MenuStop_.Text = "&Stop";
            MenuStop_.Click += MenuStop_Click;
            MenuExit_.Index = 2;
            MenuExit_.Text = "&Exit";
            MenuExit_.Click += MenuExit_Click;

            Bitmap BitmapRun = new Bitmap(@"Resources\StatusOK_32x.png");
            Bitmap BitmapStop = new Bitmap(@"Resources\StatusStop_32x.png");
            IntPtr HIconRun = BitmapRun.GetHicon();
            IntPtr HIconStop = BitmapStop.GetHicon();
            IconRun_ = Icon.FromHandle(HIconRun);
            IconStop_ = Icon.FromHandle(HIconStop);

            NotifyIcon1.ContextMenu = ContextMenu_; // mouse right click.
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
            if (StatusRunning_)
            {
                NotifyIcon1.Icon = IconRun_;
                NotifyIcon1.Text = "Running";
                MenuRun_.Enabled = false;
                MenuStop_.Enabled = true;
            }
            else
            {
                NotifyIcon1.Icon = IconStop_;
                NotifyIcon1.Text = "Stop";
                MenuRun_.Enabled = true;
                MenuStop_.Enabled = false;
            }
        }

        private void MenuStop_Click(object sender, EventArgs e)
        {
            StatusRunning_ = false;
            ShowNotifyStatus();
        }


        private void MenuRun_Click(object sender, EventArgs e)
        {
            StatusRunning_ = true;
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
