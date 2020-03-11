using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// add
using System.Threading;

namespace ThreadAndTask
{
    public class Task6
    {
        CancellationTokenSource _CTS1;
        string _Resource1; // 共用資源.
        public void Run()
        {
            Console.WriteLine($"Task6.Run(), ThreadId={Thread.CurrentThread.ManagedThreadId}");
            _Resource1 = "共用資源初始化";
            State1 myPara1 = new State1 { _Value1 = "參數初始化" };

            _CTS1 = new CancellationTokenSource(); // 需每次重建.
            CancellationToken myToken = _CTS1.Token; // (CancellationToken 取消權杖)比共用資源更安全、更有效率控制工作是否取消.
            Task<string> task1 = Task.Factory.StartNew<string>(myFunction1, myPara1, myToken); // 交付系統工作工廠排班執行 Function(參數, 取消權杖).
            Console.WriteLine("若提早按Escape鍵, 就會取消子執行緒/工作繼續執行, 並看到'共用資源使用中'. 因子執行緒/工作需要5秒才能完成.");
            Console.WriteLine("Press any key to continuing...");
            ConsoleKeyInfo key1 = Console.ReadKey();

            if (key1.Key == ConsoleKey.Escape)
                _CTS1.Cancel();
            Console.WriteLine($"_Resource1={_Resource1}.");  // 共用資源結果.
            Console.WriteLine($"myPara1._Value1={myPara1._Value1}."); // 參數處理結果.
            Console.WriteLine($"task1.Result={task1.Result}."); // Task 處理結果.
            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }
        private string myFunction1(object object1)
        {
            Console.WriteLine($"myFunction1(), ThreadId={Thread.CurrentThread.ManagedThreadId}.");
            Console.WriteLine($"Task.CurrentId={Task.CurrentId}.");

            State1 c1 = object1 as State1; // 接收輸入參數
            _Resource1 = "共用資源使用中";
            c1._Value1 = "參數使用中";

            for (int i = 0; i < 5; i++)
            {
                if (_CTS1.IsCancellationRequested)
                {
                    _Resource1 = "取消執行";
                    Console.WriteLine($"_Resource1={_Resource1}.");  // 共用資源結果.
                    return "取消執行.";
                }
                Thread.Sleep(1000);
            }

            c1._Value1 = "參數處理完成";
            _Resource1 = "共用資源處理完成";
            return "工作處理完成"; // Task 也可以回傳值.
        }

    }
}
