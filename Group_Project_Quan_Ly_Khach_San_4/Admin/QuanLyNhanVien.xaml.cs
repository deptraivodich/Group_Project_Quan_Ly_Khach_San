using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Group_Project_Quan_Ly_Khach_San_4; // Namespace chứa Class1.cs
using Group_Project_Quan_Ly_Khach_San_Nhom4; // Namespace chứa NhanVien.cs

namespace Group_Project_Quan_Ly_Khach_San_Nhom4.Admin
{
    /// <summary>
    /// Interaction logic for QuanLyNhanVien.xaml
    /// </summary>
    public partial class QuanLyNhanVien : Page
    {
        // Sử dụng Class1 để kết nối Database
        Class1 db = new Class1();

        public QuanLyNhanVien()
        {
            InitializeComponent();
            LoadNhanVien();
        }

        // ===== LOAD DATA TỪ SQL =====
        private void LoadNhanVien()
        {
            try
            {
                // Truy vấn lấy toàn bộ nhân viên từ bảng NhanVien
                // Lưu ý: SDT AS SoDienThoai để khớp với Binding trong XAML của bạn
                string query = "SELECT MaNV as Id, HoTen, ChucVu, SDT as SoDienThoai, Luong FROM NhanVien";
                DataTable dt = db.ExecuteQuery(query);

                // Chuyển DataTable thành List để dễ quản lý trong WPF
                var list = new List<NhanVien>();
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new NhanVien
                    {
                        MaNV = Convert.ToInt32(row["Id"]),
                        HoTen = row["HoTen"].ToString(),
                        ChucVu = row["ChucVu"].ToString(),
                        SDT = row["SoDienThoai"].ToString(),
                        Luong = Convert.ToDecimal(row["Luong"])
                    });
                }

                NhanVienDataGrid.ItemsSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        // Sự kiện click nút Thêm
        private void BtnThem_Click(object sender, RoutedEventArgs e)
        {
            // Mở Form nhập liệu (truyền null vì là thêm mới)
            var form = new NhanVienForm();
            if (form.ShowDialog() == true)
            {
                LoadNhanVien(); // Tải lại bảng ngay khi thêm thành công
            }
        }

        // Sự kiện click nút Sửa
        private void BtnSua_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra xem người dùng đã chọn dòng nào trên DataGrid chưa
            if (NhanVienDataGrid.SelectedItem is NhanVien nv)
            {
                var form = new NhanVienForm(nv); // Truyền đối tượng nv sang Form để sửa
                if (form.ShowDialog() == true)
                {
                    LoadNhanVien(); // Tải lại bảng sau khi sửa thành công
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần sửa!");
            }
        }

        // Sự kiện click nút Xóa
        private void BtnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (NhanVienDataGrid.SelectedItem is NhanVien nv)
            {
                if (MessageBox.Show($"Bạn có chắc muốn xóa nhân viên {nv.HoTen}?",
                    "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    // Thực hiện lệnh DELETE trong SQL
                    string query = $"DELETE FROM NhanVien WHERE MaNV = {nv.MaNV}";
                    if (db.ExecuteNonQuery(query))
                    {
                        MessageBox.Show("Đã xóa nhân viên thành công!");
                        LoadNhanVien(); // Làm mới lại danh sách
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nhân viên để xóa!");
            }
        }

        // Sự kiện click nút Làm mới
        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadNhanVien();
        }
    }
}