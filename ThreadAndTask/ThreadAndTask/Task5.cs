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
            State1 myPara1 = new State1 { _Value1 = "Init" }; // 參數初始化.
            Task<string> task1 = Task.Factory.StartNew<string>(myFunction1, myPara1);

            Console.WriteLine("若提早按鍵, 就會看到處理過程'共用資源使用中'. 因子執行緒/工作需要5秒才能完成.");
            Console.WriteLine("Press any key to continuing...");
            Console.ReadKey();
            Console.WriteLine($"_Resource1={_Resource1}.");  // 共用資源結果.
            Console.WriteLine($"myPara1._Value1={myPara1._Value1}."); // 參數處理結果.

            // Task 也可以回傳處理結果
            Console.WriteLine($"task1.Result={task1.Result}."); // Task 處理結果.
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
