using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iToday
{
    class Wether
    {
        public static string sunny = "晴";
        public static string rainy = "雨";
        public string TodayWether { set; get; }
        public string OtherInformation { set; get; }
        public Wether(string todayWether, string otherInformation)
        {
            TodayWether = todayWether;
            OtherInformation = otherInformation;
        }

    }
}
