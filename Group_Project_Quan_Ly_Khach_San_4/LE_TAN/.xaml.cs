using System;
using System.Collections.Generic;
using System.Data; // Thêm thư viện này
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    public partial class Window1 : Window
    {
        Class1 db = new Class1(); // Khởi tạo class kết nối bạn đã viết
        private RoomItem _selectedRoom;

        public Window1()
        {
            InitializeComponent();
            LoadData(); // Tải dữ liệu từ SQL khi mở máy
        }

        // --- HÀM TẢI DỮ LIỆU THẬT TỪ SQL ---
        private void LoadData()
        {
            string query = "SELECT * FROM Rooms";
            DataTable dt = db.ExecuteQuery(query);

            List<RoomItem> rooms = new List<RoomItem>();

            foreach (DataRow dr in dt.Rows)
            {
                rooms.Add(new RoomItem
                {
                    RoomName = dr["RoomName"].ToString(),
                    StatusText = dr["StatusText"].ToString(),
                    CustomerName = dr["CustomerName"].ToString(),
                    BackgroundColor = dr["BackgroundColor"].ToString()
                });
            }

            RoomList.ItemsSource = rooms;
        }

        private void BtnRoom_Click(object sender, RoutedEventArgs e)
        {
            _selectedRoom = (sender as Button).DataContext as RoomItem;
            if (_selectedRoom == null) return;

            txtRoomTitle.Text = "Phòng: " + _selectedRoom.RoomName;
            txtInputCustomer.Text = _selectedRoom.CustomerName;

            if (_selectedRoom.StatusText == "Đang dùng")
            {
                btnConfirmCheckIn.Visibility = Visibility.Collapsed;
                btnConfirmCheckOut.Visibility = Visibility.Visible;
            }
            else
            {
                btnConfirmCheckIn.Visibility = Visibility.Visible;
                btnConfirmCheckOut.Visibility = Visibility.Collapsed;
            }

            ActionOverlay.Visibility = Visibility.Visible;
        }

        // --- XỬ LÝ CHECK-IN (LƯU XUỐNG SQL) ---
        private void BtnConfirmCheckIn_Click(object sender, RoutedEventArgs e)
        {
            string customer = txtInputCustomer.Text;
            if (string.IsNullOrEmpty(customer)) { MessageBox.Show("Nhập tên khách!"); return; }

            // Câu lệnh cập nhật SQL
            string sql = $"UPDATE Rooms SET StatusText = N'Đang dùng', CustomerName = N'{customer}', BackgroundColor = '#E74C3C' WHERE RoomName = N'{_selectedRoom.RoomName}'";

            if (db.ExecuteNonQuery(sql))
            {
                LoadData(); // Load lại để cập nhật màu sắc
                ActionOverlay.Visibility = Visibility.Collapsed;
            }
        }

        // --- XỬ LÝ CHECK-OUT (CẬP NHẬT LẠI SQL) ---
        private void BtnConfirmCheckOut_Click(object sender, RoutedEventArgs e)
        {
            // Trả trạng thái về Trống
            string sql = $"UPDATE Rooms SET StatusText = N'Trống', CustomerName = N'', BackgroundColor = '#2ECC71' WHERE RoomName = N'{_selectedRoom.RoomName}'";

            if (db.ExecuteNonQuery(sql))
            {
                MessageBox.Show($"Đã Check-out cho {_selectedRoom.RoomName}. Dữ liệu đã lưu vào hệ thống.");
                LoadData();
                ActionOverlay.Visibility = Visibility.Collapsed;
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e) => ActionOverlay.Visibility = Visibility.Collapsed;

        // --- ĐĂNG XUẤT ---
        private void BtnDangXuat_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow(); // Giả sử bạn đã tạo LoginWindow
            login.Show();
            this.Close();
        }

        private void BtnDatPhong_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Mở trang Đặt Phòng...");
        private void BtnThanhToan_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Mở trang Thanh Toán...");
    }

    public class RoomItem
    {
        public string RoomName { get; set; }
        public string StatusText { get; set; }
        public string CustomerName { get; set; }
        public string BackgroundColor { get; set; }
    }
}