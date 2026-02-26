using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for DSPhongView.xaml
    /// </summary>
    public partial class DSPhongView : Window
    {
        // Danh sách hiển thị ra màn hình giao diện iVIVU
        public ObservableCollection<HotelModel> Hotels { get; set; }

        public DSPhongView()
        {
            InitializeComponent();
            Hotels = new ObservableCollection<HotelModel>();

            // --- TẠO TỰ ĐỘNG 20 KHÁCH SẠN ĐA DẠNG BẰNG VÒNG LẶP ---
            string[] names = { "InterContinental Đà Nẵng", "Đà Nẵng Marriott Resort", "Mikazuki Japanese Resorts", "Furama Resort Danang", "Vinpearl Resort & Spa" };
            string[] badges = { "3N2Đ VMB + Đặc quyền Club", "Ưu đãi bí mật + Ăn sáng", "Vui chơi công viên nước", "Giảm 20% Đặt sớm", "Villa view biển" };
            string[] imgs = {
                "https://images.unsplash.com/photo-1582719478250-c89cae4dc85b?q=80&w=600&auto=format&fit=crop",
                "https://images.unsplash.com/photo-1520250497591-112f2f40a3f4?q=80&w=600&auto=format&fit=crop",
                "https://images.unsplash.com/photo-1611892440504-42a792e24d32?q=80&w=600&auto=format&fit=crop",
                "https://images.unsplash.com/photo-1566665797739-1674de7a421a?q=80&w=600&auto=format&fit=crop",
                "https://images.unsplash.com/photo-1590490360182-c33d57733427?q=80&w=600&auto=format&fit=crop"
            };
            string[] stars = { "⭐⭐⭐⭐⭐", "⭐⭐⭐⭐⭐", "⭐⭐⭐⭐", "⭐⭐⭐⭐⭐", "⭐⭐⭐⭐⭐" };
            double[] scores = { 9.8, 9.5, 8.9, 9.2, 9.4 };
            string[] scoreTexts = { "Tuyệt vời", "Rất tốt", "Tốt", "Tuyệt vời", "Rất tốt" };
            string[] locations = { "Bãi Bắc, Bán đảo Sơn Trà", "23 Trường Sa, Ngũ Hành Sơn", "Khu du lịch Xuân Thiều", "105 Võ Nguyên Giáp", "Quận Ngũ Hành Sơn" };
            decimal[] prices = { 17817000, 6099000, 3250000, 4150000, 5500000 };
            string[] rooms = { "Classic Room", "2 Bedroom Pool Villa", "Hinode Family Room", "Ocean Studio Suite", "Deluxe Ocean View" };

            // Sinh 20 dòng dữ liệu
            for (int i = 0; i < 20; i++)
            {
                int idx = i % 5;
                Hotels.Add(new HotelModel
                {
                    BadgeText = badges[idx],
                    ImageUrl = imgs[idx],
                    // Thêm số thứ tự vào tên để thấy sự khác biệt của 20 cái
                    HotelName = names[idx] + $" (Chi nhánh {i + 1})",
                    Stars = stars[idx],
                    Score = scores[idx],
                    ScoreText = scoreTexts[idx],
                    ReviewCount = 100 + (i * 15),
                    Location = locations[idx],
                    Tags = new List<string> { "Gần biển", "Buffet sáng", "Hồ bơi vô cực" },
                    // Cộng thêm chút tiền để giá các phòng không bị giống nhau hoàn toàn
                    Price = prices[idx] + (i * 150000),
                    RoomDesc = rooms[idx]
                });
            }

            this.DataContext = this;
        }

        // ==========================================================
        // CÁC SỰ KIỆN NÚT BẤM (ĐÃ GIỮ LẠI TỪ CODE CỦA BẠN)
        // ==========================================================

        private void BtnDatPhong_Click(object sender, RoutedEventArgs e)
        {
            // Bỏ comment 2 dòng dưới nếu bạn đã kết nối form DanhSachDatPhong
            // DanhSachDatPhong datPhongWindow = new DanhSachDatPhong();
            // datPhongWindow.ShowDialog();

            MessageBox.Show("Mở form Danh sách đặt phòng!", "Thông báo");
        }

        private void MenuTrangChu_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Chức năng chuyển Menu Trang Chủ", "Thông báo");
        }

        private void MenuPhong_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Chức năng chuyển Menu Phòng", "Thông báo");
        }

        private void TxtSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            // Dùng 'sender as TextBox' để tránh lỗi nếu bạn chưa đặt x:Name bên XAML
            TextBox txtSearch = sender as TextBox;
            if (txtSearch != null && txtSearch.Text == "Tìm kiếm phòng...")
            {
                txtSearch.Text = "";
                txtSearch.Foreground = Brushes.Black;
            }
        }

        private void TxtSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox txtSearch = sender as TextBox;
            if (txtSearch != null && string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "Tìm kiếm phòng...";
                txtSearch.Foreground = Brushes.Gray;
            }
        }
        private void HotelCard_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Lấy ra cái khung Border vừa bị click
            Border clickedBorder = sender as Border;
            if (clickedBorder != null)
            {
                // Rút trích dữ liệu khách sạn đang được gán vào cái thẻ đó
                HotelModel selectedHotel = clickedBorder.DataContext as HotelModel;

                if (selectedHotel != null)
                {
                    // Mở form Chi tiết khách sạn và truyền dữ liệu của khách sạn đó sang
                    ChiTietKhachSanView detailView = new ChiTietKhachSanView(selectedHotel);
                    detailView.ShowDialog();
                }
            }
        }
    }
    public class HotelModel
    {
        public string BadgeText { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string HotelName { get; set; } = string.Empty;
        public string Stars { get; set; } = string.Empty;
        public double Score { get; set; }
        public string ScoreText { get; set; } = string.Empty;
        public int ReviewCount { get; set; }
        public string Location { get; set; } = string.Empty;
        public List<string> Tags { get; set; } = new List<string>();
        public decimal Price { get; set; }
        public string RoomDesc { get; set; } = string.Empty;
    }
}
