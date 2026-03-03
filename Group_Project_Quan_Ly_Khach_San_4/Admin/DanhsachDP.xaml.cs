using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Group_Project_Quan_Ly_Khach_San_4;

namespace Group_Project_Quan_Ly_Khach_San_Nhom4.Admin
{
    public partial class DanhsachDP : Page
    {
        Class1 db = new Class1();

        public DanhsachDP()
        {
            InitializeComponent();
            LoadData();
        }

        // 1. Cập nhật hàm LoadData để lấy đầy đủ các cột từ Database
        private void LoadData(string searchText = "")
        {
            try
            {
                // Sửa query: Lấy đầy đủ các cột mà XAML Admin đang Binding
                // Lưu ý: Đổi tên cột bằng 'AS' để khớp với Binding trong XAML nếu cần
                string query = @"SELECT BookingID AS Id, 
                                        CustomerName, 
                                        RoomName AS RoomType, 
                                        CheckIn, 
                                        CheckOut, 
                                        Nights, 
                                        TotalPrice 
                                 FROM Bookings";

                if (!string.IsNullOrEmpty(searchText))
                {
                    query += $" WHERE CustomerName LIKE N'%{searchText}%' OR RoomName LIKE N'%{searchText}%'";
                }

                DataTable dt = db.ExecuteQuery(query);

                // Gán dữ liệu vào DataGrid
                BookingDataGrid.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị danh sách: " + ex.Message);
            }
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            LoadData(txtSearch.Text);
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Clear();
            LoadData();
        }

        // 4. Xử lý nút Xóa (Dựa trên cột Id đã được AS từ BookingID)
        private void BtnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (BookingDataGrid.SelectedItem is DataRowView row)
            {
                // Vì ở trên mình đặt AS Id nên ở đây dùng "Id" hoặc "BookingID" đều được tùy DataTable
                string id = row["Id"].ToString();

                if (MessageBox.Show($"Bạn có muốn xóa đơn đặt phòng mã {id}?", "Xác nhận xóa",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    string sql = $"DELETE FROM Bookings WHERE BookingID = {id}";
                    if (db.ExecuteNonQuery(sql))
                    {
                        MessageBox.Show("Đã xóa dữ liệu thành công!");
                        LoadData();
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng trong danh sách để xóa!");
            }
        }

        private void BtnSua_Click(object sender, RoutedEventArgs e)
        {
            if (BookingDataGrid.SelectedItem is DataRowView row)
            {
                // Logic mở form sửa hoặc thông báo
                MessageBox.Show("Chức năng sửa cho khách hàng: " + row["CustomerName"]);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một khách hàng để sửa!");
            }
        }
    }
}