using System.Diagnostics;
using System.IO;

namespace WebRunLocal.Utils
{
    class ProcessUtil
    {

        /// <summary>
        /// 启动程序
        /// </summary>
        /// <param name="exePath"></param>
        public static void StartExe(string exePath)
        {
            if (File.Exists(exePath))
            {
                Process pro = new Process();
                pro.StartInfo.FileName = exePath;
                pro.Start();
            }
        }

        /// <summary>
        /// 停止程序
        /// </summary>
        /// <param name="exeName"></param>
        public static void StopExe(string exeName)
        {
            Process[] exeArray = Process.GetProcessesByName(exeName.Split('.')[0]);
            foreach (Process process in exeArray)
            {
                process.Kill();
            }
        }

    }
}
