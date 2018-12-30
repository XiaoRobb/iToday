using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iToday
{
    public class TodayHistory
    {
        public string HistoryThing { set; get; }                //今日热点事件文字说明
        public string ImageUrl { set; get; }                //今日热点事件配图链接
        public string Url { set; get; }                     //今日热点事件链接

        public TodayHistory(string hotThing, string image, string url)     //构造函数
        {
            HistoryThing = hotThing;
            ImageUrl = image;
            Url = url;
        }
    }
}
