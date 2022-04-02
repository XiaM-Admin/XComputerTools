using RetNetInfor;
using System;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using settings = My_Computer_Tools_Ⅱ.Properties.Settings;

namespace My_Computer_Tools_Ⅱ
{
    public partial class Form_Main : Form
    {

        public Form_Main()
        {
            InitializeComponent();
            WinCommand._Main = this;

            showBox_Loc = WinCommand.showBox = new ShowBox("提示标题", "提示内容", this);
        }
        private WindowsCommands WinCommand = new WindowsCommands();//窗口控件互动类
        private ShowBox showBox_Loc;//窗口控件互动类

        private bool Login_status = false;//登陆状态
        private bool LastLogin_status = false;//上次登陆状态

        //线程变量
        private Thread thread_CheckLogin=null;
        private Thread thread_FirstOpen=null;


        private void Form_Main_Load(object sender, EventArgs e)
        {

            var ret = NetAPI.NetApiCommand.Net_CheckUser();//检测用户权限
            if (ret == "1")
                Login_status = true;
            thread_CheckLogin = new Thread(Thread_Net);
            thread_CheckLogin.IsBackground = true;
            thread_CheckLogin.Start();//开启循环查询验证
            LastLogin_status = !Login_status;

            STrip_Main_init();
            Command_Init();
            Control_Init();
            //处理完毕
            WinCommand.ChangeTips("初始化完毕");

            thread_FirstOpen = new Thread(VoidFirstOpen);
            thread_FirstOpen.IsBackground = true;
            thread_FirstOpen.Start();
        }

        /// <summary>
        /// 控件初始化
        /// </summary>
        private void Control_Init()
        {
            //计算机信息处理
            GetComputerInfo computerInfo = new GetComputerInfo();
            string IpAddress = computerInfo.IpAddress;
            lab_Ip.Text += IpAddress;
            string ComputerName = computerInfo.ComputerName;
            lab_ComputerName.Text += ComputerName;
            string SizeOfMemery = (Convert.ToDouble(computerInfo.SizeOfMemery) / 1024.0 / 1024.0).ToString("0.00");
            lab_SizeMeo.Text += SizeOfMemery + " G";
            string MacAddress = computerInfo.MacAddress;
            lab_MAC.Text += MacAddress;
            lab_isAdmin.Text += Commands.IsAdministrator();
            //日月处理显示
            lab_TimeDate.Text += DateTime.Now.ToString("D") + " " + DateTime.Now.ToString("dddd");
            //RText_target设置
            StartStrikeout(RText_target, 1);

            //初始化动态弹窗提示效果
            WinCommand.ChangeTips("运行提示", "初始化中...", 1);

            //设置布局栏双缓存
            this.tlp.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(this.tlp, true, null);

            //设置账号存储分类为首
            Cbox_UserClass.SelectedIndex = 0;
        }

        private void VoidFirstOpen()
        {
            Thread.Sleep(1000);
            if (Program.FirstRunArg)
                this.Invoke(new Action(() =>
                Application.Exit()
                ));//调用一次退出 让其最小化
        }

        /// <summary>
        /// 初始化状态栏
        /// </summary>
        private void STrip_Main_init()
        {
            StaLab_Time.Text = "Time：" + DateTime.Now.ToString();
            StaLab_LoginState.Text = !Login_status ? "未登录或失效，点此登陆" : "已登陆";
            if (StaLab_LoginState.Text == "未登录或失效，点此登陆")
                StaLab_LoginState.BackColor = Color.Red;
            else
                StaLab_LoginState.BackColor = Color.White;
            lab_User.Text = "用户User：" + (Login_status ? settings.Default.LoadName : "暂未登陆");
            Thread.Sleep(0);
        }
        /// <summary>
        /// 程序部分功能初始化
        /// </summary>
        private void Command_Init()
        {
            Timer_STrip.Enabled = true;//状态栏定时器开始
            Text_NewTip.Text = NetAPI.NetApiCommand.Net_GetGG();//获取公告
            if (Login_status)
                lab_User.Text = "用户User：" + (settings.Default.LoadName != "" ? settings.Default.LoadName : "暂未登陆");
            else
                lab_User.Text = "用户User：暂未登陆";
        }

        /// <summary>
        /// 给超文本编辑框加删除线并设置绿色
        /// </summary>
        /// <param name="RTextBox"></param>
        /// <param name="i">行</param>
        private void StartStrikeout(RichTextBox RTextBox, int i)
        {
            FontStyle fontStyle = FontStyle.Strikeout;
            i--;
            var txt = RText_target.Lines[i];
            if (i > 0)
            {
                int Startlen = 0;

                for (int x = 0; x < i; x++)
                {
                    string str = RText_target.Lines[x];
                    Startlen += str.Length;
                    Startlen++;
                }
                RText_target.SelectionStart = Startlen;

            }

            RText_target.SelectionLength = txt.Length;
            RText_target.SelectionFont = new Font(RText_target.SelectionFont, fontStyle);
            RText_target.SelectionColor = Color.FromName("Green");
        }

        /// <summary>
        /// 状态栏定时器事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_STrip_Tick(object sender, EventArgs e)
        {

            StaLab_Time.Text = "Time：" + DateTime.Now.ToString();
            if (LastLogin_status != Login_status)
            {
                LastLogin_status = Login_status;
                StaLab_LoginState.Text = !Login_status ? "未登录或失效，点此登陆" : "已登陆";
                if (StaLab_LoginState.Text == "未登录或失效，点此登陆")
                    StaLab_LoginState.BackColor = Color.Red;
                else
                    StaLab_LoginState.BackColor = Color.White;
                lab_User.Text = "用户User：" + (Login_status ? settings.Default.LoadName : "暂未登陆");

                //日月处理显示
                lab_TimeDate.Text = "日期：" + DateTime.Now.ToString("D") + " " + DateTime.Now.ToString("dddd");
            }
        }
        /// <summary>
        /// 重新登陆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StaLab_LoginState_Click(object sender, EventArgs e)
        {
            if (StaLab_LoginState.Text == "未登录或失效，点此登陆")
            {
                using (Form_Load form = new Form_Load())
                {
                    form.isReLogin = true;
                    form.ShowDialog();
                    Login_status = form.ReLogin_Ret;//恢复登陆状态
                    Command_Init();//重新获取网络配置
                }

            }
        }
        /// <summary>
        /// 用户状态检测 10min/s
        /// </summary>
        private void Thread_Net()
        {
            while (true)
            {
                Thread.Sleep(10 * 1000 * 60);
                var ret = NetAPI.NetApiCommand.Net_CheckUser();//检测用户权限
                if (ret == "1")
                    Login_status = true;
                else
                    Login_status = false;
            }

        }

        private void But_ExitLogin_Click(object sender, EventArgs e)
        {
            NetAPI.NetApiCommand.Net_Exit();
            Thread.Sleep(2000);
            var ret = NetAPI.NetApiCommand.Net_CheckUser();//检测用户权限
            if (ret == "1")
                Login_status = true;
            else
                Login_status = false;

        }

        /// <summary>
        /// 测试函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lab_ProName_Click(object sender, EventArgs e)
        {
            /*
            var ret = Commands.IsAdministrator();
            WinCommand.ChangeTips("测试", "管理员身份："+ret, 4);
            
            if (Program.FirstRunArg)
                WinCommand.ChangeTips("测试", "是自启动的程序", 4);
            else
                WinCommand.ChangeTips("测试", "非自启动的程序", 4);
            */
            WinCommand.ChangeTips("测试多行提示标题", "第一行\r\n第二行\r\n第三行\r\n第四行", 4);
        }

        private void Form_Main_LocationChanged(object sender, EventArgs e)
        {
            if (Program .backWindows_State==true)
                return;

            if (showBox_Loc.ShowNow == true)
                showBox_Loc.Location = new Point(this.Location.X + (showBox_Loc.Width / 2), this.Location.Y + 1);
        }

        /// <summary>
        /// 安全退出程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 退出ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotifyIconBack.Visible = false;
            Timer_STrip.Enabled = false;
            thread_CheckLogin = null;
            GC.Collect();
            Application.Exit();
            Application.Exit();
        }

        private void 显示ShowtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.backWindows_State)
            {
                this.Show();
                Program.backWindows_State = false;
            }

        }

        private void Form_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            if (!Program.backWindows_State)
            {
                if (showBox_Loc.ShowNow == true)
                    showBox_Loc.Hide();
                e.Cancel = true;
                this.Hide();
                Program.backWindows_State = true;
                WinCommand.ChangeTips("程序提示", "主程序已转移到后台运行！\r\n请检查托盘图标！", 4);
                return;
            }
            

        }

        private void 关于abouttoolStripMenuItem_Click(object sender, EventArgs e)
        {
            WinCommand.ChangeTips("提示", "关于没做呢~嗨害嗨！", 4);
        }

        private void NotifyIconBack_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (Program.backWindows_State)
            {
                this.Show();
                Program.backWindows_State = false;
            }
        }

        private void but_SetPro_Click(object sender, EventArgs e)
        {
            Form_SetPm form = new Form_SetPm();
            form.ShowDialog();
        }

        private int i = 0;
        /// <summary>
        /// 测试按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            /*
            TextBox tb = new TextBox();
            tb.Text = "Hello World"+ i++; 
            tlp.RowCount++;
            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute,tb.Size.Height+6));
            tlp.Controls.Add(tb,0, 0);*/
            //文件测试

            /*
             ClsXMLoperate xmlfile = new ClsXMLoperate(xmlfilepath--是你设定的文件路径);
            xmlfile.GetNodeContent("根节点/父节点点/子节点");
            更新：
             xmlDoc.UpdateXmlNode("根节点/父节点点/子节点","节点内容");
            添加时使用函数InsertSingleNode( 父节点，插入节点名称，内容）就可以了。
            更新和插入成功的话会返回true，否则是false*/


            Commands.CreatFile(Program.xmlname, true);
            string path = Application.StartupPath + "\\" + Program.xmlname;
            ClsXMLoperate clsXM = new ClsXMLoperate(path);

            /*测试
            clsXM.InsertSingleNode("UserInfo", "CF");
            clsXM.InsertSingleNode("UserInfo/CF", "CF1", "1234561^1234561");
            clsXM.InsertSingleNode("UserInfo/CF", "CF2", "1234562^1234562");
            clsXM.InsertSingleNode("UserInfo/CF", "CF3", "1234563^1234563");
            clsXM.InsertSingleNode("UserInfo/LOL", "LOL1", "1234561^1234561");
            clsXM.InsertSingleNode("UserInfo/LOL", "LOL2", "1234562^1234562");
            */

            var strs = clsXM.GetNodeVsStr("UserInfo/LOL");

            //var ret = clsXM.CheckNode("UserInfo/defualt");
            //var ret1 = clsXM.CheckNode("UserInfo/Class");

            // clsXM.InsertSingleNode("UserInfo", "defualt");
            //clsXM.InsertSingleNode("UserInfo/defualt", "绝地求生小号","123456|xslxsl");

            //clsXM.Delete("UserInfo/defualt", "UserInfo/defualt/绝地求生小号");
            //clsXM.Delete("UserInfo", "UserInfo/defualt");

            //clsXM.UpdateXmlNode("UserInfo/Class","A|B|C");
            //string ret = clsXM.GetNodeContent("UserInfo/Class");
        }

        /// <summary>
        /// 分开处理 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(tabControl1.SelectedIndex.ToString());
            if (tabControl1.SelectedIndex==1)
            {
                //刷新class的items！
                Commands.CreatFile(Program.xmlname, true);
                string path = Application.StartupPath + "\\" + Program.xmlname;
                ClsXMLoperate clsXM = new ClsXMLoperate(path);
                string ret = clsXM.GetNodeContent("UserInfo/Class");
                string[] vs = ret.Split('|');
                Cbox_UserClass.Items.Clear();
                foreach (var item in vs)
                    Cbox_UserClass.Items.Add(item);
                Cbox_UserClass.SelectedIndex = 0;
            }
        }
        /// <summary>
        /// 更改账号class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void but_ChangClass_Click(object sender, EventArgs e)
        {
            using (Form_ChangeClass Form = new Form_ChangeClass())
            {
                Form.ShowDialog();
                //刷新class的items！
                Commands.CreatFile(Program.xmlname, true);
                string path = Application.StartupPath + "\\" + Program.xmlname;
                ClsXMLoperate clsXM = new ClsXMLoperate(path);
                //存到文件中 Form.retstr
                clsXM.UpdateXmlNode("UserInfo/Class", Form.retstr);
                string[] vs = Form.retstr.Split('|');
                Cbox_UserClass.Items.Clear();
                foreach (var item in vs)
                    Cbox_UserClass.Items.Add(item);
                Cbox_UserClass.SelectedIndex = 0;
            }
        }

        private void but_ShowAccC_Click(object sender, EventArgs e)
        {
            using (Form_AccountControl accform = new Form_AccountControl(Cbox_UserClass.Text))
            {
                accform.ShowDialog();


            }
        }
    }
}
