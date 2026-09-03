using System;
using System.Collections.Generic;
using QuanLySinhVien.entities;
using System.Linq;

namespace QuanLySinhVienn.DAL
{
    public class Studentdata
    {
       private List<Student> students;
       public Studentdata() 
        {
            students = new List<Student>();
        }
        private List<Student> GetAll()
        {
            return students;
        }
        public void AddStudent(Student student)
        {
            students.Add(student);
        }
        //Linq cho truy tim du lieu thay cho vong lap for
        public  List<Student> SearchByName(string keyword)
        {
            return students.Where(s => s.Name.ToLower().Contains(keyword.ToLower())).ToList();
        }
        public bool UpdateStudent(Student student)
        { 
        }
        public bool DeleteStudent(Student student)
        {
        }

    }
}
