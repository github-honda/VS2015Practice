using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// add
using System.Threading;

namespace ThreadAndTask
{
    public class Thread1
    {
        string _Resource1 = "Init"; // 共用資源.
        public void Run()
        {
            Console.WriteLine("Thread1: 送出代理函數, 無傳入參數, 系統會回呼代理函數.");
            Console.WriteLine($"Run(), ThreadId={Thread.CurrentThread.ManagedThreadId}.");
            Console.WriteLine($"_Resource1={_Resource1}.");

            Console.WriteLine("Thread.Start() 通知系統啟動子執行緒. 主執行緒不會封鎖(Block), 會立即執行下一個指令.");
            ThreadStart threadDelegate = new ThreadStart(myAction1); // 建立無傳入參數的委派物件.
            Thread thread1 = new Thread(threadDelegate); // 建立執行緒.
            thread1.Start();

            Console.WriteLine("若提早按鍵, 就會看到處理過程'共用資源使用中'. 因子執行緒/工作需要5秒才能完成.");
            Console.WriteLine("Press any key to continuing...");
            Console.ReadKey(); 
            Console.WriteLine($"_Resource1={_Resource1}.");
        }
        private void myAction1()
        {
            Console.WriteLine($"Begin myAction1(), ThreadId={Thread.CurrentThread.ManagedThreadId}.");

            _Resource1 = "共用資源使用中";
            Thread.Sleep(5000);

            _Resource1 = "共用資源處理完成";
            Console.WriteLine($"End myAction1().");
        }
    }
}
