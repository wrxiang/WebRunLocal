using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace WebRunLocal.Forms
{
    public partial class AboutForm : BaseForm
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            VersionLab.Text = "版本号：" + Application.ProductVersion;
        }

        private void LinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            Process.Start("https://github.com/wrxiang/WebRunLocal");
        }
    }
}
