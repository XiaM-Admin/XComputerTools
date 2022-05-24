using Qiniu.Http;
using Qiniu.Storage;
using Qiniu.Util;
using System;
using System.IO;
using System.Windows.Forms;

namespace My_Computer_Tools_Ⅱ
{
    internal class OSS_QiNiuSDK : Qiniu_OSS
    {
        public bool is_init = true;

        public OSS_QiNiuSDK(string Bucket, string AccessKey, string SecretKey, string ZoneID, string Domain) : base(Bucket, AccessKey, SecretKey, ZoneID, Domain)
        {
            //检查参数是否齐全
            if (string.IsNullOrEmpty(Bucket) || string.IsNullOrEmpty(AccessKey) || string.IsNullOrEmpty(SecretKey) || string.IsNullOrEmpty(ZoneID) || string.IsNullOrEmpty(Domain))
            {
                is_init = false;
            }
        }

        /// <summary>
        /// 获取所有文件
        /// </summary>
        public void GetListFiles(ListView listView)
        {
            if (!is_init) return;

            Mac mac = new Mac(AccessKey, SecretKey);
            string bucket = Bucket;
            string marker = ""; // 首次请求时marker必须为空
            string prefix = null; // 按文件名前缀保留搜索结果
            string delimiter = null; // 目录分割字符(比如"/")
            int limit = 100; // 单次列举数量限制(最大值为1000)

            Config config = new Config();
            config.Zone = dicZone[ZoneID];
            BucketManager bm = new BucketManager(mac, config);
            do
            {
                ListResult result = bm.ListFiles(bucket, prefix, marker, limit, delimiter);
                if (result.Code == 0) continue;

                foreach (var item in result.Result?.Items)
                {
                    ListViewItem lvi = new ListViewItem();
                    string[] vs = item.Key.Split('/');

                    lvi.Text = vs?[vs.Length - 1];
                    if (vs.Length == 1)
                        lvi.SubItems.Add("/");
                    else if (lvi.Text != "")
                        lvi.SubItems.Add(item.Key.Replace(lvi.Text, ""));
                    else
                        lvi.SubItems.Add(item.Key);

                    if (lvi.Text == "")
                        lvi.Text = "===========";

                    lvi.SubItems.Add(Commands.ByteTostring(item.Fsize));
                    lvi.SubItems.Add(Filetype[item.FileType]);
                    listView.Items.Add(lvi);
                }

                Console.WriteLine(result);
                marker = result?.Result?.Marker;
            } while (!string.IsNullOrEmpty(marker));
        }

        /// <summary>
        /// 上传测试1
        /// </summary>
        public bool UploadFileTest()
        {
            if (!is_init) return false;

            Mac mac = new Mac(AccessKey, SecretKey);
            Random rand = new Random();
            string key = string.Format("UploadFileTest_{0}.dat", rand.Next());

            string tempPath = Path.GetTempPath();
            int rnd = new Random().Next(1, 100000);
            string filePath = tempPath + "resumeFile" + rnd.ToString();
            char[] testBody = new char[4 * 1024 * 1024];
            FileStream stream = new FileStream(filePath, FileMode.Create);
            StreamWriter sw = new StreamWriter(stream, System.Text.Encoding.Default);
            sw.Write(testBody);
            sw.Close();
            stream.Close();

            PutPolicy putPolicy = new PutPolicy
            {
                Scope = Bucket + ":" + key
            };
            putPolicy.SetExpires(3600);
            putPolicy.DeleteAfterDays = 1;
            string token = Auth.CreateUploadToken(mac, putPolicy.ToJsonString());
            Config config = new Config
            {
                Zone = dicZone[ZoneID],
                UseHttps = true,
                UseCdnDomains = false,
                ChunkSize = ChunkUnit.U512K
            };
            FormUploader target = new FormUploader(config);
            HttpResult result = target.UploadFile(filePath, key, token, null);

            Console.WriteLine("form upload result: " + result.ToString());
            File.Delete(filePath);
            if (result.Code == 200)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 上传测试2
        /// </summary>
        /// <returns></returns>
        public bool UploadFileTest2()
        {
            if (!is_init) return false;

            Mac mac = new Mac(AccessKey, SecretKey);
            Random rand = new Random();
            string key = string.Format("UploadFileTest_{0}.dat", rand.Next());

            string tempPath = Path.GetTempPath();
            int rnd = new Random().Next(1, 100000);
            string filePath = tempPath + "resumeFile" + rnd.ToString();
            char[] testBody = new char[4 * 1024 * 1024];
            FileStream stream = new FileStream(filePath, FileMode.Create);
            StreamWriter sw = new StreamWriter(stream, System.Text.Encoding.Default);
            sw.Write(testBody);
            sw.Close();
            stream.Close();

            PutPolicy putPolicy = new PutPolicy
            {
                Scope = Bucket + ":" + key
            };
            putPolicy.SetExpires(3600);
            putPolicy.DeleteAfterDays = 1;
            string token = Auth.CreateUploadToken(mac, putPolicy.ToJsonString());
            Config config = new Config
            {
                Zone = dicZone[ZoneID],
                UseHttps = true,
                UseCdnDomains = true,
                ChunkSize = ChunkUnit.U512K
            };
            FormUploader target = new FormUploader(config);
            PutExtra extra = new PutExtra();
            extra.Version = "v2";
            extra.PartSize = 4 * 1024 * 1024;
            HttpResult result = target.UploadFile(filePath, key, token, extra);
            Console.WriteLine("form upload result: " + result.ToString());
            File.Delete(filePath);
            if (result.Code == 200)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 获取七牛云Key的文件校验码
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        private string GetFileQhash(string Key)
        {
            Mac mac = new Mac(AccessKey, SecretKey);
            Config config = new Config
            {
                Zone = dicZone[ZoneID],
                UseHttps = true,
                UseCdnDomains = true,
            };
            BucketManager manager = new BucketManager(mac, config);
            StatResult statRet = manager.Stat(Bucket, Key);
            if (statRet.Code != (int)HttpCode.OK)
            {
                Console.WriteLine("stat error: " + statRet.ToString());
                return "";
            }
            return statRet.Result.Hash;
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="path">字符Key</param>
        /// <returns></returns>
        public string UpLoadFile(string path)
        {
            if (!File.Exists(path.Replace("|", "")) || !is_init)
                return "文件不存在";
            Mac mac = new Mac(AccessKey, SecretKey);
            string key = string.Format(PathToKey(path));

            //上传检查
            if (Properties.Settings.Default.qnUpFileCheck)
            {
                string FileQhash_Online = GetFileQhash(key);
                string FileQhash_Local = CalcETag(path);
                Console.WriteLine($"文件信息比对 O:{FileQhash_Online} L:{FileQhash_Local}");
                if (FileQhash_Online == FileQhash_Local)
                {
                    Console.WriteLine(path + "文件已经存在");
                    return "文件对比一致，无须上传";
                }
            }
            path = path.Replace("|", "");
            //生成凭证
            PutPolicy putPolicy = new PutPolicy
            {
                Scope = Bucket + ":" + key
            };
            putPolicy.SetExpires(3600);
            string token = Auth.CreateUploadToken(mac, putPolicy.ToJsonString());
            //设置上传配置
            Config config = new Config
            {
                Zone = dicZone[ZoneID],
                UseHttps = true,
                UseCdnDomains = true,
                ChunkSize = ChunkUnit.U512K
            };
            FormUploader target = new FormUploader(config);
            PutExtra extra = new PutExtra();
            extra.Version = "v2";
            extra.PartSize = 4 * 1024 * 1024;
            HttpResult result = target.UploadFile(path, key, token, extra);
            if (result.Code == 200)
                return "1";
            else
                return $"{result.Code}";
        }

        /// <summary>
        /// 上传一个图片文件 后删除图片
        /// </summary>
        /// <param name="imgpath">图片地址</param>
        /// <param name="keypath">上传位置</param>
        /// <returns>返回文件直链链接</returns>
        public string UpLoadFile_One(string imgpath, string keypath)
        {
            Mac mac = new Mac(AccessKey, SecretKey);
            DateTime time = DateTime.Now;
            TimeSpan ts = DateTime.Now - new DateTime(time.Year, time.Month, time.Day, 0, 0, 0, 0);

            string key = keypath + $"/{time.Year}{time.Month}{time.Day}" + Convert.ToInt64(ts.TotalMilliseconds).ToString() + ".png";
            key = key.Replace("//", "/");

            //生成凭证
            PutPolicy putPolicy = new PutPolicy
            {
                Scope = Bucket + ":" + key
            };
            //putPolicy.SetExpires(3600);
            string token = Auth.CreateUploadToken(mac, putPolicy.ToJsonString());
            //设置上传配置
            Config config = new Config
            {
                Zone = dicZone[ZoneID],
                UseHttps = false,
                UseCdnDomains = true,
                ChunkSize = ChunkUnit.U512K
            };
            FormUploader target = new FormUploader(config);
            PutExtra extra = new PutExtra
            {
                Version = "v1",
                PartSize = 4 * 1024 * 1024
            };
            HttpResult result = target.UploadFile(imgpath, key, token, extra);

            if (result.Code == 200)
            {
                /*MarkDown格式
                普通直链链接
                Html格式*/
                File.Delete(imgpath);
                switch (Properties.Settings.Default.qnUpEnd)
                {
                    case "MarkDown格式":
                        return result.Code + $"|![](http://{GetKeyDownloadUrl(key)})";

                    case "普通直链链接":
                        return result.Code + $"|http://{GetKeyDownloadUrl(key)}";

                    case "Html格式":
                        return result.Code + $"|<img scr=\"http://{GetKeyDownloadUrl(key)}\">";

                    default:
                        return result.Code + $"|http://{GetKeyDownloadUrl(key)}";
                }
            }
            else
                return $"{result.Code}|";
        }

        /// <summary>
        /// 获取Key下载链接
        /// </summary>
        /// <returns></returns>
        public string GetKeyDownloadUrl(string key)
        {
            string publicUrl = DownloadManager.CreatePublishUrl(Domain, key);
            Console.WriteLine(publicUrl);
            return publicUrl;
        }
    }
}