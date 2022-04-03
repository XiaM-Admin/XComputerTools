namespace My_Computer_Tools_Ⅱ
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
            this.SuspendLayout();
            // 
            // lab_Name
            // 
            this.lab_Name.AutoSize = true;
            this.lab_Name.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_Name.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lab_Name.Location = new System.Drawing.Point(224, 9);
            this.lab_Name.Name = "lab_Name";
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
            // 
            // Lab_Userpwd
            // 
            this.Lab_Userpwd.ActiveLinkColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Lab_Userpwd.AutoSize = true;
            this.Lab_Userpwd.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Lab_Userpwd.LinkColor = System.Drawing.Color.Black;
            this.Lab_Userpwd.Location = new System.Drawing.Point(299, 42);
            this.Lab_Userpwd.Name = "Lab_Userpwd";
            this.Lab_Userpwd.Size = new System.Drawing.Size(93, 22);
            this.Lab_Userpwd.TabIndex = 2;
            this.Lab_Userpwd.TabStop = true;
            this.Lab_Userpwd.Text = "linkLabel2";
            this.Lab_Userpwd.VisitedLinkColor = System.Drawing.Color.Black;
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
            // Control_Show
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Lab_Userpwd);
            this.Controls.Add(this.Lab_User);
            this.Controls.Add(this.lab_Name);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
    }
}
