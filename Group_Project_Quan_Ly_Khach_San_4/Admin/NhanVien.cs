namespace Group_Project_Quan_Ly_Khach_San_Nhom4
{
    public class NhanVien
    {
        public int MaNV { get; set; }        // Khớp với IDENTITY PRIMARY KEY
        public string HoTen { get; set; }    // Khớp với NVARCHAR(100)
        public string ChucVu { get; set; }   // Khớp với NVARCHAR(50)
        public string SDT { get; set; }      // Đổi từ SoDienThoai thành SDT cho khớp SQL
        public decimal Luong { get; set; }   // Khớp với DECIMAL(18,2)
    }
}