using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace My_Computer_Tools_Ⅱ
{
    public class WebPost
    {
        private static readonly string DefaultUserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/100.0.4896.127 Safari/537.36";

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain,
            SslPolicyErrors errors)
        {
            return true; //总是接受
        }

        public static string ApiPost(string url, Dictionary<string, string> parameters)
        {
            //重试3次
            int recount = 0;
            while (recount < 3)
                try
                {
                    Encoding charset = Encoding.UTF8;

                    HttpWebRequest request = null;
                    //HTTPSQ请求
                    ServicePointManager.ServerCertificateValidationCallback = CheckValidationResult;
                    request = (HttpWebRequest)WebRequest.Create(url.Trim());
                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.UserAgent = DefaultUserAgent;
                    request.Accept = "*/*";
                    request.Expect = null;
                    //如果需要POST数据
                    if (!(parameters == null || parameters.Count == 0))
                    {
                        var buffer = new StringBuilder();
                        foreach (var key in parameters.Keys)
                        {
                            string str = System.Web.HttpUtility.UrlEncode(parameters[key]);
                            buffer.AppendFormat(buffer.Length > 0 ? "&{0}={1}" : "{0}={1}", key, str);
                        }

                        byte[] data = charset.GetBytes(buffer.ToString());
                        request.ContentLength = data.Length;
                        request.GetRequestStream().Write(data, 0, data.Length);
                    }
                    HttpWebResponse response;
                    try
                    {
                        response = (HttpWebResponse)request.GetResponse();
                    }
                    catch (WebException ex)
                    {
                        response = (HttpWebResponse)ex.Response;
                        recount++;
                        continue;
                    }
                    Stream htmlStream = response.GetResponseStream();

                    StreamReader sr = new StreamReader(htmlStream, Encoding.UTF8);

                    var html = sr.ReadToEnd();

                    return html;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"api:{url} 异常：{e.Message}");
                    recount++;
                    continue;
                }
            return null;
        }

        /// <summary>
        /// GET请求与获取结果
        /// </summary>
        public static string ApiGet(string Url, IDictionary<string, string> parameters)
        {
            try
            {
                string data = "";// "UserName=admin&Password=123";
                if (parameters != null)
                    foreach (var item in parameters)
                    {
                        data += $"{item.Key}={item.Value}&";
                    }
                if (data.Contains("&"))
                    data = data.Remove(data.Length - 1, 1);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + "?" + data);
                request.Method = "GET";
                request.ContentType = "application/json; charset=utf-8";//"text/json; charset=utf-8";
                request.UserAgent = DefaultUserAgent;

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();

                return retString;
            }
            catch (Exception e)
            {
                Console.WriteLine($"api:{Url} 异常：{e.Message}");
                return null;
            }
        }

        /// <summary>
        /// 单线程下载文件
        /// </summary>
        /// <param name="Url">直链</param>
        /// <param name="path">下载路径</param>
        /// <returns></returns>
        public static void IThreadDownloadFile(object obj, System.ComponentModel.DoWorkEventArgs e)
        {
            float percent = 0;
            Downdata downdata = (Downdata)e.Argument;

            try
            {
                HttpWebRequest Myrq = (HttpWebRequest)WebRequest.Create(downdata.Url);
                downdata.backgroundWorker.ReportProgress((int)percent, "正在获取链接数据");
                HttpWebResponse myrp = (HttpWebResponse)Myrq.GetResponse();
                long totalBytes = myrp.ContentLength;

                Stream st = myrp.GetResponseStream();
                Stream so = new FileStream(downdata.path, FileMode.Create);
                long totalDownloadedByte = 0;
                byte[] by = new byte[1024];
                int osize = st.Read(by, 0, (int)by.Length);
                while (osize > 0)
                {
                    totalDownloadedByte = osize + totalDownloadedByte;
                    so.Write(by, 0, osize);
                    osize = st.Read(by, 0, (int)by.Length);
                    percent = (float)totalDownloadedByte / (float)totalBytes * 100;
                    Console.WriteLine("已下载：" + percent.ToString("0.00") + "%");
                    downdata.backgroundWorker.ReportProgress((int)percent, "已下载：" + percent.ToString("0.00") + "%");
                }

                so.Close();
                st.Close();
                dynamic dynamic = new
                {
                    retbool = true,
                };
                e.Result = dynamic;
            }
            catch (Exception ee)
            {
                dynamic dynamic = new
                {
                    retbool = false,
                    msg = ee.Message
                };
                e.Result = dynamic;
            }
        }

        public class Downdata
        {
            public string Url;
            public string path;
            public bool OpenProg = false;
            public BackgroundWorker backgroundWorker;
        }
    }
}