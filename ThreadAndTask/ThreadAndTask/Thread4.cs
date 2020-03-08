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
        string _Resource1 = "Init"; // 共用資源.
        public void Run()
        {
            Console.WriteLine("Thread4: 背景工作. 系統會排班回呼(啟動與完成時的)代理函數.");
            Console.WriteLine($"Run(), ThreadId={Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"_Resource1={_Resource1}.");

            Console.WriteLine("BackgroundWorker.RunWorkerAsync() 通知系統啟動子執行緒, 並傳入參數. 主執行緒不會封鎖(Block), 會立即執行下一個指令.");
            BackgroundWorker bkWorker = new BackgroundWorker(); // 建立背景工作 BackgroundWorker.
            bkWorker.DoWork += new DoWorkEventHandler(myDoWork); // 指定啟動時回呼函數.
            bkWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(myCompleted); // 指定完成時回呼函數.
            bkWorker.RunWorkerAsync(new State1 { _Value1 = "Init" }); 

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }
        private void myCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Console.WriteLine($"Begin myCompleted() 執行緒 ID={Thread.CurrentThread.ManagedThreadId}");

            State1 c1 = e.Result as State1; // 取得參數結果.
            _Resource1 = c1._Value1;
             
            Console.WriteLine($"_Resource1 = {_Resource1}");
            Console.WriteLine($"End myCompleted()");
        }
        private void myDoWork(object sender, DoWorkEventArgs e)
        {
            Console.WriteLine($"Begin myDoWork(), ThreadId={Thread.CurrentThread.ManagedThreadId}");

            _Resource1 = "共用資源使用中";
            State1 c1 = e.Argument as State1; // 接收參數
            c1.Method1();
            e.Result = c1; // 存入參數結果.
            Console.WriteLine($"End myDoWork()");
        }
    }
}
