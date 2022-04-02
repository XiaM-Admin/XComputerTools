namespace My_Computer_Tools_Ⅱ
{
    partial class Form_Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Main));
            this.STrip_Main = new System.Windows.Forms.StatusStrip();
            this.StaLab_Time = new System.Windows.Forms.ToolStripStatusLabel();
            this.StaLab_Spilt1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StaLab_LoginState = new System.Windows.Forms.ToolStripStatusLabel();
            this.StaLab_State = new System.Windows.Forms.ToolStripStatusLabel();
            this.Timer_STrip = new System.Windows.Forms.Timer(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabP_Home = new System.Windows.Forms.TabPage();
            this.lab_isAdmin = new System.Windows.Forms.Label();
            this.but_SetPro = new System.Windows.Forms.Button();
            this.lab_TimeDate = new System.Windows.Forms.Label();
            this.lab_MAC = new System.Windows.Forms.Label();
            this.lab_SizeMeo = new System.Windows.Forms.Label();
            this.lab_Ip = new System.Windows.Forms.Label();
            this.lab_ComputerName = new System.Windows.Forms.Label();
            this.But_ExitLogin = new System.Windows.Forms.Button();
            this.lab_User = new System.Windows.Forms.Label();
            this.lab_WelCome = new System.Windows.Forms.Label();
            this.lab_ProName = new System.Windows.Forms.Label();
            this.GBox_NewTip = new System.Windows.Forms.GroupBox();
            this.Text_NewTip = new System.Windows.Forms.TextBox();
            this.tabP_UorP = new System.Windows.Forms.TabPage();
            this.but_ShowAccC = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lab_UserClass = new System.Windows.Forms.Label();
            this.but_ChangClass = new System.Windows.Forms.Button();
            this.Cbox_UserClass = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tlp = new System.Windows.Forms.TableLayoutPanel();
            this.tabP_About = new System.Windows.Forms.TabPage();
            this.lab_MySaid = new System.Windows.Forms.Label();
            this.GBox_Target = new System.Windows.Forms.GroupBox();
            this.RText_target = new System.Windows.Forms.RichTextBox();
            this.NotifyIconBack = new System.Windows.Forms.NotifyIcon(this.components);
            this.ContextMenuBackStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.显示ShowtoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于abouttoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lab_Sptil = new System.Windows.Forms.Label();
            this.STrip_Main.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabP_Home.SuspendLayout();
            this.GBox_NewTip.SuspendLayout();
            this.tabP_UorP.SuspendLayout();
            this.tabP_About.SuspendLayout();
            this.GBox_Target.SuspendLayout();
            this.ContextMenuBackStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // STrip_Main
            // 
            this.STrip_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StaLab_Time,
            this.StaLab_Spilt1,
            this.StaLab_LoginState,
            this.StaLab_State});
            this.STrip_Main.Location = new System.Drawing.Point(0, 460);
            this.STrip_Main.Name = "STrip_Main";
            this.STrip_Main.Size = new System.Drawing.Size(825, 22);
            this.STrip_Main.TabIndex = 0;
            this.STrip_Main.Text = "statusStrip1";
            // 
            // StaLab_Time
            // 
            this.StaLab_Time.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.StaLab_Time.Name = "StaLab_Time";
            this.StaLab_Time.Size = new System.Drawing.Size(36, 17);
            this.StaLab_Time.Text = "Time";
            // 
            // StaLab_Spilt1
            // 
            this.StaLab_Spilt1.Name = "StaLab_Spilt1";
            this.StaLab_Spilt1.Size = new System.Drawing.Size(11, 17);
            this.StaLab_Spilt1.Text = "|";
            // 
            // StaLab_LoginState
            // 
            this.StaLab_LoginState.Name = "StaLab_LoginState";
            this.StaLab_LoginState.Size = new System.Drawing.Size(32, 17);
            this.StaLab_LoginState.Text = "状态";
            this.StaLab_LoginState.Click += new System.EventHandler(this.StaLab_LoginState_Click);
            // 
            // StaLab_State
            // 
            this.StaLab_State.Name = "StaLab_State";
            this.StaLab_State.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StaLab_State.Size = new System.Drawing.Size(731, 17);
            this.StaLab_State.Spring = true;
            // 
            // Timer_STrip
            // 
            this.Timer_STrip.Interval = 999;
            this.Timer_STrip.Tick += new System.EventHandler(this.Timer_STrip_Tick);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabP_Home);
            this.tabControl1.Controls.Add(this.tabP_UorP);
            this.tabControl1.Controls.Add(this.tabP_About);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(801, 445);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabP_Home
            // 
            this.tabP_Home.Controls.Add(this.lab_isAdmin);
            this.tabP_Home.Controls.Add(this.but_SetPro);
            this.tabP_Home.Controls.Add(this.lab_TimeDate);
            this.tabP_Home.Controls.Add(this.lab_MAC);
            this.tabP_Home.Controls.Add(this.lab_SizeMeo);
            this.tabP_Home.Controls.Add(this.lab_Ip);
            this.tabP_Home.Controls.Add(this.lab_ComputerName);
            this.tabP_Home.Controls.Add(this.But_ExitLogin);
            this.tabP_Home.Controls.Add(this.lab_User);
            this.tabP_Home.Controls.Add(this.lab_WelCome);
            this.tabP_Home.Controls.Add(this.lab_ProName);
            this.tabP_Home.Controls.Add(this.GBox_NewTip);
            this.tabP_Home.Location = new System.Drawing.Point(4, 26);
            this.tabP_Home.Name = "tabP_Home";
            this.tabP_Home.Padding = new System.Windows.Forms.Padding(3);
            this.tabP_Home.Size = new System.Drawing.Size(793, 415);
            this.tabP_Home.TabIndex = 0;
            this.tabP_Home.Text = "首页";
            this.tabP_Home.UseVisualStyleBackColor = true;
            // 
            // lab_isAdmin
            // 
            this.lab_isAdmin.AutoSize = true;
            this.lab_isAdmin.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_isAdmin.Location = new System.Drawing.Point(92, 157);
            this.lab_isAdmin.Name = "lab_isAdmin";
            this.lab_isAdmin.Size = new System.Drawing.Size(132, 27);
            this.lab_isAdmin.TabIndex = 11;
            this.lab_isAdmin.Text = "管理员权限：";
            // 
            // but_SetPro
            // 
            this.but_SetPro.Location = new System.Drawing.Point(412, 351);
            this.but_SetPro.Name = "but_SetPro";
            this.but_SetPro.Size = new System.Drawing.Size(78, 23);
            this.but_SetPro.TabIndex = 10;
            this.but_SetPro.TabStop = false;
            this.but_SetPro.Text = "程序设置";
            this.but_SetPro.UseVisualStyleBackColor = true;
            this.but_SetPro.Click += new System.EventHandler(this.but_SetPro_Click);
            // 
            // lab_TimeDate
            // 
            this.lab_TimeDate.AutoSize = true;
            this.lab_TimeDate.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_TimeDate.ForeColor = System.Drawing.Color.BlueViolet;
            this.lab_TimeDate.Location = new System.Drawing.Point(92, 111);
            this.lab_TimeDate.Name = "lab_TimeDate";
            this.lab_TimeDate.Size = new System.Drawing.Size(69, 26);
            this.lab_TimeDate.TabIndex = 9;
            this.lab_TimeDate.Text = "日期：";
            // 
            // lab_MAC
            // 
            this.lab_MAC.AutoSize = true;
            this.lab_MAC.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_MAC.Location = new System.Drawing.Point(92, 348);
            this.lab_MAC.Name = "lab_MAC";
            this.lab_MAC.Size = new System.Drawing.Size(79, 27);
            this.lab_MAC.TabIndex = 8;
            this.lab_MAC.Text = "MAC：";
            // 
            // lab_SizeMeo
            // 
            this.lab_SizeMeo.AutoSize = true;
            this.lab_SizeMeo.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_SizeMeo.Location = new System.Drawing.Point(92, 311);
            this.lab_SizeMeo.Name = "lab_SizeMeo";
            this.lab_SizeMeo.Size = new System.Drawing.Size(112, 27);
            this.lab_SizeMeo.TabIndex = 7;
            this.lab_SizeMeo.Text = "运行内存：";
            // 
            // lab_Ip
            // 
            this.lab_Ip.AutoSize = true;
            this.lab_Ip.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_Ip.Location = new System.Drawing.Point(92, 273);
            this.lab_Ip.Name = "lab_Ip";
            this.lab_Ip.Size = new System.Drawing.Size(90, 27);
            this.lab_Ip.TabIndex = 6;
            this.lab_Ip.Text = "内网IP：";
            // 
            // lab_ComputerName
            // 
            this.lab_ComputerName.AutoSize = true;
            this.lab_ComputerName.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_ComputerName.Location = new System.Drawing.Point(92, 233);
            this.lab_ComputerName.Name = "lab_ComputerName";
            this.lab_ComputerName.Size = new System.Drawing.Size(152, 27);
            this.lab_ComputerName.TabIndex = 5;
            this.lab_ComputerName.Text = "计算机用户名：";
            // 
            // But_ExitLogin
            // 
            this.But_ExitLogin.Location = new System.Drawing.Point(412, 380);
            this.But_ExitLogin.Name = "But_ExitLogin";
            this.But_ExitLogin.Size = new System.Drawing.Size(78, 23);
            this.But_ExitLogin.TabIndex = 4;
            this.But_ExitLogin.TabStop = false;
            this.But_ExitLogin.Text = "退出登陆";
            this.But_ExitLogin.UseVisualStyleBackColor = true;
            this.But_ExitLogin.Click += new System.EventHandler(this.But_ExitLogin_Click);
            // 
            // lab_User
            // 
            this.lab_User.AutoSize = true;
            this.lab_User.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_User.Location = new System.Drawing.Point(92, 195);
            this.lab_User.Name = "lab_User";
            this.lab_User.Size = new System.Drawing.Size(115, 27);
            this.lab_User.TabIndex = 3;
            this.lab_User.Text = "用户User：";
            // 
            // lab_WelCome
            // 
            this.lab_WelCome.AutoSize = true;
            this.lab_WelCome.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_WelCome.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lab_WelCome.Location = new System.Drawing.Point(92, 41);
            this.lab_WelCome.Name = "lab_WelCome";
            this.lab_WelCome.Size = new System.Drawing.Size(245, 52);
            this.lab_WelCome.TabIndex = 2;
            this.lab_WelCome.Text = "WelCome，\r\nThank You For Your Use.";
            // 
            // lab_ProName
            // 
            this.lab_ProName.AutoSize = true;
            this.lab_ProName.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_ProName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lab_ProName.Location = new System.Drawing.Point(487, 19);
            this.lab_ProName.Name = "lab_ProName";
            this.lab_ProName.Size = new System.Drawing.Size(298, 39);
            this.lab_ProName.TabIndex = 1;
            this.lab_ProName.Text = " Computer Tools Ⅱ";
            this.lab_ProName.Click += new System.EventHandler(this.lab_ProName_Click);
            // 
            // GBox_NewTip
            // 
            this.GBox_NewTip.Controls.Add(this.Text_NewTip);
            this.GBox_NewTip.Location = new System.Drawing.Point(496, 71);
            this.GBox_NewTip.Name = "GBox_NewTip";
            this.GBox_NewTip.Size = new System.Drawing.Size(291, 338);
            this.GBox_NewTip.TabIndex = 0;
            this.GBox_NewTip.TabStop = false;
            this.GBox_NewTip.Text = "最新告示";
            // 
            // Text_NewTip
            // 
            this.Text_NewTip.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Text_NewTip.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Text_NewTip.Location = new System.Drawing.Point(6, 22);
            this.Text_NewTip.Multiline = true;
            this.Text_NewTip.Name = "Text_NewTip";
            this.Text_NewTip.ReadOnly = true;
            this.Text_NewTip.Size = new System.Drawing.Size(279, 310);
            this.Text_NewTip.TabIndex = 0;
            // 
            // tabP_UorP
            // 
            this.tabP_UorP.Controls.Add(this.lab_Sptil);
            this.tabP_UorP.Controls.Add(this.but_ShowAccC);
            this.tabP_UorP.Controls.Add(this.label1);
            this.tabP_UorP.Controls.Add(this.lab_UserClass);
            this.tabP_UorP.Controls.Add(this.but_ChangClass);
            this.tabP_UorP.Controls.Add(this.Cbox_UserClass);
            this.tabP_UorP.Controls.Add(this.button1);
            this.tabP_UorP.Controls.Add(this.tlp);
            this.tabP_UorP.Location = new System.Drawing.Point(4, 26);
            this.tabP_UorP.Name = "tabP_UorP";
            this.tabP_UorP.Padding = new System.Windows.Forms.Padding(3);
            this.tabP_UorP.Size = new System.Drawing.Size(793, 415);
            this.tabP_UorP.TabIndex = 1;
            this.tabP_UorP.Text = "账号本本";
            this.tabP_UorP.UseVisualStyleBackColor = true;
            // 
            // but_ShowAccC
            // 
            this.but_ShowAccC.Location = new System.Drawing.Point(15, 189);
            this.but_ShowAccC.Name = "but_ShowAccC";
            this.but_ShowAccC.Size = new System.Drawing.Size(130, 29);
            this.but_ShowAccC.TabIndex = 6;
            this.but_ShowAccC.Text = "管理分类账号  ->";
            this.but_ShowAccC.UseVisualStyleBackColor = true;
            this.but_ShowAccC.Click += new System.EventHandler(this.but_ShowAccC_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 169);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "— 账号管理";
            // 
            // lab_UserClass
            // 
            this.lab_UserClass.AutoSize = true;
            this.lab_UserClass.Location = new System.Drawing.Point(12, 39);
            this.lab_UserClass.Name = "lab_UserClass";
            this.lab_UserClass.Size = new System.Drawing.Size(73, 17);
            this.lab_UserClass.TabIndex = 4;
            this.lab_UserClass.Text = "— 账号分类";
            // 
            // but_ChangClass
            // 
            this.but_ChangClass.Location = new System.Drawing.Point(124, 58);
            this.but_ChangClass.Name = "but_ChangClass";
            this.but_ChangClass.Size = new System.Drawing.Size(24, 27);
            this.but_ChangClass.TabIndex = 3;
            this.but_ChangClass.Text = "+";
            this.but_ChangClass.UseVisualStyleBackColor = true;
            this.but_ChangClass.Click += new System.EventHandler(this.but_ChangClass_Click);
            // 
            // Cbox_UserClass
            // 
            this.Cbox_UserClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cbox_UserClass.FormattingEnabled = true;
            this.Cbox_UserClass.Items.AddRange(new object[] {
            "未分类"});
            this.Cbox_UserClass.Location = new System.Drawing.Point(12, 59);
            this.Cbox_UserClass.Name = "Cbox_UserClass";
            this.Cbox_UserClass.Size = new System.Drawing.Size(109, 25);
            this.Cbox_UserClass.TabIndex = 2;
            this.Cbox_UserClass.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(34, 386);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tlp
            // 
            this.tlp.AutoScroll = true;
            this.tlp.BackColor = System.Drawing.Color.Transparent;
            this.tlp.ColumnCount = 1;
            this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp.Location = new System.Drawing.Point(168, 6);
            this.tlp.Name = "tlp";
            this.tlp.RowCount = 5;
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp.Size = new System.Drawing.Size(620, 403);
            this.tlp.TabIndex = 0;
            // 
            // tabP_About
            // 
            this.tabP_About.Controls.Add(this.lab_MySaid);
            this.tabP_About.Controls.Add(this.GBox_Target);
            this.tabP_About.Location = new System.Drawing.Point(4, 26);
            this.tabP_About.Name = "tabP_About";
            this.tabP_About.Padding = new System.Windows.Forms.Padding(3);
            this.tabP_About.Size = new System.Drawing.Size(793, 415);
            this.tabP_About.TabIndex = 2;
            this.tabP_About.Text = "关于";
            this.tabP_About.UseVisualStyleBackColor = true;
            // 
            // lab_MySaid
            // 
            this.lab_MySaid.AutoSize = true;
            this.lab_MySaid.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_MySaid.Location = new System.Drawing.Point(23, 73);
            this.lab_MySaid.Name = "lab_MySaid";
            this.lab_MySaid.Size = new System.Drawing.Size(380, 240);
            this.lab_MySaid.TabIndex = 6;
            this.lab_MySaid.Text = "编程目的：方便快捷使用电脑，基于兴趣。\r\n\r\n登陆功能声明：本程序用到的api，其他产品的激活码等本人\r\n存放至云端只有注册登陆后才能获取使用相关功能。\r\n\r\n本" +
    "程序不会对用户计算机系统造成任何伤害。\r\n\r\n本程序完全免费。\r\n\r\n更新频率：看心情。\r\n\r\n编程语言：C Sharp -C#";
            this.lab_MySaid.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GBox_Target
            // 
            this.GBox_Target.Controls.Add(this.RText_target);
            this.GBox_Target.Location = new System.Drawing.Point(424, 6);
            this.GBox_Target.Name = "GBox_Target";
            this.GBox_Target.Size = new System.Drawing.Size(363, 403);
            this.GBox_Target.TabIndex = 5;
            this.GBox_Target.TabStop = false;
            this.GBox_Target.Text = "开发目标";
            // 
            // RText_target
            // 
            this.RText_target.BackColor = System.Drawing.Color.White;
            this.RText_target.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RText_target.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.RText_target.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.RText_target.Location = new System.Drawing.Point(6, 22);
            this.RText_target.Name = "RText_target";
            this.RText_target.ReadOnly = true;
            this.RText_target.Size = new System.Drawing.Size(351, 375);
            this.RText_target.TabIndex = 0;
            this.RText_target.TabStop = false;
            this.RText_target.Text = "1.注册登陆功能用于分析查询改善程序\n2.本地密码存储功能\n3.英雄联盟暂离自动接受对局\n4.快捷图片识别文本\n5.快捷翻译\n6.第三方软件强杀保活自启等";
            // 
            // NotifyIconBack
            // 
            this.NotifyIconBack.ContextMenuStrip = this.ContextMenuBackStrip;
            this.NotifyIconBack.Icon = ((System.Drawing.Icon)(resources.GetObject("NotifyIconBack.Icon")));
            this.NotifyIconBack.Text = "Computer Tools";
            this.NotifyIconBack.Visible = true;
            this.NotifyIconBack.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIconBack_MouseDoubleClick);
            // 
            // ContextMenuBackStrip
            // 
            this.ContextMenuBackStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.显示ShowtoolStripMenuItem,
            this.关于abouttoolStripMenuItem,
            this.退出ExitToolStripMenuItem});
            this.ContextMenuBackStrip.Name = "ContextMenuBackStrip";
            this.ContextMenuBackStrip.Size = new System.Drawing.Size(140, 70);
            // 
            // 显示ShowtoolStripMenuItem
            // 
            this.显示ShowtoolStripMenuItem.Name = "显示ShowtoolStripMenuItem";
            this.显示ShowtoolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.显示ShowtoolStripMenuItem.Text = "显示 Show";
            this.显示ShowtoolStripMenuItem.Click += new System.EventHandler(this.显示ShowtoolStripMenuItem_Click);
            // 
            // 关于abouttoolStripMenuItem
            // 
            this.关于abouttoolStripMenuItem.Name = "关于abouttoolStripMenuItem";
            this.关于abouttoolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.关于abouttoolStripMenuItem.Text = "关于 About";
            this.关于abouttoolStripMenuItem.Click += new System.EventHandler(this.关于abouttoolStripMenuItem_Click);
            // 
            // 退出ExitToolStripMenuItem
            // 
            this.退出ExitToolStripMenuItem.Name = "退出ExitToolStripMenuItem";
            this.退出ExitToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.退出ExitToolStripMenuItem.Text = "退出 Exit";
            this.退出ExitToolStripMenuItem.Click += new System.EventHandler(this.退出ExitToolStripMenuItem_Click);
            // 
            // lab_Sptil
            // 
            this.lab_Sptil.AutoSize = true;
            this.lab_Sptil.Location = new System.Drawing.Point(151, -29);
            this.lab_Sptil.Name = "lab_Sptil";
            this.lab_Sptil.Size = new System.Drawing.Size(11, 544);
            this.lab_Sptil.TabIndex = 7;
            this.lab_Sptil.Text = "|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n" +
    "|\r\n|\r\n|\r\n|\r\n|\r\n";
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(825, 482);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.STrip_Main);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form_Main";
            this.Text = "Computer Tools Main";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Main_FormClosing);
            this.Load += new System.EventHandler(this.Form_Main_Load);
            this.LocationChanged += new System.EventHandler(this.Form_Main_LocationChanged);
            this.STrip_Main.ResumeLayout(false);
            this.STrip_Main.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabP_Home.ResumeLayout(false);
            this.tabP_Home.PerformLayout();
            this.GBox_NewTip.ResumeLayout(false);
            this.GBox_NewTip.PerformLayout();
            this.tabP_UorP.ResumeLayout(false);
            this.tabP_UorP.PerformLayout();
            this.tabP_About.ResumeLayout(false);
            this.tabP_About.PerformLayout();
            this.GBox_Target.ResumeLayout(false);
            this.ContextMenuBackStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip STrip_Main;
        private System.Windows.Forms.ToolStripStatusLabel StaLab_Time;
        private System.Windows.Forms.Timer Timer_STrip;
        private System.Windows.Forms.ToolStripStatusLabel StaLab_Spilt1;
        private System.Windows.Forms.ToolStripStatusLabel StaLab_LoginState;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabP_Home;
        private System.Windows.Forms.TabPage tabP_UorP;
        private System.Windows.Forms.GroupBox GBox_NewTip;
        private System.Windows.Forms.Label lab_ProName;
        private System.Windows.Forms.Label lab_WelCome;
        private System.Windows.Forms.Label lab_User;
        private System.Windows.Forms.TabPage tabP_About;
        private System.Windows.Forms.GroupBox GBox_Target;
        private System.Windows.Forms.RichTextBox RText_target;
        private System.Windows.Forms.Label lab_MySaid;
        private System.Windows.Forms.Button But_ExitLogin;
        private System.Windows.Forms.Label lab_ComputerName;
        private System.Windows.Forms.Label lab_Ip;
        private System.Windows.Forms.Label lab_SizeMeo;
        private System.Windows.Forms.Label lab_MAC;
        public System.Windows.Forms.ToolStripStatusLabel StaLab_State;
        private System.Windows.Forms.TextBox Text_NewTip;
        private System.Windows.Forms.NotifyIcon NotifyIconBack;
        private System.Windows.Forms.ContextMenuStrip ContextMenuBackStrip;
        private System.Windows.Forms.Label lab_TimeDate;
        private System.Windows.Forms.ToolStripMenuItem 退出ExitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 显示ShowtoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关于abouttoolStripMenuItem;
        private System.Windows.Forms.Button but_SetPro;
        private System.Windows.Forms.Label lab_isAdmin;
        private System.Windows.Forms.TableLayoutPanel tlp;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox Cbox_UserClass;
        private System.Windows.Forms.Button but_ChangClass;
        private System.Windows.Forms.Label lab_UserClass;
        private System.Windows.Forms.Button but_ShowAccC;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lab_Sptil;
    }
}