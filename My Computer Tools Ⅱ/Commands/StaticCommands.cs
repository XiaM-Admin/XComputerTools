using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;

namespace My_Computer_Tools_Ⅱ
{
    public static class Commands
    {
        /// <summary>
        /// 确定当前主体是否属于具有指定 Administrator 的 Windows 用户组
        /// </summary>
        /// <returns>如果当前主体是指定的 Administrator 用户组的成员，则为 true；否则为 false。</returns>
        public static bool IsAdministrator()
        {
            bool result;
            try
            {
                WindowsIdentity identity = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                result = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="name"></param>
        /// <param name="isXml">是不是xml文档</param>
        /// <returns></returns>
        public static bool CreatFile(string name, bool isXml = false)
        {
            try
            {
                string path = Application.StartupPath + "\\" + name;
                if (File.Exists(path))
                {
                    StreamReader streamReader = new StreamReader(Application.StartupPath + "\\" + name);
                    string ret = streamReader.ReadLine();
                    streamReader.Close();
                    if (ret != "<?xml version=\"1.0\" encoding=\"utf-8\"?>")
                    {
                        File.Delete(Application.StartupPath + "\\" + name);
                        return CreatFile(name, isXml);
                    }
                    return true;
                }
                MessageBox.Show("缺失账号配置文件！\r\n将为您自动创建xml文件，记录账号！\r\n此操作必须进行，您可无视它，不进行记录。", "提醒");
                FileStream fileStream = new FileStream(path, FileMode.CreateNew, FileAccess.ReadWrite);
                if (isXml)
                {
                    fileStream.Write(Encoding.Default.GetBytes("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n"), 0, "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n".Length);
                    fileStream.Write(Encoding.Default.GetBytes("<UserInfo>\r\n"), 0, "<UserInfo>\r\n".Length);
                    fileStream.Write(Encoding.Default.GetBytes("  <Class>defualt</Class>\r\n"), 0, "  <Class>defualt</Class>\r\n".Length);
                    fileStream.Write(Encoding.Default.GetBytes("</UserInfo>\r\n"), 0, "</UserInfo>\r\n".Length);
                }

                fileStream.Close();

                return true;
            }
            catch (Exception e)
            {
                if (e.ToString().Contains("已经存在"))
                {
                    StreamReader streamReader = new StreamReader(Application.StartupPath + "\\" + name);
                    string ret = streamReader.ReadLine();
                    streamReader.Close();
                    if (ret != "<?xml version=\"1.0\" encoding=\"utf-8\"?>")
                    {
                        File.Delete(Application.StartupPath + "\\" + name);
                        return CreatFile(name, isXml);
                    }
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// string类型转为json类型
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static JObject ToJson(string str)
        {
            if (str != null)
            {
                JObject jo = (JObject)JsonConvert.DeserializeObject(str);
                return jo;
            }
            else
                return null;
        }

        /// <summary>
        /// 字节转为带单位的字符串
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static string ByteTostring(long x)
        {
            if (x < 1024)
                return x.ToString() + " " + "B";

            Dictionary<int, string> dic = new Dictionary<int, string> { { 1, "KB" }, { 2, "MB" }, { 3, "GB" }, { 4, "TB" } };
            int end = 0;
            double ret = x / 1024.0;
            end++;
            while (ret >= 1024)
            {
                ret = ret / 1024.0;
                end++;
            }
            return ret.ToString("#0.00") + " " + dic[end];
        }

        /// <summary>
        /// 遍历文件夹，包括子文件夹
        /// </summary>
        /// <returns>所有文件</returns>
        public static List<string> File_GetList(string path)
        {
            List<string> list = new List<string>();
            if (File.Exists(path))
                return list;

            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                list.Add(file.FullName);
            }
            DirectoryInfo[] dirs = dir.GetDirectories();
            foreach (DirectoryInfo d in dirs)
            {
                list.AddRange(File_GetList(d.FullName));
            }
            return list;
        }

        /// <summary>
        /// 判断目标是文件夹还是目录(目录包括磁盘)
        /// </summary>
        /// <param name="filepath">路径</param>
        /// <returns>返回true为一个文件夹，返回false为一个文件</returns>
        public static bool File_IsDir(string filepath)
        {
            FileInfo fi = new FileInfo(filepath);
            if ((fi.Attributes & FileAttributes.Directory) != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    /// <summary>
    /// 窗口控件互动日志类
    /// </summary>
    public class WindowsCommands
    {
        public Form_Main _Main;
        public ShowBox showBox;//提示窗口

        public FileStream LogFile = null;

        public WindowsCommands()
        {
        }

        /// <summary>
        /// 更改状态栏
        /// </summary>
        /// <param name="Txt"></param>
        public void ChangeTips(string Txt)
        {
            Txt = Txt.Replace("\r\n", "");
            _Main.StaLab_State.Text = Txt;
        }

        /// <summary>
        /// 更改状态栏 同时弹出提示窗
        /// </summary>
        /// <param name="Txt">标题</param>
        /// <param name="Tip">内容</param>
        /// <param name="s">显示秒数</param>
        public void ChangeTips(string Txt, string Tip, int s)
        {
            ChangeTips(Tip);
            if (Txt.Length > 4)
                Txt = Txt.Insert(4, "\r\n");
            showBox.Show(Txt, Tip, _Main, s);
        }
    }
}