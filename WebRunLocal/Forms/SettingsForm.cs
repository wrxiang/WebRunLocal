using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WebRunLocal.Managers;
using WebRunLocal.Utils;

namespace WebRunLocal.Forms
{
    public partial class SettingsForm : Form
    {
        //初始化配置的端口信息
        private string originalPort;

        public SettingsForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSettingsFormLoad(object sender, EventArgs e)
        {
            this.Icon = WebRunLocal.Properties.Resources.logo;

            string lisenerPort = ConfigurationManager.AppSettings["ListenerPort"].ToString();
            bool desktopLnk = bool.Parse(ConfigurationManager.AppSettings["DesktopLnk"]);
            bool pramaterLoggerPrint = bool.Parse(ConfigurationManager.AppSettings["PramaterLoggerPrint"]);
            bool autoStart = bool.Parse(ConfigurationManager.AppSettings["AutoStart"]);

            this.TxtPort.Text = lisenerPort;
            this.originalPort = lisenerPort;
            this.ChkAppLink.Checked = desktopLnk;
            this.ChkAutoStart.Checked = autoStart;
            this.ChkLogger.Checked = pramaterLoggerPrint;
        }

        /// <summary>
        /// 控制监听端口文本框只能输入数字和删除键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTxtPortKeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 1)
            {
                e.Handled = true;
            }
        }


        /// <summary>
        /// 设置是否开机自启动时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnBtnAutoStartCheckedChanged(object sender, EventArgs e)
        {
            bool autoStartChecked = this.ChkAutoStart.Checked;
            updateAppConfig("AutoStart", autoStartChecked.ToString());
            AutoStartByRegistry.SetMeStart(autoStartChecked);
            
        }

        /// <summary>
        /// 设置是否生成桌面快捷方式时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnChkAppLinkCheckedChanged(object sender, EventArgs e)
        {
            bool appLinkChecked = this.ChkAppLink.Checked;
            updateAppConfig("DesktopLnk", appLinkChecked.ToString());
            if (appLinkChecked)
            {
                AppLnkUtil.CreateDesktopQuick();
            }
        }

        /// <summary>
        /// 设置是否记录日志时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnChkLoggerCheckedChanged(object sender, EventArgs e)
        {
            bool chkLoggerChecked = this.ChkLogger.Checked;
            updateAppConfig("PramaterLoggerPrint", chkLoggerChecked.ToString());
        }

        /// <summary>
        /// Form关闭时事件，用以检测端口是否发生更改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSettingFormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.TxtPort.Text != this.originalPort)
            {
                MessageBoxButtons mess = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("监听端口发生更改需要重启本程序，是否立即更改并重新启动？", "提示", mess);
                if (dr == DialogResult.OK)
                {
                    updateAppConfig("ListenerPort", this.TxtPort.Text);
                    this.Dispose();
                    System.Diagnostics.Process.Start(Application.ExecutablePath);
                    Application.Exit();
                }
                else 
                {
                    this.TxtPort.Text = this.originalPort;
                    e.Cancel = true;
                }
            }
        }


        /// <summary>
        /// 更改配置文件内容
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        private void updateAppConfig(string key, string value) 
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings[key].Value = value;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        

        

    }
}
