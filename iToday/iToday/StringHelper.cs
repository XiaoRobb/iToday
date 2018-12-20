using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iToday
{
    public class StringHelper
    {

        public static bool CheckUrl(string url)
        {
            if (url.Equals("#"))
                return false;
            return true;
        }
        //初始化链接
        public static string GetPureUrl(string url)
        {
            Uri uri = new Uri(url);
            return "http://" + uri.Authority;
        }
        public static bool IsPureUrl(string url)
        {
            try
            {
                return new Uri(url).Authority.Equals(new Uri("").Authority);
            }
            catch (Exception)
            {
                return false;
            }
        }
        //判断链接是网址还是图片或样式
        public static bool CheckUrlIsLegal(string url)
        {
            return !url.Contains("http") || url.Contains("js") || url.Contains("css") || url.Contains("jpg");
        }

    }
}
