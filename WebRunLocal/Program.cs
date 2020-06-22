using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            using (System.Threading.Mutex m = new System.Threading.Mutex(true, Application.ProductName, out bool createNew))
            {
                if (createNew)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

                    MainForm mainForm = new MainForm();
                    Application.Run(mainForm);
                }
                else
                {
                    return;
                }
            }
        }
    }
}
