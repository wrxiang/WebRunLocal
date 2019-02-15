namespace WRLClient
{
    partial class Form1
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
            this.uninstallService = new System.Windows.Forms.Button();
            this.stopService = new System.Windows.Forms.Button();
            this.startService = new System.Windows.Forms.Button();
            this.installService = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // uninstallService
            // 
            this.uninstallService.Location = new System.Drawing.Point(433, 31);
            this.uninstallService.Name = "uninstallService";
            this.uninstallService.Size = new System.Drawing.Size(105, 49);
            this.uninstallService.TabIndex = 19;
            this.uninstallService.Text = "卸载服务";
            this.uninstallService.UseVisualStyleBackColor = true;
            this.uninstallService.Click += new System.EventHandler(this.uninstallService_Click);
            // 
            // stopService
            // 
            this.stopService.Location = new System.Drawing.Point(301, 31);
            this.stopService.Name = "stopService";
            this.stopService.Size = new System.Drawing.Size(103, 49);
            this.stopService.TabIndex = 18;
            this.stopService.Text = "停止服务";
            this.stopService.UseVisualStyleBackColor = true;
            this.stopService.Click += new System.EventHandler(this.stopService_Click);
            // 
            // startService
            // 
            this.startService.Location = new System.Drawing.Point(165, 31);
            this.startService.Name = "startService";
            this.startService.Size = new System.Drawing.Size(100, 49);
            this.startService.TabIndex = 17;
            this.startService.Text = "启动服务";
            this.startService.UseVisualStyleBackColor = true;
            this.startService.Click += new System.EventHandler(this.startService_Click);
            // 
            // installService
            // 
            this.installService.Location = new System.Drawing.Point(24, 31);
            this.installService.Name = "installService";
            this.installService.Size = new System.Drawing.Size(104, 49);
            this.installService.TabIndex = 16;
            this.installService.Text = "安装服务";
            this.installService.UseVisualStyleBackColor = true;
            this.installService.Click += new System.EventHandler(this.installService_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 121);
            this.Controls.Add(this.uninstallService);
            this.Controls.Add(this.stopService);
            this.Controls.Add(this.startService);
            this.Controls.Add(this.installService);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button uninstallService;
        private System.Windows.Forms.Button stopService;
        private System.Windows.Forms.Button startService;
        private System.Windows.Forms.Button installService;
    }
}

