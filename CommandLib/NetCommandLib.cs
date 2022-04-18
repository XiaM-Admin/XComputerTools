using System;
using System.Collections.Generic;
using System.Management;
using System.Windows.Forms;

namespace NetCommandLib
{
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
            moc.Dispose();
            return sizeAll.ToString();
        }

        /// <summary>
        /// 获取磁盘剩余空间
        /// </summary>
        /// <param name="str_HardDiskName"></param>
        /// <returns></returns>
        //private long GetHardDiskFreeSpace(string str_HardDiskName)
        //{
        //    long num = 0L;
        //    str_HardDiskName = str_HardDiskName + @":\";
        //    foreach (DriveInfo info in DriveInfo.GetDrives())
        //    {
        //        if (info.Name.ToUpper() == str_HardDiskName.ToUpper())
        //        {
        //            num = info.TotalFreeSpace / 0x100000L;
        //        }
        //    }
        //    return num;
        //}

        //获得CPU编号
        //private string GetCpuID()
        //{
        //    try
        //    {
        //        //获取CPU序列号代码
        //        string cpuInfo = "";//cpu序列号
        //        ManagementClass mc = new ManagementClass("Win32_Processor");
        //        ManagementObjectCollection moc = mc.GetInstances();
        //        foreach (ManagementObject mo in moc)
        //        {
        //            cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
        //        }
        //        moc = null;
        //        mc = null;
        //        return cpuInfo;
        //    }
        //    catch
        //    {
        //        return "unknow";
        //    }
        //}

        //获得Mac地址
        private string GetMacAddress()
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
        private string GetIPAddress()
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
        //private string GetDiskID()
        //{
        //    try
        //    {
        //        //获取硬盘ID
        //        String HDid = "";
        //        ManagementClass mc = new ManagementClass("Win32_DiskDrive");
        //        ManagementObjectCollection moc = mc.GetInstances();
        //        foreach (ManagementObject mo in moc)
        //        {
        //            HDid = (string)mo.Properties["Model"].Value;
        //        }
        //        moc = null;
        //        mc = null;
        //        return HDid;
        //    }
        //    catch
        //    {
        //        return "unknow";
        //    }
        //}

        /// <summary>
        /// 操作系统的登录用户名
        /// </summary>
        /// <returns></returns>
        //private string GetUserName()
        //{
        //    try
        //    {
        //        string st = "";
        //        ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
        //        ManagementObjectCollection moc = mc.GetInstances();
        //        foreach (ManagementObject mo in moc)
        //        {
        //            st = mo["UserName"].ToString();
        //        }
        //        moc = null;
        //        mc = null;
        //        return st;
        //    }
        //    catch
        //    {
        //        return "unknow";
        //    }
        //}

        /// <summary>
        /// PC类型
        /// </summary>
        /// <returns></returns>
        //private string GetSystemType()
        //{
        //    try
        //    {
        //        string st = "";
        //        ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
        //        ManagementObjectCollection moc = mc.GetInstances();
        //        foreach (ManagementObject mo in moc)
        //        {
        //            st = mo["SystemType"].ToString();
        //        }
        //        moc = null;
        //        mc = null;
        //        return st;
        //    }
        //    catch
        //    {
        //        return "unknow";
        //    }
        //}

        /// <summary>
        /// 物理内存
        /// </summary>
        /// <returns></returns>
        //private string GetTotalPhysicalMemory()
        //{
        //    try
        //    {
        //        string st = "";
        //        ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
        //        ManagementObjectCollection moc = mc.GetInstances();
        //        foreach (ManagementObject mo in moc)
        //        {
        //            st = mo["TotalPhysicalMemory"].ToString();
        //        }
        //        moc = null;
        //        mc = null;
        //        return st;
        //    }
        //    catch
        //    {
        //        return "unknow";
        //    }
        //}

        /// <summary>
        ///  获取计算机名称
        /// </summary>
        /// <returns></returns>
        private string GetComputerName()
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

    public class SetComonBoxItemCity
    {
        //北京
        private static readonly List<string> beijing = new List<string>
        {
            "北京",
            "朝阳区",
            "丰台区",
            "房山区",
            "通州区"
        };

        //天津
        private static readonly List<string> tianjin = new List<string>
        {
            "天津",
            "和平区",
            "河东区",
            "蓟州区"
        };

        //河北省
        private static readonly List<string> hebei = new List<string>
        {
            "河北",
            "石家庄市",
            "唐山市",
            "秦皇岛市",
            "邯郸市",
            "邢台市",
            "保定市",
            "张家口市",
            "承德市",
            "沧州市",
            "廊坊市",
            "衡水市"
        };

        //山西省
        private static readonly List<string> shanxi = new List<string>
        {
            "山西",
            "太原市",
            "大同市",
            "阳泉市",
            "长治市",
            "晋城市",
            "朔州市",
            "晋中市",
            "运城市",
            "忻州市",
            "临汾市",
            "吕梁市"
        };

        //内蒙古自治区
        private static readonly List<string> neimenggu = new List<string>
        {
            "内蒙古",
            "呼和浩特市",
            "包头市",
            "乌海市",
            "赤峰市",
            "通辽市",
            "鄂尔多斯市",
            "呼伦贝尔市",
            "巴彦淖尔市",
            "乌兰察布市",
            "兴安盟",
            "锡林郭勒盟",
            "阿拉善盟"
        };

        //辽宁省
        private static readonly List<string> liaoning = new List<string>
        {
            "辽宁",
            "沈阳市",
            "大连市",
            "鞍山市",
            "抚顺市",
            "本溪市",
            "丹东市",
            "锦州市",
            "营口市",
            "阜新市",
            "辽阳市",
            "盘锦市",
            "铁岭市",
            "朝阳市",
            "葫芦岛市"
        };

        //吉林省
        private static readonly List<string> jilin = new List<string>
        {
            "吉林",
            "长春市",
            "吉林市",
            "四平市",
            "辽源市",
            "通化市",
            "白山市",
            "松原市",
            "白城市",
            "延边朝鲜族自治州"
        };

        //黑龙江省
        private static readonly List<string> heilongjiang = new List<string>
        {
            "黑龙江",
            "哈尔滨市",
            "齐齐哈尔市",
            "鸡西市",
            "鹤岗市",
            "双鸭山市",
            "大庆市",
            "伊春市",
            "佳木斯市",
            "牡丹江市",
            "七台河市",
            "黑河市",
            "绥化市",
            "大兴安岭地区"
        };

        //上海市
        private static readonly List<string> shanghai = new List<string>
        {
            "上海",
            "黄浦区",
            "长宁区",
            "静安区",
            "普陀区",
            "宝山区",
            "奉贤区"
        };

        //江苏省
        private static readonly List<string> jiangsu = new List<string>
        {
            "南京市",
            "无锡市",
            "徐州市",
            "常州市",
            "苏州市",
            "南通市",
            "连云港市",
            "淮安市",
            "盐城市",
            "扬州市",
            "镇江市",
            "泰州市",
            "宿迁市"
        };

        //浙江省
        private static readonly List<string> zhejiang = new List<string>
        {
            "浙江",
            "杭州市",
            "宁波市",
            "温州市",
            "嘉兴市",
            "湖州市",
            "绍兴市",
            "金华市",
            "衢州市",
            "舟山市",
            "台州市",
            "丽水市"
        };

        //安徽省
        private static readonly List<string> anhui = new List<string>
        {
            "合肥市",
            "芜湖市",
            "蚌埠市",
            "淮南市",
            "马鞍山市",
            "淮北市",
            "铜陵市",
            "安庆市",
            "黄山市",
            "滁州市",
            "阜阳市",
            "宿州市",
            "六安市",
            "亳州市",
            "池州市",
            "宣城市"
        };

        //福建省
        private static readonly List<string> fujian = new List<string>
        {
            "福建",
            "福州市",
            "厦门市",
            "莆田市",
            "三明市",
            "泉州市",
            "漳州市",
            "南平市",
            "龙岩市",
            "宁德市"
        };

        //江西省
        private static readonly List<string> jiangxi = new List<string>
        {
            "江西",
            "南昌市",
            "景德镇市",
            "萍乡市",
            "九江市",
            "新余市",
            "鹰潭市",
            "赣州市",
            "吉安市",
            "宜春市",
            "抚州市",
            "上饶市"
        };

        //山东省
        private static readonly List<string> shandong = new List<string>
        {
            "济南市",
            "青岛市",
            "淄博市",
            "枣庄市",
            "东营市",
            "烟台市",
            "烟台市",
            "潍坊市",
            "济宁市",
            "泰安市",
            "威海市",
            "日照市",
            "临沂市",
            "德州市",
            "聊城市",
            "滨州市",
            "菏泽市"
        };

        //河南省
        private static readonly List<string> henan = new List<string>
        {
            "郑州市",
            "开封市",
            "洛阳市",
            "平顶山市",
            "安阳市",
            "鹤壁市",
            "新乡市",
            "焦作市",
            "濮阳市",
            "许昌市",
            "漯河市",
            "三门峡市",
            "南阳市",
            "商丘市",
            "信阳市",
            "周口市",
            "驻马店市",
            "济源市"
        };

        //湖北省
        private static readonly List<string> hubei = new List<string>
        {
            "武汉市",
            "黄石市",
            "十堰市",
            "宜昌市",
            "襄阳市",
            "鄂州市",
            "荆门市",
            "孝感市",
            "荆州市",
            "黄冈市",
            "咸宁市",
            "随州市",
            "恩施土家族苗族自治州",
            "仙桃市",
            "潜江市",
            "天门市",
            "神农架林区"
        };

        //湖南省
        private static readonly List<string> hunan = new List<string>
        {
            "长沙市",
            "株洲市",
            "湘潭市",
            "衡阳市",
            "邵阳市",
            "岳阳市",
            "常德市",
            "张家界市",
            "益阳市",
            "郴州市",
            "永州市",
            "怀化市",
            "娄底市",
            "湘西土家族苗族自治州"
        };

        //广东省
        private static readonly List<string> guangdong = new List<string>
        {
            "广州",
            "深圳市",
            "珠海市",
            "汕头市",
            "佛山市",
            "韶关市",
            "湛江市",
            "肇庆市",
            "惠州市",
            "梅州市",
            "汕尾市",
            "河源市",
            "清远市",
            "东莞市",
            "中山市",
            "潮州市",
            "揭阳市",
            "云浮市"
        };

        //广西壮族自治区
        private static readonly List<string> guangxi = new List<string>
        {
            "南宁市",
            "柳州市",
            "桂林市",
            "北海市",
            "防城港市",
            "钦州市",
            "贵港市",
            "玉林市",
            "百色市",
            "河池市",
            "来宾市",
            "崇左市"
        };

        //海南省
        private static readonly List<string> hainan = new List<string>
        {
            "海口市",
            "三亚市",
            "三沙市",
            "儋州市",
            "五指山市",
            "琼海市",
            "文昌市",
            "万宁市",
            "东方市",
            "定安县",
            "屯昌县",
            "澄迈县",
            "临高县",
            "白沙黎族自治县",
            "昌江黎族自治县",
            "乐东黎族自治县",
            "陵水黎族自治县",
            "保亭黎族苗族自治县",
            "琼中黎族苗族自治县"
        };

        //重庆市
        private static readonly List<string> chongqing = new List<string>
        {
            "重庆",
            "渝中区",
            "江北区",
            "南岸区",
            "黔江区",
            "巫山县",
            "巫溪县"
        };

        //四川省
        private static readonly List<string> sichuan = new List<string>
        {
            "四川",
            "成都市",
            "自贡市",
            "攀枝花市",
            "泸州市",
            "德阳市",
            "绵阳市",
            "广元市",
            "遂宁市",
            "内江市",
            "乐山市",
            "南充市",
            "眉山市",
            "宜宾市",
            "广安市",
            "达州市",
            "雅安市",
            "巴中市",
            "资阳市",
            "阿坝藏族羌族自治州",
            "甘孜藏族自治州",
            "凉山彝族自治州"
        };

        //贵州省
        private static readonly List<string> guizhou = new List<string>
        {
            "贵州",
            "贵阳市",
            "六盘水市",
            "遵义市",
            "铜仁市",
            "黔西南布依族苗族自治州",
            "黔东南苗族侗族自治州",
            "黔南布依族苗族自治州"
        };

        //云南省
        private static readonly List<string> yunnan = new List<string>
        {
            "昆明市",
            "曲靖市",
            "玉溪市",
            "保山市",
            "昭通市",
            "丽江市",
            "普洱市",
            "临沧市",
            "楚雄彝族自治州",
            "红河哈尼族彝族自治州",
            "文山壮族苗族自治州",
            "西双版纳傣族自治州",
            "大理白族自治州",
            "德宏傣族景颇族自治州",
            "怒江傈僳族自治州",
            "迪庆藏族自治州"
        };

        //西藏自治区
        private static readonly List<string> xizang = new List<string>
        {
            "拉萨市",
            "林芝市",
            "日喀则市",
            "山南市",
            "那曲市",
            "阿里地区",
            "昌都市",
            "阿里地区",
            "林芝市"
        };

        //陕西省西安
        private static readonly List<string> shanxixian = new List<string>
        {
            "陕西",
            "西安市",
            "铜川市",
            "宝鸡市",
            "咸阳市",
            "渭南市",
            "延安市",
            "汉中市",
            "榆林市",
            "安康市",
            "商洛市"
        };

        //甘肃省
        private static readonly List<string> gansu = new List<string>
        {
            "甘肃",
            "兰州市",
            "嘉峪关市",
            "金昌市",
            "白银市",
            "天水市",
            "武威市",
            "张掖市",
            "平凉市",
            "酒泉市",
            "庆阳市",
            "定西市",
            "陇南市",
            "临夏回族自治州",
            "甘南藏族自治州"
        };

        //青海省
        private static readonly List<string> qinghai = new List<string>
        {
            "青海",
            "西宁市",
            "海东市",
            "海北藏族自治州",
            "黄南藏族自治州",
            "海南藏族自治州",
            "果洛藏族自治州",
            "玉树藏族自治州",
            "海西蒙古族藏族自治州"
        };

        //宁夏回族自治区
        private static readonly List<string> ningxia = new List<string>
        {
            "宁夏回族自治区",
            "银川市",
            "石嘴山市",
            "吴忠市",
            "固原市",
            "中卫市"
        };

        //新疆维吾尔自治区
        private static readonly List<string> xinjiang = new List<string>
        {
            "乌鲁木齐市",
            "克拉玛依市",
            "哈密市",
            "昌吉回族自治州",
            "阿克苏地区",
            "喀什地区",
            "塔城地区",
            "阿勒泰地区",
            "伊犁哈萨克自治州"
        };

        //台湾
        private static readonly List<string> taiwan = new List<string>
        {
            "台北"
        };

        //香港
        private static readonly List<string> xianggang = new List<string>
        {
            "香港"
        };

        private readonly Dictionary<string, List<string>> keys = new Dictionary<string, List<string>>
        {
            {"beijing",beijing},
            {"tianjin",tianjin},
            {"hebei",hebei},
            {"shanxi",shanxi},
            {"neimenggu",neimenggu},
            {"liaoning",liaoning},
            {"jilin",jilin},
            {"heilongjiang",heilongjiang},
            {"shanghai",shanghai},
            {"jiangsu",jiangsu},
            {"zhejiang",zhejiang},
            {"anhui",anhui},
            {"fujian",fujian},
            {"jiangxi",jiangxi},
            {"shandong",shandong},
            {"henan",henan},
            {"hubei",hubei},
            {"hunan",hunan},
            {"guangdong",guangdong},
            {"guangxi",guangxi},
            {"hainan",hainan},
            {"sichuan",sichuan},
            {"guizhou",guizhou},
            {"yunnan",yunnan},
            {"xizang",xizang},
            {"shanxixian",shanxixian},
            {"gansu",gansu},
            {"qinghai",qinghai},
            {"ningxia",ningxia},
            {"xianggang",xianggang},
            {"taiwan",taiwan},
            {"chongqing",chongqing },
            { "xinjiang",xinjiang}
        };

        private readonly Dictionary<string, string> KeyEn = new Dictionary<string, string>
        {
            { "北京市","beijing" },
            { "天津市","tianjin" },
            {"河北省","hebei" },
            {"山西省","shanxi" },
            {"内蒙古自治区","neimenggu" },
            {"辽宁省","liaoning" },
            {"吉林省","jilin" },
            {"黑龙江省","heilongjiang" },
            {"上海市","shanghai" },
            {"江苏省","jiangsu" },
            {"浙江省","zhejiang" },
            {"安徽省","anhui" },
            {"福建省","fujian" },
            {"江西省","jiangxi" },
            {"山东省","shandong" },
            {"河南省","henan" },
            {"湖北省","hubei" },
            {"湖南省","hunan" },
            {"广东省","guangdong" },
            {"广西壮族自治区","guangxi" },
            {"海南省","hainan" },
            {"重庆市","chongqing" },
            {"四川省","sichuan" },
            {"贵州省","guizhou" },
            {"云南省","yunnan" },
            {"西藏自治区","xizang" },
            {"陕西省","shanxixian" },
            {"甘肃省","gansu" },
            {"青海省","qinghai" },
            {"宁夏回族自治区","ningxia" },
            {"新疆维吾尔自治区","xinjiang" },
            {"台湾省","taiwan" },
            {"香港特别行政区","xianggang" }
        };

        public SetComonBoxItemCity(ComboBox cb, string str)
        {
            if (KeyEn[str] != null)
            {
                cb.Items.Clear();
                foreach (var item in keys[KeyEn[str]])
                {
                    cb.Items.Add(item);
                }
                cb.SelectedIndex = 0;
            }
        }
    }
}