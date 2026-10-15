using System.ComponentModel.DataAnnotations;

namespace webdemo.DTO
{
    public class HanghoaDTO
    {
        public string MaHangHoa { get; set; }
        public string TenHangHoa { get; set; }
        public double DonGia { get; set; }
        public int SoLuong { get; set; }
    }
    public class CreateHanghoaDTO
    {
        [Required(ErrorMessage = "Mã hàng hóa không được để trống.")]
        [StringLength(50, ErrorMessage = "Mã hàng hóa không được vượt quá 50 ký tự.")]
        public string MaHangHoa { get; set; }

        [Required(ErrorMessage = "Tên hàng hóa không được để trống.")]
        [StringLength(200, ErrorMessage = "Tên hàng hóa không được vượt quá 200 ký tự.")]
        public string TenHangHoa { get; set; }

        [Required(ErrorMessage = "Đơn giá là bắt buộc.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Đơn giá phải lớn hơn 0.")]
        public double? DonGia { get; set; }

        [Required(ErrorMessage = "Số lượng là bắt buộc.")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng không được nhỏ hơn 0.")]
        public int? SoLuong { get; set; }
    }
    public class UpdateHanghoaDTO
    {
        [Required(ErrorMessage = "Tên hàng hóa không được để trống.")]
        [StringLength(200, ErrorMessage = "Tên hàng hóa không được vượt quá 200 ký tự.")]
        public string TenHangHoa { get; set; }

        [Required(ErrorMessage = "Đơn giá là bắt buộc.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Đơn giá phải lớn hơn 0.")]
        public double? DonGia { get; set; }

        [Required(ErrorMessage = "Số lượng là bắt buộc.")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng không được nhỏ hơn 0.")]
        public int? SoLuong { get; set; }
    }
}

