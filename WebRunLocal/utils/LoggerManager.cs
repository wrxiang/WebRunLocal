using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRunLocal.utils
{
    public class LoggerManager
    {
        /// <summary>
        /// 日志输出（按日期分割文件）
        /// </summary>
        /// <param name="logPath">日志文件路径</param>
        /// <param name="message">日志内容</param>
        public static void writeLog(string logPath, string message)
        {
            DirectoryInfo fi = new DirectoryInfo(logPath);

            if (!fi.Exists)
            {
                fi.Create();
            }

            String date = DateTime.Today.ToString("yyyyMMdd");
            message = DateTime.Now.ToString() + "  " + message + Environment.NewLine;
            File.AppendAllText(logPath + @"\" + date + "_log.txt", message);
        }

        /// <summary>
        /// 日志输出
        /// </summary>
        /// <param name="logPath">日志文件路径</param>
        /// <param name="fileName">日志文件名</param>
        /// <param name="message">日志内容</param>
        public static void writeLog(string logPath, string fileName, string message)
        {
            DirectoryInfo fi = new DirectoryInfo(logPath);
            if (!fi.Exists)
            {
                fi.Create();
            }

            String date = DateTime.Today.ToString("yyyyMMdd");
            message = DateTime.Now.ToString() + "  " + message + Environment.NewLine;
            File.AppendAllText(logPath + @"\" + fileName, message);
        }
    }
}
