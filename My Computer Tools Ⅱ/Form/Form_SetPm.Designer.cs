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
            this.label11 = new System.Windows.Forms.Label();
            this.but_ReSetIni = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.CBox_City = new System.Windows.Forms.ComboBox();
            this.CBox_GeographyPos = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.PicBox_TipFileCheck = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CBox_UpFileCheck = new System.Windows.Forms.CheckBox();
            this.tbox_email = new System.Windows.Forms.TextBox();
            this.Number_weather = new System.Windows.Forms.NumericUpDown();
            this.CBox_OpenStartRun = new System.Windows.Forms.CheckBox();
            this.CBox_ShowAccinCMBS = new System.Windows.Forms.CheckBox();
            this.CBox_UpEnd = new System.Windows.Forms.ComboBox();
            this.Tbox_ImgUpPath = new System.Windows.Forms.TextBox();
            this.FailureTryNumber = new System.Windows.Forms.NumericUpDown();
            this.CBox_qnShowMesg = new System.Windows.Forms.CheckBox();
            this.CheackFileNumber = new System.Windows.Forms.NumericUpDown();
            this.CBox_qnUpFileCheck = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBox_TipFileCheck)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Number_weather)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FailureTryNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CheackFileNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 31);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(492, 247);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tbox_email);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.but_ReSetIni);
            this.tabPage1.Controls.Add(this.Number_weather);
            this.tabPage1.Controls.Add(this.label8);
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
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(2, 107);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(82, 14);
            this.label11.TabIndex = 8;
            this.label11.Text = "提醒邮箱：";
            // 
            // but_ReSetIni
            // 
            this.but_ReSetIni.Location = new System.Drawing.Point(355, 192);
            this.but_ReSetIni.Name = "but_ReSetIni";
            this.but_ReSetIni.Size = new System.Drawing.Size(123, 23);
            this.but_ReSetIni.TabIndex = 7;
            this.but_ReSetIni.Text = "如何恢复默认配置？";
            this.but_ReSetIni.UseVisualStyleBackColor = true;
            this.but_ReSetIni.Click += new System.EventHandler(this.but_ReSetIni_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(2, 80);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(112, 14);
            this.label8.TabIndex = 5;
            this.label8.Text = "天气刷新间隔：";
            // 
            // CBox_City
            // 
            this.CBox_City.FormattingEnabled = true;
            this.CBox_City.Location = new System.Drawing.Point(89, 51);
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
            this.CBox_GeographyPos.Location = new System.Drawing.Point(10, 51);
            this.CBox_GeographyPos.Name = "CBox_GeographyPos";
            this.CBox_GeographyPos.Size = new System.Drawing.Size(72, 20);
            this.CBox_GeographyPos.TabIndex = 2;
            this.CBox_GeographyPos.SelectedIndexChanged += new System.EventHandler(this.CBox_GeographyPos_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(5, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "天气地理位置设置：";
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
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label10);
            this.tabPage3.Controls.Add(this.label9);
            this.tabPage3.Controls.Add(this.groupBox1);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.PicBox_TipFileCheck);
            this.tabPage3.Controls.Add(this.FailureTryNumber);
            this.tabPage3.Controls.Add(this.CBox_qnShowMesg);
            this.tabPage3.Controls.Add(this.CheackFileNumber);
            this.tabPage3.Controls.Add(this.CBox_qnUpFileCheck);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(484, 221);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "文件同步";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(142, 136);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 12);
            this.label10.TabIndex = 10;
            this.label10.Text = "次";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 136);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 8;
            this.label9.Text = "失败重试：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.CBox_UpEnd);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.Tbox_ImgUpPath);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(195, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(283, 107);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "剪贴板";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 53);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(251, 12);
            this.label7.TabIndex = 4;
            this.label7.Text = "上传位置最后不需要加上“/”默认时间戳上传\r\n";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 80);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 2;
            this.label6.Text = "上传后";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "上传位置";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(137, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "分钟";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "监控周期：";
            // 
            // PicBox_TipFileCheck
            // 
            this.PicBox_TipFileCheck.Image = global::My_Computer_Tools_Ⅱ.Properties.Resources.警告;
            this.PicBox_TipFileCheck.Location = new System.Drawing.Point(157, 13);
            this.PicBox_TipFileCheck.Name = "PicBox_TipFileCheck";
            this.PicBox_TipFileCheck.Size = new System.Drawing.Size(32, 32);
            this.PicBox_TipFileCheck.TabIndex = 1;
            this.PicBox_TipFileCheck.TabStop = false;
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
            // CBox_UpFileCheck
            // 
            this.CBox_UpFileCheck.AutoSize = true;
            this.CBox_UpFileCheck.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CBox_UpFileCheck.ForeColor = System.Drawing.Color.Red;
            this.CBox_UpFileCheck.Location = new System.Drawing.Point(19, 21);
            this.CBox_UpFileCheck.Name = "CBox_UpFileCheck";
            this.CBox_UpFileCheck.Size = new System.Drawing.Size(135, 16);
            this.CBox_UpFileCheck.TabIndex = 0;
            this.CBox_UpFileCheck.Text = "上传检查 推荐开启";
            this.CBox_UpFileCheck.UseVisualStyleBackColor = true;
            // 
            // tbox_email
            // 
            this.tbox_email.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::My_Computer_Tools_Ⅱ.Properties.Settings.Default, "email", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tbox_email.Location = new System.Drawing.Point(5, 124);
            this.tbox_email.Name = "tbox_email";
            this.tbox_email.Size = new System.Drawing.Size(156, 21);
            this.tbox_email.TabIndex = 9;
            this.tbox_email.Text = global::My_Computer_Tools_Ⅱ.Properties.Settings.Default.email;
            this.tbox_email.TextChanged += new System.EventHandler(this.tbox_email_TextChanged);
            // 
            // Number_weather
            // 
            this.Number_weather.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::My_Computer_Tools_Ⅱ.Properties.Settings.Default, "WeatherNumber", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Number_weather.Location = new System.Drawing.Point(105, 78);
            this.Number_weather.Name = "Number_weather";
            this.Number_weather.Size = new System.Drawing.Size(56, 21);
            this.Number_weather.TabIndex = 6;
            this.Number_weather.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Number_weather.Value = global::My_Computer_Tools_Ⅱ.Properties.Settings.Default.WeatherNumber;
            this.Number_weather.ValueChanged += new System.EventHandler(this.Number_weather_ValueChanged);
            // 
            // CBox_OpenStartRun
            // 
            this.CBox_OpenStartRun.AutoSize = true;
            this.CBox_OpenStartRun.Checked = global::My_Computer_Tools_Ⅱ.Properties.Settings.Default.OpenStartRun;
            this.CBox_OpenStartRun.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::My_Computer_Tools_Ⅱ.Properties.Settings.Default, "OpenStartRun", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.CBox_OpenStartRun.Location = new System.Drawing.Point(8, 6);
            this.CBox_OpenStartRun.Name = "CBox_OpenStartRun";
            this.CBox_OpenStartRun.Size = new System.Drawing.Size(72, 16);
            this.CBox_OpenStartRun.TabIndex = 0;
            this.CBox_OpenStartRun.Text = "开机自启";
            this.CBox_OpenStartRun.UseVisualStyleBackColor = true;
            // 
            // CBox_ShowAccinCMBS
            // 
            this.CBox_ShowAccinCMBS.AutoSize = true;
            this.CBox_ShowAccinCMBS.Checked = global::My_Computer_Tools_Ⅱ.Properties.Settings.Default.ShowAccinCMBS;
            this.CBox_ShowAccinCMBS.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::My_Computer_Tools_Ⅱ.Properties.Settings.Default, "ShowAccinCMBS", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.CBox_ShowAccinCMBS.Location = new System.Drawing.Point(19, 21);
            this.CBox_ShowAccinCMBS.Name = "CBox_ShowAccinCMBS";
            this.CBox_ShowAccinCMBS.Size = new System.Drawing.Size(144, 16);
            this.CBox_ShowAccinCMBS.TabIndex = 0;
            this.CBox_ShowAccinCMBS.Text = "是否在托盘菜单中显示";
            this.CBox_ShowAccinCMBS.UseVisualStyleBackColor = true;
            // 
            // CBox_UpEnd
            // 
            this.CBox_UpEnd.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::My_Computer_Tools_Ⅱ.Properties.Settings.Default, "qnUpEnd", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.CBox_UpEnd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_UpEnd.FormattingEnabled = true;
            this.CBox_UpEnd.Items.AddRange(new object[] {
            "MarkDown格式",
            "普通直链链接",
            "Html格式"});
            this.CBox_UpEnd.Location = new System.Drawing.Point(73, 77);
            this.CBox_UpEnd.Name = "CBox_UpEnd";
            this.CBox_UpEnd.Size = new System.Drawing.Size(158, 20);
            this.CBox_UpEnd.TabIndex = 3;
            this.CBox_UpEnd.Text = global::My_Computer_Tools_Ⅱ.Properties.Settings.Default.qnUpEnd;
            this.CBox_UpEnd.SelectedIndexChanged += new System.EventHandler(this.CBox_UpEnd_SelectedIndexChanged);
            // 
            // Tbox_ImgUpPath
            // 
            this.Tbox_ImgUpPath.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::My_Computer_Tools_Ⅱ.Properties.Settings.Default, "qnImgPath", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Tbox_ImgUpPath.Location = new System.Drawing.Point(73, 21);
            this.Tbox_ImgUpPath.Name = "Tbox_ImgUpPath";
            this.Tbox_ImgUpPath.Size = new System.Drawing.Size(158, 21);
            this.Tbox_ImgUpPath.TabIndex = 1;
            this.Tbox_ImgUpPath.Text = global::My_Computer_Tools_Ⅱ.Properties.Settings.Default.qnImgPath;
            this.Tbox_ImgUpPath.TextChanged += new System.EventHandler(this.Tbox_ImgUpPath_TextChanged);
            // 
            // FailureTryNumber
            // 
            this.FailureTryNumber.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::My_Computer_Tools_Ⅱ.Properties.Settings.Default, "FailureTryNumber", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.FailureTryNumber.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FailureTryNumber.Location = new System.Drawing.Point(77, 130);
            this.FailureTryNumber.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.FailureTryNumber.Name = "FailureTryNumber";
            this.FailureTryNumber.Size = new System.Drawing.Size(56, 23);
            this.FailureTryNumber.TabIndex = 9;
            this.FailureTryNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.FailureTryNumber.Value = global::My_Computer_Tools_Ⅱ.Properties.Settings.Default.FailureTryNumber;
            this.FailureTryNumber.ValueChanged += new System.EventHandler(this.FailureTryNumber_ValueChanged);
            // 
            // CBox_qnShowMesg
            // 
            this.CBox_qnShowMesg.AutoSize = true;
            this.CBox_qnShowMesg.Checked = global::My_Computer_Tools_Ⅱ.Properties.Settings.Default.qnShowMesg;
            this.CBox_qnShowMesg.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::My_Computer_Tools_Ⅱ.Properties.Settings.Default, "qnShowMesg", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.CBox_qnShowMesg.Location = new System.Drawing.Point(19, 61);
            this.CBox_qnShowMesg.Name = "CBox_qnShowMesg";
            this.CBox_qnShowMesg.Size = new System.Drawing.Size(120, 16);
            this.CBox_qnShowMesg.TabIndex = 6;
            this.CBox_qnShowMesg.Text = "后台静默同步上传";
            this.CBox_qnShowMesg.UseVisualStyleBackColor = true;
            this.CBox_qnShowMesg.CheckedChanged += new System.EventHandler(this.CBox_qnShowMesg_CheckedChanged);
            // 
            // CheackFileNumber
            // 
            this.CheackFileNumber.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::My_Computer_Tools_Ⅱ.Properties.Settings.Default, "CheckFileNumber", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.CheackFileNumber.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CheackFileNumber.Location = new System.Drawing.Point(77, 99);
            this.CheackFileNumber.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.CheackFileNumber.Name = "CheackFileNumber";
            this.CheackFileNumber.Size = new System.Drawing.Size(56, 23);
            this.CheackFileNumber.TabIndex = 4;
            this.CheackFileNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.CheackFileNumber.Value = global::My_Computer_Tools_Ⅱ.Properties.Settings.Default.CheckFileNumber;
            this.CheackFileNumber.ValueChanged += new System.EventHandler(this.CheackFileNumber_ValueChanged);
            // 
            // CBox_qnUpFileCheck
            // 
            this.CBox_qnUpFileCheck.AutoSize = true;
            this.CBox_qnUpFileCheck.Checked = global::My_Computer_Tools_Ⅱ.Properties.Settings.Default.qnUpFileCheck;
            this.CBox_qnUpFileCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CBox_qnUpFileCheck.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::My_Computer_Tools_Ⅱ.Properties.Settings.Default, "qnUpFileCheck", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.CBox_qnUpFileCheck.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CBox_qnUpFileCheck.ForeColor = System.Drawing.Color.Red;
            this.CBox_qnUpFileCheck.Location = new System.Drawing.Point(19, 21);
            this.CBox_qnUpFileCheck.Name = "CBox_qnUpFileCheck";
            this.CBox_qnUpFileCheck.Size = new System.Drawing.Size(135, 16);
            this.CBox_qnUpFileCheck.TabIndex = 0;
            this.CBox_qnUpFileCheck.Text = "上传检查 推荐开启";
            this.CBox_qnUpFileCheck.UseVisualStyleBackColor = true;
            this.CBox_qnUpFileCheck.CheckedChanged += new System.EventHandler(this.CBox_UpFileCheck_CheckedChanged);
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
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_SetPm_FormClosing);
            this.Load += new System.EventHandler(this.Form_SetPm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBox_TipFileCheck)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Number_weather)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FailureTryNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CheackFileNumber)).EndInit();
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
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.CheckBox CBox_qnUpFileCheck;
        private System.Windows.Forms.PictureBox PicBox_TipFileCheck;
        private System.Windows.Forms.CheckBox CBox_UpFileCheck;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown CheackFileNumber;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox CBox_qnShowMesg;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox Tbox_ImgUpPath;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox CBox_UpEnd;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown Number_weather;
        private System.Windows.Forms.Button but_ReSetIni;
        private System.Windows.Forms.NumericUpDown FailureTryNumber;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbox_email;
        private System.Windows.Forms.Label label11;
    }
}