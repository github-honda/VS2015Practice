using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
// add
using System.Configuration.Install;
using System.Reflection;
using System.Collections;

namespace WinServiceTimer
{
    static class Program
    {
        static void Main(params string[] saArg)
        {
            if (saArg.Length < 1)
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    new Service1()
                };
                ServiceBase.Run(ServicesToRun);
            }
            else
            {
                SelfInstall(saArg);
            }
        }
        static void SelfInstall(params string[] args)
        {
            // 自我安裝程式碼.
            // 服務程式可透過工具程式(InstallUtil.exe)安裝, 或是以這段程式碼執行自我安裝.
            // 透過工具程式(InstallUtil.exe)安裝, 安裝紀錄會顯示在Console中, 及 InstallUtil.InstallLog 檔案中.
            // 若是透過這段程式碼執行自我安裝, 則安裝過程紀錄會輸出至檔案(WinService1.InstallLog). "WinService1" 為本專案 Assembly 名稱.

            // 安裝工具程式 InstallUtil.exe 可在以下位置找到:
            // C:\Windows\Microsoft.NET\Framework64\v2.0.50727\
            // C:\Windows\Microsoft.NET\Framework64\v4.0.30319\ 中.

            // 說明可參考: 
            // https://docs.microsoft.com/zh-tw/dotnet/framework/tools/installutil-exe-installer-tool
            using (AssemblyInstaller installer1 = new AssemblyInstaller())
            {
                IDictionary dict1 = new Hashtable();
                installer1.UseNewContext = true;
                installer1.Path = Assembly.GetCallingAssembly().Location;
                switch (args[0].ToLower())
                {
                    case "/i":
                    case "-i":
                        installer1.Install(dict1);
                        installer1.Commit(dict1);
                        break;
                    case "/u":
                    case "-u":
                        installer1.Uninstall(dict1);
                        break;
                    default:
                        return;
                }
            }
        }
    }
}
