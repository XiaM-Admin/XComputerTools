using My_Computer_Tools_Ⅱ.Properties;
using NetCommandLib;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Forms;

namespace My_Computer_Tools_Ⅱ
{
    public partial class Form_Main : Form
    {
        /// <summary>
        /// 控件初始化
        /// </summary>
        private void Control_Init()
        {
            //标签可以单击复制 绑定函数
            this.lab_isAdmin.Click += new EventHandler(LabelClick);
            this.lab_ComputerName.Click += new EventHandler(LabelClick);
            this.lab_Ip.Click += new EventHandler(LabelClick);
            this.lab_WaiIP.Click += new EventHandler(LabelClick);
            this.lab_SizeMeo.Click += new EventHandler(LabelClick);
            this.lab_HyperData.Click += new EventHandler(LabelClick);

            //恢复LBox_FolderList数据 qnwatcherstr恢复 分隔符"^"
            LBox_FolderList.Items.Clear();
            string[] qnwatcherstr = Settings.Default.qnwatcherstr.Split('^');
            foreach (string str in qnwatcherstr)
                if (str != "")
                    LBox_FolderList.Items.Add(str);

            //计算机信息处理
            GetComputerInfo computerInfo = new GetComputerInfo();
            string IpAddress = computerInfo.IpAddress;
            lab_Ip.Text += IpAddress;
            string ComputerName = computerInfo.ComputerName;
            lab_ComputerName.Text += ComputerName;
            string SizeOfMemery = (Convert.ToDouble(computerInfo.SizeOfMemery) / 1024.0 / 1024.0).ToString("0.00");
            lab_SizeMeo.Text += SizeOfMemery + " G";
            lab_isAdmin.Text += Commands.IsAdministrator();
            //日月处理显示
            lab_TimeDate.Text += DateTime.Now.ToString("D") + " " + DateTime.Now.ToString("dddd");

            //TODO列表划线变绿设置
            StartStrikeout(1);
            StartStrikeout(2);
            StartStrikeout(3);

            //初始化动态弹窗提示效果
            WinCommand.ChangeTips("运行提示", "初始化中...", 1);

            //设置账号存储分类为首
            Cbox_UserClass.SelectedIndex = 0;
            CBox_ThreadGrade.SelectedIndex = 0;

            //托盘账号菜单初始化
            AccCMBSinit(null);

            //账号到托盘菜单
            if (Settings.Default.ShowAccinCMBS)
                ShowAccinCMBS();//显示账号到托盘菜单
            else
                HideAccinCMBS();//不显示账号
        }

        /// <summary>
        /// 初始化状态栏
        /// </summary>
        private void STrip_Main_init()
        {
            StaLab_Time.Text = "Time：" + DateTime.Now.ToString();
        }

        /// <summary>
        /// 程序部分功能初始化
        /// </summary>
        private void Command_Init()
        {
            //状态栏定时器开始
            Program._Main.CreateThread(new CreateThread()
            {
                Grade = Thread_Grade.system,
                Fun = new Fun_delegate(Form_While),
                RunMode = Thread_RunMode.thread,
                ModePar = "1000",
                Explain = "用于系统控件的刷新，数据的显示更新等，\r\n目的：刷新时间显示\t循环间隔：1秒",
                Name = "系统定时器",
                Fundata = "123123"
            });

            //网络初始化 获取ip 天气
            Program._Main.CreateThread(new CreateThread()
            {
                Grade = Thread_Grade.system,
                Fun = new Fun_delegate(Net_init),
                RunMode = Thread_RunMode.OnlyOne,
                ModePar = "100",
                Explain = "暂时用于初始化天气信息、公网ip。",
                Name = "网络初始化",
                Fundata = "123123"
            });

            //如果是开机自启，就最小化主界面
            if (Program.FirstRunArg)
            {
                Program._Main.CreateThread(new CreateThread()
                {
                    Grade = Thread_Grade.system,
                    Fun = new Fun_delegate(VoidFirstOpen),
                    RunMode = Thread_RunMode.OnlyOne,
                    ModePar = "1500",
                    Explain = "检测是否为开机自启的程序，\r\n如果是则最小化到托盘菜单。",
                    Name = "系统最小化",
                    Fundata = "123123"
                });
            }

            //添加定时刷新日期的定时任务
            Program._Main.CreateThread(new CreateThread()
            {
                Grade = Thread_Grade.system,
                Fun = new Fun_delegate(UpDateTime),
                RunMode = Thread_RunMode.timer,
                ModePar = "00:00:03",
                Explain = "每天 00:00:03 刷新主界面的日期",
                Name = "日期刷新",
                Fundata = "123123"
            });

            //自动开启文件夹同步
            if (Settings.Default.qnStartFolderW)
            {
                But_qnStart.Text = "停止监控";
                CheckQnFileUpLoadTask(true);
            }

            //检测开启自动刷新天气信息
            if (Settings.Default.WeatherNumber != 0)
            {
                //添加定时更新天气任务
                Program._Main.CreateThread(new CreateThread()
                {
                    Grade = Thread_Grade.system,
                    Fun = new Fun_delegate(UpDataWeather),
                    RunMode = Thread_RunMode.thread,
                    ModePar = (Settings.Default.WeatherNumber * 1000 * 60).ToString(),
                    Explain = $"实时循环更新天气信息\r\n循环间隔：{Settings.Default.WeatherNumber}分钟",
                    Name = "实时天气",
                    Fundata = "123123"
                });
            }
        }

        /// <summary>
        /// 网络基础获取
        /// </summary>
        /// <param name="obj"></param>
        private void Net_init(object obj)
        {
            ThreadData data = (ThreadData)obj;
            ThreadFormData CWdata = new ThreadFormData()
            {
                name = data.Name,
                id = data.ID,
                grade = data.Grade,
            };

            JObject jo;
            //暂时没有公告可以读取的
            string NewGG = "暂时没有想要公告的···\r\n如果想找点东西，去关于看看吧！";
            try
            {
                jo = Program.SendApiGet(Settings.Default.UpDataAPI, null); // 版本信息github地址
                if (jo is null) jo = Program.SendApiGet("https://api.x-tools.top/xcomputer/ver", null); // 备用版本信息
               
                if (jo?["body"] != null)
                    NewGG = jo["body"].ToString();
                if (jo?["name"] != null)
                {
                    string NewVer = jo["name"].ToString().Replace("XComputerTools ", "");
                    if (NewVer != Settings.Default.ProgramVer)
                    {
                        NewGG += $"程序版本与云端不匹配！请检查是否为最新版本！\r\n当前：{Settings.Default.ProgramVer}\r\n最新：{NewVer}";
                        Program.UpdataURL = jo["assets"][0]["browser_download_url"].ToString();
                        Text_NewTip.Invoke(new Action(() =>
                        {
                            but_DownloadDATA.Visible = true;
                        }));
                    }
                }
            }
            catch (Exception)
            {
                NewGG = "获取版本公告失败！请重试~";
            }
            Text_NewTip.Invoke(new Action(() =>
            {
                Text_NewTip.Text = NewGG;
            }));

            UpFormData(CWdata, $"公告获取为：{Text_NewTip.Text}");

            if (!Program.IsReadlyNET)
            {
                UpFormData(CWdata, $"程序当前无法连接至互联网！无法获取任何网络数据！");
                return;
            }

            //取公网ip后获取城市
            string ip = "";
            string City = "";
            if ((ip = GetPublicIp()) != "0")
            {
                UpFormData(CWdata, $"公网IP获取为：{ip}");
                if (this.IsHandleCreated)
                    this.Invoke(new Action(() =>
                    {
                        lab_WaiIP.Text = "外网IP：" + ip;
                    }));

                //获取ip城市
                try
                {
                    jo = Program.SendApiGet("https://ip.taobao.com/outGetIpInfo/", new Dictionary<string, string> {
                    { "ip", ip},{"accessKey", "alibaba-inc"}});
                    if (jo?["data"]?["city"] != null)
                        City = jo["data"]["city"].ToString();
                    UpFormData(CWdata, $"IP城市获取为：{City}");
                }
                catch (Exception)
                {
                    UpFormData(CWdata, $"IP城市获取失败", Log_Type.Warning);
                }
            }
            else
                if (this.IsHandleCreated)
                this.Invoke(new Action(() =>
                {
                    lab_WaiIP.Text = "外网IP：未获取到";
                }));

            //如果没获取到城市信息 就用用户设置城市
            Program.city = City = City == "" ? Settings.Default.City?.Split('|')[0] : City;

            if (City == "")
            {
                if (this.IsHandleCreated)
                    this.Invoke(new Action(() =>
                    {
                        lab_HyperData.Text = $"暂时没有设置城市哦~\r\n设置后就能看到天气啦~";
                    }));
                return;
            }

            var ret1 = Program.SendApiGet("https://api.seniverse.com/v3/weather/now.json", new Dictionary<string, string> {
                            { "key","S4kppU2W4Du0Nuw3J"},{"location",City},{"language","zh-Hans" },{ "unit","c"}
                        });
            if (ret1?["results"] != null)
            {
                if (this.IsHandleCreated)
                    this.Invoke(new Action(() =>
                    {
                        lab_HyperData.Text = $"{City}：{ret1["results"][0]["now"]["text"]} ，温度：{ret1["results"][0]["now"]["temperature"]} ℃\r\n" +
                                                        $"更新时间：{ret1["results"][0]["last_update"]}";
                        NotifyIconBack.Text = "Computer Tools\r\n" + lab_HyperData.Text;
                        PBox_Data.Image = Program.GetResourceImage($"weather_{ret1["results"][0]["now"]["code"]}");
                    }));
            }
            else
            {
                if (this.IsHandleCreated)
                    this.Invoke(new Action(() =>
                    {
                        PBox_Data.Image = Program.GetResourceImage("weather_99");
                        lab_HyperData.Text = $"{City} \r\n暂未查询到天气数据";
                        NotifyIconBack.Text = "Computer Tools";
                    }));

                //天气刷新重试
                if (Program._Main.Get_ThreadDatainList("天气重新获取") == null)
                    Program._Main.CreateThread(new CreateThread()
                    {
                        Grade = Thread_Grade.system,
                        Fun = new Fun_delegate(UpDataWeather),
                        RunMode = Thread_RunMode.OnlyOne,
                        ModePar = (1 * 60 * 1000).ToString(),
                        Explain = "尝试重新获取ip天气数据",
                        Name = "天气重新获取",
                        Fundata = "123123"
                    });
            }
            UpFormData(CWdata, $"天气获取为：{lab_HyperData.Text}");
        }

        /// <summary>
        /// 更新天气数据
        /// </summary>
        /// <param name="obj"></param>
        private void UpDataWeather(object obj)
        {
            ThreadData data = (ThreadData)obj;
            ThreadFormData CWdata = new ThreadFormData()
            {
                name = data.Name,
                id = data.ID,
                grade = data.Grade,
            };

            if (!Program.IsReadlyNET)
            {
                UpFormData(CWdata, $"程序当前无法连接至互联网！无法获取任何网络数据！");
                return;
            }

            var ret1 = Program.SendApiGet("https://api.seniverse.com/v3/weather/now.json", new Dictionary<string, string> {
                            { "key","S4kppU2W4Du0Nuw3J"},{"location",Program.city},{"language","zh-Hans" },{ "unit","c"}
                        });
            if (ret1?["results"] != null)
            {
                if (this.IsHandleCreated)
                    this.Invoke(new Action(() =>
                    {
                        lab_HyperData.Text = $"{Program.city}：{ret1["results"][0]["now"]["text"]} ，温度：{ret1["results"][0]["now"]["temperature"]} ℃\r\n" +
                                                        $"更新时间：{ret1["results"][0]["last_update"]}";
                        NotifyIconBack.Text = "Computer Tools\r\n" + lab_HyperData.Text;
                        PBox_Data.Image = Program.GetResourceImage($"weather_{ret1["results"][0]["now"]["code"]}");
                    }));
            }
            else
            {
                if (this.IsHandleCreated)
                    this.Invoke(new Action(() =>
                    {
                        PBox_Data.Image = Program.GetResourceImage("weather_99");
                        lab_HyperData.Text = $"{Program.city} \r\n暂未查询到天气数据";
                        NotifyIconBack.Text = "Computer Tools";
                    }));

                //天气刷新重试
                if (Program._Main.Get_ThreadDatainList("天气重新获取") == null)
                    Program._Main.CreateThread(new CreateThread()
                    {
                        Grade = Thread_Grade.system,
                        Fun = new Fun_delegate(UpDataWeather),
                        RunMode = Thread_RunMode.OnlyOne,
                        ModePar = (1 * 60 * 1000).ToString(),
                        Explain = "尝试重新获取ip天气数据",
                        Name = "天气重新获取",
                        Fundata = "123123"
                    });
            }
            UpFormData(CWdata, $"天气获取为：{lab_HyperData.Text}");
        }

        /// <summary>
        /// 取公共ip
        /// </summary>
        /// <returns></returns>
        public string GetPublicIp()
        {
            string[] url = { "https://pv.sohu.com/cityjson?ie=utf-8", "https://api.nn.ci/ip", "https://api.ipify.org/?format=json", "http://ip.cip.cc/" };
            string ipv = "";
            foreach (var item in url)
            {
                ipv = Program.SendApiGet_Str(item, null);
                if (ipv != null)
                    break;
            }
            if (ipv == null)
                return "0";

            Regex r = new Regex("((25[0-5]|2[0-4]\\d|1\\d\\d|[1-9]\\d|\\d)\\.){3}(25[0-5]|2[0-4]\\d|1\\d\\d|[1-9]\\d|[1-9])", RegexOptions.None);
            Match mc = r.Match(ipv);
            string tempip = mc.Groups[0].Value;

            //判断字符串是否符合ip格式
            Regex r1 = new Regex("^((2[0-4]\\d|25[0-5]|[01]?\\d\\d?)\\.){3}(2[0-4]\\d|25[0-5]|[01]?\\d\\d?)$");
            if (r1.IsMatch(tempip))
                return tempip;
            else
                return "0";
        }

        /// <summary>
        /// 给超文本编辑框加删除线并设置绿色
        /// </summary>
        /// <param name="i">行</param>
        private void StartStrikeout(int i)
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
        /// 如果是开机自启，就最小化主界面
        /// </summary>
        private void VoidFirstOpen(object obj)
        {
            if (this.IsHandleCreated)
                this.Invoke(new Action(() =>
                    Application.Exit()
                    ));//调用一次退出 让其最小化
        }

        /// <summary>
        /// 更新！
        /// </summary>
        private void UpdateUserACC()
        {
            if (this.IsHandleCreated)//判断控件句柄是否存在
                this.Invoke(new Action(() =>
                {
                    ClsXMLoperate clsXM = Program.CreaterXMLHelper();
                    //先删除tlp的所有控件
                    tlp.Controls.Clear();
                    tlp.RowCount = 0;
                    //获取账号 添加
                    var Vsstr = clsXM.GetNodeVsStr("UserInfo/" + Cbox_UserClass.SelectedItem.ToString());//取所有账号名
                    foreach (string str in Vsstr)
                    {
                        string userAcc = clsXM.GetNodeContent("UserInfo/" + Cbox_UserClass.SelectedItem.ToString() + "/" + str);//取账号数据
                        string[] vs = userAcc.Split(' ');
                        if (vs.Length == 0)
                            continue;
                        Control_Show control_Show = new Control_Show(Cbox_UserClass.SelectedItem.ToString(), str, vs[0], vs[1], new Fun_delegate(AccCMBSinit));

                        tlp.RowCount++;
                        tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, control_Show.Size.Height + 5));
                        tlp.Controls.Add(control_Show, 0, 0);
                    }
                }));

            AccCMBSinit(null);
        }

        /// <summary>
        /// 更新All，相当于初始化
        /// </summary>
        private void UpdateUserAcc_All()
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

            UpdateUserACC();
        }

        /// <summary>
        /// 初始化\更新账号托盘
        /// </summary>
        private void AccCMBSinit(object obj)
        {
            try
            {
                this.Invoke(new Action(() =>
                {
                    AcctoolStripMenuItem.DropDownItems.Clear();
                    //遍历所有分类class
                    ClsXMLoperate clsXM = Program.CreaterXMLHelper();
                    string ret = clsXM.GetNodeContent("UserInfo/Class");
                    string[] vs = ret.Split('|');
                    foreach (var item in vs)
                    {
                        //添加分类
                        ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(item);
                        AcctoolStripMenuItem.DropDownItems.Add(toolStripMenuItem);
                        //遍历分类下的账号
                        List<string> Vsstr = clsXM.GetNodeVsStr("UserInfo/" + item);//取所有账号名
                        foreach (string str in Vsstr)
                        {
                            string userAcc = clsXM.GetNodeContent("UserInfo/" + item + "/" + str);//取账号数据
                            string[] vs1 = userAcc.Split(' ');
                            if (vs1.Length == 0)
                                continue;
                            ToolStripMenuItem toolStripMenuItem1 = new ToolStripMenuItem(str);
                            toolStripMenuItem.DropDownItems.Add(toolStripMenuItem1);

                            ToolStripMenuItem toolStripMenuItem_user = new ToolStripMenuItem(vs1[0]);
                            toolStripMenuItem_user.Click += new EventHandler(ToolStripMenuItemCilck);
                            toolStripMenuItem1.DropDownItems.Add(toolStripMenuItem_user);

                            ToolStripMenuItem toolStripMenuItem_pwd = new ToolStripMenuItem(vs1[1]);
                            toolStripMenuItem_pwd.Click += new EventHandler(ToolStripMenuItemCilck);
                            toolStripMenuItem1.DropDownItems.Add(toolStripMenuItem_pwd);
                        }
                    }
                }));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>
        /// 单击托盘菜单复制到剪贴板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemCilck(object sender, EventArgs e)
        {
            string str = ((ToolStripMenuItem)sender).Text;
            Clipboard.SetText(str);
            WinCommand.ChangeTips($"{str}已复制");
            NotifyIconBack.ShowBalloonTip(2, "本本提醒", "您点击的账号信息已经复制到剪贴板中！", ToolTipIcon.Info);
        }

        /// <summary>
        /// 刷新托盘菜单账号数据
        /// </summary>
        private void ShowAccinCMBS()
        {
            AcctoolStripMenuItem.Visible = true;
        }

        /// <summary>
        /// 隐藏删除托盘菜单账号本本项
        /// </summary>
        private void HideAccinCMBS()
        {
            AcctoolStripMenuItem.Visible = false;
        }

        /// <summary>
        /// 统计账号分类数量 并且显示
        /// </summary>
        private void Accstatistical()
        {
            int classcount = Cbox_UserClass.Items.Count;
            int Acccount = 0;
            ClsXMLoperate clsXM = Program.CreaterXMLHelper();
            string ret = clsXM.GetNodeContent("UserInfo/Class");
            string[] vs = ret.Split('|');
            foreach (var item in vs)
            {
                //添加分类
                ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(item);
                AcctoolStripMenuItem.DropDownItems.Add(toolStripMenuItem);
                //遍历分类下的账号
                List<string> Vsstr = clsXM.GetNodeVsStr("UserInfo/" + item);//取所有账号名
                foreach (string str in Vsstr)
                {
                    Acccount++;
                }
            }
            label_AccStical.Text = $"当前分类有{classcount}个，\r\n一共保管了{Acccount}个账号！";
        }

        /// <summary>
        /// 将ListBox的数据放到对应的settings中
        /// </summary>
        /// <param name="listBox"></param>
        private void ListBoxSave(ListBox listBox)
        {
            string savestr = "";
            foreach (string item in listBox.Items)
            {
                if (item != "")
                    savestr += item + "^";
            }
            //删除字符串最后一个^
            if (savestr.Contains("^"))
                savestr = savestr.Remove(savestr.Length - 1, 1);

            switch (listBox.Name)
            {
                case "LBox_FolderList":
                    Settings.Default.qnwatcherstr = savestr;
                    break;

                default:
                    return;
            }
            Settings.Default.Save();
        }

        /// <summary>
        /// 删除ListBox选中项
        /// </summary>
        private void ListBoxDelItem(ListBox listBox)
        {
            //删除ListBox选中项
            if (listBox.SelectedIndex != -1)
            {
                listBox.Items.RemoveAt(listBox.SelectedIndex);
                ListBoxSave(listBox);
            }
        }

        /// <summary>
        /// 更新线程窗体显示 给线程类使用的日志记录
        /// </summary>
        /// <param name="x">ThreadFormData</param>
        private void UpFormData(object x)
        {
            ThreadFormData data = (ThreadFormData)x;
            //控制任务显示指定输出

            if (this.IsHandleCreated)
                this.Invoke(new Action(() =>
                {
                    TBox_Tip.AppendText($"{DateTime.Now} id[{data.id}][{data.name}] ：{data.text}\r\n");
                    TBox_Tip.Update();
                }));

            Program.logmain.Write(data);
        }

        /// <summary>
        /// 更新线程窗体显示 重载 任务调用的线程
        /// </summary>
        /// <param name="x">ThreadFormData</param>
        /// <param name="text"></param>
        /// <param name="type"></param>
        private void UpFormData(object x, string text, Log_Type type = Log_Type.Info)
        {
            ThreadFormData data = (ThreadFormData)x;
            data.type = type;
            data.text = text;
            int id = -1;
            if (ListBox_Name.IsHandleCreated)
                this.Invoke(new Action(() =>
                {
                    if (ListBox_Name.SelectedIndex > 0)
                        id = Convert.ToInt32(ListBox_Name.SelectedItem.ToString().Split(' ')[0]);
                }));

            //控制任务显示指定输出
            if (this.IsHandleCreated && data.id == id)
                this.Invoke(new Action(() =>
                {
                    TBox_Tip.AppendText($"{DateTime.Now} id[{data.id}][{data.name}] ：{data.text}\r\n");
                    TBox_Tip.Update();
                }));
            Program.logmain.Write(data);
        }

        /// <summary>
        /// 刷新任务池UI界面
        /// </summary>
        private void RefreshTaskUI(object obj)
        {
            this.Invoke(new Action(() =>
            {
                ListBox_Name.Items.Clear();
                List<string> ListTaskName = new List<string>();

                if (CBox_ThreadGrade.Text == "系统")
                    ListTaskName = Program._Main.Get_ListTaskNames(Thread_Grade.system);
                else if (CBox_ThreadGrade.Text == "用户")
                    ListTaskName = Program._Main.Get_ListTaskNames(Thread_Grade.user);

                foreach (string TaskName in ListTaskName)
                    ListBox_Name.Items.Add(TaskName);

                if (ListBox_Name.Items.Count > 0)
                    ListBox_Name.SelectedIndex = 0;
            }));
        }

        /// <summary>
        /// 刷新显示任务信息
        /// </summary>
        private void RefreshTaskini()
        {
            this.Invoke(new Action(() =>
            {
                if (ListBox_Name.SelectedIndex == -1)
                    return;
                TBox_Tip.Clear();//清空日志
                int id = Convert.ToInt32(ListBox_Name.SelectedItem.ToString().Split(' ')[0]);
                ThreadData data = Program._Main.Get_ThreadDatainList(id);
                Tbox_TaskName.Text = data.Name;
                Tbox_TaskID.Text = data.ID.ToString();
                switch (data.RunMode)
                {
                    case Thread_RunMode.timer:
                        Tbox_TaskMode.Text = "定时执行模式";
                        break;

                    case Thread_RunMode.thread:
                        Tbox_TaskMode.Text = "循环执行模式";
                        break;

                    case Thread_RunMode.OnlyOne:
                        Tbox_TaskMode.Text = "延迟执行模式";
                        break;

                    default:
                        break;
                }
                switch (data.Grade)
                {
                    case Thread_Grade.user:
                        Tbox_TaskGrade.Text = "用户级";
                        break;

                    case Thread_Grade.system:
                        Tbox_TaskGrade.Text = "系统级";
                        break;

                    default:
                        break;
                }

                TBox_TaskModePar.Text = data.ModePar;
                TBox_TaskExplain.Text = data.Explain;
                //日志窗口读取Log文件显示
                ThreadFormData Logdata = new ThreadFormData()
                {
                    name = data.Name,
                    id = data.ID,
                    grade = data.Grade,
                };
                List<string> list = Program.logmain.GetPathLog(Logdata);
                foreach (var item in list)
                {
                    TBox_Tip.Text += item + "\r\n";
                }
                TBox_Tip.SelectionStart = TBox_Tip.TextLength - 1;
            }));
        }

        /// <summary>
        /// 七牛云获取需要同步的列表
        /// </summary>
        /// <returns></returns>
        private List<string> Get_qnFileList()
        {
            List<string> files = new List<string>();
            string[] strs = Settings.Default.qnwatcherstr.Split('^');
            if (strs.Length == 1 && strs[0] == "")
            {
                MessageBox.Show("请先设置需要上传同步的本地文件夹\n后再试！", "错误！", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return files;
            }
            foreach (var item in strs)
            {
                if (Commands.File_IsDir(item))
                {
                    files.AddRange(Commands.File_GetList(item));
                }
                else if (!string.IsNullOrWhiteSpace(item))
                    files.Add(item);
            }
            //处理文件开始分割位置
            for (int i = 0; i < files.Count; i++)
            {
                foreach (var item in strs)
                {
                    if (Commands.File_IsDir(item))
                    {
                        string[] vs = item.Split('\\');
                        files[i] = files[i].Replace(vs[vs.Length - 1], "|" + vs[vs.Length - 1]);
                        //再进行分割判断 '|'
                        string[] vs2 = files[i].Split('|');
                        if (vs2.Length > 2)
                        {
                            //重新拼接
                            files[i] = vs2[0] + "|" + vs2[1];
                            for (int j = 2; j < vs2.Length; j++)
                            {
                                files[i] += vs2[j];
                            }
                        }
                    }
                }
            }
            if (files.Count == 0)
                NotifyIconBack.ShowBalloonTip(3000, "上传提示", "本次预上传的文件数为0，请检查目录是否存在！", ToolTipIcon.Info);
            return files;
        }

        /// <summary>
        /// 检查七牛云同步任务的状态
        /// 如果存在则关闭，不存在则开启
        /// </summary>
        private void CheckQnFileUpLoadTask(bool isStart)
        {
            if (isStart)
            {
                //如果任务不存在则开启
                if (Program._Main.Get_ThreadDatainList("七牛云同步") == null)
                {
                    Program._Main.CreateThread(new CreateThread()
                    {
                        Grade = Thread_Grade.user,
                        Fun = new Fun_delegate(UpLoad),
                        RunMode = Thread_RunMode.thread,
                        ModePar = (Settings.Default.CheckFileNumber * 60 * 1000).ToString(),
                        Explain = $"用于间隔检查用户文件夹与云端的数据，存在差异自动上传文件。\r\n用户设置：{Settings.Default.CheckFileNumber}分钟检查一次文件夹并且同步。",
                        Name = "七牛云同步",
                        Fundata = Get_qnFileList()
                    });
                }
            }
            else
            {
                ThreadData data;
                if ((data = Program._Main.Get_ThreadDatainList("七牛云同步")) != null)
                {
                    Program._Main.Set_ThreadExit(data.ID);
                }
            }
        }

        private static int checkcount = 0;

        /// <summary>
        /// 任务：主窗口控件循环
        /// </summary>
        /// <param name="obj"></param>
        private void Form_While(object obj)
        {
            if (checkcount == 30)
            {
                Program.IsReadlyNET = Program.CheckNet();
                checkcount = 0;
            }

            ThreadData data = (ThreadData)obj;
            ThreadFormData CWdata = new ThreadFormData()
            {
                name = data.Name,
                id = data.ID,
                grade = data.Grade,
            };
            try
            {
                if (this.IsHandleCreated)
                    this.Invoke(new Action(() =>
                    {
                        StaLab_Time.Text = "Time：" + DateTime.Now.ToString();
                    }));
            }
            catch (Exception)
            {
                Console.WriteLine("循环窗口已经结束或不存在时，此线程还没结束，会发生异常，无视即可。");
            }

            Program.DateTime = DateTime.Now;
            checkcount++;
        }

        /// <summary>
        /// 定时任务：每天凌晨刷新日期
        /// </summary>
        /// <param name="obj"></param>
        private void UpDateTime(object obj)
        {
            ThreadData data = (ThreadData)obj;
            ThreadFormData CWdata = new ThreadFormData()
            {
                name = data.Name,
                id = data.ID,
                grade = data.Grade,
            };
            this.Invoke(new Action(() =>
            {
                lab_TimeDate.Text += Program.DateTime.ToString("D") + " " + Program.DateTime.ToString("dddd");
            }));
            UpFormData(CWdata, $"主程序日期已刷新：{Program.DateTime}");
        }

        /// <summary>
        /// 任务：手动上传 重试
        /// </summary>
        /// <param name="FileList"></param>
        private void UpLoad(List<string> FileList)
        {
            OSS_QiNiuSDK oSS_QiNiu = new OSS_QiNiuSDK(Settings.Default.qiniuBucket, Settings.Default.qiniuAK, Settings.Default.qiniuSK, Settings.Default.qiniuZoneID, Settings.Default.qiniuDomain);
            if (!oSS_QiNiu.is_init)
            {
                MessageBox.Show("您没有填齐相关参数！\n请填完并且上传测试成功后，再次上传！", "错误！", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Fun_delegate fun = Program._ProgressBar.Show(this);
            ProgressData data = new ProgressData()
            {
                ProgressMaxValue = FileList.Count,
                ProgressNowValue = 0,
                FormName = "文件同步",
                FormPGTip = "",
                FormText = "正在同步文件，请稍等"
            };
            int i = 0;
            int TrueCount = 0;
            int FalseCount = 0;
            int BreakCount = 0;
            List<string> FailureTryList = new List<string>();//失败重试队列

            fun(data);
            foreach (var item in FileList)
            {
                data.FormText = $"进度：{i + 1}/{FileList.Count}";
                data.FormPGTip = $"上传进度：{i + 1}/{FileList.Count}，{item.Replace("|", "")}";
                fun(data);
                string ret = oSS_QiNiu.UpLoadFile(item);
                if (ret == "1")
                    TrueCount++;
                else if (ret == "文件对比一致，无须上传")
                    BreakCount++;
                else
                {
                    FailureTryList.Add(item);
                    FalseCount++;
                }
                i++;
                data.ProgressNowValue = i;
                fun(data);
            }
            data.FormText = "上传完毕";
            data.FormPGTip = $"成功{TrueCount}个 跳过{BreakCount}个 失败{FalseCount}个";
            fun(data);

            i = 0;
            int trycount = 0;
            List<string> TryOverList = new List<string>();
            while ((FailureTryList.Count - TryOverList.Count) > 0 && trycount < Settings.Default.FailureTryNumber)
            {
                TrueCount = 0;
                FalseCount = 0;
                BreakCount = 0;
                data.ProgressMaxValue = FailureTryList.Count - TryOverList.Count;
                data.ProgressNowValue = 0;
                foreach (var item in FailureTryList)
                {
                    data.FormText = $"重试{trycount + 1} 进度：{FailureTryList.Count - TryOverList.Count}";
                    data.FormPGTip = $"上传进度：{FailureTryList.Count - TryOverList.Count}，{item.Replace("|", "")}";
                    fun(data);
                    if (!TryOverList.Contains(item))
                    {
                        string ret = oSS_QiNiu.UpLoadFile(item);
                        if (ret == "1")
                        {
                            if (!TryOverList.Contains(item)) TryOverList.Add(item);
                            TrueCount++;
                        }
                        else if (ret == "文件对比一致，无须上传")
                        {
                            if (!TryOverList.Contains(item)) TryOverList.Add(item);
                            BreakCount++;
                        }
                        else
                            FalseCount++;
                        i++;
                    }
                    data.ProgressNowValue = i;
                }
                trycount++;
            }
            if (FailureTryList.Count != 0)
            {
                data.FormText = "重试上传完毕";
                data.FormPGTip = $"成功{TrueCount}个 跳过{BreakCount}个 失败{FalseCount}个";
                fun(data);
            }
        }

        /// <summary>
        /// 任务：执行一次上传
        /// </summary>
        private void UpLoad(object obj)
        {
            ThreadData data = (ThreadData)obj;
            ThreadFormData CWdata = new ThreadFormData()
            {
                name = data.Name,
                id = data.ID,
                grade = data.Grade,
            };

            if (!Program.IsReadlyNET)
            {
                UpFormData(CWdata, $"程序当前无法连接至互联网！无法获取任何网络数据！无法进行文件夹同步！");
                return;
            }

            int i = 0;

            int TrueCount = 0;
            int FalseCount = 0;
            int BreakCount = 0;
            List<string> FileList = (List<string>)data.Fundata;

            List<string> FailureTryList = new List<string>();//失败重试队列
            List<string> TryOverList = new List<string>(); //失败但上传成功的队列

            if (!Settings.Default.qnShowMesg)
                NotifyIconBack.ShowBalloonTip(3000, "上传提示", $"文件夹开始同步！共{FileList.Count}个文件待上传！", ToolTipIcon.Info);

            UpFormData(CWdata, $"循环时间到咯！开始检测上传文件啦！");

            OSS_QiNiuSDK oSS_QiNiu = new OSS_QiNiuSDK(Settings.Default.qiniuBucket, Settings.Default.qiniuAK, Settings.Default.qiniuSK, Settings.Default.qiniuZoneID, Settings.Default.qiniuDomain);
            foreach (var item in FileList)
            {
                UpFormData(CWdata, $"上传进度：{i + 1}/{FileList.Count}，开始上传：{item.Replace("|", "")}");
                string ret = oSS_QiNiu.UpLoadFile(item);
                if (ret == "1")
                {
                    UpFormData(CWdata, $"[第{i + 1}个] 文件路径[{item.Replace("|", "")}] 上传完毕。");
                    TrueCount++;
                }
                else if (ret == "文件对比一致，无须上传")
                {
                    UpFormData(CWdata, $"[第{i + 1}个] 文件路径[{item.Replace("|", "")}] 对比一致，跳过上传。");
                    BreakCount++;
                }
                else
                {
                    UpFormData(CWdata, $"[第{i + 1}个] 文件路径[{item.Replace("|", "")}] 上传失败：网络连接表示为{ret}网络可能不稳定！", Log_Type.Warning);
                    FalseCount++;
                    FailureTryList.Add(item);
                }
                i++;
            }

            i = 0;
            int trycount = 0;
            int tryTrueCount = 0;
            while ((FailureTryList.Count - TryOverList.Count) > 0 && trycount < Settings.Default.FailureTryNumber)
            {
                foreach (var item in FailureTryList)
                {
                    if (!TryOverList.Contains(item))
                    {
                        UpFormData(CWdata, $"重试{trycount + 1}进度 {FailureTryList.Count - TryOverList.Count}：开始上传：{item.Replace("|", "")}");
                        string ret = oSS_QiNiu.UpLoadFile(item);
                        if (ret == "1")
                        {
                            UpFormData(CWdata, $"{FailureTryList.Count - TryOverList.Count}个：上传成功");
                            if (!TryOverList.Contains(item)) TryOverList.Add(item);
                            tryTrueCount++;
                        }
                        else if (ret == "文件对比一致，无须上传")
                        {
                            UpFormData(CWdata, $"{FailureTryList.Count - TryOverList.Count}个：文件一致");
                            if (!TryOverList.Contains(item)) TryOverList.Add(item);
                            tryTrueCount++;
                        }
                        else
                            UpFormData(CWdata, $"{FailureTryList.Count - TryOverList.Count}个：上传失败");
                        i++;
                    }
                }
                trycount++;
                UpFormData(CWdata, $"重试第{trycount + 1}轮结束！还剩{FailureTryList.Count - TryOverList.Count}个！");
            }

            UpFormData(CWdata, $"文件夹上传完毕\r\n上传完毕{TrueCount}个 跳过上传{BreakCount}个 上传失败{FalseCount}个\r\n" +
                    $"重试{trycount}轮 成功了{tryTrueCount}个");

            if (!Settings.Default.qnShowMesg)
                NotifyIconBack.ShowBalloonTip(3000, "上传提示", $"文件夹上传完毕\r\n上传完毕{TrueCount}个 跳过上传{BreakCount}个 上传失败{FalseCount}个\r\n" +
                    $"重试了{trycount}轮 成功了{tryTrueCount}个", ToolTipIcon.Info);
        }

        /// <summary>
        /// 打开设置 减少代码冗余
        /// </summary>
        private void OpenSetting()
        {
            Form_SetPm form = new Form_SetPm();
            form.ShowDialog();
            if (form.UpDateWeather)
            {
                Program._Main.CreateThread(new CreateThread()
                {
                    Grade = Thread_Grade.system,
                    Fun = new Fun_delegate(Net_init),
                    RunMode = Thread_RunMode.OnlyOne,
                    ModePar = "100",
                    Explain = "暂时用于初始化天气信息、公网ip。",
                    Name = "网络刷新",
                    Fundata = "123123"
                });
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
        /// 重启当前程序
        /// </summary>
        public void ResRunThis()
        {
            Application.Exit();
            Application.Exit();
            Process ps = new Process();
            ps.StartInfo.FileName = Application.ExecutablePath;
            ps.Start();
        }

        /// <summary>
        /// 任务：关机
        /// </summary>
        private void shoutdown(object obj)
        {
            ThreadData data = (ThreadData)obj;
            ThreadFormData CWdata = new ThreadFormData()
            {
                name = data.Name,
                id = data.ID,
                grade = data.Grade,
            };
            UpFormData(CWdata, $"shoutdown自动关机开始执行...");

            if ((bool)data.Fundata )
                if(Settings.Default.email != null)
                {
                    string Text = $"<p>当前时间：{DateTime.Now:yyyy年MM月dd HH时mm分ss秒}<br>您的电脑开始执行关机操作！<p>";
                    string title = "X-Tools关机提醒";
                    Commands.SendEmail(Settings.Default.email, title, Text, true);
                }else
                    UpFormData(CWdata, $"您勾选了邮件提示，但是没有设置邮箱！所以我们无法发送邮件提醒你~请去设置中填写邮箱！",Log_Type.Warning);

            Process p = new Process();//实例化一个独立进程
            p.StartInfo.FileName = "cmd.exe";//进程打开的文件为Cmd
            p.StartInfo.UseShellExecute = false;//是否启动系统外壳选否
            p.StartInfo.RedirectStandardInput = true;//这是是否从StandardInput输入
            p.StartInfo.CreateNoWindow = true;//这里是启动程序是否显示窗体
            p.Start();//启动
            p.StandardInput.WriteLine("shutdown -s -t 0");//运行关机命令shutdown (-s)是关机 (-t)是延迟的时间 这里用秒计算 10就是10秒后关机

            p.StandardInput.WriteLine("exit");//退出cmd
            UpFormData(CWdata, $"既然关机了，那我也不能留下了... 我先走一步了..");
            Application.Exit();
        }
        /// <summary>
        /// 任务：执行cmd指令
        /// </summary>
        /// <param name="obj"></param>
        private void runcmd(object obj)
        {
            ThreadData data = (ThreadData)obj;
            ThreadFormData CWdata = new ThreadFormData()
            {
                name = data.Name,
                id = data.ID,
                grade = data.Grade,
            };
            UpFormData(CWdata, $"runcmd函数开始执行...");
            List<string> cmdlist = (List<string>)data.Fundata;
            Process p = new Process();//实例化一个独立进程
            p.StartInfo.FileName = "cmd.exe";//进程打开的文件为Cmd
            p.StartInfo.UseShellExecute = false;//是否启动系统外壳选否
            p.StartInfo.RedirectStandardInput = true;//这是是否从StandardInput输入
            p.StartInfo.RedirectStandardOutput = true;//StandardOutput输出
            p.StartInfo.CreateNoWindow = true;//这里是否显示程序
            p.StartInfo.StandardOutputEncoding = Encoding.UTF8;//设置编码
            p.Start();//启动
            foreach (var cmd in cmdlist)
            {
                p.StandardInput.WriteLine(cmd);//运行cmd命令
                UpFormData(CWdata, $"执行 cmd命令：{cmd}");
            }
            p.StandardInput.WriteLine("exit");//退出cmd
            string txt = "";
            while (!p.StandardOutput.EndOfStream)
                txt = p.StandardOutput.ReadToEnd();//输出
            p.Close();
            UpFormData(CWdata, $"cmd执行记录：\r\n{txt}");
        }
    }
}