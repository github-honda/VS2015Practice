using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
// add
using System.IO;
using System.Timers;
namespace WinServiceTimer
{
    public partial class Service1 : ServiceBase
    {
        const string csProjectID = "TimerService";
        string msDirectory = @"c:\temp";
        Timer mTimer1;
        long miLastRun;
        public Service1()
        {
            InitializeComponent();
        }
        protected override void OnStart(string[] args)
        {
            miLastRun = long.Parse(DateTime.Now.AddMinutes(-1).ToString("yyyyMMddHHmm"));
            mTimer1 = new Timer(1000);
            mTimer1.Elapsed += MTimer1_Elapsed;
            mTimer1.Start();
        }
        private void MTimer1_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                long iThisRun = long.Parse(DateTime.Now.ToString("yyyyMMddHHmm"));
                if (iThisRun <= miLastRun)
                    return;
                mTimer1.Stop(); 
                myOnStart(); 
                miLastRun = iThisRun;
                mTimer1.Start(); 
            }
            catch (Exception ex)
            {
                WriteException(ex);
            }
        }
        void myOnStart()
        {
            // 服務啟動時, 測試寫入1個檔案以及1筆EventLog, 內容均包含OnStart時間.
            try
            {
                BuildDirectoryTemp();
                string sFile = Path.Combine(msDirectory, $"{csProjectID}-OnStart.txt");
                string sMsg = string.Format($"{csProjectID}-OnStart, {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
                EventLog.WriteEntry(sMsg);
                using (StreamWriter sw = new StreamWriter(sFile, true, Encoding.UTF8))
                {
                    sw.WriteLine(sMsg);
                }
            }
            catch (Exception ex)
            {
                WriteException(ex);
            }
        }
        protected override void OnStop()
        {
            // 服務結束時, 測試寫入1個檔案以及1筆EventLog, 內容均包含OnStop時間.
            try
            {
                BuildDirectoryTemp();
                string sFile = Path.Combine(msDirectory, $"{csProjectID}-OnStop.txt");
                string sMsg = string.Format($"{csProjectID}-OnStop, {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
                EventLog.WriteEntry(sMsg);
                using (StreamWriter sw = new StreamWriter(sFile, true, Encoding.UTF8))
                {
                    sw.WriteLine(sMsg);
                }
            }
            catch (Exception ex)
            {
                WriteException(ex);
            }
        }
        private void BuildDirectoryTemp()
        {
            if (!Directory.Exists(msDirectory))
                Directory.CreateDirectory(msDirectory);
        }
        private void WriteException(Exception ex)
        {
            BuildDirectoryTemp();
            string sFile = Path.Combine(msDirectory, $"{csProjectID}-Exception.txt");
            string sMsg = string.Format($"{csProjectID}-Exception, {DateTime.Now.ToString("HH:mm:ss")}, {ex.Message}");
            EventLog.WriteEntry(sMsg);
            using (StreamWriter sw = new StreamWriter(sFile, true, Encoding.UTF8))
            {
                sw.WriteLine(sMsg);
                sw.WriteLine(ex.Message);
                sw.WriteLine(ex.StackTrace);
            }
        }
    }
}
