using NetCommandLib;
using System;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace My_Computer_Tools_Ⅱ
{
    public partial class Form_Main : Form
    {

        public Form_Main()
        {
            InitializeComponent();
            WinCommand._Main = this;

            showBox_Loc = WinCommand.showBox = new ShowBox("提示标题", "提示内容", this);
            Program.WinCommand = WinCommand;//同步到Program里
        }
        private WindowsCommands WinCommand = new WindowsCommands();//窗口控件互动类
        private ShowBox showBox_Loc;//窗口控件互动类

        private int UserClassindex = 0;//用户账号分类索引

        //线程变量
        private Thread thread_FirstOpen = null;


        private void Form_Main_Load(object sender, EventArgs e)
        {
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
            StartStrikeout(RText_target, 2);

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
            Thread.Sleep(0);
        }
        
        /// <summary>
        /// 程序部分功能初始化
        /// </summary>
        private void Command_Init()
        {
            Timer_STrip.Enabled = true;//状态栏定时器开始


        }

        /// <summary>
        /// 给超文本编辑框加删除线并设置绿色
        /// </summary>
        /// <param name="RTextBox"></param>
        /// <param name="i">行</param>
        private void StartStrikeout(RichTextBox RTextBox, int i)
        {
            //给超文本编辑框加删除线

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
            //日月处理显示
            lab_TimeDate.Text = "日期：" + DateTime.Now.ToString("D") + " " + DateTime.Now.ToString("dddd");
        }
        /// <summary>
        /// 重新登陆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void Form_Main_LocationChanged(object sender, EventArgs e)
        {
            if (Program.backWindows_State == true)
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

        /// <summary>
        /// 分开处理 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
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
                Cbox_UserClass.SelectedIndex = UserClassindex;
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
                UpdateUserACC();
            }
        }

        //更新显示账号显示区域！
        private void Cbox_UserClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUserACC();
            UserClassindex = Cbox_UserClass.SelectedIndex;
        }

        /// <summary>
        /// 更新！
        /// </summary>
        private void UpdateUserACC()
        {
            string path = Application.StartupPath + "\\" + Program.xmlname;
            ClsXMLoperate clsXM = new ClsXMLoperate(path);

            //先删除tlp的所有控件
            tlp.Controls.Clear();
            //获取账号 添加
            var Vsstr = clsXM.GetNodeVsStr("UserInfo/" + Cbox_UserClass.SelectedItem.ToString());//取所有账号名
            foreach (string str in Vsstr)
            {
                string userAcc = clsXM.GetNodeContent("UserInfo/" + Cbox_UserClass.SelectedItem.ToString() + "/" + str);//取账号数据
                string[] vs = userAcc.Split('^');
                if (vs.Length == 0)
                    continue;
                Control_Show control_Show = new Control_Show(str, vs[0], vs[1]);
                tlp.RowCount++;
                tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, control_Show.Size.Height + 5));
                tlp.Controls.Add(control_Show, 0, 0);
            }


        }
    }
}
