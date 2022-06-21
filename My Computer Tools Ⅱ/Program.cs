using My_Computer_Tools_Ⅱ.Properties;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Resources;
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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            foreach (var item in args)
            {
                if (item == "-autorun")
                    FirstRunArg = true;//自启动启动的程序
            }

            Application.Run(new Form_Main());
        }

        //程序变量
        public static string UpdataURL = "";

        public const string xmlname = "Account.xml";//账号的存储名字
        public static string city = "";
        public static bool backWindows_State = false;//现在是否处于后台状态
        public static bool FirstRunArg = false;//是不是自启动的程序
        public static bool IsReadlyNET = true;//网络连接状态
        public static WindowsCommands WinCommand = new WindowsCommands();//自定义消息提示类
        public static Log logmain = new Log();//全局日志类
        public static Thread_Main _Main;//全局多线程控制
        public static Form_ProgressBar _ProgressBar;//等待ui
        public static DateTime DateTime;//统一定时时间

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