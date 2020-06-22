using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRunLocal.Utils
{
    public class LoggerHelper
    {
        /// <summary>
        /// 静态只读实体对象info信息
        /// </summary>
        public static readonly log4net.ILog logInfo = log4net.LogManager.GetLogger("loginfo");

        /// <summary>
        /// 静态只读实体对象error信息
        /// </summary>
        public static readonly log4net.ILog logError = log4net.LogManager.GetLogger("logerror");

        /// <summary>
        ///  添加info信息
        /// </summary>
        /// <param name="info">自定义日志内容说明</param>
        public static void WriteLog(string info)
        {
            if (logInfo.IsInfoEnabled)
            {
                logInfo.Info(info);
            }
        }

        /// <summary>
        /// 添加异常信息
        /// </summary>
        /// <param name="info">自定义日志内容说明</param>
        /// <param name="ex">异常信息</param>
        public static void WriteLog(string info, Exception ex)
        {
            if (logError.IsErrorEnabled)
            {
                logError.Error(info, ex);
            }
        }
    }
}
