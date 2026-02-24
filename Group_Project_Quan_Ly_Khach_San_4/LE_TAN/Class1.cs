using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace WpfApp1
{
    class Class1
    {
        // Cần sửa: Sửa Initial Catalog thành QuanLyKhachSan 
        // Thêm: TrustServerCertificate=True để tránh lỗi bảo mật kết nối
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
                // Thông báo lỗi cụ thể để dễ sửa
                MessageBox.Show("Lỗi kết nối SQL: " + ex.Message);
            }
            return data;
        }

        public bool ExecuteNonQuery(string query)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    int result = command.ExecuteNonQuery();
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thực thi: " + ex.Message);
                return false;
            }
        }
    }
}