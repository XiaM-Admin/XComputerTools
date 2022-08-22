using My_Computer_Tools_Ⅱ.Properties;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace My_Computer_Tools_Ⅱ
{
    /// <summary>
    /// 一个参数委托
    /// </summary>
    public delegate void Fun_delegate(object data);

    /// <summary>
    /// 空参委托
    /// </summary>
    public delegate void Fun_delegate_void();

    internal static class Program
    {

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            Process instance = RunningInstance();     //获取正在运行的实例
            if (instance != null)                     //设置程序只能启动一次
            {
                HandleRunningInstance(instance);    
                MessageBox.Show("程序已经存在咯！\r\n请检查托盘菜单！","注意：",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            foreach (var item in args)
                if (item == "-autorun")
                    FirstRunArg = true;//自启动启动的程序

            Application.Run(new Form_Main());
        }


        #region  设置程序只能启动一次（多次运行激活第一个实例,使其获得焦点,并在最前端显示）
        /// 
        /// 获取正在运行的实例，没有运行的实例返回null;
        /// 
        public static Process RunningInstance()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            foreach (Process process in processes)
            {
                if (process.Id != current.Id)
                {
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName)
                    {
                        return process;
                    }
                }
            }
            return null;
        }

        /// 
        /// 显示已运行的程序。
        /// 
        public static void HandleRunningInstance(Process instance)
        {
            ShowWindowAsync(instance.MainWindowHandle, 1); //显示窗口。0关闭窗口,1正常大小显示窗口,2最小化窗口,3最大化窗口
            SetForegroundWindow(instance.MainWindowHandle);            //放到前端
        }

        /// 
        /// 该函数设置由不同线程产生的窗口的显示状态。
        /// 
        /// 窗口句柄
        /// 指定窗口如何显示。0关闭窗口,1正常大小显示窗口,2最小化窗口,3最大化窗口。
        /// 
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);

        /// 
        /// 该函数将创建指定窗口的线程设置到前台，并且激活该窗口。系统给创建前台窗口的线程分配的权限稍高于其他线程。
        /// 
        /// 将被激活并被调入前台的窗口句柄。
        /// 
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        #endregion

        #region 程序变量

        /// <summary>
        /// 更新程序链接
        /// </summary>
        public static string UpdataURL = "";

        /// <summary>
        /// 账号存储的文件名
        /// </summary>
        public const string xmlname = "Account.xml";

        /// <summary>
        /// 天气城市
        /// </summary>
        public static string city = "";

        /// <summary>
        /// 是否处于后台状态?
        /// </summary>
        public static bool backWindows_State = false;

        /// <summary>
        /// 是自启动的程序?
        /// </summary>
        public static bool FirstRunArg = false;

        /// <summary>
        /// 网络连接状态
        /// </summary>
        public static bool IsReadlyNET = true;

        /// <summary>
        /// 账号xml加密状态
        /// </summary>
        public static bool AccXmlEncrypt;

        /// <summary>
        /// 自定义消息提示实例
        /// </summary>
        public static WindowsCommands WinCommand = new WindowsCommands();

        /// <summary>
        /// 全局日志实例
        /// </summary>
        public static Log logmain = new Log();

        /// <summary>
        /// 任务池控制实例
        /// </summary>
        public static Thread_Main _Main;

        /// <summary>
        /// 手动上传 等待ui实例
        /// </summary>
        public static Form_ProgressBar _ProgressBar;

        /// <summary>
        /// 程序全局时间
        /// </summary>
        public static DateTime DateTime;

        /// <summary>
        /// 系统温度变量
        /// </summary>
        public static COMPUTER_TEMP computer_temp = new COMPUTER_TEMP();

        public class COMPUTER_TEMP
        {
            public float CPUTemp=0;
            public float GPUTemp=0;
        }

        /// <summary>
        /// 系统温度获取
        /// </summary>
        public static Class.ComputerCore CCore= new Class.ComputerCore();

        #endregion 程序变量

        /// <summary>
        /// 创建一个操作xml的对象
        /// </summary>
        /// <returns></returns>
        public static ClsXMLoperate CreaterXMLHelper()
        {
            string path = Application.StartupPath + "\\" + Program.xmlname;
            ClsXMLoperate clsXM = new ClsXMLoperate(path);
            return clsXM;
        }

        /// <summary>
        /// 发送POST请求 返回json格式
        /// </summary>
        /// <param name="url"></param>
        /// <param name="Dic"></param>
        /// <returns></returns>
        public static JObject SendApiPost(string url, Dictionary<string, string> Dic)
        {
            string ret = WebPost.ApiPost(url, Dic);
            try
            {
                return Commands.ToJson(ret);
            }
            catch (Exception)
            {
                return (JObject)ret;
            }
        }

        /// <summary>
        /// 发送POST请求 返回字符串
        /// </summary>
        /// <param name="url"></param>
        /// <param name="Dic"></param>
        /// <returns></returns>
        public static string SendApiPost_Str(string url, Dictionary<string, string> Dic)
        {
            return WebPost.ApiPost(url, Dic);
        }

        /// <summary>
        /// 发送GET请求 返回字符串
        /// </summary>
        /// <param name="url"></param>
        /// <param name="Dic"></param>
        /// <returns></returns>
        public static string SendApiGet_Str(string url, Dictionary<string, string> Dic)
        {
            return WebPost.ApiGet(url, Dic);
        }

        /// <summary>
        /// 发送GET请求 返回json格式
        /// </summary>
        /// <param name="url"></param>
        /// <param name="Dic"></param>
        /// <returns></returns>
        public static JObject SendApiGet(string url, Dictionary<string, string> Dic)
        {
            var ret = WebPost.ApiGet(url, Dic);
            return Commands.ToJson(ret);
        }

        /// <summary>
        /// 获取资源文件图片
        /// </summary>
        /// <param name="imageName"></param>
        /// <returns></returns>
        public static Image GetResourceImage(string imageName)
        {
            ResourceManager resourceManager = new ResourceManager(typeof(Resources));
            return (Image)resourceManager.GetObject(imageName);
        }

        /// <summary>
        /// 检查网络通讯状态
        /// </summary>
        /// <returns></returns>
        public static bool CheckNet()
        {
            string ret = WebPost.ApiGet("https://www.baidu.com", null);

            if (ret == null) return false;
            else return true;
        }
    }
}