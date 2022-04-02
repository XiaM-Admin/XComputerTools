using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.Principal;

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
        public static bool CreatFile(string name,bool isXml=false)
        {
            try
            {
                string path = Application.StartupPath+"\\"+ name;
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

                FileStream fileStream = new FileStream(path, FileMode.CreateNew, FileAccess.ReadWrite);
                if (isXml)
                {
                    /*
                    <?xml version="1.0" encoding="utf-8"?>\r\n
                    <UserInfo>\r\n
                      <Class>未分类</Class>\r\n
                    </UserInfo>\r\n
                    */
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

    }

    /// <summary>
    /// 窗口控件互动日志类
    /// </summary>
    public  class WindowsCommands
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
        public void ChangeTips(string Txt,string Tip,int s)
        {
            ChangeTips(Tip);
            if (Txt.Length > 4)
                Txt=Txt.Insert(4,"\r\n");
            showBox.Show(Txt,Tip, _Main, s);
        }
    }
}
