namespace My_Computer_Tools_Ⅱ
{
    partial class Form_SetPm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_SetPm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.CBox_City = new System.Windows.Forms.ComboBox();
            this.CBox_GeographyPos = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.CBox_OpenStartRun = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.CBox_ShowAccinCMBS = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 31);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(492, 247);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.CBox_City);
            this.tabPage1.Controls.Add(this.CBox_GeographyPos);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.CBox_OpenStartRun);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(484, 221);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Program";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // CBox_City
            // 
            this.CBox_City.FormattingEnabled = true;
            this.CBox_City.Location = new System.Drawing.Point(86, 76);
            this.CBox_City.Name = "CBox_City";
            this.CBox_City.Size = new System.Drawing.Size(72, 20);
            this.CBox_City.TabIndex = 4;
            // 
            // CBox_GeographyPos
            // 
            this.CBox_GeographyPos.FormattingEnabled = true;
            this.CBox_GeographyPos.Items.AddRange(new object[] {
            "北京市",
            "天津市",
            "河北省",
            "山西省",
            "辽宁省",
            "吉林省",
            "上海市",
            "江苏省",
            "浙江省",
            "安徽省",
            "福建省",
            "江西省",
            "山东省",
            "河南省",
            "湖北省",
            "湖南省",
            "广东省",
            "海南省",
            "重庆市",
            "四川省",
            "贵州省",
            "云南省",
            "陕西省",
            "甘肃省",
            "青海省",
            "台湾省",
            "黑龙江省",
            "西藏自治区",
            "宁夏回族自治区",
            "新疆维吾尔自治区",
            "香港特别行政区",
            "内蒙古自治区",
            "广西壮族自治区"});
            this.CBox_GeographyPos.Location = new System.Drawing.Point(6, 76);
            this.CBox_GeographyPos.Name = "CBox_GeographyPos";
            this.CBox_GeographyPos.Size = new System.Drawing.Size(72, 20);
            this.CBox_GeographyPos.TabIndex = 2;
            this.CBox_GeographyPos.SelectedIndexChanged += new System.EventHandler(this.CBox_GeographyPos_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(16, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "天气地理位置设置：";
            // 
            // CBox_OpenStartRun
            // 
            this.CBox_OpenStartRun.AutoSize = true;
            this.CBox_OpenStartRun.Location = new System.Drawing.Point(19, 21);
            this.CBox_OpenStartRun.Name = "CBox_OpenStartRun";
            this.CBox_OpenStartRun.Size = new System.Drawing.Size(72, 16);
            this.CBox_OpenStartRun.TabIndex = 0;
            this.CBox_OpenStartRun.Text = "开机自启";
            this.CBox_OpenStartRun.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.CBox_ShowAccinCMBS);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(484, 221);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "账号本本";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // CBox_ShowAccinCMBS
            // 
            this.CBox_ShowAccinCMBS.AutoSize = true;
            this.CBox_ShowAccinCMBS.Location = new System.Drawing.Point(19, 21);
            this.CBox_ShowAccinCMBS.Name = "CBox_ShowAccinCMBS";
            this.CBox_ShowAccinCMBS.Size = new System.Drawing.Size(144, 16);
            this.CBox_ShowAccinCMBS.TabIndex = 0;
            this.CBox_ShowAccinCMBS.Text = "是否在托盘菜单中显示";
            this.CBox_ShowAccinCMBS.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label1.Location = new System.Drawing.Point(13, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "设置";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form_SetPm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 290);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_SetPm";
            this.Text = "程序设置";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_SetPm_FormClosed);
            this.Load += new System.EventHandler(this.Form_SetPm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.CheckBox CBox_OpenStartRun;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox CBox_ShowAccinCMBS;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox CBox_GeographyPos;
        private System.Windows.Forms.ComboBox CBox_City;
    }
}