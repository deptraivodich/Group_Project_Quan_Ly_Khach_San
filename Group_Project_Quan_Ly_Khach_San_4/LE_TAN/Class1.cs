using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Group_Project_Quan_Ly_Khach_San_4
{
    public class Class1
    {
        private string connectionString = @"Data Source=LAPTOP-EOECVQC4\SQLEXPRESS;Initial Catalog=QuanLyKhachSan;Integrated Security=True;TrustServerCertificate=True";

        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public DataTable ExecuteQuery(string query)
        {
            DataTable data = new DataTable();
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(data);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi SQL (Query): " + ex.Message);
            }
            return data;
        }

        public object ExecuteScalar(string query)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                return cmd.ExecuteScalar();
            }
        }

        public bool ExecuteNonQuery(string query)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi SQL (Execute): " + ex.Message);
                return false;
            }
        }

        // ============================================================
        // HÀM THÊM MỚI: Lấy thông tin tiền phòng để thanh toán
        // ============================================================
        public DataTable GetBookingInfo(string roomName)
        {
            // Lấy bản ghi mới nhất của phòng đó để hiện tổng tiền
            string query = $@"SELECT TOP 1 CustomerName, TotalPrice 
                              FROM Bookings 
                              WHERE RoomName = N'{roomName}' 
                              ORDER BY BookingID DESC";
            return ExecuteQuery(query);
        }

        // ============================================================
        // HÀM THÊM MỚI: Xử lý trả phòng (Đổi từ Đỏ sang Xanh)
        // ============================================================
        public bool ProcessCheckOut(string roomName)
        {
            // Cập nhật lại StatusText thành 'Trống', xóa tên khách và đổi màu về Xanh #2ECC71
            string query = $@"UPDATE Rooms 
                              SET StatusText = N'Trống', 
                                  CustomerName = '', 
                                  BackgroundColor = '#2ECC71' 
                              WHERE RoomName = N'{roomName}'";
            return ExecuteNonQuery(query);
        }
    }
}