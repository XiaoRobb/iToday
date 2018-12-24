using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iToday
{

    class TodayDate
    {
        public DateTime today = DateTime.Now;
        public DayOfWeek week = DateTime.Now.DayOfWeek;
        public string todayNular;
        public TodayDate(string todayNular)
        {
            this.todayNular = todayNular;
        }      
    }
}
