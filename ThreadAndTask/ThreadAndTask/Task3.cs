﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// add
using System.Threading;

namespace ThreadAndTask
{
    public class Task3
    {
        string _Resource1; // 共用資源.
        public void Run()
        {
            Console.WriteLine($"Task3.Run(), ThreadId={Thread.CurrentThread.ManagedThreadId}");
            _Resource1 = "共用資源初始化";
            State1 myPara1 = new State1 { _Value1 = "參數初始化" };

            Task<string> task1 = new Task<string>(myFunction1, myPara1); // 建立工作並傳入參數.
            task1.Start(); // Task(Function, 參數).Start() 啟動執行(有參數傳入且有回傳值)的 Function.
            task1.Wait(); // 封鎖主執行緒, 等候工作結束.
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

            Thread.Sleep(5000);

            c1._Value1 = "參數處理完成";
            _Resource1 = "共用資源處理完成";
            return "工作處理完成"; // Task 也可以回傳值.
        }
    }
}
