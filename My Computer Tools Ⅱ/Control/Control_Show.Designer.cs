﻿namespace My_Computer_Tools_Ⅱ
{
    partial class Control_Show
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lab_Name = new System.Windows.Forms.Label();
            this.Lab_User = new System.Windows.Forms.LinkLabel();
            this.Lab_Userpwd = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.but_ShowHide = new System.Windows.Forms.Button();
            this.but_ChangPwd = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lab_Name
            // 
            this.lab_Name.AutoSize = true;
            this.lab_Name.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_Name.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lab_Name.Location = new System.Drawing.Point(238, 9);
            this.lab_Name.Name = "lab_Name";
            this.lab_Name.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lab_Name.Size = new System.Drawing.Size(71, 26);
            this.lab_Name.TabIndex = 0;
            this.lab_Name.Text = "label1";
            // 
            // Lab_User
            // 
            this.Lab_User.ActiveLinkColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Lab_User.AutoSize = true;
            this.Lab_User.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Lab_User.LinkColor = System.Drawing.Color.Black;
            this.Lab_User.Location = new System.Drawing.Point(3, 42);
            this.Lab_User.Name = "Lab_User";
            this.Lab_User.Size = new System.Drawing.Size(93, 22);
            this.Lab_User.TabIndex = 1;
            this.Lab_User.TabStop = true;
            this.Lab_User.Text = "linkLabel1";
            this.Lab_User.VisitedLinkColor = System.Drawing.Color.Black;
            this.Lab_User.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Lab_User_LinkClicked);
            // 
            // Lab_Userpwd
            // 
            this.Lab_Userpwd.ActiveLinkColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Lab_Userpwd.AutoSize = true;
            this.Lab_Userpwd.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Lab_Userpwd.LinkColor = System.Drawing.Color.Black;
            this.Lab_Userpwd.Location = new System.Drawing.Point(305, 42);
            this.Lab_Userpwd.Name = "Lab_Userpwd";
            this.Lab_Userpwd.Size = new System.Drawing.Size(93, 22);
            this.Lab_Userpwd.TabIndex = 2;
            this.Lab_Userpwd.TabStop = true;
            this.Lab_Userpwd.Text = "linkLabel2";
            this.Lab_Userpwd.VisitedLinkColor = System.Drawing.Color.Black;
            this.Lab_Userpwd.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Lab_Userpwd_LinkClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(-112, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1074, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "—————————————————————————————————————————————————————————————————————————————————" +
    "—\r\n";
            // 
            // but_ShowHide
            // 
            this.but_ShowHide.Location = new System.Drawing.Point(281, 42);
            this.but_ShowHide.Name = "but_ShowHide";
            this.but_ShowHide.Size = new System.Drawing.Size(23, 23);
            this.but_ShowHide.TabIndex = 4;
            this.but_ShowHide.Text = "S";
            this.but_ShowHide.UseVisualStyleBackColor = true;
            this.but_ShowHide.Click += new System.EventHandler(this.but_ShowHide_Click);
            // 
            // but_ChangPwd
            // 
            this.but_ChangPwd.Location = new System.Drawing.Point(511, 9);
            this.but_ChangPwd.Name = "but_ChangPwd";
            this.but_ChangPwd.Size = new System.Drawing.Size(75, 23);
            this.but_ChangPwd.TabIndex = 5;
            this.but_ChangPwd.Text = "更改密码";
            this.but_ChangPwd.UseVisualStyleBackColor = true;
            this.but_ChangPwd.Click += new System.EventHandler(this.but_ChangPwd_Click);
            // 
            // Control_Show
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.but_ChangPwd);
            this.Controls.Add(this.but_ShowHide);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Lab_Userpwd);
            this.Controls.Add(this.Lab_User);
            this.Controls.Add(this.lab_Name);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Control_Show";
            this.Size = new System.Drawing.Size(589, 83);
            this.Load += new System.EventHandler(this.Control_Show_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label lab_Name;
        public System.Windows.Forms.LinkLabel Lab_User;
        public System.Windows.Forms.LinkLabel Lab_Userpwd;
        private System.Windows.Forms.Button but_ShowHide;
        private System.Windows.Forms.Button but_ChangPwd;
    }
}
