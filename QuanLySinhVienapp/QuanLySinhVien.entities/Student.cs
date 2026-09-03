using System;

namespace QuanLySinhVien.entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Major {  get; set; }
        public override string ToString()
        {
            return $"ID: {Id} | Tên: {Name,-15} | Tuổi: {Age,-3} | Ngành: {Major}";
        }
    }
}
