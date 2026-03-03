using System;
using System.Data;
using System.Windows;
using Group_Project_Quan_Ly_Khach_San_4.LE_TAN;
// Đảm bảo bạn đã thêm using namespace của Admin để gọi được AdminDashboard
using Group_Project_Quan_Ly_Khach_San_Nhom4;

namespace Group_Project_Quan_Ly_Khach_San_4
{
    public partial class LoginWindow : Window
    {
        Class1 db = new Class1();

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string user = txtUsername.Text.Trim();
            string pass = txtPassword.Password;

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                txtError.Text = "Vui lòng nhập đầy đủ thông tin!";
                txtError.Visibility = Visibility.Visible;
                return;
            }

            string query = $"SELECT Role FROM Users WHERE Username = N'{user}' AND Password = N'{pass}'";
            DataTable dt = db.ExecuteQuery(query);

            if (dt != null && dt.Rows.Count > 0)
            {
                string role = dt.Rows[0]["Role"].ToString();

                MessageBox.Show($"Đăng nhập thành công với quyền: {role}!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                // ===== PHẦN SỬA TRỰC TIẾP TẠI ĐÂY =====
                if (role.Trim().Equals("Admin", StringComparison.OrdinalIgnoreCase))
                {
                    // Nếu là Admin -> Mở Dashboard của Admin
                    AdminDashboard adminWin = new AdminDashboard();
                    adminWin.Show();
                }
                else
                {
                    // Nếu không phải Admin (là Lễ tân) -> Mở Window1
                    Window1 leTanWin = new Window1();
                    leTanWin.Show();
                }

                this.Close();
            }
            else
            {
                txtError.Text = "Sai tài khoản hoặc mật khẩu!";
                txtError.Visibility = Visibility.Visible;
            }
        }

        private void ShowRegister_Click(object sender, RoutedEventArgs e)
        {
            DangKy registerWin = new DangKy();
            txtError.Visibility = Visibility.Collapsed;
            registerWin.ShowDialog();
        }
    }
}