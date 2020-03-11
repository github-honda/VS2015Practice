using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// add
using System.Threading;

namespace ThreadAndTask
{
    public class Thread6
    {
        string _Resource1; // 共用資源.
        Boolean _Cancel; // 共用資源, 是否取消執行?
        public void Run()
        {
            Console.WriteLine($"Thread6.Run(), ThreadId={Thread.CurrentThread.ManagedThreadId}");
            _Resource1 = "共用資源初始化";
            State1 myPara1 = new State1 { _Value1 = "參數初始化" };
            _Cancel = false;

            ThreadPool.QueueUserWorkItem(new WaitCallback(myAction1), myPara1); // 交付系統佇列排班執行 Action(參數).
            Console.WriteLine("若提早按Escape鍵, 就會取消子執行緒/工作繼續執行, 並看到'共用資源使用中'. 因子執行緒/工作需要5秒才能完成.");
            Console.WriteLine("Press any key to continuing...");
            ConsoleKeyInfo key1 = Console.ReadKey();

            if (key1.Key == ConsoleKey.Escape)
                _Cancel = true;
            Console.WriteLine($"_Resource1={_Resource1}.");  // 共用資源結果.
            Console.WriteLine($"myPara1._Value1={myPara1._Value1}."); // 參數處理結果.
            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }
        private void myAction1(object object1)
        {
            Console.WriteLine($"myAction1(), ThreadId={Thread.CurrentThread.ManagedThreadId}.");

            State1 c1 = object1 as State1; // 接收輸入參數
            _Resource1 = "共用資源使用中";
            c1._Value1 = "參數使用中";

            for (int i = 0; i < 5; i++)
            {
                if (_Cancel)
                {
                    _Resource1 = "取消執行";
                    Console.WriteLine($"_Resource1={_Resource1}.");  // 共用資源結果.
                    return;
                }
                Thread.Sleep(1000);
            }

            c1._Value1 = "參數處理完成";
            _Resource1 = "共用資源處理完成";
        }
    }
}
