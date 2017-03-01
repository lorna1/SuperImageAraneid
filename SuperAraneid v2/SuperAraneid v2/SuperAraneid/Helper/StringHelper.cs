using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SuperAraneid.Helper
{
    public class StringHelper
    {
        public static bool CheckUrl(string url)
        {
            if (url.Equals("#"))
                return false;
            return true;
        }

        public static string GetPureUrl(string url)
        {
            Uri uri = new Uri(url);
            return "http://" + uri.Authority;
        }

        public static bool IsPureUrl(string url)
        {
            try
            {
                return new Uri(url).Authority.Equals(new Uri(Global.WebUrl).Authority);
            }
            catch (Exception)
            {

                return false;
            }
           
        }

        public static bool CheckUrlIsLegal(string url)
        {
            return !url.Contains("http") || url.Contains("js") || url.Contains("css") || url.Contains("jpg");
        }
    }
}
