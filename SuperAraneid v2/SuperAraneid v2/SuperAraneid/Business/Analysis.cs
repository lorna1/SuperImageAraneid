using SuperAraneid.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperAraneid.Business
{
    public class Analysis
    {

        /// <summary>
        /// 分析该URL下面所有的url
        /// </summary>
        /// <param name="url">原始URL</param>
        /// <returns>URL列表</returns>
        public static void AnalysisUrlList(string url, Action<List<string>> PushWaitUrlList, Action<List<string>> PushWaitImagesList)
        {
            string originalHtml = HttpHelper.HtmlCode(url);

            //// 解析原始的url
            PushWaitUrlList(HttpHelper.GetLinksV2(originalHtml));

            //// 解析原始的图片URL
            PushWaitImagesList(HttpHelper.GetHtmlImageUrlListV2(originalHtml));
        }
    }
}
