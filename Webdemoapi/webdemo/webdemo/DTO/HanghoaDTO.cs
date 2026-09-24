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
        public string MaHangHoa { get; set; }
        public string TenHangHoa { get; set; }
        public double DonGia { get; set; }
        public int SoLuong { get; set; }
    }
    public class UpdateHanghoaDTO
    {
            public string MaHangHoa { get; set; }
            public string TenHangHoa { get; set; }
            public double DonGia { get; set; }
            public int SoLuong { get; set; }
    }
}


