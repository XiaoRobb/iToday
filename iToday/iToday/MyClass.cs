using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iToday
{

    public class MyClass
    {
        [Key]
        public string LessonName { set; get; }                       //课程名
        public string Day { set; get; }                              //课程名
        public string TeacherName { set; get; }                      //老师姓名
        public string BeginTime { set; get; }                        //开始时间
        public string EndTime { set; get; }                          //结束时间
        public string BeginWeek { set; get; }                        //开始周
        public string EndWeek { set; get; }                          //结束周
        public string Detail { set; get; }                           //信息

        public static DateTime startDay = Convert.ToDateTime("2018-9-2 0:00:00");//开学日期

        public static string nowWeek =((DateTime.Now - startDay).Days/7+1).ToString();//当前周
        public MyClass(string lessonName, string day,string teacherName, string beginTime, string endTime, string detail, string beginWeek, string endWeek)
        {
            this.LessonName = lessonName;
            this.Day = day;
            this.TeacherName = teacherName;
            this.BeginTime = beginTime;
            this.EndTime = endTime;
            this.Detail = detail;
            this.BeginWeek = beginWeek;
            this.EndWeek = endWeek;
        }
        public MyClass()
        {

        }
    }
}
