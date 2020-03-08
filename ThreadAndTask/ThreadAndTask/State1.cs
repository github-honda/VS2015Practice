using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// add
using System.Threading;

namespace ThreadAndTask
{
    public class State1
    {
        public string _Value1 { get; set; }

        public void Method1()
        {
            Console.WriteLine($"Begin Method1(), Init _Value1={_Value1}.");

            _Value1 = "狀態物件使用中";
            Console.WriteLine($"Running _Value1={_Value1}.");

            Thread.Sleep(5000);

            _Value1 = "狀態物件處理完成";
            Console.WriteLine($"End Method1(), _Value1={_Value1}.");
        }
    }
}
