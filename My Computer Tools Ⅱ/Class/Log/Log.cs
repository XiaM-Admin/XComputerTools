using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace My_Computer_Tools_Ⅱ
{
    public enum Log_Type
    {
        Info,
        Warning,
        Error
    }

    internal class Log
    {
        private readonly string Log_File_Path = Application.StartupPath + "\\Log\\" + DateTime.Now.ToString("yyyyMMdd");
        private readonly string Log_User_Path;
        private readonly string Log_System_Path;

        /// <summary>
        /// 构造函数
        /// </summary>
        public Log()
        {
            //检查日志文件夹是否存在
            if (!Directory.Exists(Log_File_Path))
            {
                Directory.CreateDirectory(Log_File_Path);
            }
            Log_User_Path = Log_File_Path + "\\用户";
            Log_System_Path = Log_File_Path + "\\系统";
            Clear(Log_User_Path);
            Clear(Log_System_Path);
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="data"></param>
        public void Write(ThreadFormData data)
        {
            string Filename = $"{data.id}_{data.name}_Log.txt";
            string Filepath;
            switch (data.grade)
            {
                case Thread_Grade.user:
                    PathisExists(Log_User_Path);
                    Filepath = Log_User_Path + "\\" + Filename;
                    break;

                case Thread_Grade.system:
                    PathisExists(Log_System_Path);
                    Filepath = Log_System_Path + "\\" + Filename;
                    break;

                default:
                    return;
            }
            FileisExists(Filepath);
            //处理一下换行的text
            string[] text = data.text.Split('\n');
            data.text = text[0];
            for (int i = 1; i < text.Length; i++)
            {
                string str = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{data.type}] ：";
                data.text += "\n" + text[i].PadLeft(str.Length + text[i].Length + 1);
            }

            using (StreamWriter sw = new StreamWriter(Filepath, true))
            {
                sw.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{data.type}] ：{data.text}");
            }
        }

        /// <summary>
        /// 获取指定路径的Log信息
        /// 读取
        /// </summary>
        /// <param name="data">ThreadFormData</param>
        /// <returns></returns>
        public List<string> GetPathLog(ThreadFormData data)
        {
            List<string> retlist = new List<string>();
            string Filename = $"{data.id}_{data.name}_Log.txt";
            string Filepath;
            switch (data.grade)
            {
                case Thread_Grade.user:
                    PathisExists(Log_User_Path);
                    Filepath = Log_User_Path + "\\" + Filename;
                    break;

                case Thread_Grade.system:
                    PathisExists(Log_System_Path);
                    Filepath = Log_System_Path + "\\" + Filename;
                    break;

                default:
                    return retlist;
            }
            FileisExists(Filepath);
            using (StreamReader sr = new StreamReader(Filepath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                    retlist.Add(line);
            }

            //限制大小
            int index = 0;
            int endindex = retlist.Count - 33;
            while (index < endindex)
            {
                retlist.RemoveAt(index);
                endindex--;
            }
            return retlist;
        }

        /// <summary>
        /// 返回指定任务日志文件路径
        /// </summary>
        /// <param name="grade">任务等级</param>
        /// <param name="id">id</param>
        /// <param name="taskname">任务名</param>
        /// <returns></returns>
        public string GetTaskLog(Thread_Grade grade, int id, string taskname)
        {
            string Filename = $"{id}_{taskname}_Log.txt";
            string Filepath;
            switch (grade)
            {
                case Thread_Grade.user:
                    PathisExists(Log_User_Path);
                    Filepath = Log_User_Path + "\\" + Filename;
                    break;

                case Thread_Grade.system:
                    PathisExists(Log_System_Path);
                    Filepath = Log_System_Path + "\\" + Filename;
                    break;

                default:
                    return "";
            }
            return Filepath;
        }

        /// <summary>
        /// 清空其他天数的文件夹
        /// </summary>
        public int ClearOver()
        {
            List<string> path = new List<string>();
            int count = 0;

            DirectoryInfo d = new DirectoryInfo(Application.StartupPath + "\\Log\\");
            DirectoryInfo[] files = d.GetDirectories();
            foreach (DirectoryInfo fsinfo in files)
            {
                if (fsinfo.FullName != Log_File_Path)
                    path.Add(fsinfo.FullName);
            }

            foreach (var item in path)
            {
                DirectoryInfo del = new DirectoryInfo(item);
                del.Delete(true);
                count++;
            }
            return count;
        }

        /// <summary>
        /// 文件夹是否存在 不存在创建
        /// </summary>
        /// <param name="Path"></param>
        private void PathisExists(string Path)
        {
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }
        }

        /// <summary>
        /// 检查文件是否存在，不存在就创建
        /// </summary>
        /// <param name="Path"></param>
        private void FileisExists(string Path)
        {
            if (!File.Exists(Path))
            {
                FileStream file = File.Create(Path);
                file.Close();
            }
        }

        /// <summary>
        /// 清空文件夹
        /// </summary>
        /// <param name="path"></param>
        private void Clear(string path)
        {
            try
            {
                if (string.IsNullOrEmpty(path)
                    || !Directory.Exists(path))
                {
                    return;
                }
                foreach (string strFile in Directory.GetFiles(path))
                {
                    File.Delete(strFile);
                }
                foreach (string strDir in Directory.GetDirectories(path))
                {
                    Directory.Delete(strDir, true);
                }

                return;
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}