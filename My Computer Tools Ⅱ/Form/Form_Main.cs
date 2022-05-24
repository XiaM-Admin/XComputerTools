using My_Computer_Tools_Ⅱ.Properties;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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
                    Cbox_UserClass.Items.Clear();
                    foreach (var item in vs)
                        Cbox_UserClass.Items.Add(item);
                    Cbox_UserClass.SelectedIndex = UserClassindex;
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
            }
        }

        /// <summary>
        /// 调用显示账号管理界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void But_ShowAccC_Click(object sender, EventArgs e)
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
                    string[] vs = userAcc.Split('^');
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
                CheckQnFileUpLoadTask(true);
                But_qnStart.Text = "停止监控";
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
                    if (!File.Exists("Temp.png"))
                    {
                        WinCommand.ChangeTips("上传失败", "剪贴板没有图片信息！多试几次吧~", 3);
                        return;
                    }
                }
                else
                    img.Save("Temp.png");

                OSS_QiNiuSDK oSS_QiNiu = new OSS_QiNiuSDK(Settings.Default.qiniuBucket, Settings.Default.qiniuAK, Settings.Default.qiniuSK, Settings.Default.qiniuZoneID, Settings.Default.qiniuDomain);
                string retstr = oSS_QiNiu.UpLoadFile_One(Application.StartupPath + "\\Temp.png", Settings.Default.qnImgPath);
                string[] vs = retstr.Split('|');
                if (vs[0] == "200")
                {
                    WinCommand.ChangeTips("上传完成", $"{vs[1]}已置剪贴板", 3);
                    Clipboard.SetText(vs[1]);
                }
                else
                    WinCommand.ChangeTips("上传失败", "剪贴板有图但是失败可能是网络问题！多试几次吧~", 3);
            }
            catch (Exception er)
            {
                MessageBox.Show($"错误信息：{er}", "剪贴板上传出错");
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
    }
}