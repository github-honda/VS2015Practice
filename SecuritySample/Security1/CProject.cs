using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security1
{
    class CProject
    {
        public const string csProjectID = "Security1";

        string[] msaArgs;

        public CProject(string[] args)
        {
            msaArgs = args;
        }

        public static void Create(string[] args)
        {
            new CProject(args).Run();
        }

        public void Run()
        {
            // remove comment for test:
            //new SampleRSAMS().Run();
            new SampleRSA().Run();

            Console.WriteLine("Press any key to end.");
            Console.ReadKey();
        }
    }
}
