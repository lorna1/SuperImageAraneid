using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace SuperAraneid.Helper
{
    public class HttpHelper
    {
        /// <summary>
        /// 获取HTML所有的源代码
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string HtmlCode(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return "";
            }
            try
            {
                //创建一个请求
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(url);
                webReq.KeepAlive = false;
                webReq.Method = "GET";
                webReq.UserAgent = "Mozilla/5.0 (Windows NT 5.1; rv:19.0) Gecko/20100101 Firefox/19.0";
                webReq.ServicePoint.Expect100Continue = false;
                webReq.Timeout = 5000;
                webReq.AllowAutoRedirect = true;//是否允许302
                ServicePointManager.DefaultConnectionLimit = 20;
                //获取响应
                HttpWebResponse webRes = (HttpWebResponse)webReq.GetResponse();
                ////使用GB2312的编码方式 获取响应的文本流             
                //StreamReader sReader = new StreamReader(webRes.GetResponseStream(), System.Text.Encoding.GetEncoding("utf-8"));
                string content = string.Empty;
                using (System.IO.Stream stream = webRes.GetResponseStream())
                {
                    using (System.IO.StreamReader reader = new StreamReader(stream, System.Text.Encoding.GetEncoding("utf-8")))
                    {
                        content = reader.ReadToEnd();
                    }
                }
                webReq.Abort();
                return content;
            }
            catch (Exception)
            {
                return "";
            }

        }

        /// <summary>   
        /// 取得HTML中所有图片的 URL。   
        /// </summary>   
        /// <param name="sHtmlText">HTML代码</param>   
        /// <returns>图片的URL列表</returns>   
        public static List<string> GetHtmlImageUrlList(string url)
        {
            string html = HttpHelper.HtmlCode(url);
            if (string.IsNullOrEmpty(html))
            {
                return new List<string>();
            }
            // 定义正则表达式用来匹配 img 标签   
            Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);

            // 搜索匹配的字符串   
            MatchCollection matches = regImg.Matches(html);
            List<string> sUrlList = new List<string>();

            // 取得匹配项列表   
            foreach (Match match in matches)
                sUrlList.Add(match.Groups["imgUrl"].Value);
            return sUrlList;
        }

        /// <summary>   
        /// 取得HTML中所有图片的 URL。   
        /// </summary>   
        /// <param name="sHtmlText">HTML代码</param>   
        /// <returns>图片的URL列表</returns>   
        public static List<string> GetHtmlImageUrlListV2(string html)
        {
            if (string.IsNullOrEmpty(html))
            {
                return new List<string>();
            }
            // 定义正则表达式用来匹配 img 标签   
            Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);

            // 搜索匹配的字符串   
            MatchCollection matches = regImg.Matches(html);
            List<string> sUrlList = new List<string>();

            // 取得匹配项列表   
            foreach (Match match in matches)
                sUrlList.Add(match.Groups["imgUrl"].Value);
            return sUrlList;
        }

        /// <summary>
        /// 提取页面链接
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static List<string> GetLinks(string url)
        {
            string html = HttpHelper.HtmlCode(url);
            if (string.IsNullOrEmpty(html))
            {
                return new List<string>();
            }
            //匹配http链接
            const string pattern2 = @"http://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";
            Regex r2 = new Regex(pattern2, RegexOptions.IgnoreCase);
            //获得匹配结果
            MatchCollection m2 = r2.Matches(html);
            List<string> links = new List<string>();
            foreach (Match url2 in m2)
            {
                if (StringHelper.CheckUrlIsLegal(url2.ToString()) || !StringHelper.IsPureUrl(url2.ToString()) || links.Contains(url2.ToString()))
                    continue;
                links.Add(url2.ToString());
            }
            //匹配href里面的链接
            const string pattern = @"(?i)<a\s[^>]*?href=(['""]?)(?!javascript|__doPostBack)(?<url>[^'""\s*#<>]+)[^>]*>"; ;
            Regex r = new Regex(pattern, RegexOptions.IgnoreCase);
            //获得匹配结果
            MatchCollection m = r.Matches(html);
            // List<string> links = new List<string>();
            foreach (Match url1 in m)
            {
                string href1 = url1.Groups["url"].Value;
                if (!href1.Contains("http"))
                {
                    href1 = Global.WebUrl + href1;
                }
                if (!StringHelper.IsPureUrl(href1) || links.Contains(href1)) continue;
                links.Add(href1);
            }
            return links;
        }

        /// <summary>
        /// 提取页面链接
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static List<string> GetLinksV2(string html)
        {
            if (string.IsNullOrEmpty(html))
            {
                return new List<string>();
            }
            //匹配http链接
            const string pattern2 = @"http://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";
            Regex r2 = new Regex(pattern2, RegexOptions.IgnoreCase);
            //获得匹配结果
            MatchCollection m2 = r2.Matches(html);
            List<string> links = new List<string>();
            foreach (Match url2 in m2)
            {
                if (StringHelper.CheckUrlIsLegal(url2.ToString()) || !StringHelper.IsPureUrl(url2.ToString()) || links.Contains(url2.ToString()))
                    continue;
                links.Add(url2.ToString());
            }
            //匹配href里面的链接
            const string pattern = @"(?i)<a\s[^>]*?href=(['""]?)(?!javascript|__doPostBack)(?<url>[^'""\s*#<>]+)[^>]*>"; ;
            Regex r = new Regex(pattern, RegexOptions.IgnoreCase);
            //获得匹配结果
            MatchCollection m = r.Matches(html);
            // List<string> links = new List<string>();
            foreach (Match url1 in m)
            {
                string href1 = url1.Groups["url"].Value;
                if (!href1.Contains("http"))
                {
                    href1 = Global.WebUrl + href1;
                }
                if (!StringHelper.IsPureUrl(href1) || links.Contains(href1)) continue;
                links.Add(href1);
            }
            return links;
        }
    }
}
