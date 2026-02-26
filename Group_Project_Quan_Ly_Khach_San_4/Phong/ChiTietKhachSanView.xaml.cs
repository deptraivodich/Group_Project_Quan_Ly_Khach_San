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

namespace Group_Project_Quan_Ly_Khach_San_4.Phong
{
    /// <summary>
    /// Interaction logic for ChiTietKhachSanView.xaml
    /// </summary>
    public partial class ChiTietKhachSanView : Window
    {
        // Yêu cầu phải truyền vào một đối tượng HotelModel khi mở form này
        public ChiTietKhachSanView(HotelModel hotel)
        {
            InitializeComponent();

            // Lấy dữ liệu khách sạn đó gán làm DataContext cho toàn bộ giao diện
            this.DataContext = hotel;
        }
    }
}
