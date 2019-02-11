using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WRL;
using WRL.dto;
using WRL.strategy;
using WRL.utils;

namespace WRLClient
{
    public partial class Form1 : Form
    {

        string serviceName = "WRL";
        string serviceFilePath = Application.StartupPath + @"\WRL.exe";

        public Form1()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 安装服务按钮触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void installService_Click(object sender, EventArgs e)
        {
            if (ServiceManager.isServiceExisted(serviceName))
            {
                ServiceManager.uninstallService(serviceFilePath);
            }
            ServiceManager.installService(serviceFilePath);
        }

        /// <summary>
        /// 启动服务按钮触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startService_Click(object sender, EventArgs e)
        {
            if (ServiceManager.isServiceExisted(serviceName))
            {
                ServiceManager.serviceStart(serviceName);
            }
        }

        /// <summary>
        /// 停止服务按钮触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stopService_Click(object sender, EventArgs e)
        {
            if (ServiceManager.isServiceExisted(serviceName))
            {
                ServiceManager.serviceStop(serviceName);
            }
        }

        /// <summary>
        /// 卸载服务按钮触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uninstallService_Click(object sender, EventArgs e)
        {
            if (ServiceManager.isServiceExisted(serviceName))
            {
                ServiceManager.serviceStop(serviceName);
                ServiceManager.uninstallService(serviceFilePath);
            }
        }

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            DirectoryInfo fi = new DirectoryInfo(Application.StartupPath + @"\Plugins");
            if (!fi.Exists)
            {
                fi.Create();
            }

        }
    }
}
