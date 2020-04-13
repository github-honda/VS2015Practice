using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotifyIconFormLess
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Application.Run(new Form1());
            using (CNotifyIcon notify1 = new CNotifyIcon())
            {
                 // Begins running a standard application message loop on the current thread, without a form.
                Application.Run(); // 執行完 ApplicationExit 後才會結束本行指令.
            }
        }
    }
}
