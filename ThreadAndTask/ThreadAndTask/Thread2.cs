﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// add
using System.Threading;

namespace ThreadAndTask
{
    public class Thread2
    {
        string _Resource1 = "Init"; // 共用資源.
        public void Run()
        {
            Console.WriteLine("Thread2: 送出代理函數, 傳入參數, 系統會回呼代理函數.");
            Console.WriteLine($"Run(), ThreadId={Thread.CurrentThread.ManagedThreadId}.");
            Console.WriteLine($"_Resource1={_Resource1}.");

            Console.WriteLine("Thread.Start() 通知系統啟動子執行緒. 主執行緒不會封鎖(Block), 會立即執行下一個指令.");
            ParameterizedThreadStart threadDelegate = new ParameterizedThreadStart(myAction1); // 建立包含傳入參數的委派物件.
            Thread thread1 = new Thread(threadDelegate); // 建立執行緒.
            thread1.Start(new State1 { _Value1 = "Init" }); // 通知系統啟動子執行緒. 主執行緒不會封鎖(Block), 會立即執行下一個指令.

            Console.WriteLine("子執行緒(5秒)未完成前按鍵, 才會看到'共用資源使用中'.");
            Console.WriteLine("Press any key to continuing...");
            Console.ReadKey();
            Console.WriteLine($"_Resource1={_Resource1}.");
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
