using My_Computer_Tools_Ⅱ.Properties;
using NetCommandLib;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
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
            this.lab_MAC.Click += new EventHandler(LabelClick);
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
            string MacAddress = computerInfo.MacAddress;
            lab_MAC.Text += MacAddress;
            lab_isAdmin.Text += Commands.IsAdministrator();
            //日月处理显示
            lab_TimeDate.Text += DateTime.Now.ToString("D") + " " + DateTime.Now.ToString("dddd");

            //RText_target设置
            StartStrikeout(1);

            //初始化动态弹窗提示效果
            WinCommand.ChangeTips("运行提示", "初始化中...", 1);

            //设置布局栏双缓存
            //this.tlp.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(this.tlp, true, null);

            //设置账号存储分类为首
            Cbox_UserClass.SelectedIndex = 0;

            //托盘账号菜单初始化
            AccCMBSinit();

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
                Explain = "用于系统控件的刷新，数据的显示更新等，\r\n目的：刷新时间显示\t循环间隔：1S",
                Name = "系统定时器",
                Fundata = "123123"
            });

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
            try
            {
                throw new Exception("获取公告失败");
            }
            catch (Exception)
            {
                Text_NewTip.Invoke(new Action(() =>
                {
                    Text_NewTip.Text = "暂时没有公告T-T";
                }));
            }
            UpFormData(CWdata, $"公告获取为：{Text_NewTip.Text}");

            //取公网ip后获取城市
            string ip = "";
            string City = "";
            if ((ip = GetPublicIp()) != "0")
            {
                UpFormData(CWdata, $"公网IP获取为：{ip}");
                if (this.IsHandleCreated)
                    this.Invoke(new Action(() =>
                    {
                        lab_WaiIP.Text += ip;
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
                    lab_WaiIP.Text += "未获取到。";
                }));

            //如果没获取到城市信息 就用用户设置城市
            City = City == "" ? Settings.Default.City?.Split('|')[0] : City;

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
                    }));
            }
            UpFormData(CWdata, $"天气获取为：{lab_HyperData.Text}");
        }

        /// <summary>
        /// 取公共ip
        /// </summary>
        /// <returns></returns>
        public string GetPublicIp()
        {
            string[] url = { "https://api.ipify.org/?format=json", "https://pv.sohu.com/cityjson?ie=utf-8" };
            string ipv = "";
            foreach (var item in url)
            {
                ipv = Program.SendApiGet_Str(item, new Dictionary<string, string> { });
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
            ClsXMLoperate clsXM = Program.CreaterXMLHelper();

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
                Control_Show control_Show = new Control_Show(Cbox_UserClass.SelectedItem.ToString(), str, vs[0], vs[1]);
                tlp.RowCount++;
                tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, control_Show.Size.Height + 5));
                tlp.Controls.Add(control_Show, 0, 0);
            }

            AccCMBSinit();
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
        private void AccCMBSinit()
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
                    string[] vs1 = userAcc.Split('^');
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
        /// 更新线程窗体显示
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
                }));
            Program.logmain.Write(data);
        }

        /// <summary>
        /// 更新线程窗体显示 重载
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
                id = Convert.ToInt32(ListBox_Name.SelectedItem.ToString().Split(' ')[0]);

            //控制任务显示指定输出
            if (this.IsHandleCreated && data.id == id)
                this.Invoke(new Action(() =>
                {
                    TBox_Tip.AppendText($"{DateTime.Now} id[{data.id}][{data.name}] ：{data.text}\r\n");
                }));
            Program.logmain.Write(data);
        }

        /// <summary>
        /// 刷新任务池UI界面
        /// </summary>
        private void RefreshTaskUI()
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
        }

        /// <summary>
        /// 刷新显示任务信息
        /// </summary>
        private void RefreshTaskini()
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
        }

        /// <summary>
        /// 任务：主窗口控件循环
        /// </summary>
        /// <param name="obj"></param>
        private void Form_While(object obj)
        {
            ThreadData data = (ThreadData)obj;
            ThreadFormData CWdata = new ThreadFormData()
            {
                name = data.Name,
                id = data.ID,
                grade = data.Grade,
            };
            if (this.IsHandleCreated)
                this.Invoke(new Action(() =>
                {
                    StaLab_Time.Text = "Time：" + DateTime.Now.ToString();
                }));
            //UpFormData(CWdata, $"设置StaLab_Time.Text为{DateTime.Now}");
        }

        /// <summary>
        /// 任务：执行一次上传
        /// </summary>
        private void UpLoad(object Files)
        {
            ThreadData data = (ThreadData)Files;
            ThreadFormData CWdata = new ThreadFormData()
            {
                name = data.Name,
                id = data.ID,
                grade = data.Grade,
            };

            int i = 0;
            List<string> FileList = (List<string>)data.Fundata;

            OSS_QiNiuSDK oSS_QiNiu = new OSS_QiNiuSDK(Settings.Default.qiniuBucket, Settings.Default.qiniuAK, Settings.Default.qiniuSK, Settings.Default.qiniuZoneID, Settings.Default.qiniuDomain);
            foreach (var item in FileList)
            {
                UpFormData(CWdata, $"上传进度：{i + 1}/{FileList.Count}，开始上传：{item.Replace("|", "")}");
                string ret = oSS_QiNiu.UpLoadFile(item);
                if (ret != "1")
                    UpFormData(CWdata, $"[第{i + 1}个] 文件路径[{item.Replace("|", "")}] 上传失败：{ret}", Log_Type.Warning);
                else
                    UpFormData(CWdata, $"[第{i + 1}个] 文件路径[{item.Replace("|", "")}] 上传完毕");
                i++;
            }
        }
    }
}