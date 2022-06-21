using My_Computer_Tools_Ⅱ.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
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

        private readonly WindowsCommands WinCommand = new WindowsCommands();//窗口控件互动类

        private readonly ShowBox showBox_Loc;//窗口控件互动类

        private int UserClassindex = 0;//用户账号分类索引

        private void Form_Main_Load(object sender, EventArgs e)
        {
            Program.IsReadlyNET = Program.CheckNet();

            Program._Main = new Thread_Main(new Fun_delegate(UpFormData), new Fun_delegate(RefreshTaskUI));
            Program._ProgressBar = new Form_ProgressBar("初始化", "正在初始化，请耐心等待。", 100, this);
            STrip_Main_init();
            Control_Init();
            Command_Init();

            //处理完毕
            WinCommand.ChangeTips("初始化完毕");
        }

        /// <summary>
        /// 状态栏定时器事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_STrip_Tick(object sender, EventArgs e)
        {
            StaLab_Time.Text = "Time：" + DateTime.Now.ToString();
            //日月处理显示 线程队列搞定后移动到线程中
            lab_TimeDate.Text = "日期：" + DateTime.Now.ToString("D") + " " + DateTime.Now.ToString("dddd");
            //日月处理显示 线程队列搞定后移动到线程中
            if (!lab_HyperData.Text.Contains("未查询到") || !lab_HyperData.Text.Contains("天气数据"))
                NotifyIconBack.Text = "XComputerTools\r\n" + lab_HyperData.Text;
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
                //激活窗口 最前显示一下
                this.TopMost = true;
                this.TopMost = false;
            }
            else
            {
                //激活窗口 最前显示一下
                this.TopMost = true;
                this.TopMost = false;
            }
        }

        /// <summary>
        /// 调用设置界面\r\n在关闭时处理一些事情
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void But_SetPro_Click(object sender, EventArgs e)
        {
            OpenSetting();
        }

        /// <summary>
        /// 分开处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 1:
                    //刷新class的items！
                    Commands.CreatFile(Program.xmlname, true);
                    ClsXMLoperate clsXM = Program.CreaterXMLHelper();
                    string ret = clsXM.GetNodeContent("UserInfo/Class");
                    string[] vs = ret.Split('|');
                    if (vs.Length != Cbox_UserClass.Items.Count)
                    {
                        Cbox_UserClass.Items.Clear();
                        foreach (var item in vs)
                            Cbox_UserClass.Items.Add(item);
                        Cbox_UserClass.SelectedIndex = UserClassindex;
                        Accstatistical();
                    }
                    break;

                case 3:
                    RefreshTaskUI(null);//刷新任务列表
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// 更改账号class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void But_ChangClass_Click(object sender, EventArgs e)
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
                Accstatistical();
            }
        }

        /// <summary>
        /// 调用显示账号管理界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void But_ShowAccC_Click(object sender, EventArgs e)
        {
            if (Cbox_UserClass.Text == "defualt")
            {
                MessageBox.Show("您当前选择的是默认分类，\r\n不可使用默认分类进行账号存储！\r\n请点击分类后面的'+'按钮，\r\n新增分类，或选择其它分类！", "错误：");
                return;
            }

            using (Form_AccountControl accform = new Form_AccountControl(Cbox_UserClass.Text))
            {
                accform.ShowDialog();

                Thread thread = new Thread(UpdateUserACC);
                thread.Start();
                Accstatistical();
                //UpdateUserACC();
            }
        }

        /// <summary>
        /// 更新显示账号显示区域！
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cbox_UserClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            Thread thread = new Thread(UpdateUserACC);
            thread.Start();
            UserClassindex = Cbox_UserClass.SelectedIndex;
        }

        /// <summary>
        /// 导出账号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void But_exportAcc_Click(object sender, EventArgs e)
        {
            if (File.Exists("Account.xml"))
            {
                //导出文件
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "xml文件|*.xml",
                    FileName = "Account.xml"
                };
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
        private void But_ImportAcc_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("导入配置文件，会覆盖当前存在的配置文件\r\n" +
                "确定要继续导入文件吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "xml文件|*.xml",
                    FileName = "Account.xml",
                    Title = "请确保导入的文件名为：Account.xml，否则无法选择到！"
                };
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
        private void But_Find_Click(object sender, EventArgs e)
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
                    string[] vs = userAcc.Split(' ');
                    if (vs.Length == 0)
                        continue;
                    if (vs[0].Contains(FindStr) || vs[1].Contains(FindStr) || str.Contains(FindStr))
                    {
                        Control_Show control_Show = new Control_Show(item.ToString(), str, vs[0], vs[1], new Fun_delegate(AccCMBSinit));
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
                But_Find_Click(null, null);
        }

        /// <summary>
        /// 标签单击复制 -事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 编辑框回车保存 -事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextEnterSave(object sender, KeyPressEventArgs e)
        {
            TextBox TBox = (TextBox)sender;//Tbox_qiniuAK
            if (e.KeyChar == (char)Keys.Enter)
            {
                switch (TBox.Name)
                {
                    case "Tbox_qiniuAK":
                        Settings.Default.qiniuAK = TBox.Text.Trim();
                        break;//七牛云AK

                    case "Tbox_qiniuSK":
                        Settings.Default.qiniuSK = TBox.Text.Trim();
                        break;//七牛云SK

                    case "Tbox_qiniuBucket":
                        Settings.Default.qiniuBucket = TBox.Text.Trim();
                        break;//七牛云存储地域

                    case "TBox_Domain":
                        Settings.Default.qiniuDomain = TBox_Domain.Text.Trim();
                        break;//七牛云域名

                    default:
                        break;
                }
                Settings.Default.Save();
                WinCommand.ChangeTips($"{TBox.Text}已保存");
            }
        }

        /// <summary>
        /// 下拉框改变存储
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectedIndexChangedSave(object sender, EventArgs e)
        {
            ComboBox box = (ComboBox)sender;
            switch (box.Name)
            {
                case "CBox_qiniuZoneID":
                    Settings.Default.qiniuZoneID = box.Text;
                    break;//七牛云存储地域
                default:
                    break;
            }
            Settings.Default.Save();
            WinCommand.ChangeTips($"{box.Text}已保存");
        }

        /// <summary>
        /// 上传测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void But_UpTest_Click(object sender, EventArgs e)
        {
            OSS_QiNiuSDK oSS_QiNiu = new OSS_QiNiuSDK(Settings.Default.qiniuBucket, Settings.Default.qiniuAK, Settings.Default.qiniuSK, Settings.Default.qiniuZoneID, Settings.Default.qiniuDomain);
            bool ret = oSS_QiNiu.UploadFileTest2();
            if (ret)
                MessageBox.Show("上传测试完毕，通信正常！", "提示");
            else
                MessageBox.Show("上传测试失败，请检查重填配置！", "提示");
        }

        /// <summary>
        /// 七牛云刷新七牛云存储列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void But_Subfolder_Click(object sender, EventArgs e)
        {
            listView_FileList.Items.Clear();
            OSS_QiNiuSDK oSS_QiNiu = new OSS_QiNiuSDK(Settings.Default.qiniuBucket, Settings.Default.qiniuAK, Settings.Default.qiniuSK, Settings.Default.qiniuZoneID, Settings.Default.qiniuDomain);
            oSS_QiNiu.GetListFiles(listView_FileList);
        }

        /// <summary>
        /// 软件打开自动开启监控七牛文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CBox_qnAutoStart_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.qnStartFolderW = CBox_qnAutoStart.Checked;
            Settings.Default.Save();
        }

        /// <summary>
        /// 选择监控文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void But_qnAddFolder_Click(object sender, EventArgs e)
        {
            MessageBox.Show("选择文件夹后，请勿再选择子文件夹,\r\n选择文件夹后，文件夹中所有文件,\r\n包括子文件夹,子文件,都会被记录上传！", "提示");
            //选择文件夹添加到LBox_FolderList中
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "选择文件夹后，请勿再选择子文件夹";
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                LBox_FolderList.Items.Add(fbd.SelectedPath);
            }
            ListBoxSave(LBox_FolderList);
        }

        /// <summary>
        /// 删除选中项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void But_qnDelItem_Click(object sender, EventArgs e)
        {
            ListBoxDelItem(LBox_FolderList);
        }

        /// <summary>
        /// 手动进行文件上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void But_UpLoadFile_Click(object sender, EventArgs e)
        {
            if (But_qnStart.Text == "停止监控")
            {
                MessageBox.Show("自动监控中···\r\n两种操作不可同时进行！", "ERROR：");
                return;
            }
            if (!Program.IsReadlyNET)
            {
                MessageBox.Show("程序当前无法连接至互联网！无法获取任何网络数据！\r\n请重试！");
                return;
            }
            UpLoad(Get_qnFileList());
        }

        /// <summary>
        /// 开启监控文件夹上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void But_qnStart_Click(object sender, EventArgs e)
        {
            But_qnStart.Enabled = false;
            if (But_qnStart.Text == "开启监控")
            {
                if (!Program.IsReadlyNET)
                    MessageBox.Show("程序当前无法连接至互联网！无法获取任何网络数据！\r\n请重试！");
                else
                {
                    CheckQnFileUpLoadTask(true);
                    But_qnStart.Text = "停止监控";
                }
            }
            else
            {
                CheckQnFileUpLoadTask(false);
                But_qnStart.Text = "开启监控";
            }
            //异步执行
            Task.Delay(1500).ContinueWith(t =>
            {
                Application.DoEvents();
                if (this.IsHandleCreated)
                    this.Invoke(new Action(() =>
                    {
                        But_qnStart.Enabled = true;
                    }));
            });
        }

        /// <summary>
        /// 任务系统分类被更改刷新任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CBox_ThreadGrade_SelectedValueChanged(object sender, EventArgs e)
        {
            RefreshTaskUI(null);//刷新任务列表
        }

        /// <summary>
        /// 任务名字被更改刷新任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBox_Name_SelectedValueChanged(object sender, EventArgs e)
        {
            RefreshTaskini();
        }

        /// <summary>
        /// 七牛云下载文件按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void But_DownloadFile_Click(object sender, EventArgs e)
        {
            if (backgroundWorker.IsBusy)
                return;

            if (listView_FileList.SelectedIndices?.Count == 0)
                return;
            string key = listView_FileList.SelectedItems[0].SubItems[1].Text + listView_FileList.SelectedItems[0].Text;
            OSS_QiNiuSDK oSS_QiNiu = new OSS_QiNiuSDK(Settings.Default.qiniuBucket, Settings.Default.qiniuAK, Settings.Default.qiniuSK, Settings.Default.qiniuZoneID, Settings.Default.qiniuDomain);
            string returl = oSS_QiNiu.GetKeyDownloadUrl(key);
            if (returl != "")
            {
                if (MessageBox.Show("使用此软件单线程下载速度较慢，目前仅适合小文件下载！\r\n请使用获取直链后使用心仪的下载器下载\r\n或浏览器打开此链接，是否使用浏览器进行下载？", "提示：", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string Url = "http://" + returl;
                    //在浏览器打开链接
                    System.Diagnostics.Process.Start(Url);
                    return;
                }
                //gBgwDownload = new BackgroundWorker();
                backgroundWorker = new System.ComponentModel.BackgroundWorker()
                {
                    WorkerReportsProgress = true,
                    WorkerSupportsCancellation = false,
                };

                WebPost.Downdata downdata = new WebPost.Downdata()
                {
                    Url = "http://" + returl,
                    path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + listView_FileList.SelectedItems[0].Text,
                    OpenProg = true,
                    backgroundWorker = backgroundWorker,
                };

                backgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;
                backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(WebPost.IThreadDownloadFile);
                backgroundWorker.RunWorkerCompleted += (o, ea) =>
                {
                    //如果任务异常结束 使用动态匿名对象 如果解析是不存在 则报错
                    dynamic res = ea.Result;
                    if ((bool)res?.retbool != true)
                    {
                        MessageBox.Show("下载失败！" + res?.msg, "发生了错误");
                        return;
                    }
                    else
                    {
                        MessageBox.Show("下载完成！", "提示：");
                        return;
                    }
                };

                backgroundWorker.RunWorkerAsync(downdata);
            }
            else
            {
                MessageBox.Show("下载失败！\n获取直链出错！", "ERROR：");
            }
        }

        /// <summary>
        /// 第一个参数 是进度条百分比 第二个是自定义参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void BackgroundWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            WinCommand.ChangeTips(e.UserState.ToString());
            ProgressBarinTool.Value = Convert.ToInt32(e.ProgressPercentage);
        }

        /// <summary>
        /// 七牛云获取直链
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void But_GetFileDownloadurl_Click(object sender, EventArgs e)
        {
            if (listView_FileList.SelectedIndices?.Count == 0)
                return;

            string key = listView_FileList.SelectedItems[0].SubItems[1].Text + listView_FileList.SelectedItems[0].Text;

            OSS_QiNiuSDK oSS_QiNiu = new OSS_QiNiuSDK(Settings.Default.qiniuBucket, Settings.Default.qiniuAK, Settings.Default.qiniuSK, Settings.Default.qiniuZoneID, Settings.Default.qiniuDomain);
            string returl = oSS_QiNiu.GetKeyDownloadUrl(key);
            Clipboard.SetDataObject(returl);
            MessageBox.Show("直链获取完毕：\r\n" +
                $"Key:{key}\r\n" +
                $"Url:{returl}\r\n\n" +
                "已复制到剪贴板中，如有异常请重试，或者提交bug。", "提示：");
        }

        /// <summary>
        /// 托盘菜单打开设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 设置toolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSetting();
        }

        /// <summary>
        /// 托盘菜单手动上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 手动上传ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            But_UpLoadFile_Click(null, null);
        }

        /// <summary>
        /// 剪贴板上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void but_qnclipboard_Click(object sender, EventArgs e)
        {
            try
            {
                Image img = Clipboard.GetImage();
                if (img == null)
                {
                    if (!File.Exists(@"C:\Temp.png"))
                    {
                        WinCommand.ChangeTips("上传失败", "剪贴板没有图片信息！\r\n多试几次吧！", 3);
                        return;
                    }
                }
                else
                    img.Save(@"C:\Temp.png");

                OSS_QiNiuSDK oSS_QiNiu = new OSS_QiNiuSDK(Settings.Default.qiniuBucket, Settings.Default.qiniuAK, Settings.Default.qiniuSK, Settings.Default.qiniuZoneID, Settings.Default.qiniuDomain);
                string retstr = oSS_QiNiu.UpLoadFile_One(@"C:\Temp.png", Settings.Default.qnImgPath);
                string[] vs = retstr.Split('|');
                if (vs[0] == "200")
                {
                    WinCommand.ChangeTips("上传完成", $"{vs[1]}\r\n已置剪贴板", 3);
                    Clipboard.SetText(vs[1]);
                }
                else
                {
                    //失败重新上传
                    int trynumber = 0;
                    while (trynumber < Settings.Default.FailureTryNumber)
                    {
                        Thread.Sleep(100);
                        retstr = oSS_QiNiu.UpLoadFile_One(@"C:\Temp.png", Settings.Default.qnImgPath);
                        vs = retstr.Split('|');
                        if (vs[0] == "200")
                        {
                            WinCommand.ChangeTips("上传完成", $"{vs[1]}\r\n已置剪贴板", 3);
                            Clipboard.SetText(vs[1]);
                            break;
                        }
                        else
                            trynumber++;
                    }
                    if (trynumber == Settings.Default.FailureTryNumber)
                        WinCommand.ChangeTips("上传失败", "剪贴板存在图片我，但是无法上传！\r\n1.多试几次\r\n2.点击Go按钮重启软件后再试", 3, new Fun_delegate_void(ResRunThis));
                }
            }
            catch (Exception er)
            {
                MessageBox.Show($"错误信息：{er}", "剪贴板上传出错！\r\n并且不能重试！");
            }
        }

        /// <summary>
        /// 调用剪贴板上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 剪贴板上传ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            but_qnclipboard_Click(null, null);
        }

        /// <summary>
        /// 关于里面的设置按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void but_Set_Click(object sender, EventArgs e)
        {
            OpenSetting();
        }

        /// <summary>
        /// 打开Log文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void But_OpenLogEx_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Application.StartupPath + "\\Log\\");
        }

        /// <summary>
        /// 清理Log文件夹，清理非当天的日志文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void But_ClearLog_Click(object sender, EventArgs e)
        {
            int ret = Program.logmain.ClearOver();
            MessageBox.Show($"清理完成！\r\n共清理{ret}个Log文件夹！", "处理完成");
        }

        /// <summary>
        /// 打开外链 七牛C#SDK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel_QNSDKLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://developer.qiniu.com/kodo/1237/csharp");
        }

        /// <summary>
        /// 打开外链 心知天气
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel_WeatherLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.seniverse.com/");
        }

        /// <summary>
        /// 打开外链 淘宝ip地址库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel_TaoBaoAPILink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://ip.taobao.com/instructions");
        }

        /// <summary>
        /// 打开外链 我的api文章
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel_WaiIpLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://blog.x-tools.top/index.php/archives/15/");
        }

        /// <summary>
        /// 打开外链 我的博客
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel_BLogLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://blog.x-tools.top");
        }

        private void but_DownloadDATA_Click(object sender, EventArgs e)
        {
            Process.Start(Program.UpdataURL);
        }

        /// <summary>
        /// 七牛云 图片show
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void But_ImgShow_Click(object sender, EventArgs e)
        {
            if (listView_FileList.Items.Count == 0)
            {
                MessageBox.Show("请点击按钮'刷新列表'后重试！", "错误！");
                return;
            }
            using (imageShow imageShowi = new imageShow(listView_FileList, Settings.Default.qiniuDomain))
            {
                imageShowi.ShowDialog();
            }
        }

        private void 图片ShowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //刷新列表
            But_Subfolder_Click(null, null);
            //打开Show
            using (imageShow imageShowi = new imageShow(listView_FileList, Settings.Default.qiniuDomain))
            {
                imageShowi.ShowDialog();
            }
        }

        /// <summary>
        /// 双击显示图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_FileList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listView_FileList.SelectedItems is null)
                return;
            if (!(listView_FileList.SelectedItems[0].SubItems[0].Text.Contains("png") || listView_FileList.SelectedItems[0].SubItems[0].Text.Contains("jpg") || listView_FileList.SelectedItems[0].SubItems[0].Text.Contains("bmp")))
                return;
            string url = $"http://{Settings.Default.qiniuDomain}/{listView_FileList.SelectedItems[0].SubItems[1].Text + listView_FileList.SelectedItems[0].SubItems[0].Text}";
            using (imageShow imageShowi = new imageShow(null, url))
            {
                imageShowi.ShowDialog();
            }
        }

        /// <summary>
        /// 按钮 关机任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void But_shoutdown_Click(object sender, EventArgs e)
        {

            int sec =Convert.ToInt32(numbermin.Value) * 60 + Convert.ToInt32(numbersec.Value);
            bool isemail = cbox_shoutdown.Checked;
            if(sec == 0)
            {
                MessageBox.Show("延时关机暂不支持0s的关机...\r\n您可以手动关闭机器！","错误！");
                return;
            }
            //加入到任务列表
            if (Program._Main.Get_ThreadDatainList("延时关机") == null)
            {
                Program._Main.CreateThread(new CreateThread()
                {
                    Grade = Thread_Grade.user,
                    Fun = new Fun_delegate(shoutdown),
                    RunMode = Thread_RunMode.OnlyOne,
                    ModePar = (sec * 1000).ToString(),
                    Explain = $"顾名思义，就是延时关机的意思哦...\r\n目的：延时关机\t延时间隔：{sec}秒",
                    Name = "延时关机",
                    Fundata = isemail
                });
                MessageBox.Show("任务添加完成！在'任务池'-'用户'项中即可查看！","提示");
            }
            else
                MessageBox.Show("已经存在一个延时关机任务，请去任务池结束后重试！","错误");
        }

        /// <summary>
        /// 菜单 调试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lab_ProName_Click(object sender, EventArgs e)
        {
            //其实是我测试函数的.. 不想删了
            MessageBox.Show("人生没有捷径，就像去二仙桥，就必须要走成华大道...","Boom");
        }

        private void Button_cmdStart_Click(object sender, EventArgs e)
        {
            int sec = Convert.ToInt32(numbe_cmdtimemin.Value) * 60 + Convert.ToInt32(numbe_cmdtimesecc.Value);
            if (sec == 0)
            {
                MessageBox.Show("暂不支持0s的设置...\r\n您可以手动执行cmd指令！", "错误！");
                return;
            }
            if (tbox_cmdtxt.Text.Trim() == "")
            {
                MessageBox.Show("请输入要执行的命令！", "错误！");
                return;
            }
            Settings.Default.Save();
            List<string> listcmd = new List<string>();
            foreach (string str in tbox_cmdtxt.Lines)
            {
                if (str.Trim() != "")
                    listcmd.Add(str);
            }
            Thread_RunMode runmode = radio_cmdThread.Checked ? Thread_RunMode.thread : Thread_RunMode.OnlyOne;

            //加入到任务列表
            if (Program._Main.Get_ThreadDatainList("CMD执行") == null)
            {
                Program._Main.CreateThread(new CreateThread()
                {
                    Grade = Thread_Grade.user,
                    Fun = new Fun_delegate(runcmd),
                    RunMode = runmode,
                    ModePar = (sec * 1000).ToString(),
                    Explain = $"后台自动执行cmd指令\r\n目的：定时执行cmd命令\t延时间隔：{sec}秒",
                    Name = "CMD执行",
                    Fundata = listcmd
                });
                MessageBox.Show("任务添加完成！在'任务池'-'用户'项中即可查看！", "CMD执行");
            }
            else
                MessageBox.Show("已经存在一个CMD执行任务，请去任务池结束后重试！", "错误");
        }
        /// <summary>
        /// 终止当前选中任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_StopTask_Click(object sender, EventArgs e)
        {
            if(CBox_ThreadGrade.Text=="系统")
            {
                MessageBox.Show("很抱歉！\r\n系统任务不能终止！", "错误！");
                return;
            }
            if (Program._Main.Get_ThreadDatainList(Tbox_TaskName.Text) != null)
            {
                //结束任务
                ThreadData data = Program._Main.Get_ThreadDatainList(Tbox_TaskName.Text);
                Program._Main.Set_ThreadExit(data.ID);
                //特殊处理下
                if(Tbox_TaskName.Text== "七牛云同步")
                    But_qnStart.Text = "开启监控";
            }
            MessageBox.Show("任务终止成功！\r\n有些任务需耐心等待一会才会生效！", "提示");
        }

        private void Button_OpenLog_Click(object sender, EventArgs e)
        {
            string Path = "";
            try
            {
                Thread_Grade grade = CBox_ThreadGrade.Text == "系统" ? Thread_Grade.system : Thread_Grade.user;
                Path = Program.logmain.GetTaskLog(grade, Convert.ToInt32(Tbox_TaskID.Text), Tbox_TaskName.Text);
                Process.Start(Path);
            }
            catch (Exception)
            {
                MessageBox.Show($"打开日志错误！可能文件不存在或未找到！\r\n路径：{Path}","错误");
            }

        }
    }
}