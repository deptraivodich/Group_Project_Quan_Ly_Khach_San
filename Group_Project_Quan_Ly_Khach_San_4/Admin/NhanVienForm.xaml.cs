using System;
using System.Windows;
using Group_Project_Quan_Ly_Khach_San_4; // Namespace chứa Class1
using Group_Project_Quan_Ly_Khach_San_Nhom4;
namespace Group_Project_Quan_Ly_Khach_San_Nhom4.Admin
{
    public partial class NhanVienForm : Window
    {
        Class1 db = new Class1();
        // Thuộc tính này dùng để nhận/trả dữ liệu nhân viên
        public NhanVien NhanVienData { get; set; }

        public NhanVienForm(NhanVien nv = null)
        {
            InitializeComponent(); // Sau khi sửa Bước 1, lệnh này sẽ hết lỗi

            if (nv != null)
            {
                NhanVienData = nv;
                txtHoTen.Text = nv.HoTen;
                txtChucVu.Text = nv.ChucVu;
                txtSDT.Text = nv.SDT;
                txtLuong.Text = nv.Luong.ToString();
            }
            else
            {
                NhanVienData = new NhanVien();
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtHoTen.Text))
                {
                    MessageBox.Show("Vui lòng nhập họ tên!");
                    return;
                }

                // Gán lại dữ liệu từ Form vào Object
                NhanVienData.HoTen = txtHoTen.Text;
                NhanVienData.ChucVu = txtChucVu.Text;
                NhanVienData.SDT = txtSDT.Text;
                decimal.TryParse(txtLuong.Text, out decimal luong);
                NhanVienData.Luong = luong;

                string sql = "";
                if (NhanVienData.MaNV == 0) // Thêm mới
                {
                    sql = $@"INSERT INTO NhanVien (HoTen, ChucVu, SDT, Luong) 
                             VALUES (N'{NhanVienData.HoTen}', N'{NhanVienData.ChucVu}', 
                                     '{NhanVienData.SDT}', {NhanVienData.Luong})";
                }
                else // Cập nhật
                {
                    sql = $@"UPDATE NhanVien SET HoTen = N'{NhanVienData.HoTen}', 
                             ChucVu = N'{NhanVienData.ChucVu}', SDT = '{NhanVienData.SDT}', 
                             Luong = {NhanVienData.Luong} WHERE MaNV = {NhanVienData.MaNV}";
                }

                if (db.ExecuteNonQuery(sql))
                {
                    MessageBox.Show("Lưu thành công!");
                    this.DialogResult = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
    }
}