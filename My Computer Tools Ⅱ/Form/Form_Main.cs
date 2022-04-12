using My_Computer_Tools_Ⅱ.Properties;
using System;
using System.Drawing;
using System.IO;
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

        private Thread thread_FirstOpen = null;//线程变量
        private void Form_Main_Load(object sender, EventArgs e)
        {
            STrip_Main_init();
            Command_Init();
            Control_Init();
            //处理完毕
            WinCommand.ChangeTips("初始化完毕");

            if (Program.FirstRunArg)//如果是开机自启，就最小化主界面
            {
                thread_FirstOpen = new Thread(VoidFirstOpen);
                thread_FirstOpen.IsBackground = true;
                thread_FirstOpen.Start();
            }

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
        /// 提示窗口跟随主界面移动
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

        /// <summary>
        /// 显示主界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 显示ShowtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.backWindows_State)
            {
                this.Show();
                Program.backWindows_State = false;
            }

        }

        /// <summary>
        /// 不关闭程序，最小化到托盘
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// 托盘图标被双击显示主界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NotifyIconBack_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (Program.backWindows_State)
            {
                this.Show();
                Program.backWindows_State = false;
            }
        }
        /// <summary>
        /// 调用设置界面\r\n在关闭时处理一些事情
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void but_SetPro_Click(object sender, EventArgs e)
        {
            Form_SetPm form = new Form_SetPm();
            form.ShowDialog();
            if (form.UpDateWeather)
            {
                Net_GetWeather(Settings.Default.City.Split('|')[0]);
                form.UpDateWeather = !form.UpDateWeather;
            }

            //检查ShowAccinCMBS的值
            if (Settings.Default.ShowAccinCMBS)
                ShowAccinCMBS();//显示账号到托盘菜单
            else
                HideAccinCMBS();//不显示账号

            //释放form
            form.Dispose();

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

                ClsXMLoperate clsXM = Program.CreaterXMLHelper();
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

                ClsXMLoperate clsXM = Program.CreaterXMLHelper();
                //存到文件中 Form.retstr
                clsXM.UpdateXmlNode("UserInfo/Class", Form.retstr);
                string[] vs = Form.retstr.Split('|');
                Cbox_UserClass.Items.Clear();
                foreach (var item in vs)
                    Cbox_UserClass.Items.Add(item);
                Cbox_UserClass.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 调用显示账号管理界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void but_ShowAccC_Click(object sender, EventArgs e)
        {
            using (Form_AccountControl accform = new Form_AccountControl(Cbox_UserClass.Text))
            {
                accform.ShowDialog();
                UpdateUserACC();
            }
        }

        /// <summary>
        /// 更新显示账号显示区域！
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cbox_UserClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUserACC();
            UserClassindex = Cbox_UserClass.SelectedIndex;
        }

        /// <summary>
        /// 导出账号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void but_exportAcc_Click(object sender, EventArgs e)
        {
            if (File.Exists("Account.xml"))
            {
                //导出文件
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "xml文件|*.xml";
                saveFileDialog.FileName = "Account.xml";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    File.Copy("Account.xml", saveFileDialog.FileName, true);
                    WinCommand.ChangeTips("提示", "导出成功！", 4);
                }

            }
            else
            {
                MessageBox.Show("没有账号文件可以导出，\r\n" +
                    "通常是程序出现了问题，或配置文件没有生成。\r\n" +
                    "请尝试重新启动程序。", "错误：");
                return;
            }
        }
        /// <summary>
        /// 导入账号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void but_ImportAcc_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("导入配置文件，会覆盖当前存在的配置文件\r\n" +
                "确定要继续导入文件吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "xml文件|*.xml";
                openFileDialog.FileName = "Account.xml";
                openFileDialog.Title = "请确保导入的文件名为：Account.xml，否则无法选择到！";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    File.Copy(openFileDialog.FileName, "Account.xml", true);
                    WinCommand.ChangeTips("提示", "导入成功！", 4);
                    UpdateUserAcc_All();
                }
                else
                {
                    WinCommand.ChangeTips("导入操作被取消");
                }
            }

        }

        /// <summary>
        /// 查找账号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void but_Find_Click(object sender, EventArgs e)
        {
            string FindStr = Tbox_Find.Text.Trim();
            if (FindStr == "")
            {
                UpdateUserAcc_All();
                return;
            }

            ClsXMLoperate clsXM = Program.CreaterXMLHelper();
            //先删除tlp的所有控件
            tlp.Controls.Clear();
            //阻塞窗口 禁止用户操作
            this.Enabled = false;
            WinCommand.ChangeTips("账号搜索", "开始搜索全部账号！", 3);
            //获取账号 添加 时间复杂度O(n^2) 
            foreach (var item in Cbox_UserClass.Items)
            {
                var Vsstr = clsXM.GetNodeVsStr("UserInfo/" + item.ToString());//取所有账号名
                foreach (string str in Vsstr)
                {
                    string userAcc = clsXM.GetNodeContent("UserInfo/" + item.ToString() + "/" + str);//取账号数据
                    string[] vs = userAcc.Split('^');
                    if (vs.Length == 0)
                        continue;
                    if (vs[0].Contains(FindStr) || vs[1].Contains(FindStr) || str.Contains(FindStr))
                    {
                        Control_Show control_Show = new Control_Show(item.ToString(), str, vs[0], vs[1]);
                        tlp.RowCount++;
                        tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, control_Show.Size.Height + 5));
                        tlp.Controls.Add(control_Show, 0, 0);
                    }
                }
            }
            WinCommand.ChangeTips("账号搜索", "账号搜索完毕！", 3);
            //解除窗口阻塞
            this.Enabled = true;

        }
        /// <summary>
        /// 查找账号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tbox_Find_KeyPress(object sender, KeyPressEventArgs e)
        {
            //按下回车
            if (e.KeyChar == (char)Keys.Enter)
                but_Find_Click(null, null);

        }

        private void LabelClick(object sender, EventArgs e)
        {
            //标签被单击复制到剪贴板
            if (((Label)sender).Name == "lab_HyperData")
            {
                Clipboard.SetText(((Label)sender).Text);
                WinCommand.ChangeTips($"{((Label)sender).Text}已复制");
                return;
            }
            string str = ((Label)sender).Text.Split('：')[1];
            Clipboard.SetText(str);
            WinCommand.ChangeTips($"{str}已复制");
        }
    }



}
