using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WebRunLocal
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool createNew;

            using (System.Threading.Mutex m = new System.Threading.Mutex(true, Application.ProductName, out createNew))
            {
                if (createNew)
                {
                    var log4netConfigPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "log4net.config");
                    var fi = new System.IO.FileInfo(log4netConfigPath);

                    log4net.Config.XmlConfigurator.Configure();
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new MainForm());
                }
                else
                {
                    return;
                }
            }
        }
    }
}
