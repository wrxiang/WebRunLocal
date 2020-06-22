using System;
using System.Configuration;
using System.Windows.Forms;
using WebRunLocal.Managers;
using WebRunLocal.Utils;

namespace WebRunLocal.Forms
{
    public partial class SettingForm : BaseForm
    {
        //初始化配置的信息
        private string originalPort;
        private string originalRetainLogDays;

        public SettingForm()
        {
            InitializeComponent();
        }

        private void SettingForm_Load(object sender, EventArgs e)
        {
            toolTip.SetToolTip(TxtPort, "回车保存");
            toolTip.SetToolTip(TxtLogDays, "回车保存");


            string lisenerPort = ConfigurationManager.AppSettings["ListenerPort"];
            bool desktopLnk = bool.Parse(ConfigurationManager.AppSettings["DesktopLnk"]);
            bool pramaterLoggerPrint = bool.Parse(ConfigurationManager.AppSettings["PramaterLoggerPrint"]);
            bool autoStart = bool.Parse(ConfigurationManager.AppSettings["AutoStart"]);
            bool autoUpdate = bool.Parse(ConfigurationManager.AppSettings["AutoUpdate"]);
            originalRetainLogDays = ConfigurationManager.AppSettings["RetainLogDays"].ToString();

            TxtPort.Text = lisenerPort;
            originalPort = lisenerPort;
            ChkAppLink.Checked = desktopLnk;
            ChkAutoStart.Checked = autoStart;
            ChkAutoUpdate.Checked = autoUpdate;
            ChkLogger.Checked = pramaterLoggerPrint;
            TxtLogDays.Text = originalRetainLogDays;

        }

        /// <summary>
        /// 控制监听端口文本框只能输入数字和删除键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && this.TxtPort.Text != this.originalPort) 
            {
                MessageBoxButtons mess = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("监听端口发生更改需要重启本程序，是否立即更改并重新启动？", "提示", mess);
                if (dr == DialogResult.OK)
                {
                    UpdateAppConfig("ListenerPort", this.TxtPort.Text);
                    this.Dispose();
                    System.Diagnostics.Process.Start(Application.ExecutablePath);
                    Application.Exit();
                }
            }

            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 1)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 设置是否开机自启动时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkAutoStart_CheckedChanged(object sender, EventArgs e)
        {
            bool autoStartChecked = this.ChkAutoStart.Checked;
            UpdateAppConfig("AutoStart", autoStartChecked.ToString());
            AutoStartByRegistry.SetMeStart(autoStartChecked);
        }

        /// <summary>
        /// 设置是否生成桌面快捷方式时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkAppLink_CheckedChanged(object sender, EventArgs e)
        {
            bool appLinkChecked = this.ChkAppLink.Checked;
            UpdateAppConfig("DesktopLnk", appLinkChecked.ToString());
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
        private void ChkLogger_CheckedChanged(object sender, EventArgs e)
        {
            bool chkLoggerChecked = this.ChkLogger.Checked;
            UpdateAppConfig("PramaterLoggerPrint", chkLoggerChecked.ToString());
            if (!chkLoggerChecked)
            {
                TxtLogDays.Text = string.Empty;
                TxtLogDays.ReadOnly = true;
                TxtLogDays.Enabled = false;
            }
            else 
            {
                TxtLogDays.ReadOnly = false;
                TxtLogDays.Enabled = true;
                TxtLogDays.Text = originalRetainLogDays;
            }
        }

        /// <summary>
        /// 控制日志保留天数文本框只能输入数字和删除键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtLogDays_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && TxtLogDays.Text != this.originalRetainLogDays)
            {
                UpdateAppConfig("RetainLogDays", TxtLogDays.Text);
                toolTip.SetToolTip((Control)sender, "保存成功");
            }

            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 1)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 更改配置文件内容
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        private void UpdateAppConfig(string key, string value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings[key].Value = value;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        /// <summary>
        /// TextBox的TextChanged事件提示回车保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Txt_TextChanged(object sender, EventArgs e)
        {
            toolTip.SetToolTip((Control)sender, "回车保存");
        }

        /// <summary>
        /// 是否自动更新设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ChkAutoUpdate_CheckedChanged(object sender, EventArgs e)
        {
            bool checkedStatus = this.ChkAutoUpdate.Checked;
            if (checkedStatus)
            {
                ProcessUtil.StartExe(WrlServiceManager.autoUpdateExePath);
            }
            else 
            {
                string[] pathSplitArray = WrlServiceManager.autoUpdateExePath.Split('\\');
                string exeName = pathSplitArray[pathSplitArray.Length - 1].Split('.')[0];
                ProcessUtil.StopExe(exeName);
            }
        }

    }
}
