using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace iToday
{
    
    class TodayDateCrawler
    {
        public TodayDate todayDate;
        private readonly string urlstart = "http://www.lssdjt.com/";                //爬取的网站
        public bool Crawl()                     //开始爬取
        {
            try
            {
                string strRegex = @"<h4><b>今天是[\s\S]*?\)</b></h4>";
                string url = HttpHelper.GetInformation(urlstart, strRegex); //爬取网站的Html,并定位于有效信息
                string temp;
                strRegex = @"\([\s\S]*?\)";
                Regex regex = new Regex(strRegex, RegexOptions.IgnoreCase);
                temp = regex.Match(url).ToString();
                todayDate = new TodayDate(temp);
                return true;                                                //获取成功
            }
            catch (Exception)
            {
                return false;                                               //获取失败
            }
        }
    }
}
