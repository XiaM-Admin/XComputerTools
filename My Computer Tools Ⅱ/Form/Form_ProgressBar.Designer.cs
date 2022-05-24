namespace My_Computer_Tools_Ⅱ
{
    partial class Form_ProgressBar
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
            this.Lab_Tip = new System.Windows.Forms.Label();
            this.Progress1 = new System.Windows.Forms.ProgressBar();
            this.Lab_ProgressTip = new System.Windows.Forms.Label();
            this.but_Close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Lab_Tip
            // 
            this.Lab_Tip.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Lab_Tip.AutoSize = true;
            this.Lab_Tip.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Lab_Tip.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.Lab_Tip.Location = new System.Drawing.Point(149, 10);
            this.Lab_Tip.Name = "Lab_Tip";
            this.Lab_Tip.Size = new System.Drawing.Size(59, 22);
            this.Lab_Tip.TabIndex = 0;
            this.Lab_Tip.Text = "label1";
            this.Lab_Tip.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Progress1
            // 
            this.Progress1.Location = new System.Drawing.Point(12, 39);
            this.Progress1.Name = "Progress1";
            this.Progress1.Size = new System.Drawing.Size(397, 23);
            this.Progress1.Step = 1;
            this.Progress1.TabIndex = 1;
            // 
            // Lab_ProgressTip
            // 
            this.Lab_ProgressTip.AutoSize = true;
            this.Lab_ProgressTip.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Lab_ProgressTip.ForeColor = System.Drawing.SystemColors.Desktop;
            this.Lab_ProgressTip.Location = new System.Drawing.Point(12, 68);
            this.Lab_ProgressTip.Name = "Lab_ProgressTip";
            this.Lab_ProgressTip.Size = new System.Drawing.Size(43, 17);
            this.Lab_ProgressTip.TabIndex = 2;
            this.Lab_ProgressTip.Text = "label2";
            // 
            // but_Close
            // 
            this.but_Close.Location = new System.Drawing.Point(334, 68);
            this.but_Close.Name = "but_Close";
            this.but_Close.Size = new System.Drawing.Size(75, 23);
            this.but_Close.TabIndex = 3;
            this.but_Close.Text = "关闭";
            this.but_Close.UseVisualStyleBackColor = true;
            this.but_Close.Visible = false;
            this.but_Close.Click += new System.EventHandler(this.but_Close_Click);
            // 
            // Form_ProgressBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 108);
            this.ControlBox = false;
            this.Controls.Add(this.but_Close);
            this.Controls.Add(this.Lab_ProgressTip);
            this.Controls.Add(this.Progress1);
            this.Controls.Add(this.Lab_Tip);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_ProgressBar";
            this.Opacity = 0.88D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "text";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Lab_Tip;
        private System.Windows.Forms.Label Lab_ProgressTip;
        private System.Windows.Forms.ProgressBar Progress1;
        private System.Windows.Forms.Button but_Close;
    }
}