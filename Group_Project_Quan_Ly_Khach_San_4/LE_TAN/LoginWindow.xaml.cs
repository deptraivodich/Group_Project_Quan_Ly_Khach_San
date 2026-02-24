using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string user = txtUsername.Text;
            string pass = txtPassword.Password;
            string role = (cbRole.SelectedItem as ComboBoxItem).Content.ToString();

            // Giả lập kiểm tra tài khoản (Sau này bạn sẽ viết SQL SELECT ở đây)
            if (user == "admin" && pass == "123" && role == "Admin")
            {
                MessageBox.Show("Chào mừng Admin!");
                // Mở trang Admin (Bạn cần tạo Window Admin trước)
                // AdminWindow adminWin = new AdminWindow();
                // adminWin.Show();
                this.Close();
            }
            else if (user == "letan" && pass == "123" && role == "Lễ Tân")
            {
                MessageBox.Show("Chào mừng Lễ Tân!");
                // Mở trang Lễ Tân (Window1 là trang bạn đã làm)
                Window1 leTanWin = new Window1();
                leTanWin.Show();
                this.Close();
            }
            else
            {
                txtError.Text = "Sai tài khoản hoặc mật khẩu!";
                txtError.Visibility = Visibility.Visible;
            }
        }
    }
}

