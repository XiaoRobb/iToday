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

    public class TodayHotCrawler
    {
        private List<string> Url = new List<string>();                              //今日热点事件的链接             
        private List<string> ImageUrl = new List<string>();                         //今日热点事件的配图链接
        private List<String> HistoryThing = new List<string>();                         //今日热点事件的文字说明
        public List<TodayHotPoint> list = new List<TodayHotPoint>();                //今日热点事件
        private readonly string urlstart = "http://www.szhk.com/";                           //爬取的网站
        public bool Crawl()                     //开始爬取
        {
            try
            {
                string strRegex = @"<div id=""fsD1"" class=""focus"">[\s\S]*?<div class=""fbg"">";
                string html = HttpHelper.GetInformation(urlstart, strRegex);           //爬取网站的Html,并定位于有效信息

                string temp = @"<a target=""_blank"" href=""";
                strRegex = @"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";
                strRegex = temp + strRegex;
                Url = HttpHelper.getUrls(html, strRegex, temp.Length);                              //获取今日热点事件的链接 


                ImageUrl = HttpHelper.getImageUrls(html);                    //获取今日热点事件的配图链接

                string temp1 = @".html"">[^<]";
                string temp2 = @"</a>";
                strRegex = @"[\s\S]*?";
                strRegex = temp1 + strRegex + temp2;
                HistoryThing = HttpHelper.getTitle(html, strRegex, 7, 10);                        //获取热点事件的文字说明
                for (int i = 0; i < Url.Count && i < ImageUrl.Count; i++)
                {
                    //创建一个今日热点事件  （一共一般5个）
                    list.Add(new TodayHotPoint(HistoryThing.ElementAt(i), ImageUrl.ElementAt(i), Url.ElementAt(i)));
                }
                return true;                                                //获取成功
            }
            catch (Exception)
            {
                return false;                                               //获取失败
            }
        }


    }
}
