namespace My_Computer_Tools_Ⅱ
{
    partial class ShowBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowBox));
            this.lab_Tip = new System.Windows.Forms.Label();
            this.lab_Txt = new System.Windows.Forms.Label();
            this.lab_About = new System.Windows.Forms.Label();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // lab_Tip
            // 
            this.lab_Tip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lab_Tip.AutoSize = true;
            this.lab_Tip.Font = new System.Drawing.Font("微软雅黑", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_Tip.Location = new System.Drawing.Point(0, 19);
            this.lab_Tip.Name = "lab_Tip";
            this.lab_Tip.Size = new System.Drawing.Size(50, 26);
            this.lab_Tip.TabIndex = 0;
            this.lab_Tip.Text = "标题";
            this.lab_Tip.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lab_Txt
            // 
            this.lab_Txt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lab_Txt.AutoSize = true;
            this.lab_Txt.Location = new System.Drawing.Point(116, 9);
            this.lab_Txt.Name = "lab_Txt";
            this.lab_Txt.Size = new System.Drawing.Size(37, 20);
            this.lab_Txt.TabIndex = 1;
            this.lab_Txt.Text = "文本";
            this.lab_Txt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lab_About
            // 
            this.lab_About.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lab_About.AutoSize = true;
            this.lab_About.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lab_About.Location = new System.Drawing.Point(301, 38);
            this.lab_About.Name = "lab_About";
            this.lab_About.Size = new System.Drawing.Size(116, 20);
            this.lab_About.TabIndex = 2;
            this.lab_About.Text = "Computer Tools";
            this.lab_About.Click += new System.EventHandler(this.Lab_About_Click);
            // 
            // ShowBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleTurquoise;
            this.ClientSize = new System.Drawing.Size(431, 61);
            this.ControlBox = false;
            this.Controls.Add(this.lab_About);
            this.Controls.Add(this.lab_Txt);
            this.Controls.Add(this.lab_Tip);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimizeBox = false;
            this.Name = "ShowBox";
            this.Opacity = 0.7D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ShowBox";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ShowBox_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lab_Tip;
        private System.Windows.Forms.Label lab_Txt;
        private System.Windows.Forms.Label lab_About;
        private System.Windows.Forms.ToolTip ToolTip;
    }
}