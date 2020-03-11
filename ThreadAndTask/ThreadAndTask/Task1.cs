using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// add
using System.Threading;

namespace ThreadAndTask
{
    public class Task1
    {
        string _Resource1; // 共用資源.
        public void Run()
        {
            Console.WriteLine($"Task1.Run(), ThreadId={Thread.CurrentThread.ManagedThreadId}");
            _Resource1 = "共用資源初始化";

            Task task1 = new Task(myAction1); // 建立工作.
            task1.Start(); // Task(Action).Start() 啟動執行無參數傳入的 Action
            Console.WriteLine("若提早按鍵, 就會看到'共用資源使用中'. 因子執行緒/工作需要5秒才能完成.");
            Console.WriteLine("Press any key to continuing...");
            Console.ReadKey();

            Console.WriteLine($"_Resource1={_Resource1}.");  // 共用資源結果.
            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }
        private void myAction1()
        {
            Console.WriteLine($"myAction1(), ThreadId={Thread.CurrentThread.ManagedThreadId}.");
            Console.WriteLine($"Task.CurrentId={Task.CurrentId}.");

            _Resource1 = "共用資源使用中";
            Thread.Sleep(5000);
            _Resource1 = "共用資源處理完成";
        }
    }
}
