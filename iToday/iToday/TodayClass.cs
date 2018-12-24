using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iToday
{

    public class TodayClass
    {
        public string lessonName;                       //课程名
        public string teacherName;                      //老师姓名
        public string beginTime;                        //开始时间
        public string endTime;                          //结束时间
        public string beginWeek;                        //开始周
        public string endWeek;                          //结束周
        public string detail;                           //信息
        public static string nowWeek;
        public TodayClass(string lessonName, string teacherName, string beginTime, string endTime, string detail, string beginWeek, string endWeek)
        {
            this.lessonName = lessonName;
            this.teacherName = teacherName;
            this.beginTime = beginTime;
            this.endTime = endTime;
            this.detail = detail;
            this.beginWeek = beginWeek;
            this.endWeek = endWeek;
        }
    }
}
