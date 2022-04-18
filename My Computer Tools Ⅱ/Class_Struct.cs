using Qiniu.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace My_Computer_Tools_Ⅱ
{
    internal class Qiniu_OSS
    {
        /// <summary>
        /// Access Key
        /// </summary>
        public string AccessKey { get; set; }

        /// <summary>
        /// Secret Key
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        /// Bucket Name
        /// </summary>
        public string Bucket { get; set; }

        /// <summary>
        /// 地域ID
        /// </summary>
        public string ZoneID { get; set; }

        /// <summary>
        /// 域名
        /// </summary>
        public string Domain { get; set; }

        public Dictionary<string, Zone> dicZone = new Dictionary<string, Zone>
        {
            { "华东",Zone.Z0},
            { "华北",Zone.Z1},
            { "华南",Zone.Z2},
            { "北美",Zone.NA0},
            { "东南亚",Zone.AS0},
            { "华东-浙江2",Zone.CN_EAST_2},
        };

        public Dictionary<int, string> Filetype = new Dictionary<int, string>
        {
            {0,"标准存储" },
            {1,"低频存储" },
            {2,"归档存储" },
            {3,"深度归档存储" }
        };

        public Qiniu_OSS(string Bucket, string AccessKey, string SecretKey, string ZoneID, string Domain)
        {
            this.Bucket = Bucket;
            this.AccessKey = AccessKey;
            this.SecretKey = SecretKey;
            this.ZoneID = ZoneID;
            this.Domain = Domain;
        }

        /// <summary>
        /// 将路径值转换为七牛云的Key
        /// 只要删除盘符，将\\替换为/
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public string PathToKey(string Path)
        {
            string[] vs = Path.Split('|');

            Path = vs[vs.Length - 1];

            return Path.Replace(@"\", "/");
        }

        #region 七牛云hash算法

        private int CHUNK_SIZE = 1 << 22;

        public byte[] sha1(byte[] data)
        {
            return System.Security.Cryptography.SHA1.Create().ComputeHash(data);
        }

        public string urlSafeBase64Encode(byte[] data)
        {
            string encodedString = Convert.ToBase64String(data);
            encodedString = encodedString.Replace('+', '-').Replace('/', '_');
            return encodedString;
        }

        public string calcETag(string path)
        {
            path = path.Replace("|", "");
            FileStream fs;
            fs = File.OpenRead(path);
            long fileLength = fs.Length;
            string etag;
            if (fileLength <= CHUNK_SIZE)
            {
                byte[] fileData = new byte[(int)fileLength];
                fs.Read(fileData, 0, (int)fileLength);
                byte[] sha1Data = sha1(fileData);
                int sha1DataLen = sha1Data.Length;
                byte[] hashData = new byte[sha1DataLen + 1];

                System.Array.Copy(sha1Data, 0, hashData, 1, sha1DataLen);
                hashData[0] = 0x16;
                etag = urlSafeBase64Encode(hashData);
            }
            else
            {
                int chunkCount = (int)(fileLength / CHUNK_SIZE);
                if (fileLength % CHUNK_SIZE != 0)
                {
                    chunkCount += 1;
                }
                byte[] allSha1Data = new byte[0];
                for (int i = 0; i < chunkCount; i++)
                {
                    byte[] chunkData = new byte[CHUNK_SIZE];
                    int bytesReadLen = fs.Read(chunkData, 0, CHUNK_SIZE);
                    byte[] bytesRead = new byte[bytesReadLen];
                    System.Array.Copy(chunkData, 0, bytesRead, 0, bytesReadLen);
                    byte[] chunkDataSha1 = sha1(bytesRead);
                    byte[] newAllSha1Data = new byte[chunkDataSha1.Length
                            + allSha1Data.Length];
                    System.Array.Copy(allSha1Data, 0, newAllSha1Data, 0,
                            allSha1Data.Length);
                    System.Array.Copy(chunkDataSha1, 0, newAllSha1Data,
                            allSha1Data.Length, chunkDataSha1.Length);
                    allSha1Data = newAllSha1Data;
                }
                byte[] allSha1DataSha1 = sha1(allSha1Data);
                byte[] hashData = new byte[allSha1DataSha1.Length + 1];
                System.Array.Copy(allSha1DataSha1, 0, hashData, 1,
                        allSha1DataSha1.Length);
                hashData[0] = (byte)0x96;
                etag = urlSafeBase64Encode(hashData);
            }
            fs.Close();
            return etag;
        }

        #endregion 七牛云hash算法
    }

    /// <summary>
    /// 线程分类
    /// </summary>
    public enum Thread_Grade
    { user = 1, system = 0 }

    public enum Thread_State
    { run = 1, stop = 0, exit = -1 }

    /// <summary>
    /// 运行模式
    /// timer 定时模式
    /// thread 循环模式
    /// OnlyOne 延迟执行一次
    /// </summary>
    public enum Thread_RunMode
    { timer = 0, thread = 1, OnlyOne = 2 }

    public class ThreadFormData
    {
        /// <summary>
        /// 提示信息
        /// </summary>
        public string text = "";

        /// <summary>
        /// 任务名字
        /// </summary>
        public string name;

        /// <summary>
        /// 任务id
        /// </summary>
        public int id;

        /// <summary>
        /// 任务分级
        /// </summary>
        public Thread_Grade grade;

        /// <summary>
        /// 任务提示级别
        /// </summary>
        public Log_Type type = Log_Type.Info;
    }

    internal class Thread_Main
    {
        /// <summary>
        /// 总线程数量
        /// </summary>
        private int Count;

        /// <summary>
        /// id值
        /// </summary>
        private int idnum = 1;

        /// <summary>
        /// 监控线程的日志输出
        /// </summary>
        private Fun_delegate Main_CW;

        private List<ThreadData> threadDatas = new List<ThreadData>();

        public Thread_Main(Fun_delegate fun)
        {
            Count = 1;
            this.Main_CW = fun;
            ThreadFormData CWdata = new ThreadFormData()
            {
                text = "系统监控线程启动",
                name = "系统监控线程",
                id = 0,
                grade = Thread_Grade.system,
                type = Log_Type.Info
            };

            Main_CW(CWdata);
            Thread thread = new Thread(new ThreadStart(ThreadMain));
            thread.IsBackground = true;
            thread.Start();
        }

        /// <summary>
        /// 主线程 控制线程
        /// </summary>
        private void ThreadMain()
        {
            Console.WriteLine("ThreadMain 启动");
            while (true)
            {
                foreach (ThreadData threadData in threadDatas)
                {
                    if (threadData.State == Thread_State.stop || threadData.State == Thread_State.exit)
                        continue;

                    Console.WriteLine($"任务名:{threadData.Name} id:{threadData.ID} 状态:{threadData.thread.ThreadState}");//检查失效线程

                    //运行模式为延迟执行，并且线程状态为终止count-- 线程状态设置为抛弃
                    if (threadData.RunMode == Thread_RunMode.OnlyOne && threadData.thread.ThreadState == ThreadState.Stopped)
                    {
                        Count--;
                        threadData.State = Thread_State.exit;
                    }
                }

                CheckExitThread();
                Console.WriteLine($"当前总线程{Count}");
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// 检查被抛弃的线程list删除
        /// </summary>
        private void CheckExitThread()
        {
            for (int i = 0; i < threadDatas.Count; i++)
            {
                if (threadDatas[i].State == Thread_State.exit && threadDatas[i].thread.ThreadState == ThreadState.Stopped)
                {
                    Console.WriteLine($"废弃的任务{threadDatas[i].Name} id:{threadDatas[i].ID}已经移除");
                    threadDatas.Remove(threadDatas[i]);
                    Count--;
                }
            }
        }

        /// <summary>
        /// 循环执行模式
        /// </summary>
        /// <param name="obj">ThreadData</param>
        private void Thread_while(object obj)
        {
            ThreadData threadData = (ThreadData)obj;
            Console.WriteLine($"循环任务 {threadData.Name} 启动");
            ThreadFormData CWdata = new ThreadFormData()
            {
                text = $"循环任务 {threadData.Name} 启动",
                name = threadData.Name,
                id = threadData.ID,
                grade = Thread_Grade.system,
                type = Log_Type.Info
            };
            Main_CW(CWdata);
            while (true)
            {
                Thread.Sleep(Convert.ToInt32(threadData.ModePar));
                if (Get_ThreadState(threadData.ID) == Thread_State.exit)
                {
                    break;
                }
                threadData.Fun(threadData);
            }

            Console.WriteLine($"循环任务 {threadData.Name} 已结束");
            CWdata.text = $"循环任务 {threadData.Name} 已结束";
            Main_CW(CWdata);
        }

        /// <summary>
        /// 创建线程
        /// </summary>
        /// <returns></returns>
        public bool CreateThread(CreateThread data)
        {
            ThreadData threadData = new ThreadData()
            {
                Grade = data.Grade,
                Name = data.Name,
                RunMode = data.RunMode,
                ModePar = data.ModePar,
                Explain = data.Explain,
                Fundata = data.Fundata,
                Fun = data.Fun,

                State = Thread_State.stop,
                ID = idnum++,
            };
            threadData.State = Thread_State.run;

            //分析模式和参数
            switch (data.RunMode)
            {
                case Thread_RunMode.timer:
                    //DateTime dt = DateTime.ParseExact(threadData.ModePar, "mmss", System.Globalization.CultureInfo.CurrentCulture);
                    break;

                case Thread_RunMode.thread:
                    Console.WriteLine($"{DateTime.Now}：任务{threadData.Name}  开始循环执行 循环间隔{threadData.ModePar}毫秒");
                    threadData.thread = new Thread(new ParameterizedThreadStart(Thread_while));
                    threadData.thread.IsBackground = true;//后台线程
                    threadData.thread.Start(threadData);
                    break;

                case Thread_RunMode.OnlyOne:
                    Console.WriteLine($"{DateTime.Now}：任务{threadData.Name}  等待{threadData.ModePar}毫秒");
                    threadData.thread = new Thread(new ParameterizedThreadStart(threadData.Fun));
                    threadData.thread.IsBackground = true;//后台线程
                    Task.Delay(Convert.ToInt16(threadData.ModePar)).ContinueWith(t =>
                    {
                        Console.WriteLine($"{DateTime.Now}：任务{threadData.Name}启动了，任务状态 {threadData.thread.ThreadState}");
                        threadData.thread.Start(threadData);
                    });
                    break;

                default:
                    return false;
            }

            Count++;
            threadDatas.Add(threadData);
            return true;
        }

        /// <summary>
        /// 退出线程
        /// </summary>
        public void Set_ThreadExit(int id)
        {
            foreach (var threadData in threadDatas)
            {
                if (id == threadData.ID)
                {
                    threadData.State = Thread_State.exit;
                    Console.WriteLine($"设置id {id} 的线程状态为抛弃");
                }
            }
        }

        /// <summary>
        /// 获取线程状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Thread_State Get_ThreadState(int id)
        {
            foreach (var threadData in threadDatas)
            {
                if (id == threadData.ID)
                {
                    //Console.WriteLine($"找到id {id} 的线程状态为 {threadData.State}");
                    return threadData.State;
                }
            }
            return Thread_State.stop;
        }
        /// <summary>
        /// 获取当前正在执行所有的任务名列表
        /// </summary>
        /// <returns></returns>
        public List<string> Get_ListTaskNames()
        {
            List<string> retlist = new List<string>();
            foreach (var threadData in threadDatas)
            {
                if (threadData.State==Thread_State.run)
                    retlist.Add($"{threadData.ID} {threadData.Name}");
            }
            return retlist;
        }

        /// <summary>
        /// 获取当前正在执行指定分类的任务名列表
        /// </summary>
        /// <returns></returns>
        public List<string> Get_ListTaskNames(Thread_Grade grade)
        {
            List<string> retlist = new List<string>();
            foreach (var threadData in threadDatas)
            {
                if (threadData.State == Thread_State.run && threadData.Grade == grade)
                    retlist.Add($"{threadData.ID} {threadData.Name}");
            }
            return retlist;
        }

        /// <summary>
        /// 获取任务线程数据
        /// </summary>
        /// <param name="i">任务的唯一id</param>
        /// <returns></returns>
        public ThreadData Get_ThreadDatainList(int i)
        {
            foreach (var threadData in threadDatas)
            {
                if (threadData.ID == i)
                    return threadData;
            }
            return null;
        }
        /// <summary>
        /// 获取任务线程数据
        /// </summary>
        /// <param name="name">任务名 容易重复</param>
        /// <returns></returns>
        public ThreadData Get_ThreadDatainList(string name)
        {
            foreach (var threadData in threadDatas)
            {
                if (threadData.Name == name)
                    return threadData;
            }
            return null;
        }

    }

    /// <summary>
    /// 创建线程的参数
    /// </summary>
    public class CreateThread
    {
        /// <summary>
        /// 线程分类
        /// </summary>
        public Thread_Grade Grade;

        /// <summary>
        /// 运行模式
        /// </summary>
        public Thread_RunMode RunMode;

        /// <summary>
        /// 模式参数
        /// 定时模式填入时间
        /// 循环模式填入循环间隔 ms
        /// 延迟执行一次填入等待时间 ms
        /// </summary>
        public string ModePar;

        /// <summary>
        /// 任务名
        /// </summary>
        public string Name;

        /// <summary>
        /// 说明信息
        /// </summary>
        public string Explain;

        /// <summary>
        /// 线程执行函数
        /// </summary>
        public Fun_delegate Fun;

        /// <summary>
        /// 函数参数
        /// </summary>
        public object Fundata;
    }

    /// <summary>
    /// 线程执行的信息
    /// </summary>
    public class ThreadData : CreateThread
    {
        public Thread_State State;

        public int ID;

        public Thread thread;
    }
}