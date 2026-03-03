using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
// Namespace của trang Đặt Phòng
using Group_Project_Quan_Ly_Khach_San_Nhom4.Dat_Phong;
// Đảm bảo namespace này đúng với folder chứa ThanhToanView của bạn
using Group_Project_Quan_Ly_Khach_San_4.ThanhToan;

namespace Group_Project_Quan_Ly_Khach_San_4
{
    public partial class Window1 : Window
    {
        Class1 db = new Class1();
        private RoomItem _selectedRoom;

        public Window1()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                string query = "SELECT RoomID, RoomName, StatusText, CustomerName, BackgroundColor FROM Rooms";
                DataTable dt = db.ExecuteQuery(query);

                List<RoomItem> rooms = new List<RoomItem>();
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        rooms.Add(new RoomItem
                        {
                            RoomID = Convert.ToInt32(dr["RoomID"]),
                            RoomName = dr["RoomName"]?.ToString(),
                            StatusText = dr["StatusText"]?.ToString(),
                            CustomerName = dr["CustomerName"]?.ToString() ?? "",
                            BackgroundColor = dr["BackgroundColor"]?.ToString() ?? "#2ECC71"
                        });
                    }
                }
                RoomList.ItemsSource = rooms;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void BtnRoom_Click(object sender, RoutedEventArgs e)
        {
            _selectedRoom = (sender as Button).DataContext as RoomItem;
            if (_selectedRoom == null) return;

            // Nếu phòng màu đỏ (đang có khách)
            if (_selectedRoom.BackgroundColor.ToUpper() == "#E74C3C")
            {
                // 1. Lấy dữ liệu thực tế từ Database thông qua Class1
                DataTable dt = db.GetBookingInfo(_selectedRoom.RoomName);

                if (dt != null && dt.Rows.Count > 0)
                {
                    string khach = dt.Rows[0]["CustomerName"].ToString();
                    decimal tongTien = Convert.ToDecimal(dt.Rows[0]["TotalPrice"]);

                    // 2. MỞ FORM THANH TOÁN (Thay vì hiện MessageBox)
                    ThanhToanView bill = new ThanhToanView(_selectedRoom.RoomName, khach, tongTien);

                    // 3. Nếu bấm nút Thanh Toán trong form đó thành công
                    if (bill.ShowDialog() == true)
                    {
                        LoadData(); // Cập nhật lại sơ đồ phòng ngay lập tức
                    }
                }
            }
            else // Nếu phòng màu xanh (trống)
            {
                DatPhong dpWindow = new DatPhong();
                if (dpWindow.DataContext is DatPhongViewModel vm)
                {
                    vm.SelectedRoomName = _selectedRoom.RoomName;
                }
                dpWindow.ShowDialog();
                LoadData();
            }
        }

        private void BtnDangXuat_Click(object sender, RoutedEventArgs e) => this.Close();

        private void BtnThanhToan_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Vui lòng nhấn trực tiếp vào phòng màu đỏ để thanh toán.");

        private void Btndsdt_Click(object sender, RoutedEventArgs e)
        {
            DanhSachDatPhong w = new DanhSachDatPhong();
            w.DataContext = this;
            w.ShowDialog();
        }
    }

    public class RoomItem
    {
        public int RoomID { get; set; }
        public string RoomName { get; set; }
        public string StatusText { get; set; }
        public string CustomerName { get; set; }
        public string BackgroundColor { get; set; }
    }
}