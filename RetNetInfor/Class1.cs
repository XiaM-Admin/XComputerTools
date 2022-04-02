using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.IO;


namespace RetNetInfor
{
    public class NetInfor
    {
        public string Ret_Ch = "";
        public NetInfor(string info)
        {
            Ret_Ch = GetMessage(info);

        }
        public string GetMessage(string info)
        {
            switch (info)
            {
                case "1":
                    return "成功";
                case "-1":
                    return "网络连接失败";
                case "-2":
                    return "空程序密钥";
                case "-3":
                case "-4":
                    return "数据异常";
                case "-6":
                    return "还未登陆";
                case "-8":
                    return "账户余额不足.";
                case "-9":
                    return "注册用户达到上限.";
                case "-101":
                    return "用户名填写错误,必须以字母开头6-16位字母或数字!";
                case "-102":
                    return "用户不存在.";
                case "-104":
                    return "密码填写错误,请输入6-16位密码！.";
                case "-105":
                    return "邮箱填写错误,请正确输入邮箱,最大长度 32！.";
                case "-106":
                    return "用户名重复.";
                case "-107	":
                    return "邮箱重复.";
                case "-108":
                    return "新密码输入错误.";
                case "-109":
                    return "用户名或密码错误";
                case "-110":
                    return "用户使用时间已到期";
                case "-115":
                    return "用户已被禁用";
                case "-116":
                    return "密码修改申请过于频繁.";
                case "-120":
                    return "注册失败,注册次数超过限制.";
                case "-123":
                    return "用户密码输入错误";
                case "-124":
                    return "用户登录数达到最大";
                case "-210":
                    return "程序停止新用户注册";
                default:
                    return "未知错误 代码："+info;
            }
            
        }
    }

    /// <summary> 
    /// 计算机信息类
    /// </summary> 
    public class GetComputerInfo
    {
        public string MacAddress;//Mac地址
        public string IpAddress;//IP地址
        public string ComputerName;//计算机名称
        public string SizeOfMemery;
        private static GetComputerInfo _instance;

        public static GetComputerInfo GetInstance()
        {
            if (_instance == null)
                _instance = new GetComputerInfo();
            return _instance;
        }
        public GetComputerInfo()
        {
            MacAddress = GetMacAddress();
            IpAddress = GetIPAddress();
            ComputerName = GetComputerName();
            SizeOfMemery = GetSizeOfMemery();
        }
        /// <summary>
        /// 获取CPU的个数
        /// </summary>
        /// <returns></returns>
        public static int GetCpuCount()
        {
            try
            {
                using (ManagementClass mCpu = new ManagementClass("Win32_Processor"))
                {
                    ManagementObjectCollection cpus = mCpu.GetInstances();
                    return cpus.Count;
                }
            }
            catch
            {
            }
            return -1;
        }

        /// <summary>
        /// 获取CPU的频率 这里之所以使用string类型的数组，主要是因为cpu的多核
        /// </summary>
        /// <returns></returns>
        public static string[] GetCpuMHZ()
        {
            ManagementClass mc = new ManagementClass("Win32_Processor");
            ManagementObjectCollection cpus = mc.GetInstances();
            string[] mHz = new string[cpus.Count];
            int c = 0;
            ManagementObjectSearcher mySearch = new ManagementObjectSearcher("select * from Win32_Processor");
            foreach (ManagementObject mo in mySearch.Get())
            {
                mHz[c] = mo.Properties["CurrentClockSpeed"].Value.ToString();
                c++;
            }
            mc.Dispose();
            mySearch.Dispose();
            return mHz;
        }
        /// <summary>
        /// 获取本机硬盘的大小
        /// </summary>
        /// <returns></returns>
        public static string GetSizeOfDisk()
        {
            ManagementClass mc = new ManagementClass("Win32_DiskDrive");
            ManagementObjectCollection moj = mc.GetInstances();
            foreach (ManagementObject m in moj)
            {
                return m.Properties["Size"].Value.ToString();
            }
            return "-1";
        }
        /// <summary>
        ///  获取本机内存的大小：
        /// </summary>
        /// <returns></returns>
        public static string GetSizeOfMemery()
        {
            ManagementClass mc = new ManagementClass("Win32_OperatingSystem");
            ManagementObjectCollection moc = mc.GetInstances();
            double sizeAll = 0.0;
            foreach (ManagementObject m in moc)
            {
                if (m.Properties["TotalVisibleMemorySize"].Value != null)
                {
                    sizeAll += Convert.ToDouble(m.Properties["TotalVisibleMemorySize"].Value.ToString());
                }
            }
            mc = null;
            moc.Dispose();
            return sizeAll.ToString();
        }

        /// <summary>
        /// 获取磁盘剩余空间
        /// </summary>
        /// <param name="str_HardDiskName"></param>
        /// <returns></returns>
        long GetHardDiskFreeSpace(string str_HardDiskName)
        {
            long num = 0L;
            str_HardDiskName = str_HardDiskName + @":\";
            foreach (DriveInfo info in DriveInfo.GetDrives())
            {
                if (info.Name.ToUpper() == str_HardDiskName.ToUpper())
                {
                    num = info.TotalFreeSpace / 0x100000L;
                }
            }
            return num;
        }

        //获得CPU编号
        string GetCpuID()
        {
            try
            {
                //获取CPU序列号代码 
                string cpuInfo = "";//cpu序列号 
                ManagementClass mc = new ManagementClass("Win32_Processor");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
                }
                moc = null;
                mc = null;
                return cpuInfo;
            }
            catch
            {
                return "unknow";
            }
        }
        //获得Mac地址
        string GetMacAddress()
        {
            try
            {
                //获取网卡硬件地址 
                string mac = "";
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        mac = mo["MacAddress"].ToString();
                        break;
                    }
                }
                moc = null;
                mc = null;
                return mac;
            }
            catch
            {
                return "unknow";
            }
        }
        //获得Ip地址
        string GetIPAddress()
        {
            try
            {
                //获取IP地址 
                string st = "";
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        //st=mo["IpAddress"].ToString(); 
                        System.Array ar;
                        ar = (System.Array)(mo.Properties["IpAddress"].Value);
                        st = ar.GetValue(0).ToString();
                        break;
                    }
                }
                moc = null;
                mc = null;
                return st;
            }
            catch
            {
                return "unknow";
            }
        }
        //获得磁盘Id
        string GetDiskID()
        {
            try
            {
                //获取硬盘ID 
                String HDid = "";
                ManagementClass mc = new ManagementClass("Win32_DiskDrive");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    HDid = (string)mo.Properties["Model"].Value;
                }
                moc = null;
                mc = null;
                return HDid;
            }
            catch
            {
                return "unknow";
            }
        }
        /// <summary> 
        /// 操作系统的登录用户名 
        /// </summary> 
        /// <returns></returns> 
        string GetUserName()
        {
            try
            {
                string st = "";
                ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    st = mo["UserName"].ToString();
                }
                moc = null;
                mc = null;
                return st;
            }
            catch
            {
                return "unknow";
            }
        }
        /// <summary> 
        /// PC类型 
        /// </summary> 
        /// <returns></returns> 
        string GetSystemType()
        {
            try
            {
                string st = "";
                ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    st = mo["SystemType"].ToString();
                }
                moc = null;
                mc = null;
                return st;
            }
            catch
            {
                return "unknow";
            }
        }

        /// <summary> 
        /// 物理内存 
        /// </summary> 
        /// <returns></returns> 
        string GetTotalPhysicalMemory()
        {
            try
            {
                string st = "";
                ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    st = mo["TotalPhysicalMemory"].ToString();
                }
                moc = null;
                mc = null;
                return st;
            }
            catch
            {
                return "unknow";
            }
        }
        /// <summary> 
        ///  获取计算机名称
        /// </summary> 
        /// <returns></returns> 
        string GetComputerName()
        {
            try
            {
                return System.Environment.GetEnvironmentVariable("ComputerName");
            }
            catch
            {
                return "unknow";
            }
        }
    }
}
