namespace WebRunLocal.Forms
{
    partial class AboutForm
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
            this.LbAboutSystem = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LbAboutSystem
            // 
            this.LbAboutSystem.AutoSize = true;
            this.LbAboutSystem.Font = new System.Drawing.Font("微软雅黑", 10.8F);
            this.LbAboutSystem.Location = new System.Drawing.Point(22, 31);
            this.LbAboutSystem.Name = "LbAboutSystem";
            this.LbAboutSystem.Size = new System.Drawing.Size(67, 25);
            this.LbAboutSystem.TabIndex = 0;
            this.LbAboutSystem.Text = "label1";
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 292);
            this.Controls.Add(this.LbAboutSystem);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "关于";
            this.Load += new System.EventHandler(this.OnAboutFormLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LbAboutSystem;

    }
}