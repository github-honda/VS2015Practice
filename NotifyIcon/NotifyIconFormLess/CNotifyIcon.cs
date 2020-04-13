using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// add
using System.Windows.Forms;
using System.Drawing;

namespace NotifyIconFormLess
{
    class CNotifyIcon: IDisposable
    {
        // 20200410, Follow .NET coding convention naming rules.
        NotifyIcon NotifyIcon_;
        Boolean StatusRunning_;
        Icon IconRun_;
        Icon IconStop_;
        ToolStripMenuItem MenuRun_;
        ToolStripMenuItem MenuStop_;

        public CNotifyIcon()
        {
            ContextMenuStrip MenuStrip1;
            ToolStripMenuItem MenuExit;

            MenuStrip1 = new ContextMenuStrip();
            MenuRun_ = new ToolStripMenuItem();
            MenuStop_ = new ToolStripMenuItem();
            MenuExit = new ToolStripMenuItem();
            NotifyIcon_ = new NotifyIcon();

            StatusRunning_ = true;

            MenuRun_.Text = "&Run";
            MenuRun_.Click += MenuRun_Click;
            MenuStrip1.Items.Add(MenuRun_);

            MenuStop_.Text = "&Stop";
            MenuStop_.Click += MenuStop_Click;
            MenuStrip1.Items.Add(MenuStop_);

            MenuStrip1.Items.Add(new ToolStripSeparator());

            MenuExit.Text = "E&xit";
            MenuExit.Click += MenuExit_Click;
            MenuStrip1.Items.Add(MenuExit);

            Bitmap BitmapRun = new Bitmap(@"Resources\StatusOK_32x.png");
            Bitmap BitmapStop = new Bitmap(@"Resources\StatusStop_32x.png");
            IntPtr HIconRun = BitmapRun.GetHicon();
            IntPtr HIconStop = BitmapStop.GetHicon();
            IconRun_ = Icon.FromHandle(HIconRun);
            IconStop_ = Icon.FromHandle(HIconStop);

            NotifyIcon_.ContextMenuStrip = MenuStrip1; // mouse right click.
            NotifyIcon_.Visible = true;
            //NotifyIcon1.DoubleClick += MyNotifyIcon_DoubleClick;
            NotifyIcon_.MouseClick += MyNotifyIcon_MouseClick;
            NotifyIcon_.Text = Application.ProductName + ", " + Application.ProductVersion;
            NotifyIcon_.Visible = true;
            ShowNotifyStatus();
        }


        private void MyNotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show("MyNotifyIcon_DoubleClick");
        }

        private void MenuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

        private void MyNotifyIcon_MouseClick(object sender, MouseEventArgs e)
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
                ZSetIcon(NotifyIcon_, IconRun_);
                NotifyIcon_.Text = "Running";
                MenuRun_.Enabled = false;
                MenuStop_.Enabled = true;
            }
            else
            {
                ZSetIcon(NotifyIcon_, IconStop_);
                NotifyIcon_.Text = "Stop";
                MenuRun_.Enabled = true;
                MenuStop_.Enabled = false;
            }
        }

        public static void ZSetIcon(NotifyIcon NotifyIcon1, Icon icon1)
        {
            // If you want to use the same image in multiple PictureBox controls, create a clone of the image for each PictureBox. 
            if (NotifyIcon1.Icon != null)
            {
                NotifyIcon1.Icon.Dispose();
                NotifyIcon1.Icon = null;
            }
            NotifyIcon1.Icon = icon1;
        }


        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (NotifyIcon_ != null)
                        NotifyIcon_.Dispose();
                }
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
