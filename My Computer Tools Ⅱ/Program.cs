using My_Computer_Tools_Ⅱ.Properties;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace My_Computer_Tools_Ⅱ
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
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

        ///程序变量
        public static bool backWindows_State = false;
        public static bool FirstRunArg = false;
        public const string xmlname = "Account.xml";//账号的存储名字
        public static WindowsCommands WinCommand = new WindowsCommands();

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

    }
}
