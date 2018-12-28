using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iToday
{
    public class StudentDB:DbContext
    {
        public StudentDB() : base("StudentDataBase") { }//指定connectString
        public DbSet<Student> StudentsDB { get; set; }    //可以用来访问Student表
        public DbSet<MyClass> MyClassesDB { get; set; }  //可以用来访问Class表
    }
}
