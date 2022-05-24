﻿using System;
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

        //"Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain,
            SslPolicyErrors errors)
        {
            return true; //总是接受
        }

        public static string ApiPost(string url, IDictionary<string, string> parameters)
        {
            try
            {
                Encoding charset = Encoding.UTF8;
                HttpWebRequest request = null;
                //HTTPSQ请求
                ServicePointManager.ServerCertificateValidationCallback = CheckValidationResult;
                request = WebRequest.Create(url.Trim()) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
                request.Method = "POST";
                request.ContentType = "application/json;charset=utf-8";
                request.UserAgent = DefaultUserAgent;

                //如果需要POST数据
                if (!(parameters == null || parameters.Count == 0))
                {
                    var buffer = new StringBuilder();
                    foreach (var key in parameters.Keys)
                    {
                        buffer.AppendFormat(buffer.Length > 0 ? "&{0}={1}" : "{0}={1}", key, parameters[key]);
                    }
                    byte[] data = charset.GetBytes(buffer.ToString());
                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                }
                HttpWebResponse response = (request.GetResponse() as HttpWebResponse);

                Stream htmlStream = response.GetResponseStream();

                StreamReader sr = new StreamReader(htmlStream);

                var html = sr.ReadToEnd();

                return html;
            }
            catch (Exception e)
            {
                Console.WriteLine($"api:{url} 异常：{e.Message}");
                return null;
            }
        }

        /// <summary>
        /// GET请求与获取结果
        /// </summary>
        public static string ApiGet(string Url, IDictionary<string, string> parameters)
        {
            try
            {
                string data = "";// "UserName=admin&Password=123";
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