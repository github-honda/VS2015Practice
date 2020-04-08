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
        NotifyIcon NotifyIcon1;
        Boolean StatusRunning;
        Icon IconRun;
        Icon IconStop;
        ToolStripMenuItem MenuRun;
        ToolStripMenuItem MenuStop;

        public CNotifyIcon()
        {
            ContextMenuStrip contextMenu1;
            ToolStripMenuItem menuExit;

            contextMenu1 = new ContextMenuStrip();
            MenuRun = new ToolStripMenuItem();
            MenuStop = new ToolStripMenuItem();
            menuExit = new ToolStripMenuItem();
            NotifyIcon1 = new NotifyIcon();

            StatusRunning = true;

            MenuRun.Text = "&Run";
            MenuRun.Click += MenuRun_Click;
            contextMenu1.Items.Add(MenuRun);

            MenuStop.Text = "&Stop";
            MenuStop.Click += MenuStop_Click;
            contextMenu1.Items.Add(MenuStop);

            contextMenu1.Items.Add(new ToolStripSeparator());

            menuExit.Text = "E&xit";
            menuExit.Click += MenuExit_Click;
            contextMenu1.Items.Add(menuExit);

            Bitmap BitmapRun = new Bitmap(@"Resources\StatusOK_32x.png");
            Bitmap BitmapStop = new Bitmap(@"Resources\StatusStop_32x.png");
            IntPtr HIconRun = BitmapRun.GetHicon();
            IntPtr HIconStop = BitmapStop.GetHicon();
            IconRun = Icon.FromHandle(HIconRun);
            IconStop = Icon.FromHandle(HIconStop);

            NotifyIcon1.ContextMenuStrip = contextMenu1; // mouse right click.
            NotifyIcon1.Visible = true;
            //NotifyIcon1.DoubleClick += MyNotifyIcon_DoubleClick;
            NotifyIcon1.MouseClick += MyNotifyIcon_MouseClick;
            NotifyIcon1.Text = Application.ProductName + ", " + Application.ProductVersion;
            NotifyIcon1.Visible = true;
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
            StatusRunning = false;
            ShowNotifyStatus();
        }

        private void MenuRun_Click(object sender, EventArgs e)
        {
            StatusRunning = true;
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
            if (StatusRunning)
            {
                NotifyIcon1.Icon = IconRun;
                NotifyIcon1.Text = "Running";
                MenuRun.Enabled = false;
                MenuStop.Enabled = true;
            }
            else
            {
                NotifyIcon1.Icon = IconStop;
                NotifyIcon1.Text = "Stop";
                MenuRun.Enabled = true;
                MenuStop.Enabled = false;
            }
        }


        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (NotifyIcon1 != null)
                        NotifyIcon1.Dispose();
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
