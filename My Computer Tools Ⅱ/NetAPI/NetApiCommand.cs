using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using settings = My_Computer_Tools_Ⅱ.Properties.Settings;
using RetNetInfor;

namespace My_Computer_Tools_Ⅱ.NetAPI
{
    /// <summary>
    /// 易游验证整合类
    /// </summary>
    static public class NetApiCommand
    {
        
        private static string Login_url = "https://w.eydata.net/3DB929DF771CB65E";//登陆
        private static string Reg_url = "https://w.eydata.net/E175FCF94C33A77C";//注册
        private static string Exit_url = "https://w.eydata.net/237867D0A291FFE7";//退出登陆
        private static string var_url = "https://w.eydata.net/CE127CC56A218E90";//获取变量
        private static string Tip_url = "https://w.eydata.net/10E3A12E86A50616";//公告
        private static string SetUservar_url = "https://w.eydata.net/289E0844FC828C1D";//设置用户数据
        private static string GetUservar_url = "https://w.eydata.net/18CE68C8A4C7D4E2";//获取用户数据
        private static string Check_url = "https://w.eydata.net/584E774A3FD66625";//检测用户状态
        public static IDictionary<string, string> UserRegin_Up(IDictionary<string, string> dyIn, int api)
        {
            string retVal = string.Empty;
            StringBuilder buffer = new StringBuilder();
            foreach (var key in dyIn.Keys)
            {
                buffer.AppendFormat(buffer.Length > 0 ? "&{0}={1}" : "{0}={1}", key, dyIn[key]);
            }
            string varIn = buffer.ToString();
            int[] mKey = { 2, 107, 14, 176, 177, 189, 116, 230, 16, 187, 196, 88, 117, 50, 210, 210, 222, 87, 113, 145, 63, 186, 78, 94, 207, 206, 94 };
            int mKeyLen = (mKey.Length);
            int varInLen = varIn.Length;
            for (int i = 0; i < varInLen; i++)
            {
                int mCode = varIn[i];
                mCode = (mCode - 90) ^ mKey[i % mKeyLen];
                if (mCode < 0)
                {
                    mCode = -mCode;
                    retVal = retVal + "-";
                }
                retVal = retVal + mCode.ToString("X") + ",";
            }
            byte[] bytes = Encoding.GetEncoding("gbk").GetBytes(retVal);
            string str = Convert.ToBase64String(bytes);
            IDictionary<string, string> dyRet = new Dictionary<string, string>();
            dyRet.Add("p", str);
            dyRet.Add("api", api.ToString());
            return dyRet;
        }
        public static IDictionary<string, string> UserLogin_Up(IDictionary<string, string> dyIn, int api)
        {
            string retVal = string.Empty;
            StringBuilder buffer = new StringBuilder();
            foreach (var key in dyIn.Keys)
            {
                buffer.AppendFormat(buffer.Length > 0 ? "&{0}={1}" : "{0}={1}", key, dyIn[key]);
            }
            string varIn = buffer.ToString();
            int[] mKey = { 136, 142, 225, 97, 72, 13, 85, 243, 94, 87, 129, 27, 65, 5, 128, 200, 88, 10, 135, 91, 55, 40, 79, 20 };
            int mKeyLen = (mKey.Length);
            int varInLen = varIn.Length;
            for (int i = 0; i < varInLen; i++)
            {
                int mCode = varIn[i];
                mCode = (mCode + 122) ^ mKey[i % mKeyLen];
                if (mCode < 0)
                {
                    mCode = -mCode;

                    retVal = retVal + "-";
                }
                retVal = retVal + mCode.ToString("X") + ",";
            }
            byte[] bytes = Encoding.GetEncoding("gbk").GetBytes(retVal);
            string str = Convert.ToBase64String(bytes);
            IDictionary<string, string> dyRet = new Dictionary<string, string>();
            dyRet.Add("p", str);
            dyRet.Add("api", api.ToString());
            return dyRet;
        }
        public static string UserRegin_Down(string varIn)
        {
            string m3 = string.Empty;
            byte[] outputb = Convert.FromBase64String(varIn);
            varIn = Encoding.GetEncoding("gbk").GetString(outputb);
            string[] inS = varIn.Split(',');
            int n = inS.Length;
            int[] mKey = { 178, 97, 185, 35, 60, 227, 119, 87, 90, 231, 72, 242, 131, 151, 13, 243, 199, 148, 64, 140 };
            int mKeyLen = mKey.Length;
            for (int i = 0; i < n; i++)
            {
                string s = inS[i];
                int d = 0;
                if (s[0] == '-')
                {
                    d = Convert.ToInt32(s.Substring(1), 16);
                    d = -d;
                }
                else
                {
                    d = Convert.ToInt32(s, 16);
                }
                m3 = m3 + (char)((d ^ mKey[i % mKeyLen]) + 109);
            }
            return m3;
        }
        public static string UserLogin_Down(string varIn)
        {
            string m3 = string.Empty;
            byte[] outputb = Convert.FromBase64String(varIn);
            varIn = Encoding.GetEncoding("gbk").GetString(outputb);
            string[] inS = varIn.Split(',');
            int n = inS.Length;
            int[] mKey = { 63, 38, 80, 157, 74, 251, 10, 189, 41, 109, 206, 57, 15, 106, 21, 254, 244, 144, 26 };
            int mKeyLen = mKey.Length;
            for (int i = 0; i < n; i++)
            {
                string s = inS[i];
                int d = 0;
                if (s[0] == '-')
                {
                    d = Convert.ToInt32(s.Substring(1), 16);
                    d = -d;
                }
                else
                {
                    d = Convert.ToInt32(s, 16);
                }
                m3 = m3 + (char)((d ^ mKey[i % mKeyLen]) - 192);
            }
            return m3;
        }
        public static bool Net_Login(string UserName,string UserPwd)
        {
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            try
            {
                string code = settings.Default.LoadCode;
                var upName = settings.Default.LoadName;
                if (code.Length > 0 && upName.Length > 0)
                {
                    parameters.Add("StatusCode", code);
                    parameters.Add("UserName", upName);

                    WebPost.ApiPost(Exit_url, parameters);

                    parameters.Clear();
                }

                //  这里改成自己的参数名称
                parameters.Add("UserName", UserName.Trim());
                parameters.Add("UserPwd", UserPwd);
                parameters.Add("Version", settings.Default.ProgramVer);
                parameters.Add("Mac", "");
                //37413
                parameters = UserLogin_Up(parameters, 37413);
                string ret = WebPost.ApiPost(Login_url, parameters);
                ret = UserLogin_Down(ret);
                string[] vs = ret.Split('|');

                if (vs[0].Length == 32)
                {
                    settings.Default.LoadCode = vs[0];
                    settings.Default.LoadName = UserName;
                    settings.Default.LoadPwd = UserPwd;
                    settings.Default.Save();
                    return true;
                }
                else
                {
                    NetInfor netInfor = new NetInfor(vs[0]);
                    MessageBox.Show("登陆失败, " + netInfor.Ret_Ch, "错误 Time:" + vs[1]);
                }

            }
            catch (Exception)
            {
                MessageBox.Show("网络连接失败!");
            }

            return false;
        }
        public static bool Net_Reg(string RegUserName,string RegPwd,string RegEmail)
        {
            IDictionary<string, string> parameters = new Dictionary<string, string>();

            try
            {
                GetComputerInfo computerInfo = new GetComputerInfo();


                parameters.Add("UserName", RegUserName.Trim());
                parameters.Add("UserPwd", RegPwd);
                parameters.Add("Email", RegEmail);
                parameters.Add("Mac", computerInfo.MacAddress);

                parameters = UserRegin_Up(parameters, 37414);
                var ret = WebPost.ApiPost(Reg_url, parameters);
                ret = UserRegin_Down(ret);

                string[] vs = ret.Split('|');
                if (vs[0] == "1")
                {
                    MessageBox.Show("注册成功!");
                    return true;
                }
                else
                {
                    NetInfor netInfor = new NetInfor(vs[0]);
                    
                    MessageBox.Show("注册失败, " + netInfor.Ret_Ch,"错误 Time:"+ vs[1]);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("网络连接失败!");
            }

            return false;
        }
        public static string Net_GetGG()
        {
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            try
            {
                parameters.Add("", "");
                var ret = WebPost.ApiPost(Tip_url, parameters);
                return ret;
            }
            catch (Exception)
            {
                return "暂无";
            }

        }
        public static string Net_CheckUser(string code="",string user="")
        {
            if (code=="")
                code = settings.Default.LoadCode;
            if (user=="")
                user = settings.Default.LoadName;
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            try
            {
                parameters.Add("StatusCode", code);
                parameters.Add("UserName", user);
                var ret = WebPost.ApiPost(Check_url, parameters);
                if (ret =="1")
                    return ret;
                else
                {
                    settings.Default.Save();
                    return ret;
                }
            }
            catch (Exception)
            {
                return "0";
            }

            
        }
        /// <summary>
        /// 退出登陆
        /// </summary>
        /// <returns></returns>
        public static bool Net_Exit(string code = "", string user = "")
        {
            if (code == "")
                code = settings.Default.LoadCode;
            if (user == "")
                user = settings.Default.LoadName;
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            try
            {
                parameters.Add("StatusCode", code);
                parameters.Add("UserName", user);
                var ret = WebPost.ApiPost(Exit_url, parameters);
                if (ret == "1")
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

    }
}
