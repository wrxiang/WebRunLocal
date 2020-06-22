using NetFwTypeLib;
using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using WebRunLocal.Services;
using WebRunLocal.Utils;

namespace WebRunLocal.Managers
{
    public class WrlServiceManager
    {

        private static HttpService _http;
        public static string autoUpdateExePath = Path.Combine(Application.StartupPath, "WRL自动更新.exe");

        /// <summary>
        /// 启动WRL服务并进行初始化操作
        /// </summary>
        public static async void StartWrlServiceAsync()
        {
            string lisenerPort = ConfigurationManager.AppSettings["ListenerPort"].ToString();
            var port = Convert.ToInt32(lisenerPort);
            _http = new HttpService(port);
            await _http.StartHttpServer();

            //创建系统所需要的目录
            DirectoryInfo fi = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "Plugins"));
            if (!fi.Exists)
            {
                fi.Create();
            }
            fi = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "Log"));
            if (!fi.Exists)
            {
                fi.Create();
            }

            //设置软件自启动
            AutoStartByRegistry.SetMeStart(bool.Parse(ConfigurationManager.AppSettings["AutoStart"]));
            //创建桌面快捷方式
            if (bool.Parse(ConfigurationManager.AppSettings["DesktopLnk"].ToString()))
            {
                AppLnkUtil.CreateDesktopQuick();
            }

            //将监听端口的端口添加到防火墙例外
            FireWallUtil.NetFwAddPorts("WRL-PORT", int.Parse(lisenerPort), NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP);

            //清理日志文件
            int retainLogDays = Convert.ToInt32(ConfigurationManager.AppSettings["RetainLogDays"]);
            FileUtil.DeleteFiles(Path.Combine(Directory.GetCurrentDirectory(), @"Log"), retainLogDays);

            //是否启动自动更新
            if (bool.Parse(ConfigurationManager.AppSettings["AutoUpdate"]) && File.Exists(autoUpdateExePath))
            {
                ProcessUtil.StartExe(autoUpdateExePath);
            }

        }
    }
}
