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
        public List<Student> GetAll()
        {
            return students;
        }
        public void AddStudent(Student student)
        {
            students.Add(student);
        }
        //Linq 
        public  List<Student> SearchByName(string keyword)
        {
            return students.Where(s => s.Name.ToLower().Contains(keyword.ToLower())).ToList();
        }
        public bool UpdateStudent(Student student)
        {
            var existing = students.FirstOrDefault(s => s.Id == student.Id);
            if (existing != null)
            {
                existing.Name = student.Name;
                existing.Age = student.Age;
                existing.Major = student.Major;
                return true;
            }
            return false;
        }
        public bool DeleteStudent(Student student)
        {
            var existing = students.FirstOrDefault(s => s.Id == student.Id);
            if (existing != null)
            {
                students.Remove(existing);
                return true;
            }
            return false;
        }

    }
}
