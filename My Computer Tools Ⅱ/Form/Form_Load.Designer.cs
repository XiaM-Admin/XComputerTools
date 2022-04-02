namespace My_Computer_Tools_Ⅱ
{
    partial class Form_Load
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Load));
            this.Lab_GotoMain = new System.Windows.Forms.Label();
            this.lab_Name = new System.Windows.Forms.Label();
            this.Lab_User = new System.Windows.Forms.Label();
            this.Text_User = new System.Windows.Forms.TextBox();
            this.Text_Pwd = new System.Windows.Forms.TextBox();
            this.Lab_Pwd = new System.Windows.Forms.Label();
            this.But_Load = new System.Windows.Forms.Button();
            this.lab_Lab = new System.Windows.Forms.Label();
            this.Lab_Register = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.GBox_Tips = new System.Windows.Forms.GroupBox();
            this.Text_TipsGG = new System.Windows.Forms.TextBox();
            this.GBox_Tips.SuspendLayout();
            this.SuspendLayout();
            // 
            // Lab_GotoMain
            // 
            this.Lab_GotoMain.AutoSize = true;
            this.Lab_GotoMain.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Lab_GotoMain.ForeColor = System.Drawing.SystemColors.Highlight;
            this.Lab_GotoMain.Location = new System.Drawing.Point(196, 252);
            this.Lab_GotoMain.Name = "Lab_GotoMain";
            this.Lab_GotoMain.Size = new System.Drawing.Size(74, 21);
            this.Lab_GotoMain.TabIndex = 0;
            this.Lab_GotoMain.Text = "以后再说";
            this.Lab_GotoMain.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Lab_GotoMain.Click += new System.EventHandler(this.Lab_GotoMain_Click);
            // 
            // lab_Name
            // 
            this.lab_Name.AutoSize = true;
            this.lab_Name.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_Name.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lab_Name.Location = new System.Drawing.Point(78, 18);
            this.lab_Name.Name = "lab_Name";
            this.lab_Name.Size = new System.Drawing.Size(132, 27);
            this.lab_Name.TabIndex = 1;
            this.lab_Name.Text = "登陆这个程序";
            // 
            // Lab_User
            // 
            this.Lab_User.AutoSize = true;
            this.Lab_User.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Lab_User.Location = new System.Drawing.Point(115, 68);
            this.Lab_User.Name = "Lab_User";
            this.Lab_User.Size = new System.Drawing.Size(51, 20);
            this.Lab_User.TabIndex = 2;
            this.Lab_User.Text = "用户名";
            // 
            // Text_User
            // 
            this.Text_User.Location = new System.Drawing.Point(36, 91);
            this.Text_User.Name = "Text_User";
            this.Text_User.Size = new System.Drawing.Size(209, 23);
            this.Text_User.TabIndex = 3;
            this.Text_User.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Text_Pwd
            // 
            this.Text_Pwd.Location = new System.Drawing.Point(36, 148);
            this.Text_Pwd.Name = "Text_Pwd";
            this.Text_Pwd.Size = new System.Drawing.Size(209, 23);
            this.Text_Pwd.TabIndex = 5;
            this.Text_Pwd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Lab_Pwd
            // 
            this.Lab_Pwd.AutoSize = true;
            this.Lab_Pwd.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Lab_Pwd.Location = new System.Drawing.Point(122, 125);
            this.Lab_Pwd.Name = "Lab_Pwd";
            this.Lab_Pwd.Size = new System.Drawing.Size(37, 20);
            this.Lab_Pwd.TabIndex = 4;
            this.Lab_Pwd.Text = "密码";
            // 
            // But_Load
            // 
            this.But_Load.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.But_Load.Location = new System.Drawing.Point(89, 196);
            this.But_Load.Name = "But_Load";
            this.But_Load.Size = new System.Drawing.Size(105, 34);
            this.But_Load.TabIndex = 6;
            this.But_Load.Text = "-登陆-";
            this.But_Load.UseVisualStyleBackColor = true;
            this.But_Load.Click += new System.EventHandler(this.But_Load_Click);
            // 
            // lab_Lab
            // 
            this.lab_Lab.AutoSize = true;
            this.lab_Lab.Location = new System.Drawing.Point(33, 47);
            this.lab_Lab.Name = "lab_Lab";
            this.lab_Lab.Size = new System.Drawing.Size(216, 17);
            this.lab_Lab.TabIndex = 7;
            this.lab_Lab.Text = "————————————————";
            // 
            // Lab_Register
            // 
            this.Lab_Register.AutoSize = true;
            this.Lab_Register.Font = new System.Drawing.Font("微软雅黑", 10.5F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Lab_Register.ForeColor = System.Drawing.Color.LightCoral;
            this.Lab_Register.Location = new System.Drawing.Point(12, 254);
            this.Lab_Register.Name = "Lab_Register";
            this.Lab_Register.Size = new System.Drawing.Size(37, 19);
            this.Lab_Register.TabIndex = 8;
            this.Lab_Register.Text = "注册";
            this.Lab_Register.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Lab_Register.Click += new System.EventHandler(this.Lab_Register_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.IndianRed;
            this.label1.Location = new System.Drawing.Point(61, 176);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 17);
            this.label1.TabIndex = 9;
            this.label1.Text = "第一次登陆以后将自动登录！";
            // 
            // GBox_Tips
            // 
            this.GBox_Tips.Controls.Add(this.Text_TipsGG);
            this.GBox_Tips.Location = new System.Drawing.Point(276, 12);
            this.GBox_Tips.Name = "GBox_Tips";
            this.GBox_Tips.Size = new System.Drawing.Size(214, 270);
            this.GBox_Tips.TabIndex = 10;
            this.GBox_Tips.TabStop = false;
            this.GBox_Tips.Text = "网络公告";
            // 
            // Text_TipsGG
            // 
            this.Text_TipsGG.Location = new System.Drawing.Point(6, 22);
            this.Text_TipsGG.Multiline = true;
            this.Text_TipsGG.Name = "Text_TipsGG";
            this.Text_TipsGG.ReadOnly = true;
            this.Text_TipsGG.Size = new System.Drawing.Size(202, 242);
            this.Text_TipsGG.TabIndex = 0;
            // 
            // Form_Load
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(498, 294);
            this.Controls.Add(this.GBox_Tips);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Lab_Register);
            this.Controls.Add(this.lab_Lab);
            this.Controls.Add(this.But_Load);
            this.Controls.Add(this.Text_Pwd);
            this.Controls.Add(this.Lab_Pwd);
            this.Controls.Add(this.Text_User);
            this.Controls.Add(this.Lab_User);
            this.Controls.Add(this.lab_Name);
            this.Controls.Add(this.Lab_GotoMain);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form_Load";
            this.Text = "   ";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.GBox_Tips.ResumeLayout(false);
            this.GBox_Tips.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Lab_GotoMain;
        private System.Windows.Forms.Label lab_Name;
        private System.Windows.Forms.Label Lab_User;
        private System.Windows.Forms.TextBox Text_User;
        private System.Windows.Forms.TextBox Text_Pwd;
        private System.Windows.Forms.Label Lab_Pwd;
        private System.Windows.Forms.Button But_Load;
        private System.Windows.Forms.Label lab_Lab;
        private System.Windows.Forms.Label Lab_Register;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox GBox_Tips;
        private System.Windows.Forms.TextBox Text_TipsGG;
    }
}

