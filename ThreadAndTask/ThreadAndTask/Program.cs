using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadAndTask
{
    class Program
    {
        static void Main(string[] args)
        {
            // Comment out the functions you don't need to test:
            new Thread1().Run();
            new Thread2().Run();
            new Thread3().Run();
            new Thread4().Run();
            new Thread5().Run();
            new Thread6().Run();

            new Task1().Run();
            new Task2().Run();
            new Task3().Run();
            new Task4().Run();
            new Task5().Run();
            new Task6().Run();

            Console.WriteLine("Main() End.");
            Console.WriteLine("Press any key to continuing...");
            Console.ReadKey();
        }
    }
}
