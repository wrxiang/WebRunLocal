using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WebRunLocal.Entity;
using WebRunLocal.Managers;
using WebRunLocal.Utils;

namespace WebRunLocal.Forms
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnAboutFormLoad(object sender, EventArgs e)
        {
            this.Icon = WebRunLocal.Properties.Resources.logo;

            string systemAboutText = "本机IP: ";
            List<string> ipList = IPUtil.GetIpByLocalAll();
            string lisenerPort = ConfigurationManager.AppSettings["ListenerPort"].ToString();
            string httpAddress = PublicValue.httpListenerAddress;

            foreach (string ip in ipList)
            {
                systemAboutText += ip + Environment.NewLine;
            }

            systemAboutText += Environment.NewLine + "端口: " + lisenerPort + Environment.NewLine;
            systemAboutText += Environment.NewLine + "地址: " + httpAddress + Environment.NewLine;
            systemAboutText += Environment.NewLine + "版本: 0.0.1" + Environment.NewLine;

            LbAboutSystem.Text = systemAboutText;
        }
    }
}
