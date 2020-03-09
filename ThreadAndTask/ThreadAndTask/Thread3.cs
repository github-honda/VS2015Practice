using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// add
using System.Threading;
using System.Diagnostics;

namespace ThreadAndTask
{
    public class Thread3
    {
        string _Resource1 = "Init"; // 共用資源.
        public void Run()
        {
            Console.WriteLine("Thread3: 封鎖主執行緒, 等候子執行緒結束.");
            Console.WriteLine($"Run(), ThreadId={Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"_Resource1={_Resource1}.");

            Console.WriteLine("Thread.Start() 通知系統啟動子執行緒, 並傳入參數. 主執行緒不會封鎖(Block), 會立即執行下一個指令.");
            ParameterizedThreadStart threadDelegate = new ParameterizedThreadStart(myAction1); // 建立包含傳入參數的委派物件.
            State1 myPara1 = new State1 { _Value1 = "Init" }; // 參數初始化.
            Thread thread1 = new Thread(threadDelegate);
            thread1.Start(myPara1); 

            Console.WriteLine("Thread.Join() 封鎖主執行緒, 等候子執行緒結束.");
            thread1.Join();

            Console.WriteLine($"_Resource1={_Resource1}.");  // 共用資源結果.
            Console.WriteLine($"myPara1._Value1={myPara1._Value1}."); // 參數處理結果.
            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }
        private void myAction1(object object1)
        {
            Console.WriteLine($"Begin myAction1(), ThreadId={Thread.CurrentThread.ManagedThreadId}.");

            _Resource1 = "共用資源使用中";
            State1 c1 = object1 as State1;
            c1.Method1();

            _Resource1 = c1._Value1; 
            Console.WriteLine($"End myAction1().");
        }
    }
}
