using System;
using System.Windows;
using System.Windows.Controls;
// Gọi các Page nằm trong thư mục Admin
using Group_Project_Quan_Ly_Khach_San_Nhom4.Admin;
// Gọi Class1 từ namespace của bộ phận Lễ tân
using Group_Project_Quan_Ly_Khach_San_4;

namespace Group_Project_Quan_Ly_Khach_San_Nhom4
{
    public partial class AdminDashboard : Window
    {
        // Sử dụng Class1 để kết nối Database
        Class1 db = new Class1();

        public AdminDashboard()
        {
            InitializeComponent();

            // Trang mặc định hiển thị khi mở Dashboard là Danh sách đặt phòng
            try
            {
                MainFrame.Navigate(new DanhsachDP());
            }
            catch (Exception ex)
            {
                // Ghi log nhẹ nếu trang chưa tồn tại
                System.Diagnostics.Debug.WriteLine("Lỗi khởi tạo trang mặc định: " + ex.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // Kiểm tra kết nối SQL. 
                // Nếu bảng 'nguoidung' chưa có, bạn có thể đổi thành 'NhanVien' để test
                var result = db.ExecuteScalar("SELECT COUNT(*) FROM NhanVien");
            }
            catch (Exception ex)
            {
                // Hiển thị lỗi nếu kết nối Database thất bại (chuỗi kết nối sai, v.v.)
                MessageBox.Show("Cảnh báo kết nối Database: " + ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // ================= ĐIỀU HƯỚNG SIDEBAR =================

        private void BtnNhanVien_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Điều hướng Frame sang trang Quản lý nhân viên
                MainFrame.Navigate(new QuanLyNhanVien());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Trang QuanLyNhanVien chưa sẵn sàng hoặc bị lỗi: " + ex.Message);
            }
        }

        private void BtnDatPhong_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Điều hướng Frame sang trang Danh sách đặt phòng
                MainFrame.Navigate(new DanhsachDP());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Trang DanhsachDP chưa sẵn sàng hoặc bị lỗi: " + ex.Message);
            }
        }

        // Nếu bạn muốn thêm nút Đăng xuất
        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn đăng xuất không?", "Xác nhận", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
        // Thêm tạm cái này nếu bạn không tìm thấy nút trong XAML để xóa
        private void BtnThongKe_Click(object sender, RoutedEventArgs e)
        {
            // Không làm gì cả
        }
    }
}