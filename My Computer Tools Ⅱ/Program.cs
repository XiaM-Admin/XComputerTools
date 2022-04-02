using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using settings = My_Computer_Tools_Ⅱ.Properties.Settings;

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
            var ret = NetAPI.NetApiCommand.Net_CheckUser();//检测用户权限
            foreach (var item in args)
            {
                if (item == "-autorun")
                    FirstRunArg = true;//自启动启动的程序
            }


            if (settings.Default.ShowLoad && ret != "1")//检查变量 弹出登陆窗口
            {
                Application.Run(new Form_Load());
            }
            else
            {
                Application.Run(new Form_Main());
            }
            
        }

        ///程序变量
        public static bool backWindows_State = false;
        public static bool FirstRunArg = false;
        public const string xmlname = "Account.xml";//账号的存储名字

    }
}
