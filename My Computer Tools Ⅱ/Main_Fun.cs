using My_Computer_Tools_Ⅱ.Properties;
using NetCommandLib;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
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

            Thread NetTread = new Thread(Net_init);
            NetTread.IsBackground = true;
            NetTread.Start();

        }

        private void Net_init()
        {
            JObject jo;
            //获取公告
            try
            {
                jo = Program.SendApiGet("http://8.130.100.111/", new Dictionary<string, string> {
                { "s", "Pro.GetNotice"},{"Ver", "T1"}});
                if (jo?["ret"].ToString() == "200" && jo?["data"]["Notice"] != null)
                {
                    string Notice = jo["data"]["Notice"].ToString();
                    this.Invoke(new Action(() =>
                    {
                        Text_NewTip.Text = Notice;
                    }));
                }
                else
                    throw new Exception("获取公告失败");
            }
            catch (Exception)
            {
                Text_NewTip.Invoke(new Action(() =>
                {
                    Text_NewTip.Text = "网络错误，或服务器更改，或服务器没了...";
                }));
            }

            //取公网ip后获取城市
            string ip = "";
            string City = "";
            if ((ip = GetPublicIp()) != "0")
            {

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
                }
                catch (Exception)
                {

                }
            }
            else
                this.Invoke(new Action(() =>
                {
                    lab_WaiIP.Text += "未获取到。";
                }));


            //如果没获取到城市信息 就用用户设置城市
            City = City == "" ? Settings.Default.City?.Split('|')[0] : City;

            if (City == "")
            {
                this.Invoke(new Action(() =>
                {
                    lab_HyperData.Text = $"暂时没有设置城市哦~\r\n设置后就能看到天气啦~";
                }));
                return;
            }
            Net_GetWeather(City);
        }

        /// <summary>
        /// 刷新更新天气信息
        /// </summary>
        /// <param name="City"></param>
        private void Net_GetWeather(string City)
        {
            //获取天气
            var ret1 = Program.SendApiGet("https://api.seniverse.com/v3/weather/now.json", new Dictionary<string, string> {
                            { "key","S4kppU2W4Du0Nuw3J"},{"location",City},{"language","zh-Hans" },{ "unit","c"}
                        });
            if (ret1?["results"] != null)
            {
                this.Invoke(new Action(() =>
                {
                    lab_HyperData.Text = $"{City}：{ret1["results"][0]["now"]["text"]} ，温度：{ret1["results"][0]["now"]["temperature"]} ℃\r\n" +
                                                    $"更新时间：{ret1["results"][0]["last_update"]}";
                    PBox_Data.Image = Program.GetResourceImage($"weather_{ret1["results"][0]["now"]["code"]}");
                }));
            }
            else
            {
                this.Invoke(new Action(() =>
                {
                    PBox_Data.Image = Program.GetResourceImage("weather_99");
                    lab_HyperData.Text = $"{City} \r\n暂未查询到天气数据";
                }));

            }
        }

        /// <summary>
        /// 取公共ip
        /// </summary>
        /// <returns></returns>
        public string GetPublicIp()
        {
            string tempip = "";
            string ipv = Program.SendApiGet_Str("https://pv.sohu.com/cityjson?ie=utf-8", new Dictionary<string, string> { });
            if (ipv == null)
                return "0";
            Regex r = new Regex("((25[0-5]|2[0-4]\\d|1\\d\\d|[1-9]\\d|\\d)\\.){3}(25[0-5]|2[0-4]\\d|1\\d\\d|[1-9]\\d|[1-9])", RegexOptions.None);
            Match mc = r.Match(ipv);
            tempip = mc.Groups[0].Value;

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
        /// 如果是开机自启，就最小化主界面
        /// </summary>
        private void VoidFirstOpen()
        {
            Thread.Sleep(1000);
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
        /// 刷新托盘菜单账号数据
        /// </summary>
        private void ShowAccinCMBS()
        {

        }

        /// <summary>
        /// 隐藏删除托盘菜单账号本本项
        /// </summary>
        private void HideAccinCMBS()
        {

        }

    }
}
