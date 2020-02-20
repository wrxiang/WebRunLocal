namespace WebRunLocal
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.IcnTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.CmsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TsmiRestore = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.linkLabel = new System.Windows.Forms.LinkLabel();
            this.CmsMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // IcnTray
            // 
            this.IcnTray.ContextMenuStrip = this.CmsMenu;
            this.IcnTray.DoubleClick += new System.EventHandler(this.OnIcnTrayDBClick);
            // 
            // CmsMenu
            // 
            this.CmsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsmiRestore,
            this.TsmiSetting,
            this.TsmiAbout,
            this.TsmiLogout});
            this.CmsMenu.Name = "cmsMenu";
            this.CmsMenu.Size = new System.Drawing.Size(109, 100);
            // 
            // TsmiRestore
            // 
            this.TsmiRestore.Name = "TsmiRestore";
            this.TsmiRestore.Size = new System.Drawing.Size(108, 24);
            this.TsmiRestore.Text = "还原";
            this.TsmiRestore.Click += new System.EventHandler(this.OnTsmiRestoreClick);
            // 
            // TsmiSetting
            // 
            this.TsmiSetting.Name = "TsmiSetting";
            this.TsmiSetting.Size = new System.Drawing.Size(108, 24);
            this.TsmiSetting.Text = "设置";
            this.TsmiSetting.Click += new System.EventHandler(this.OnTsmiSettingClick);
            // 
            // TsmiAbout
            // 
            this.TsmiAbout.Name = "TsmiAbout";
            this.TsmiAbout.Size = new System.Drawing.Size(108, 24);
            this.TsmiAbout.Text = "关于";
            this.TsmiAbout.Click += new System.EventHandler(this.OnTsmiAboutClick);
            // 
            // TsmiLogout
            // 
            this.TsmiLogout.Name = "TsmiLogout";
            this.TsmiLogout.Size = new System.Drawing.Size(108, 24);
            this.TsmiLogout.Text = "退出";
            this.TsmiLogout.Click += new System.EventHandler(this.OnTsmiLogoutClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("黑体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(22, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "WebRunLocal";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(21, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(413, 91);
            this.label2.TabIndex = 2;
            this.label2.Text = "网页调用本地程序，低耦合，兼容性强，全版本浏览器兼容，彻底解决网页调用本地程序出现的各种问题";
            // 
            // linkLabel
            // 
            this.linkLabel.AutoSize = true;
            this.linkLabel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linkLabel.Location = new System.Drawing.Point(21, 175);
            this.linkLabel.Name = "linkLabel";
            this.linkLabel.Size = new System.Drawing.Size(92, 27);
            this.linkLabel.TabIndex = 3;
            this.linkLabel.TabStop = true;
            this.linkLabel.Text = "软件详情";
            this.linkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnLinkClicked);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 229);
            this.Controls.Add(this.linkLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "MainForm";
            this.Text = "WRL";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnMainFormClosing);
            this.Load += new System.EventHandler(this.OnMainFormLoad);
            this.CmsMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon IcnTray;
        private System.Windows.Forms.ContextMenuStrip CmsMenu;
        private System.Windows.Forms.ToolStripMenuItem TsmiAbout;
        private System.Windows.Forms.ToolStripMenuItem TsmiLogout;
        private System.Windows.Forms.ToolStripMenuItem TsmiSetting;
        private System.Windows.Forms.ToolStripMenuItem TsmiRestore;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel linkLabel;
    }
}

