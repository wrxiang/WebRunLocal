using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using WebRunLocal.Forms;
using WebRunLocal.Managers;

namespace WebRunLocal
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            WindowState = FormWindowState.Minimized;

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //设置托盘程序属性
            string[] pathSplitArray = Process.GetCurrentProcess().MainModule.FileName.Split('\\');
            string appName = pathSplitArray[pathSplitArray.Length - 1].Split('.')[0];
            IcnTray.Visible = true;
            IcnTray.Icon = Properties.Resources.logo;
            IcnTray.Text = appName;

            //设置主面板属性
            Icon = Properties.Resources.logo;
            StartPosition = FormStartPosition.Manual;
            TopMost = true;
            Hide();

            //设置折叠面板按钮绑定的窗体
            buttonSetting.Tag = new SettingForm();
            buttonAbout.Tag = new AboutForm();
            FoldButtonClick(buttonSetting, e);

            //启动WRL服务并进行系统初始化
            WrlServiceManager.StartWrlServiceAsync();
        }

        /// <summary>
        /// 折叠面板按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FoldButtonClick(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            int clickedButtonTabIndex = clickedButton.TabIndex;

            foreach (Control ctl in FoldPanelGroup.Controls)
            {
                if (ctl is Button)
                {
                    Button btn = (Button)ctl;
                    if (btn.TabIndex > clickedButtonTabIndex)
                    {
                        if (btn.Dock != DockStyle.Bottom)
                        {
                            btn.Dock = DockStyle.Bottom;
                            btn.BringToFront();
                        }
                    }
                    else
                    {
                        if (btn.Dock != DockStyle.Top)
                        {
                            btn.Dock = DockStyle.Top;
                            btn.BringToFront();
                        }
                    }
                }
            }

            FoldPanel.BringToFront();

            Form form = (Form)clickedButton.Tag;
            if (form != null)
            {
                form.TopLevel = false;
                FoldPanel.Controls.Add(form);
                form.Dock = DockStyle.Fill;
                form.FormBorderStyle = FormBorderStyle.None;
                form.BringToFront();
                form.Show();
            }
            else 
            {
                FoldPanel.Controls.Clear();
            }
            
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmiLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要退出吗？退出后本地程序可能无法使用!", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                IcnTray.Visible = false;
                Close();
                Dispose();
                Environment.Exit(System.Environment.ExitCode);
            }
        }
       
        /// <summary>
        /// 还原
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmiRestore_Click(object sender, EventArgs e)
        {
            Visible = true;
            WindowState = FormWindowState.Normal;
            Activate();
        }

        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmiSetting_Click(object sender, EventArgs e)
        {
            SettingForm settingsForm = new SettingForm();
            settingsForm.Show();
        }


        /// <summary>
        /// MainForm关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 注意判断关闭事件reason来源于窗体按钮，否则用菜单退出时无法退出!
            if (e.CloseReason == CloseReason.UserClosing)
            {
                //取消"关闭窗口"事件
                e.Cancel = true; // 取消关闭窗体 

                //使关闭时窗口向右下角缩小的效果
                WindowState = FormWindowState.Minimized;
                IcnTray.Visible = true;
                Hide();
                return;
            }
        }
        
        
        /// <summary>
        /// 托盘程序双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IcnTray_DoubleClick(object sender, EventArgs e)
        {
            if (Visible)
            {
                WindowState = FormWindowState.Minimized;
                IcnTray.Visible = true;
                Hide();
            }
            else
            {
                Visible = true;
                WindowState = FormWindowState.Normal;
                Activate();
            }
        }
    }
}
