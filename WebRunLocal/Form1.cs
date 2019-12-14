using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WebRunLocal.dto;
using WebRunLocal.utils;
using WebRunLocal.strategy;
using System.Management;

namespace WebRunLocal
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            this.WindowState = FormWindowState.Minimized;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            this.Hide();
            this.mainNotifyIcon.Visible = true;

            DirectoryInfo fi = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "Plugins"));
            if (!fi.Exists)
            {
                fi.Create();
            }

            this.mainNotifyIcon.Text = ConfigurationManager.AppSettings["QuickLnkName"].ToString();


            //设置软件自动启动
            AutoStartByRegistry.setMeStart(bool.Parse(ConfigurationManager.AppSettings["AutoStart"]));

            //创建桌面快捷方式
            if (bool.Parse(ConfigurationManager.AppSettings["DesktopLnk"].ToString()))
            {
                LnkUtil.createDesktopQuick();
            }

            //监听端口
            string lisenerPort = ConfigurationManager.AppSettings["ListenerPort"].ToString();
            //获取本地电脑IP列表
            List<string> ipList = IPUtils.GetIpByLocal();

            //将应用程序添加到防火墙例外
            //FireWallHelp.NetFwAddApps("WRL-APP", System.Windows.Forms.Application.ExecutablePath);
            //添加防火墙例外端口
            FireWallHelp.NetFwAddPorts("WRL-PORT", int.Parse(lisenerPort), "TCP");

            //开启http监听并处理业务
            HttpListenerManager httpListenerManager = new HttpListenerManager();
            httpListenerManager.startHttpListener(ipList, lisenerPort);



            setSystemAbout(ipList, lisenerPort, httpListenerManager.HttpListenerAddress);

        }

        /// <summary>
        /// 设置系统程序展示信息
        /// </summary>
        private void setSystemAbout(List<string> ipList, string lisenerPort, string httpAddress) 
        {
            string SystemAboutText = "本机IP:";
            foreach (string ip in ipList)
            {
                SystemAboutText += ip + Environment.NewLine;
            }

            SystemAboutText += Environment.NewLine + "端口: " + lisenerPort + Environment.NewLine;

            SystemAboutText += Environment.NewLine + "地址: " + httpAddress + Environment.NewLine;

            SystemAboutText += Environment.NewLine + "版本: 0.1.1" +  Environment.NewLine;

            SystemAboutLable.Text = SystemAboutText;
        }



        /// <summary>
        /// 窗体的关闭方法，实现关闭后最小化到托盘
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 注意判断关闭事件reason来源于窗体按钮，否则用菜单退出时无法退出!
            if (e.CloseReason == CloseReason.UserClosing)
            {
                //取消"关闭窗口"事件
                e.Cancel = true; // 取消关闭窗体 

                //使关闭时窗口向右下角缩小的效果
                this.WindowState = FormWindowState.Minimized;
                this.mainNotifyIcon.Visible = true;
                this.Hide();
                return;
            }
        }



        /// <summary>
        /// 托盘双击操作，
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mainNotifyIcon_MouseDoubleClick(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                this.WindowState = FormWindowState.Minimized;
                this.mainNotifyIcon.Visible = true;
                this.Hide();
            }
            else
            {
                this.Visible = true;
                this.WindowState = FormWindowState.Normal;
                this.Activate();
            }
        }

        /// <summary>
        /// 最小化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.mainNotifyIcon.Visible = true;
            this.Hide();
        }

        /// <summary>
        /// 还原
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemNormal_Click(object sender, EventArgs e)
        {
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemQuit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要退出吗？退出后所有插件均不能使用!", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                this.mainNotifyIcon.Visible = false;
                this.Close();
                this.Dispose();
                System.Environment.Exit(System.Environment.ExitCode);
            }
        }

        

    }
}
