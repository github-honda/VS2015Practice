using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// add
using System.Threading;

namespace ThreadAndTask
{
    public class Task5
    {
        string _Resource1 = "Init"; // 共用資源.
        public void Run()
        {
            Console.WriteLine("Task5: Task.Factory 減輕系統負擔.");
            Console.WriteLine($"Run(), ThreadId={Thread.CurrentThread.ManagedThreadId}");

            Console.WriteLine("Task.Factory.StartNew() 通知系統啟動工作, 並傳入參數. 主執行緒不會封鎖(Block), 會立即執行下一個指令.");
            Task<string> task1 = Task.Factory.StartNew<string>(myFunction1, new State1 { _Value1 = "Init" });

            Console.WriteLine("Task.Wait() 封鎖主執行緒, 等候工作結束.");
            task1.Wait();

            Console.WriteLine($"Result = {task1.Result}"); // 接收工作結果.
            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }
        private string myFunction1(object object1)
        {
            Console.WriteLine($"Begin myFunction1(), ThreadId={Thread.CurrentThread.ManagedThreadId}.");
            Console.WriteLine($"Task.CurrentId={Task.CurrentId}.");

            _Resource1 = "共用資源使用中";
            State1 c1 = object1 as State1;
            c1.Method1();

            _Resource1 = c1._Value1; 
            Console.WriteLine($"End myFunction1().");

            return c1._Value1; // 回傳 Task 處理結果.
        }
    }
}
