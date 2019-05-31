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

namespace WinService1
{
    public partial class Service1 : ServiceBase
    {
        const string csProjectID = "myProjectID";
        string msDirectory = @"c:\temp";
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
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
