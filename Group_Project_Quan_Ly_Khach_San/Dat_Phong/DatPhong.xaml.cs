using System.Windows;

namespace Group_Project_Quan_Ly_Khach_San.Dat_Phong
{
    public partial class DatPhong : Window
    {
        public DatPhong()
        {
            InitializeComponent();
            DataContext = new DatPhongViewModel();
        }
    }
}
