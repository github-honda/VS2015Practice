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
            Boolean bAll = true;

            // remove comment for test:
            if (!new SampleRSA().Run()) bAll = false;
            if (!new SampleMD5().Run()) bAll = false;
            if (!new SampleSHA1().Run()) bAll = false;
            if (!new SampleAES().Run()) bAll = false;
            if (!new RSAPerformance().Run()) bAll = false;
            if (!new AESPerformance().Run()) bAll = false;

            if (bAll)
                Console.WriteLine("All OK !");
            else
                Console.WriteLine("Fail !");

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }
    }
}
