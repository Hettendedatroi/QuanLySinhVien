using System.ComponentModel.DataAnnotations;
namespace webdemo.Models
{
    public class Hanghoa
    {
        [Key]
        public string MaHangHoa { get; set; }
        public string TenHangHoa { get; set; }
        public double DonGia { get; set; }
        public int SoLuong { get; set; } 
    }
}
