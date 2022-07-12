using Microsoft.Win32;
using NetCommandLib;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using settings = My_Computer_Tools_Ⅱ.Properties.Settings;

namespace My_Computer_Tools_Ⅱ
{
    public partial class Form_SetPm : Form
    {
        public Form_SetPm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 是否立刻刷新天气
        /// </summary>
        public bool UpDateWeather = false;

        private void Form_SetPm_Load(object sender, EventArgs e)
        {
            //PicBox_TipFileCheck
            ToolTip tip = new ToolTip();
            tip.ToolTipIcon = ToolTipIcon.Info;
            tip.ToolTipTitle = "注意：";
            tip.InitialDelay = 300;
            tip.ReshowDelay = 500;
            tip.ShowAlways = true;
            tip.SetToolTip(PicBox_TipFileCheck, "开启项功能后，你的每一次上传任务\r\n" +
                "都将会进行文件对比，将云存储的文件和当前文件对比，\r\n" +
                "如果文件不一致，才会进行上传操作。");
            tip.SetToolTip(Number_weather, "刷新更新天气的间隔，设置为0时不更新天气。");
            CBox_UpEnd.Text = settings.Default.qnUpEnd;

            //初始化配置
            if (settings.Default.City != "")
            {
                string[] vs = settings.Default.City.Split('|');
                try
                {
                    CBox_GeographyPos.Text = vs[1];
                    SetComonBoxItemCity SetBoxItem = new SetComonBoxItemCity(CBox_City, CBox_GeographyPos.Text);
                    CBox_City.Text = vs[0];
                }
                catch (Exception)
                {
                    settings.Default.City = "";
                }
            }
        }

        #region 开启自启

        /// <summary>
        /// 将本程序设为开启自启
        /// </summary>
        /// <param name="onOff">自启开关</param>
        /// <returns></returns>
        public static bool SetMeStart(bool onOff)
        {
            string appName = Process.GetCurrentProcess().MainModule.ModuleName;
            string appPath = Process.GetCurrentProcess().MainModule.FileName;
            bool isOk = SetAutoStart(onOff, appName, appPath + " -autorun");
            return isOk;
        }

        /// <summary>
        /// 将应用程序设为或不设为开机启动
        /// </summary>
        /// <param name="onOff">自启开关</param>
        /// <param name="appName">应用程序名</param>
        /// <param name="appPath">应用程序完全路径</param>
        public static bool SetAutoStart(bool onOff, string appName, string appPath)
        {
            bool isOk = true;
            //如果从没有设为开机启动设置到要设为开机启动
            if (!IsExistKey(appName) && onOff)
            {
                isOk = SelfRunning(onOff, appName, @appPath);
            }
            //如果从设为开机启动设置到不要设为开机启动
            else if (IsExistKey(appName) && !onOff)
            {
                isOk = SelfRunning(onOff, appName, @appPath);
            }
            return isOk;
        }

        /// <summary>
        /// 判断注册键值对是否存在，即是否处于开机启动状态
        /// </summary>
        /// <param name="keyName">键值名</param>
        /// <returns></returns>
        private static bool IsExistKey(string keyName)
        {
            try
            {
                bool _exist = false;
                RegistryKey local = Registry.LocalMachine;
                RegistryKey runs = local.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                if (runs == null)
                {
                    RegistryKey key2 = local.CreateSubKey("SOFTWARE");
                    RegistryKey key3 = key2.CreateSubKey("Microsoft");
                    RegistryKey key4 = key3.CreateSubKey("Windows");
                    RegistryKey key5 = key4.CreateSubKey("CurrentVersion");
                    RegistryKey key6 = key5.CreateSubKey("Run");
                    runs = key6;
                }
                string[] runsName = runs.GetValueNames();
                foreach (string strName in runsName)
                {
                    if (strName.ToUpper() == keyName.ToUpper())
                    {
                        _exist = true;
                        return _exist;
                    }
                }
                return _exist;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 写入或删除注册表键值对,即设为开机启动或开机不启动
        /// </summary>
        /// <param name="isStart">是否开机启动</param>
        /// <param name="exeName">应用程序名</param>
        /// <param name="path">应用程序路径带程序名</param>
        /// <returns></returns>
        private static bool SelfRunning(bool isStart, string exeName, string path)
        {
            try
            {
                RegistryKey local = Registry.LocalMachine;
                RegistryKey key = local.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                if (key == null)
                {
                    local.CreateSubKey("SOFTWARE//Microsoft//Windows//CurrentVersion//Run");
                }
                //若开机自启动则添加键值对
                if (isStart)
                {
                    key.SetValue(exeName, path);
                    key.Close();
                }
                else//否则删除键值对
                {
                    string[] keyNames = key.GetValueNames();
                    foreach (string keyName in keyNames)
                    {
                        if (keyName.ToUpper() == exeName.ToUpper())
                        {
                            key.DeleteValue(exeName);
                            key.Close();
                        }
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        #endregion 开启自启

        private void CBox_GeographyPos_SelectedIndexChanged(object sender, EventArgs e)
        {
            new SetComonBoxItemCity(CBox_City, CBox_GeographyPos.Text);
        }

        private void CBox_UpFileCheck_CheckedChanged(object sender, EventArgs e)
        {
            settings.Default.qnUpFileCheck = CBox_qnUpFileCheck.Checked;
        }

        private void CheackFileNumber_ValueChanged(object sender, EventArgs e)
        {
            settings.Default.CheckFileNumber = CheackFileNumber.Value;
        }

        private void CBox_qnShowMesg_CheckedChanged(object sender, EventArgs e)
        {
            settings.Default.qnShowMesg = CBox_qnShowMesg.Checked;
        }

        private void CBox_UpEnd_SelectedIndexChanged(object sender, EventArgs e)
        {
            settings.Default.qnUpEnd = CBox_UpEnd.Text;
        }

        private void Number_weather_ValueChanged(object sender, EventArgs e)
        {
            settings.Default.WeatherNumber = Number_weather.Value;
        }

        private void Tbox_ImgUpPath_TextChanged(object sender, EventArgs e)
        {
            settings.Default.qnImgPath = Tbox_ImgUpPath.Text;
        }

        private void but_ReSetIni_Click(object sender, EventArgs e)
        {
            DialogResult but = MessageBox.Show("我还不会自动恢复配置哦~\r\n" +
                "1.打开路径：\"C:\\Users\\Administrator\\AppData\\Local\\My_Computer_Tools_Ⅱ\"\r\n" +
                "路径中用户名等自己查找自己的\r\n" +
                "2.删除里面的所有文件夹" +
                "3.重启此程序即可！\r\n" +
                "选择\"确定\"，打开默认的路径！如果路径不正确，请手动寻找！", "注意：", MessageBoxButtons.OKCancel);
            if (but == DialogResult.OK)
                Process.Start("explorer.exe", "C:\\Users\\Administrator\\AppData\\Local\\My_Computer_Tools_Ⅱ\\");
        }

        private void FailureTryNumber_ValueChanged(object sender, EventArgs e)
        {
            settings.Default.FailureTryNumber = FailureTryNumber.Value;
        }

        private void Form_SetPm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SetMeStart(settings.Default.OpenStartRun);

            if (CBox_City.Text != "")
                if (settings.Default.City != CBox_City.Text + "|" + CBox_GeographyPos.Text)
                {
                    settings.Default.City = CBox_City.Text + "|" + CBox_GeographyPos.Text;
                    UpDateWeather = true;
                }

            if (settings.Default.qnUpEnd == "" || CBox_UpEnd.Text == "")
                settings.Default.qnUpEnd = "MarkDown格式";

            //校验tbox_email是否符合邮箱格式
            if (tbox_email.Text.Length > 0)
            {
                if (tbox_email.Text.Contains("@") && tbox_email.Text.Contains("."))
                {
                    settings.Default.email = tbox_email.Text;
                }
                else
                {
                    MessageBox.Show("邮箱格式不正确，请重新输入！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControl1.SelectedIndex = 0;
                    tbox_email.Focus();
                    e.Cancel = true;
                    return;
                }
            }
            else
                settings.Default.email = "";
        }

        /// <summary>
        /// 加密测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void But_AccTest_Click(object sender, EventArgs e)
        {
            Program.AccXmlEncrypt = Program.CreaterXMLHelper().CheckNode("EncryptedData");
            if (Program.AccXmlEncrypt)
            {
                MessageBox.Show("当前账号文本已被加密！请勿重新加密！\r\n需更换密码，请先解锁！", "错误");
                return;
            }
            if (settings.Default.AccEncryptPwd == "")
            {
                MessageBox.Show("密码不能设置空！", "错误");
                return;
            }
            string retstr = XmlEncrypt.EncryptXml_test(Application.StartupPath + "\\" + Program.xmlname, settings.Default.AccEncryptPwd, "UserInfo");
            MessageBox.Show(retstr + "\r\n如果下面的信息出现\"EncryptedData\"则加密正确！\r\n自动保存成功...", "加密后信息：");
        }

        private void TBox_EncryptPwd_TextChanged(object sender, EventArgs e)
        {
            settings.Default.AccEncryptPwd = TBox_EncryptPwd.Text.Trim();
        }

        /// <summary>
        /// 手动加密
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void But_AccEncrypt_Click(object sender, EventArgs e)
        {
            Program.AccXmlEncrypt = Program.CreaterXMLHelper().CheckNode("EncryptedData");
            if (Program.AccXmlEncrypt)
            {
                MessageBox.Show("当前账号文本已被加密！请勿重新加密！\r\n需更换密码，请先解锁！", "错误");
                return;
            }
            if (settings.Default.AccEncryptPwd == "")
            {
                MessageBox.Show("密码不能设置空！", "错误");
                return;
            }
            string retstr = XmlEncrypt.EncryptXml(Application.StartupPath + "\\" + Program.xmlname, settings.Default.AccEncryptPwd, "UserInfo");
            if (retstr != "1")
                MessageBox.Show(retstr, "加密失败");
            else
                MessageBox.Show("加密完成，去直接界面账号本本看看吧！", "加密成功");
        }
    }
}