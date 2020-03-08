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
        string _Resource1 = "Init"; // 共用資源.
        public void Run()
        {
            Console.WriteLine("Task1: 送出動作函數, 無傳入參數, 系統會回呼動作函數.");
            Console.WriteLine($"Run(), ThreadId={Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"_Resource1={_Resource1}.");

            Console.WriteLine("Task.Start() 通知系統啟動工作. 主執行緒不會封鎖(Block), 會立即執行下一個指令.");
            Task task1 = new Task(myAction1); // 建立工作.
            task1.Start(); 

            Console.WriteLine("子執行緒(5秒)未完成前按鍵, 才會看到'共用資源使用中'.");
            Console.WriteLine("Press any key to continuing...");
            Console.ReadKey();
            Console.WriteLine($"_Resource1={_Resource1}.");
        }
        private void myAction1()
        {
            Console.WriteLine($"Begin myAction1(), ThreadId={Thread.CurrentThread.ManagedThreadId}.");
            Console.WriteLine($"Task.CurrentId={Task.CurrentId}.");

            _Resource1 = "共用資源使用中";
            Thread.Sleep(5000);

            _Resource1 = "共用資源處理完成";
            Console.WriteLine($"End myAction1().");
        }
    }
}
