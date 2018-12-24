using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace iToday
{
    public class TodayWether
    {
        private static Dictionary<string, string> imageDictonary = new Dictionary<string, string>();
        public string Wether { set; get; }
        public string Temperature { set; get; }
        public string Wind { set; get; }

        public string ImageUrl;
        public TodayWether(string todayWether, string todayTemper, string todayWind)
        {
            Wether = todayWether;
            Temperature = todayTemper;
            Wind = todayWind;
            imageDictonary.Add("晴", "../../Resource//sun.png");
            imageDictonary.Add("雨", "../../Resource//rain.png");
            imageDictonary.Add("多云", "../../Resource//duoyun.png");
            imageDictonary.Add("阴", "../../Resource//yin.png");
            imageDictonary.Add("雷电", "../../Resource//thunder.png");
            imageDictonary.Add("雪", "../../Resource//snow.png");
            setImg();
        }

        private void setImg()
        {
            foreach(string reg in imageDictonary.Keys)
            {
                Regex r = new Regex(reg, RegexOptions.IgnoreCase);
                Match match = r.Match(Wether);
                if (match.Success)
                {
                    ImageUrl = imageDictonary[reg];
                    return ;
                }
            }
            ImageUrl = null;
        }
    }
}
