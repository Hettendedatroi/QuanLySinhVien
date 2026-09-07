using System;
using System.Collections.Generic;
using QuanLySinhVien.entities;
using QuanLySinhVienn.DAL;

namespace QuanLySinhVien.BLL
{
    public class StudentBLL
    {
        private Studentdata _dal;
        public StudentBLL() 
        {
            _dal = new Studentdata();
        }
        public  List<Student> GetAll()
        {
            return _dal.GetAll();
        }
        public bool AddStudent(Student student)
        {
            if (student.Id < 0)
            {
                Console.WriteLine("Id của sinh viên không hợp lệ");
                return false;
            }
            if (string.IsNullOrWhiteSpace(student.Name)) 
            {
                Console.WriteLine("Không được để trống phần tên");
                return false;
            }
            if (student.Age < 16 || student.Age > 100)
            {
                Console.WriteLine("Tuổi sinh viên không hợp lệ");
                return false;
            }
             var allStudents =  _dal.GetAll();
            if (allStudents.Exists(s => s.Id == student.Id))
            {
                Console.WriteLine("Id này đã tồn tại");
                return false;
            }    
            _dal.AddStudent(student);
            return true;
        }
        public bool UpdateStudent(Student student)
        {
            if (string.IsNullOrWhiteSpace(student.Name))
            {
                Console.WriteLine("Không được để trống phần tên");
                return false;
            }
            if (student.Age < 16 || student.Age > 100)
            {
                Console.WriteLine("Tuổi sinh viên không hợp lệ");
                return false;
            }
            return _dal.UpdateStudent(student);
        }
        public bool DeleteStudent(Student student)
        {
            if (student.Id <= 0)
            {
                Console.WriteLine("Id: Không Hợp lệ");
                return false;
            }
            var allStudents = _dal.GetAll();

            var studentToDelete = allStudents.Find(s => s.Id == student.Id);

            if (studentToDelete == null)
            {
                Console.WriteLine("Không tìm thấy id sinh viên trong hệ thống");
                return false;
            }
            return _dal.DeleteStudent(student);
        }
        public List<Student> SearchByName(string keyword) 
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                Console.WriteLine("Không được để trống phần tên");
                return new List<Student>();
            }
            return _dal.SearchByName(keyword);
        }
    }
}
