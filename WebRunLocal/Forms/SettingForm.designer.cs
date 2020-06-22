namespace WebRunLocal.Forms
{
    partial class SettingForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ChkAutoUpdate = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TxtLogDays = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ChkLogger = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ChkAppLink = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ChkAutoStart = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtPort = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Window;
            this.groupBox1.Controls.Add(this.ChkAutoUpdate);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.TxtLogDays);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.ChkLogger);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.ChkAppLink);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.ChkAutoStart);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.TxtPort);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 20, 3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(370, 258);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // ChkAutoUpdate
            // 
            this.ChkAutoUpdate.AutoSize = true;
            this.ChkAutoUpdate.Location = new System.Drawing.Point(177, 117);
            this.ChkAutoUpdate.Name = "ChkAutoUpdate";
            this.ChkAutoUpdate.Size = new System.Drawing.Size(18, 17);
            this.ChkAutoUpdate.TabIndex = 10;
            this.ChkAutoUpdate.UseVisualStyleBackColor = true;
            this.ChkAutoUpdate.CheckedChanged += new System.EventHandler(this.ChkAutoUpdate_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 10.8F);
            this.label6.Location = new System.Drawing.Point(64, 111);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 25);
            this.label6.TabIndex = 9;
            this.label6.Text = "自动更新：";
            // 
            // TxtLogDays
            // 
            this.TxtLogDays.Font = new System.Drawing.Font("微软雅黑", 10.8F);
            this.TxtLogDays.Location = new System.Drawing.Point(177, 213);
            this.TxtLogDays.Name = "TxtLogDays";
            this.TxtLogDays.ShortcutsEnabled = false;
            this.TxtLogDays.Size = new System.Drawing.Size(100, 31);
            this.TxtLogDays.TabIndex = 8;
            this.TxtLogDays.TextChanged += new System.EventHandler(this.Txt_TextChanged);
            this.TxtLogDays.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtLogDays_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 10.8F);
            this.label5.Location = new System.Drawing.Point(26, 216);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(145, 25);
            this.label5.TabIndex = 7;
            this.label5.Text = "日志保留天数：";
            // 
            // ChkLogger
            // 
            this.ChkLogger.AutoSize = true;
            this.ChkLogger.Location = new System.Drawing.Point(177, 181);
            this.ChkLogger.Name = "ChkLogger";
            this.ChkLogger.Size = new System.Drawing.Size(18, 17);
            this.ChkLogger.TabIndex = 0;
            this.ChkLogger.UseVisualStyleBackColor = true;
            this.ChkLogger.CheckedChanged += new System.EventHandler(this.ChkLogger_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 10.8F);
            this.label4.Location = new System.Drawing.Point(26, 181);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(145, 25);
            this.label4.TabIndex = 6;
            this.label4.Text = "是否输出日志：";
            // 
            // ChkAppLink
            // 
            this.ChkAppLink.AutoSize = true;
            this.ChkAppLink.Location = new System.Drawing.Point(177, 151);
            this.ChkAppLink.Name = "ChkAppLink";
            this.ChkAppLink.Size = new System.Drawing.Size(18, 17);
            this.ChkAppLink.TabIndex = 5;
            this.ChkAppLink.UseVisualStyleBackColor = true;
            this.ChkAppLink.CheckedChanged += new System.EventHandler(this.ChkAppLink_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10.8F);
            this.label3.Location = new System.Drawing.Point(26, 145);
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
            this.ChkAutoStart.CheckedChanged += new System.EventHandler(this.ChkAutoStart_CheckedChanged);
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
            this.TxtPort.TextChanged += new System.EventHandler(this.Txt_TextChanged);
            this.TxtPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtPort_KeyPress);
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
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 270);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingForm";
            this.Text = "设置";
            this.Load += new System.EventHandler(this.SettingForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox ChkLogger;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox ChkAppLink;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox ChkAutoStart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TxtLogDays;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.CheckBox ChkAutoUpdate;
        private System.Windows.Forms.Label label6;
    }
}