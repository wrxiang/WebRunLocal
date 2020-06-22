using System;
using System.Windows.Forms;

namespace AutoUpdateClient
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
                    Application.Run(new Form1());
                }
                else
                {
                    return;
                }
            }
        }
    }
}
