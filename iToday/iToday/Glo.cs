using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iToday
{
    class Glo
    {
        public static List<Student> students;
        public static Student user;

        public static void GloInit()
        {
            GetAllStudents();
            user = students.Where(student => student.IsUser).FirstOrDefault();
        }

        public static void GetAllStudents()
        {
            using (var db = new StudentDB())
            {
                students = db.StudentsDB.Include("Classes").ToList<Student>();
            }
        }

        public static bool AddStudnet(Student student)
        {
            try
            {
                using (var db = new StudentDB())
                {
                    db.StudentsDB.Add(student);
                    db.SaveChanges();
                }
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public static bool DeletStudnet(Student student)
        {
            try
            {
                using (var db = new StudentDB())
                {
                    Student student1 = db.StudentsDB.Include("Classes").SingleOrDefault(mystudent => mystudent.Id == student.Id&&mystudent.Pwd==student.Pwd);
                    db.MyClassesDB.RemoveRange(student1.Classes);
                    db.SaveChanges();
                    db.StudentsDB.Remove(student1);
                    db.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool UpdateStudnet(Student student)
        {
            try
            {
                using (var db = new StudentDB())
                {
                    db.StudentsDB.Attach(student);
                    db.Entry(student).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static Student FindStudent(String id,String pwd)
        {
            try
            {
                using (var db = new StudentDB())
                {
                    Student student = db.StudentsDB.SingleOrDefault(mystudent => mystudent.Id == id && mystudent.Pwd == pwd);
                    return student;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
