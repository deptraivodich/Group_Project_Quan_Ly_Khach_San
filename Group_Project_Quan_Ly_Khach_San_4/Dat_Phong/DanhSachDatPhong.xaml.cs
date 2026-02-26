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

namespace Group_Project_Quan_Ly_Khach_San_Nhom4.Dat_Phong
{
    /// <summary>
    /// Interaction logic for DanhSachDatPhong.xaml
    /// </summary>
    public partial class DanhSachDatPhong : Window
    {
        public DanhSachDatPhong()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
