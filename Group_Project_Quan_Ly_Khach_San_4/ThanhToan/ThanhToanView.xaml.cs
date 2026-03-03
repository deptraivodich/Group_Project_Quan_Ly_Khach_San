using System;
using System.Windows;

namespace Group_Project_Quan_Ly_Khach_San_4.ThanhToan
{
    public partial class ThanhToanView : Window
    {
        Class1 db = new Class1();

        // Constructor nhận dữ liệu từ Window1 truyền sang
        public ThanhToanView(string roomName, string customerName, decimal totalPrice)
        {
            InitializeComponent();

            // Đổ dữ liệu vào các control theo đúng x:Name bạn đã đặt
            txtTenPhong.Text = roomName;
            txtKhachHang.Text = customerName;
            txtTongTien.Text = totalPrice.ToString("N0"); // Hiển thị số tiền từ Database

            // Tự động tính toán ngày cơ bản
            dpNgayVao.SelectedDate = DateTime.Now.AddDays(-1);
            dpNgayRa.SelectedDate = DateTime.Now;
            txtSoNgay.Text = "1";
        }

        private void BtnThanhToan_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Gọi hàm ProcessCheckOut từ Class1 để cập nhật database (đổi màu xanh)
                if (db.ProcessCheckOut(txtTenPhong.Text))
                {
                    MessageBox.Show(
                        $"Đã thanh toán thành công phòng {txtTenPhong.Text}!",
                        "Thông báo",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information
                    );

                    // Trả kết quả về cho Window1 để LoadData() lại sơ đồ
                    this.DialogResult = true;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thanh toán: " + ex.Message);
            }
        }

        private void BtnHuy_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}