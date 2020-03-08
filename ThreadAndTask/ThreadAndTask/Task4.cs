using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// add
using System.Threading;

namespace ThreadAndTask
{
    public class Task4
    {
        string _Resource1 = "Init"; // 共用資源.
        public void Run()
        {
            Console.WriteLine("Task4: Task.Run() 簡化呼叫方式, 並使用預設工作排班回呼工作");
            Console.WriteLine($"Run(), ThreadId={Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"_Resource1={_Resource1}.");

            // Task.Run() 是 Task.Factory.StartNew() 的簡化版本, 以預設參數帶入, 可簡化複雜的呼叫參數.
            // Task.Run() 預設為:
            // (cancellation token) = CancellationToken.None.
            // (CreationOptions) = TaskCreationOptions.DenyChildAttach.
            // Uses the default task scheduler.
            Console.WriteLine("Task.Run() 通知系統啟動工作, 並傳入參數. 主執行緒不會封鎖(Block), 會立即執行下一個指令.");
            Task<string>task1 = Task.Run<string>(() => myFunction1(new State1 { _Value1 = "Init" }));
            _Resource1 = task1.Result;

            Console.WriteLine("Task.Wait() 封鎖主執行緒, 等候工作結束.");
            task1.Wait();

            Console.WriteLine($"_Resource1={_Resource1}.");
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
