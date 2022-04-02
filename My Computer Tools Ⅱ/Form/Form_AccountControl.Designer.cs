namespace My_Computer_Tools_Ⅱ
{
    partial class Form_AccountControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_AccountControl));
            this.lbox_AccList = new System.Windows.Forms.ListBox();
            this.but_AddChang = new System.Windows.Forms.Button();
            this.lab_TipClass = new System.Windows.Forms.Label();
            this.but_Up = new System.Windows.Forms.Button();
            this.but_Down = new System.Windows.Forms.Button();
            this.but_Del = new System.Windows.Forms.Button();
            this.lab_split = new System.Windows.Forms.Label();
            this.lab_UserTip = new System.Windows.Forms.Label();
            this.Text_User = new System.Windows.Forms.TextBox();
            this.Text_Username = new System.Windows.Forms.TextBox();
            this.lab_Username = new System.Windows.Forms.Label();
            this.Text_UserPwd = new System.Windows.Forms.TextBox();
            this.lab_Userpwd = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbox_AccList
            // 
            this.lbox_AccList.FormattingEnabled = true;
            this.lbox_AccList.ItemHeight = 12;
            this.lbox_AccList.Location = new System.Drawing.Point(13, 24);
            this.lbox_AccList.Name = "lbox_AccList";
            this.lbox_AccList.Size = new System.Drawing.Size(319, 232);
            this.lbox_AccList.TabIndex = 0;
            // 
            // but_AddChang
            // 
            this.but_AddChang.Location = new System.Drawing.Point(242, 400);
            this.but_AddChang.Name = "but_AddChang";
            this.but_AddChang.Size = new System.Drawing.Size(104, 29);
            this.but_AddChang.TabIndex = 1;
            this.but_AddChang.Text = "添加/修改账号";
            this.but_AddChang.UseVisualStyleBackColor = true;
            // 
            // lab_TipClass
            // 
            this.lab_TipClass.AutoSize = true;
            this.lab_TipClass.Location = new System.Drawing.Point(12, 6);
            this.lab_TipClass.Name = "lab_TipClass";
            this.lab_TipClass.Size = new System.Drawing.Size(17, 12);
            this.lab_TipClass.TabIndex = 2;
            this.lab_TipClass.Text = "--";
            // 
            // but_Up
            // 
            this.but_Up.Location = new System.Drawing.Point(337, 80);
            this.but_Up.Name = "but_Up";
            this.but_Up.Size = new System.Drawing.Size(31, 30);
            this.but_Up.TabIndex = 3;
            this.but_Up.Text = "↑";
            this.but_Up.UseVisualStyleBackColor = true;
            this.but_Up.Click += new System.EventHandler(this.but_Up_Click);
            // 
            // but_Down
            // 
            this.but_Down.Location = new System.Drawing.Point(337, 116);
            this.but_Down.Name = "but_Down";
            this.but_Down.Size = new System.Drawing.Size(31, 30);
            this.but_Down.TabIndex = 4;
            this.but_Down.Text = "↓";
            this.but_Down.UseVisualStyleBackColor = true;
            this.but_Down.Click += new System.EventHandler(this.but_Down_Click);
            // 
            // but_Del
            // 
            this.but_Del.Location = new System.Drawing.Point(337, 152);
            this.but_Del.Name = "but_Del";
            this.but_Del.Size = new System.Drawing.Size(31, 30);
            this.but_Del.TabIndex = 5;
            this.but_Del.Text = "×";
            this.but_Del.UseVisualStyleBackColor = true;
            this.but_Del.Click += new System.EventHandler(this.but_Del_Click);
            // 
            // lab_split
            // 
            this.lab_split.AutoSize = true;
            this.lab_split.Location = new System.Drawing.Point(-18, 264);
            this.lab_split.Name = "lab_split";
            this.lab_split.Size = new System.Drawing.Size(467, 12);
            this.lab_split.TabIndex = 6;
            this.lab_split.Text = "-----------------------------------------------------------------------------";
            // 
            // lab_UserTip
            // 
            this.lab_UserTip.AutoSize = true;
            this.lab_UserTip.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_UserTip.Location = new System.Drawing.Point(27, 329);
            this.lab_UserTip.Name = "lab_UserTip";
            this.lab_UserTip.Size = new System.Drawing.Size(39, 16);
            this.lab_UserTip.TabIndex = 7;
            this.lab_UserTip.Text = "账号";
            // 
            // Text_User
            // 
            this.Text_User.Location = new System.Drawing.Point(72, 327);
            this.Text_User.Name = "Text_User";
            this.Text_User.Size = new System.Drawing.Size(274, 21);
            this.Text_User.TabIndex = 8;
            // 
            // Text_Username
            // 
            this.Text_Username.Location = new System.Drawing.Point(72, 289);
            this.Text_Username.Name = "Text_Username";
            this.Text_Username.Size = new System.Drawing.Size(274, 21);
            this.Text_Username.TabIndex = 10;
            // 
            // lab_Username
            // 
            this.lab_Username.AutoSize = true;
            this.lab_Username.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_Username.Location = new System.Drawing.Point(20, 291);
            this.lab_Username.Name = "lab_Username";
            this.lab_Username.Size = new System.Drawing.Size(55, 16);
            this.lab_Username.TabIndex = 9;
            this.lab_Username.Text = "账号名";
            // 
            // Text_UserPwd
            // 
            this.Text_UserPwd.Location = new System.Drawing.Point(72, 363);
            this.Text_UserPwd.Name = "Text_UserPwd";
            this.Text_UserPwd.Size = new System.Drawing.Size(274, 21);
            this.Text_UserPwd.TabIndex = 12;
            // 
            // lab_Userpwd
            // 
            this.lab_Userpwd.AutoSize = true;
            this.lab_Userpwd.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_Userpwd.Location = new System.Drawing.Point(27, 365);
            this.lab_Userpwd.Name = "lab_Userpwd";
            this.lab_Userpwd.Size = new System.Drawing.Size(39, 16);
            this.lab_Userpwd.TabIndex = 11;
            this.lab_Userpwd.Text = "密码";
            // 
            // Form_AccountControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 441);
            this.Controls.Add(this.Text_UserPwd);
            this.Controls.Add(this.lab_Userpwd);
            this.Controls.Add(this.Text_Username);
            this.Controls.Add(this.lab_Username);
            this.Controls.Add(this.Text_User);
            this.Controls.Add(this.lab_UserTip);
            this.Controls.Add(this.lab_split);
            this.Controls.Add(this.but_Del);
            this.Controls.Add(this.but_Down);
            this.Controls.Add(this.but_Up);
            this.Controls.Add(this.lab_TipClass);
            this.Controls.Add(this.but_AddChang);
            this.Controls.Add(this.lbox_AccList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_AccountControl";
            this.Load += new System.EventHandler(this.Form_AccountControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbox_AccList;
        private System.Windows.Forms.Button but_AddChang;
        private System.Windows.Forms.Label lab_TipClass;
        private System.Windows.Forms.Button but_Up;
        private System.Windows.Forms.Button but_Down;
        private System.Windows.Forms.Button but_Del;
        private System.Windows.Forms.Label lab_split;
        private System.Windows.Forms.Label lab_UserTip;
        private System.Windows.Forms.TextBox Text_User;
        private System.Windows.Forms.TextBox Text_Username;
        private System.Windows.Forms.Label lab_Username;
        private System.Windows.Forms.TextBox Text_UserPwd;
        private System.Windows.Forms.Label lab_Userpwd;
    }
}