using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuperAraneid.Helper
{
    public class DownloadHelper
    {
        /// <summary>
        /// 下载指定图片
        /// </summary>
        /// <param name="url">图片url</param>
        /// <returns>图片名称</returns>
        public string DownLoadImage(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = 2000;
                request.Method = "GET";
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.154 Safari/537.36 LBBROWSER";
                request.ServicePoint.Expect100Continue = false;
                request.AllowAutoRedirect = true;//是否允许302
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream reader = response.GetResponseStream())
                    {
                        string aFirstName = Guid.NewGuid().ToString();  //文件名
                        string suffix = this.GetSuffix(url);
                        string fileUrl = string.Format("{0}{1}.{2}", Global.FloderUrl, aFirstName, suffix);
                        using (FileStream writer = new FileStream(fileUrl, FileMode.OpenOrCreate, FileAccess.Write))
                        {
                            byte[] buff = new byte[512];

                            int c = 0; //实际读取的字节数
                            while ((c = reader.Read(buff, 0, buff.Length)) > 0)
                            {
                                writer.Write(buff, 0, c);
                            }
                            return url;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

        }

        /// <summary>
        /// 获取文件后缀
        /// </summary>
        /// <param name="url">地址</param>
        /// <returns>文件后缀</returns>
        private string GetSuffix(string url)
        {
            Uri u = new Uri(url);

            string[] arrayurl = u.PathAndQuery.Split('?');
            if (arrayurl.Length > 0)
            {
                string[] pathArr = arrayurl[0].Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

                string[] suffxArr = pathArr[pathArr.Length - 1].Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                return suffxArr[suffxArr.Length - 1];
            }
            return "";
        }
    }
}
