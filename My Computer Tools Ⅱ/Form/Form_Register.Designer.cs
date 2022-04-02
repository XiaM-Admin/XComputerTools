namespace My_Computer_Tools_Ⅱ
{
    partial class Register
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Register));
            this.lab_WinName = new System.Windows.Forms.Label();
            this.lab_UserName = new System.Windows.Forms.Label();
            this.lab_UserPwd = new System.Windows.Forms.Label();
            this.Text_UserName = new System.Windows.Forms.TextBox();
            this.Text_UserPwd = new System.Windows.Forms.TextBox();
            this.lab_Email = new System.Windows.Forms.Label();
            this.Text_Email = new System.Windows.Forms.TextBox();
            this.lab_EmailTip = new System.Windows.Forms.Label();
            this.but_Register = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lab_WinName
            // 
            this.lab_WinName.AutoSize = true;
            this.lab_WinName.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.lab_WinName.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lab_WinName.Location = new System.Drawing.Point(117, 12);
            this.lab_WinName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lab_WinName.Name = "lab_WinName";
            this.lab_WinName.Size = new System.Drawing.Size(72, 27);
            this.lab_WinName.TabIndex = 0;
            this.lab_WinName.Text = "注册我";
            // 
            // lab_UserName
            // 
            this.lab_UserName.AutoSize = true;
            this.lab_UserName.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_UserName.Location = new System.Drawing.Point(124, 58);
            this.lab_UserName.Name = "lab_UserName";
            this.lab_UserName.Size = new System.Drawing.Size(58, 21);
            this.lab_UserName.TabIndex = 1;
            this.lab_UserName.Text = "用户名";
            // 
            // lab_UserPwd
            // 
            this.lab_UserPwd.AutoSize = true;
            this.lab_UserPwd.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_UserPwd.Location = new System.Drawing.Point(132, 120);
            this.lab_UserPwd.Name = "lab_UserPwd";
            this.lab_UserPwd.Size = new System.Drawing.Size(42, 21);
            this.lab_UserPwd.TabIndex = 2;
            this.lab_UserPwd.Text = "密码";
            // 
            // Text_UserName
            // 
            this.Text_UserName.Location = new System.Drawing.Point(40, 83);
            this.Text_UserName.Name = "Text_UserName";
            this.Text_UserName.Size = new System.Drawing.Size(230, 23);
            this.Text_UserName.TabIndex = 1;
            this.Text_UserName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Text_UserPwd
            // 
            this.Text_UserPwd.Location = new System.Drawing.Point(40, 144);
            this.Text_UserPwd.Name = "Text_UserPwd";
            this.Text_UserPwd.Size = new System.Drawing.Size(230, 23);
            this.Text_UserPwd.TabIndex = 2;
            this.Text_UserPwd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lab_Email
            // 
            this.lab_Email.AutoSize = true;
            this.lab_Email.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_Email.Location = new System.Drawing.Point(116, 183);
            this.lab_Email.Name = "lab_Email";
            this.lab_Email.Size = new System.Drawing.Size(74, 21);
            this.lab_Email.TabIndex = 3;
            this.lab_Email.Text = "电子邮箱";
            // 
            // Text_Email
            // 
            this.Text_Email.Location = new System.Drawing.Point(40, 207);
            this.Text_Email.Name = "Text_Email";
            this.Text_Email.Size = new System.Drawing.Size(230, 23);
            this.Text_Email.TabIndex = 4;
            this.Text_Email.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lab_EmailTip
            // 
            this.lab_EmailTip.AutoSize = true;
            this.lab_EmailTip.ForeColor = System.Drawing.Color.IndianRed;
            this.lab_EmailTip.Location = new System.Drawing.Point(193, 187);
            this.lab_EmailTip.Name = "lab_EmailTip";
            this.lab_EmailTip.Size = new System.Drawing.Size(56, 17);
            this.lab_EmailTip.TabIndex = 5;
            this.lab_EmailTip.Text = "找回密码";
            // 
            // but_Register
            // 
            this.but_Register.Location = new System.Drawing.Point(103, 269);
            this.but_Register.Name = "but_Register";
            this.but_Register.Size = new System.Drawing.Size(100, 38);
            this.but_Register.TabIndex = 6;
            this.but_Register.Text = "注册";
            this.but_Register.UseVisualStyleBackColor = true;
            this.but_Register.Click += new System.EventHandler(this.but_Register_Click);
            // 
            // Register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(316, 336);
            this.Controls.Add(this.but_Register);
            this.Controls.Add(this.lab_EmailTip);
            this.Controls.Add(this.Text_Email);
            this.Controls.Add(this.lab_Email);
            this.Controls.Add(this.Text_UserPwd);
            this.Controls.Add(this.Text_UserName);
            this.Controls.Add(this.lab_UserPwd);
            this.Controls.Add(this.lab_UserName);
            this.Controls.Add(this.lab_WinName);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Register";
            this.ShowInTaskbar = false;
            this.Text = "Form_Register";
            this.Load += new System.EventHandler(this.Form_Register_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lab_WinName;
        private System.Windows.Forms.Label lab_UserName;
        private System.Windows.Forms.Label lab_UserPwd;
        private System.Windows.Forms.TextBox Text_UserName;
        private System.Windows.Forms.TextBox Text_UserPwd;
        private System.Windows.Forms.Label lab_Email;
        private System.Windows.Forms.TextBox Text_Email;
        private System.Windows.Forms.Label lab_EmailTip;
        private System.Windows.Forms.Button but_Register;
    }
}