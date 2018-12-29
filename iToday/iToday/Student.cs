using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iToday
{
    public class Student
    {
        [Key]
        public string Id { set; get; }              //学号
        public string Pwd { set; get; }             //密码

        public List<MyClass> Classes { set; get; }

        public bool IsUser { set; get; }

        public List<MyClass> todayClasses = new List<MyClass>();
        public Student()
        {
            Classes = new List<MyClass>();
        }
        public Student(string id,string pwd,bool isUser,List<MyClass> myClasses)
        {
            Id = id;
            Pwd = pwd;
            IsUser = isUser;
            Classes = myClasses;
        }

        public void GetTodayClasses()
        {
            int w = 0;
            DayOfWeek week = DateTime.Now.DayOfWeek;
            switch (week)
            {
                case DayOfWeek.Monday:
                    w = 1;
                    break;
                case DayOfWeek.Tuesday:
                    w = 2;
                    break;
                case DayOfWeek.Wednesday:
                    w = 3;
                    break;
                case DayOfWeek.Thursday:
                    w = 4;
                    break;
                case DayOfWeek.Friday:
                    w = 5;
                    break;
                case DayOfWeek.Saturday:
                    w = 6;
                    break;
                case DayOfWeek.Sunday:
                    w = 7;
                    break;
            }
            foreach (MyClass myclass in Classes)
            {
                if (string.Compare(MyClass.nowWeek, myclass.EndTime) == 1 || string.Compare(MyClass.nowWeek, myclass.BeginTime) == -1)
                {
                    
                }else
                {                   
                    int tmp = int.Parse(myclass.Day);
                    if (tmp==w)
                        todayClasses.Add(myclass);
                }
            }
        }
    }
}
