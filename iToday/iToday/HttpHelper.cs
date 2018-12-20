using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Xml;
using System.Collections;

namespace iToday
{
    public class HttpHelper                             //链接处理
    {

        /// <summary>
        /// 获取HTML所有的源代码
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string HtmlCodeRequest(string Url)
        {
            if (string.IsNullOrEmpty(Url))
            {
                return "";
            }
            try
            {
                //创建一个请求
                HttpWebRequest httprequst = (HttpWebRequest)WebRequest.Create(Url);
                //不建立持久性链接
                httprequst.KeepAlive = true;
                //设置请求的方法
                httprequst.Method = "GET";
                //设置标头值
                httprequst.UserAgent = "User-Agent:Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705";
                httprequst.Accept = "*/*";
                httprequst.Headers.Add("Accept-Language", "zh-cn,en-us;q=0.5");
                httprequst.ServicePoint.Expect100Continue = false;
                httprequst.Timeout = 5000;
                httprequst.AllowAutoRedirect = true;//是否允许302
                ServicePointManager.DefaultConnectionLimit = 30;
                //获取响应
                HttpWebResponse webRes = (HttpWebResponse)httprequst.GetResponse();
                //获取响应的文本流
                string content = string.Empty;
                using (System.IO.Stream stream = webRes.GetResponseStream())
                {
                    using (System.IO.StreamReader reader = new StreamReader(stream, System.Text.Encoding.GetEncoding("utf-8")))
                    {
                        content = reader.ReadToEnd();
                    }
                }
                //取消请求
                httprequst.Abort();
                //返回数据内容
                return content;
            }
            catch (Exception)
            {
                return "";
            }
        }
        public static string GetInformation(string html, string strRegex)
        {
            string url = HttpHelper.HtmlCodeRequest(html);
            Regex r = new Regex(strRegex, RegexOptions.IgnoreCase);
            Match match = r.Match(url);
            string shorthtml = match.ToString();
            return shorthtml;
        }
        public static List<string> getUrls(string url, string strRegex, int n)
        {
            if (string.IsNullOrEmpty(url))
            {
                return new List<string>();
            }
            Regex r = new Regex(strRegex, RegexOptions.IgnoreCase);
            MatchCollection matchs = r.Matches(url);
            List<string> links = new List<string>();
            foreach (Match url2 in matchs)
            {
                string xx = url2.ToString().Substring(n);
                links.Add(xx);
            }
            return links;
        }

        public static List<string> getImageUrls(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return new List<string>();
            }
            // 定义正则表达式用来匹配 img 标签   
            Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);

            // 搜索匹配的字符串   
            MatchCollection matches = regImg.Matches(url);
            List<string> sUrlList = new List<string>();

            // 取得匹配项列表   
            foreach (Match match in matches)
                sUrlList.Add(match.Groups["imgUrl"].Value);
            return sUrlList;
        }

        public static List<string> getTitle(string url, string strRegex, int start, int length)
        {
            if (string.IsNullOrEmpty(url))
            {
                return new List<string>();
            }
            Regex r = new Regex(strRegex, RegexOptions.IgnoreCase);
            MatchCollection matchs = r.Matches(url);
            List<string> hotThings = new List<string>();
            foreach (Match url2 in matchs)
            {
                string str = url2.ToString();
                string xx = str.Substring(start, str.Length - 1 - length);
                hotThings.Add(xx);
            }
            return hotThings;
        }
    }
}
