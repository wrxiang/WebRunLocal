namespace WebRunLocal.Forms
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ChkLogger = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ChkAppLink = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ChkAutoStart = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtPort = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ChkLogger);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.ChkAppLink);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.ChkAutoStart);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.TxtPort);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(306, 210);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "常规";
            // 
            // ChkLogger
            // 
            this.ChkLogger.AutoSize = true;
            this.ChkLogger.Location = new System.Drawing.Point(177, 154);
            this.ChkLogger.Name = "ChkLogger";
            this.ChkLogger.Size = new System.Drawing.Size(18, 17);
            this.ChkLogger.TabIndex = 0;
            this.ChkLogger.UseVisualStyleBackColor = true;
            this.ChkLogger.CheckedChanged += new System.EventHandler(this.OnChkLoggerCheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 10.8F);
            this.label4.Location = new System.Drawing.Point(26, 150);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(145, 25);
            this.label4.TabIndex = 6;
            this.label4.Text = "是否输出日志：";
            // 
            // ChkAppLink
            // 
            this.ChkAppLink.AutoSize = true;
            this.ChkAppLink.Location = new System.Drawing.Point(177, 117);
            this.ChkAppLink.Name = "ChkAppLink";
            this.ChkAppLink.Size = new System.Drawing.Size(18, 17);
            this.ChkAppLink.TabIndex = 5;
            this.ChkAppLink.UseVisualStyleBackColor = true;
            this.ChkAppLink.CheckedChanged += new System.EventHandler(this.OnChkAppLinkCheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10.8F);
            this.label3.Location = new System.Drawing.Point(26, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(145, 25);
            this.label3.TabIndex = 4;
            this.label3.Text = "桌面快捷方式：";
            // 
            // ChkAutoStart
            // 
            this.ChkAutoStart.AutoSize = true;
            this.ChkAutoStart.Location = new System.Drawing.Point(177, 80);
            this.ChkAutoStart.Name = "ChkAutoStart";
            this.ChkAutoStart.Size = new System.Drawing.Size(18, 17);
            this.ChkAutoStart.TabIndex = 3;
            this.ChkAutoStart.UseVisualStyleBackColor = true;
            this.ChkAutoStart.CheckedChanged += new System.EventHandler(this.OnBtnAutoStartCheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.8F);
            this.label2.Location = new System.Drawing.Point(45, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "开机自启动：";
            // 
            // TxtPort
            // 
            this.TxtPort.Font = new System.Drawing.Font("微软雅黑", 10.8F);
            this.TxtPort.Location = new System.Drawing.Point(177, 36);
            this.TxtPort.Name = "TxtPort";
            this.TxtPort.ShortcutsEnabled = false;
            this.TxtPort.Size = new System.Drawing.Size(100, 31);
            this.TxtPort.TabIndex = 1;
            this.TxtPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnTxtPortKeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.8F);
            this.label1.Location = new System.Drawing.Point(64, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "监听端口：";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 270);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnSettingFormClosing);
            this.Load += new System.EventHandler(this.OnSettingsFormLoad);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox ChkAutoStart;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox ChkAppLink;
        private System.Windows.Forms.CheckBox ChkLogger;
        private System.Windows.Forms.Label label4;
    }
}