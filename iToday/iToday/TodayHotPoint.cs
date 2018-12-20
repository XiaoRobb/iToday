using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iToday
{
    public class TodayHotPoint
    {
        public string HotThing { set; get; }                //今日热点事件文字说明
        public string ImageUrl { set; get; }                //今日热点事件配图链接
        public string Url { set; get; }                     //今日热点事件链接

        public TodayHotPoint(string hotThing, string image, string url)     //构造函数
        {
            HotThing = hotThing;
            ImageUrl = image;
            Url = url;
        }
    }
}
