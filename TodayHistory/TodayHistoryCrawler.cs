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

    public class TodayHistoryCrawler
    {
        private List<string> Url = new List<string>();                              //今日热点事件的链接             
        private List<string> ImageUrl = new List<string>();                         //今日热点事件的配图链接
        private List<String> HotThing = new List<string>();                         //今日热点事件的文字说明
        public List<TodayHistory> list = new List<TodayHistory>();                //今日热点事件
        private readonly string urlstart = "http://www.lssdjt.com/";                //爬取的网站
        public bool Crawl()                     //开始爬取
        {
            try
            {
                string strRegex = @"<div  class=""current"">[\s\S]*?<div class=""box mt5 p5 clearfix"">";
                string url = HttpHelper.GetInformation(urlstart, strRegex); //爬取网站的Html,并定位于有效信息


                string temp = @"<p><a href=""";
                strRegex = @"/[a-zA-Z]/[0-9]*.htm";
                strRegex = temp + strRegex;
                Url = HttpHelper.getUrls(url, strRegex, temp.Length);       //获取今日热点事件的链接 


                ImageUrl = HttpHelper.getImageUrls(url);                    //获取今日热点事件的配图链接

                string temp1 = @"_blank"">[^<]";
                string temp2 = @"</a>";
                strRegex = @"[\s\S]*?";
                strRegex = temp1 + strRegex + temp2;
                HotThing = HttpHelper.getTitle(url, strRegex, 8, 11);                        //获取热点事件的文字说明
                for (int i = 0; i < Url.Count && i < ImageUrl.Count; i++)
                {
                    //创建一个今日热点事件  （一共一般5个）
                    list.Add(new TodayHistory(HotThing.ElementAt(i), ImageUrl.ElementAt(i), urlstart + Url.ElementAt(i)));
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
