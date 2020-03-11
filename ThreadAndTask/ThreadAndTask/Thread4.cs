using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// add
using System.Threading;
using System.ComponentModel;
using System.Diagnostics;

namespace ThreadAndTask
{
    public class Thread4
    {
        string _Resource1; // 共用資源.
        public void Run()
        {
            Console.WriteLine($"Thread4.Run(), ThreadId={Thread.CurrentThread.ManagedThreadId}");
            _Resource1 = "共用資源初始化";
            State1 myPara1 = new State1 { _Value1 = "參數初始化" };

            BackgroundWorker bkWorker = new BackgroundWorker(); // 建立背景工作 BackgroundWorker.
            bkWorker.DoWork += new DoWorkEventHandler(myDoWork); // 指定啟動時回呼函數.
            bkWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(myCompleted); // 指定完成時回呼函數.
            bkWorker.RunWorkerAsync(myPara1); // 啟動背景工作, 等候(啟動或完成)事件通知執行子執行緒.
            Console.WriteLine("若提早按鍵, 就會看到'共用資源使用中'. 因子執行緒/工作需要5秒才能完成.");
            Console.WriteLine("Press any key to continuing...");
            Console.ReadKey();

            Console.WriteLine($"_Resource1={_Resource1}.");  // 共用資源結果.
            Console.WriteLine($"myPara1._Value1={myPara1._Value1}."); // 參數處理結果.
            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }
        private void myCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Console.WriteLine($"myCompleted() ThreadId={Thread.CurrentThread.ManagedThreadId}");

            State1 c1 = e.Result as State1; // 接收參數處理結果
            c1._Value1 = "參數處理完成";
            _Resource1 = "共用資源處理完成";

        }
        private void myDoWork(object sender, DoWorkEventArgs e)
        {
            Console.WriteLine($"myDoWork(), ThreadId={Thread.CurrentThread.ManagedThreadId}");

            State1 c1 = e.Argument as State1; // 接收參數
            _Resource1 = "共用資源使用中";
            c1._Value1 = "參數使用中";

            Thread.Sleep(5000);

            e.Result = c1; // 存入參數處理結果.
        }
    }
}
