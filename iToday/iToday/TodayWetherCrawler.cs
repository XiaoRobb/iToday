using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace iToday
{

    public class TodayWetherCrawler
    {
        public TodayWether wether;
        private readonly string urlstart = "http://www.weather.com.cn/weather/101200101.shtml";                //爬取的网站
        public bool Crawl()                     //开始爬取
        {
            try
            {
                string strRegex = @"<h1>[0-9]*?日（今天）</h1>[\s\S]*?<h1>[0-9]*?日（明天）</h1>";
                string url = HttpHelper.GetInformation(urlstart, strRegex); //爬取网站的Html,并定位于有效信息

                strRegex = @"""wea"">[\s\S]*?<";
                Regex r = new Regex(strRegex, RegexOptions.IgnoreCase);
                Match match = r.Match(url);
                string str = match.ToString();
                string todayWether = str.Substring(6, str.Length-6-1);

                strRegex = @"-?[0-9]*℃";
                r = new Regex(strRegex, RegexOptions.IgnoreCase);
                match = r.Match(url);
                str = match.ToString();
                string todayTemper = str;

                strRegex = @"<span title=""[\s\S]*?""";
                r = new Regex(strRegex, RegexOptions.IgnoreCase);
                match = r.Match(url);
                str = match.ToString();
                string todayWind = str.Substring(13, str.Length-13-1);


                wether = new TodayWether(todayWether, todayTemper, todayWind);
                return true;                                                //获取成功
            }
            catch (Exception)
            {
                return false;                                               //获取失败
            }
        }


    }
}
