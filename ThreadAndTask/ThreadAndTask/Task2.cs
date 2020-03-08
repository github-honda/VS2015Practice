using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// add
using System.Threading;

namespace ThreadAndTask
{
    class Task2
    {
        string _Resource1 = "Init"; // 共用資源.
        public void Run()
        {
            Console.WriteLine("Task2: 送出動作函數, 無傳入參數, 系統會回呼動作函數.");
            Console.WriteLine($"Run(), ThreadId={Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"_Resource1={_Resource1}.");

            Console.WriteLine("Task.Start() 通知系統啟動工作. 主執行緒不會封鎖(Block), 會立即執行下一個指令.");
            Task task1 = new Task(myAction1, new State1 { _Value1 = "Init" }); // 建立工作並傳入參數.
            task1.Start();

            Console.WriteLine("子執行緒(5秒)未完成前按鍵, 才會看到'共用資源使用中'.");
            Console.WriteLine("Press any key to continuing...");
            Console.ReadKey();
            Console.WriteLine($"_Resource1={_Resource1}.");
        }
        private void myAction1(object object1)
        {
            Console.WriteLine($"Begin myAction1(), ThreadId={Thread.CurrentThread.ManagedThreadId}.");
            Console.WriteLine($"Task.CurrentId={Task.CurrentId}.");

            _Resource1 = "共用資源使用中";
            State1 c1 = object1 as State1;
            c1.Method1();

            _Resource1 = c1._Value1; // 共用資源存入處理結果.
            Console.WriteLine($"End myAction1().");
        }
    }
}
