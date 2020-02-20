using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WebRunLocal.Forms;
using WebRunLocal.Managers;
using WebRunLocal.Utils;

namespace WebRunLocal
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            this.WindowState = FormWindowState.Minimized;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            InitializeComponent();
        }

        private Font ft = new Font("黑体", 12);
        private static Panel iPanel = new Panel();
        private static PictureBox iPict = new PictureBox();


        /// <summary>
        /// 设置并启动监听服务
        /// </summary>
        private void SettingAndStartService() 
        {
            //设置软件自动启动
            AutoStartByRegistry.SetMeStart(bool.Parse(ConfigurationManager.AppSettings["AutoStart"]));

            //创建桌面快捷方式
            if (bool.Parse(ConfigurationManager.AppSettings["DesktopLnk"].ToString()))
            {
                AppLnkUtil.CreateDesktopQuick();
            }

            //将监听端口的端口添加到防火墙例外
            string lisenerPort = ConfigurationManager.AppSettings["ListenerPort"].ToString();
            FireWallUtil.NetFwAddPorts("WRL-PORT", int.Parse(lisenerPort), "TCP");

            //启动Http监听服务
            HttpListenerManager httpListenerManager = new HttpListenerManager();
            httpListenerManager.startHttpListener(lisenerPort);
        }


        /// <summary>
        /// MainForm的Load事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMainFormLoad(object sender, EventArgs e)
        {
            string[] pathSplitArray = Process.GetCurrentProcess().MainModule.FileName.Split('\\');
            string appName = pathSplitArray[pathSplitArray.Length - 1].Split('.')[0];

            this.IcnTray.Visible = true;
            this.IcnTray.Icon = WebRunLocal.Properties.Resources.logo;
            this.IcnTray.Text = appName;
            this.Icon = WebRunLocal.Properties.Resources.logo;
            this.Hide();

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

            SettingAndStartService();
        }

        /// <summary>
        /// 窗体的关闭方法，实现关闭后最小化到托盘
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMainFormClosing(object sender, FormClosingEventArgs e)
        {
            // 注意判断关闭事件reason来源于窗体按钮，否则用菜单退出时无法退出!
            if (e.CloseReason == CloseReason.UserClosing)
            {
                //取消"关闭窗口"事件
                e.Cancel = true; // 取消关闭窗体 

                //使关闭时窗口向右下角缩小的效果
                this.WindowState = FormWindowState.Minimized;
                this.IcnTray.Visible = true;
                this.Hide();
                return;
            }
        }

        /// <summary>
        /// 托盘双击操作，
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnIcnTrayDBClick(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                this.WindowState = FormWindowState.Minimized;
                this.IcnTray.Visible = true;
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
        /// 还原
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTsmiRestoreClick(object sender, EventArgs e)
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
        private void OnTsmiLogoutClick(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要退出吗？退出后本地程序可能无法使用!", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                this.IcnTray.Visible = false;
                this.Close();
                this.Dispose();
                System.Environment.Exit(System.Environment.ExitCode);
            }
        }

        /// <summary>
        /// 托盘程序关于按钮触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTsmiAboutClick(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.Show();
        }

        /// <summary>
        /// 托盘程序设置按钮触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTsmiSettingClick(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.Show();
        }

        /// <summary>
        /// LinkLable点击事件触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.linkLabel.LinkVisited = true;
            Process.Start("IExplore", "https://github.com/wrxiang/WebRunLocal");
        }

    }
}
