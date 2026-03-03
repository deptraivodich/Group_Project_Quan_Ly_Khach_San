using System;
using System.Windows;
using System.Windows.Controls;

namespace Group_Project_Quan_Ly_Khach_San_4.LE_TAN
{
    public partial class DangKy : Window
    {
        Class1 db = new Class1(); // Sử dụng Class1 bạn đã cung cấp

        public DangKy()
        {
            InitializeComponent();
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            string user = txtUsername.Text.Trim();
            string pass = txtPassword.Password;
            string confirm = txtConfirmPassword.Password;
            string role = (cbRole.SelectedItem as ComboBoxItem)?.Content.ToString();

            // 1. Kiểm tra đầu vào
            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            if (pass != confirm)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp!");
                return;
            }

            // 2. Kiểm tra xem tên đăng nhập đã tồn tại chưa
            string checkQuery = $"SELECT COUNT(*) FROM Users WHERE Username = N'{user}'";
            var result = db.ExecuteQuery(checkQuery);
            if (result != null && int.Parse(result.Rows[0][0].ToString()) > 0)
            {
                MessageBox.Show("Tên đăng nhập này đã tồn tại!");
                return;
            }

            // 3. Thực hiện lưu vào Database
            string insertQuery = $"INSERT INTO Users (Username, Password, Role) VALUES (N'{user}', N'{pass}', N'{role}')";

            if (db.ExecuteNonQuery(insertQuery))
            {
                MessageBox.Show("Đăng ký tài khoản thành công!", "Thông báo");
                this.Close(); // Đóng form đăng ký sau khi xong
            }
            else
            {
                MessageBox.Show("Đăng ký thất bại, vui lòng thử lại.");
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Quay lại màn hình đăng nhập
        }
    }
}