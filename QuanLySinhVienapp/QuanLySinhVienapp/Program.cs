using System;
using System.Collections.Generic;
using QuanLySinhVien.entities;
using QuanLySinhVien.BLL;

namespace QuanLySinhVien
{
    class Program
    {
        private static StudentBLL bll = new StudentBLL();
        static void Main(string[] args)
        {
            Console.InputEncoding = System.Text.Encoding.UTF8;
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            bool Running = true;
            while (Running)
        {
            Console.Clear();
            Console.WriteLine("Hệ thống quản lí sinh viên");
            Console.WriteLine("1. Thêm sinh viên");
            Console.WriteLine("2. Sửa thông tin sinh viên");
            Console.WriteLine("3. Xóa sinh viên");
            Console.WriteLine("4. Tìm kiếm sinh viên theo tên");
            Console.WriteLine("5. Hiển thị danh sách sinh viên");
            Console.WriteLine("0. Thoát chương trình");
            Console.Write("Hãy chọn chức năng bạn muốn sử dụng (0-5): ");

            string choice = Console.ReadLine();
            Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        AddStudent();
                        break;
                    case "2":
                        UpdateStudent();
                        break;
                    case "3":
                        DeleteStudent();
                        break;
                    case "4":
                        SearchStudent();
                        break;
                    case "5":
                        DisplayStudent();
                        break;
                    case "0":
                        Running = false;
                        Console.WriteLine("TẠM BIỆT");
                        break;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ");
                        break;
                }
                if (Running) 
                {
                    Console.WriteLine("hãy bấm phím bất kì để quay về màn hình chính");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }
        private static void AddStudent()
        {
            try
            {
                Console.WriteLine("Hãy nhập tên sinh viên");
                Student student = new Student();

                Console.Write("Nhập ID: ");
                student.Id = int.Parse(Console.ReadLine());

                Console.Write("Nhập Họ và Tên: ");
                student.Name = Console.ReadLine();

                Console.Write("Nhập Tuổi: ");
                student.Age = int.Parse(Console.ReadLine());

                Console.Write("Nhập Chuyên ngành: ");
                student.Major = Console.ReadLine();

                if (bll.AddStudent(student))
                {
                    Console.WriteLine("-> Thêm sinh viên thành công!");
                }
                else
                {
                    Console.WriteLine("-> Thêm sinh viên thất bại!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Hãy nhập số thay vì chữ");
            }
        }
        private static void UpdateStudent()
        {
            try
            {
                Console.WriteLine("Chỉnh sửa danh sách sinh viên");

                Student student = new Student();

                Console.Write("Nhập ID sinh viên cần sửa: ");
                student.Id = int.Parse(Console.ReadLine());

                Console.Write("Nhập Tên mới: ");
                student.Name = Console.ReadLine();

                Console.Write("Nhập Tuổi mới: ");
                student.Age = int.Parse(Console.ReadLine());

                Console.Write("Nhập Chuyên ngành mới: ");
                student.Major = Console.ReadLine();

                if (bll.UpdateStudent(student))
                {
                    Console.WriteLine("Cập nhật thông tin thành công!");
                }
                else
                {
                    Console.WriteLine("Cập nhật thất bại!");
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine("Hãy nhập số thay vì chữ");
            }
        }
            

        private static void DeleteStudent()
        {
            try
            {
                Console.WriteLine("Xóa sinh viên");

                Console.Write("Nhập ID sinh viên cần xóa: ");
                int id = int.Parse(Console.ReadLine());

                if (bll.DeleteStudent(new Student { Id = id }))
                {
                    Console.WriteLine("Xóa sinh viên thành công!");
                }
                else
                {
                    Console.WriteLine("Lỗi.Không tìm thấy sinh viên có ID này.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Hãy nhập chữ thay vì số");
            }
        }

        private static void SearchStudent()
        {
            Console.WriteLine("--- 4. TÌM KIẾM SINH VIÊN THEO TÊN ---");

            Console.Write("Nhập từ khóa tên cần tìm: ");
            string keyword = Console.ReadLine();

            List<Student> results = bll.SearchByName(keyword);

            if (results != null && results.Count > 0)
            {
                Console.WriteLine($"\nTìm thấy {results.Count} sinh viên phù hợp:");
                foreach (var s in results)
                {
                    Console.WriteLine(s.ToString());
                }
            }
            else
            {
                Console.WriteLine("-> Không tìm thấy sinh viên nào phù hợp.");
            }
        }
        private static void DisplayStudent()
        {
            Console.WriteLine("Hiển thị danh sách sinh viên");

            List<Student> list = bll.GetAll();

            if (list.Count > 0)
            {
                foreach (var s in list)
                {
                    Console.WriteLine(s.ToString());
                }
                Console.WriteLine($"\nTổng số sinh viên: {list.Count}");
            }
            else
            {
                Console.WriteLine("Danh sách sinh viên hiện đang trống.");
            }
        }
    }
}