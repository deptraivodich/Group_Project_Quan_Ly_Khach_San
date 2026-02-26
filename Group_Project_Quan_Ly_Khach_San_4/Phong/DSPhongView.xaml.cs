using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Group_Project_Quan_Ly_Khach_San_4.Phong
{
    public partial class DSPhongView : Window
    {
        // Danh sách phòng truyền ra màn hình
        public ObservableCollection<HotelModel> DanhSachPhong { get; set; }

        public DSPhongView()
        {
            InitializeComponent();
            DanhSachPhong = new ObservableCollection<HotelModel>();

            // --- TẠO DỮ LIỆU GIẢ LẬP KHỚP VỚI DATABASE ---
            string[] loaiPhongs = { "Phòng Standard (Tiêu chuẩn)", "Phòng Superior (Cao cấp)", "Phòng Deluxe (Hướng biển)", "Phòng Suite VIP", "Phòng Family (Gia đình)" };
            string[] moTas = {
                "- Diện tích 25m2, thiết kế tối giản, hiện đại.\n- Cửa sổ kính lớn đón ánh sáng tự nhiên.\n- Phù hợp cho khách đi công tác ngắn ngày.\n- Miễn phí 2 chai nước khoáng mỗi ngày.",
                "- Diện tích 35m2, không gian thoáng đãng.\n- View nhìn ra trung tâm thành phố nhộn nhịp.\n- Có góc làm việc riêng tư tiện lợi.\n- Tặng kèm voucher buffet sáng 2 người.",
                "- Diện tích 45m2, ban công riêng siêu rộng.\n- Tầm nhìn hướng thẳng ra biển đón bình minh.\n- Phòng tắm đứng và bồn tắm nằm riêng biệt.\n- Miễn phí sử dụng hồ bơi vô cực trên tầng thượng.",
                "- Diện tích 60m2, thiết kế đẳng cấp hoàng gia.\n- Phòng khách và phòng ngủ tách biệt.\n- Đặc quyền nhận phòng VIP không cần chờ đợi.\n- Thưởng thức trà chiều miễn phí tại Lounge.",
                "- Diện tích 50m2, thiết kế 2 giường đôi cỡ lớn.\n- Không gian rộng rãi cho gia đình 4 người.\n- Có khu vực bếp nhỏ tiện lợi hâm nóng đồ ăn.\n- Tặng kèm vé khu vui chơi trẻ em."
            };
            decimal[] giaPhongs = { 500000, 850000, 1200000, 2500000, 1500000 };
            int[] soKhach = { 2, 2, 2, 2, 4 };
            string[] trangThais = { "Trống", "Đang ở", "Bảo trì", "Trống", "Đang dọn" };

            string[] imgs = {
                "https://images.unsplash.com/photo-1611892440504-42a792e24d32?q=80&w=600&auto=format&fit=crop",
                "https://images.unsplash.com/photo-1582719478250-c89cae4dc85b?q=80&w=600&auto=format&fit=crop",
                "https://images.unsplash.com/photo-1590490360182-c33d57733427?q=80&w=600&auto=format&fit=crop",
                "https://images.unsplash.com/photo-1566665797739-1674de7a421a?q=80&w=600&auto=format&fit=crop",
                "https://images.unsplash.com/photo-1631049307264-da0ec9d70304?q=80&w=600&auto=format&fit=crop"
            };

            // Sinh 20 dòng dữ liệu
            for (int i = 0; i < 20; i++)
            {
                int idx = i % 5;
                int tang = (i / 5) + 1;
                string soPhong = $"{tang}0{(i % 5) + 1}"; // Sinh ra 101, 102, 201...

                // Gọi là HotelModel cho khớp ảnh của bạn
                DanhSachPhong.Add(new HotelModel
                {
                    // Dữ liệu thật từ Database
                    SoPhong = soPhong,
                    TenLoaiPhong = loaiPhongs[idx],
                    GiaMotDem = giaPhongs[idx] + (i * 50000), // Cộng xíu tiền cho khác biệt
                    SoKhachToiDa = soKhach[idx],
                    TrangThai = trangThais[idx],
                    MotaChiTiet = moTas[idx],
                    // Dữ liệu giả để giữ form iVIVU cho đẹp
                    ImageUrl = imgs[idx],
                    Stars = "⭐⭐⭐⭐⭐",
                    Score = 9.8 - (i * 0.1),
                    ScoreText = "Tuyệt vời",
                    ReviewCount = 100 + (i * 15),
                    Location = $"Tầng {tang}, Khu A",
                    Tags = new List<string> { "TV Thông minh", "Tủ lạnh mini", "Ban công" }
                });
            }

            this.DataContext = this;
        }

        // ==========================================================
        // CÁC SỰ KIỆN NÚT BẤM
        // ==========================================================

        private void MenuTrangChu_Click(object sender, RoutedEventArgs e) { MessageBox.Show("Chức năng chuyển Menu Trang Chủ", "Thông báo"); }
        private void MenuPhong_Click(object sender, RoutedEventArgs e) { MessageBox.Show("Chức năng chuyển Menu Phòng", "Thông báo"); }
        private void BtnDatPhong_Click(object sender, RoutedEventArgs e) { MessageBox.Show("Mở form Danh sách đặt phòng!", "Thông báo"); }

        private void TxtSearch_GotFocus(object sender, RoutedEventArgs e)
        {
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

        private void HotelCard_Click(object sender, MouseButtonEventArgs e)
        {
            Border clickedBorder = sender as Border;
            if (clickedBorder != null)
            {
                // Ép kiểu về lại HotelModel cho khớp logic của bạn
                HotelModel selectedHotel = clickedBorder.DataContext as HotelModel;
                if (selectedHotel != null)
                {
                    // Đẩy cục HotelModel này qua form ChiTietKhachSanView
                    var detailView = new ChiTietKhachSanView(selectedHotel);
                    detailView.ShowDialog();
                }
            }
        }
    }

    // ==========================================================
    // CLASS CHỨA DỮ LIỆU ĐÃ ĐƯỢC ĐỔI TÊN THÀNH HotelModel
    // ==========================================================
    public class HotelModel
    {
        // Thuộc tính DB
        public string SoPhong { get; set; } = string.Empty;
        public string TenLoaiPhong { get; set; } = string.Empty;
        public decimal GiaMotDem { get; set; }
        public int SoKhachToiDa { get; set; }
        public string TrangThai { get; set; } = string.Empty;

        // Thuộc tính trang trí
        public string ImageUrl { get; set; } = string.Empty;
        public string Stars { get; set; } = string.Empty;
        public double Score { get; set; }
        public string ScoreText { get; set; } = string.Empty;
        public int ReviewCount { get; set; }
        public string Location { get; set; } = string.Empty;
        public string MotaChiTiet { get; set; } = string.Empty;
        public List<string> Tags { get; set; } = new List<string>();

        // Màu trạng thái
        public string TrangThaiColor
        {
            get
            {
                if (TrangThai == "Trống") return "#10B981";      // Xanh lá
                if (TrangThai == "Đang ở") return "#EF4444";     // Đỏ
                if (TrangThai == "Bảo trì") return "#F59E0B";    // Cam
                if (TrangThai == "Đang dọn") return "#3B82F6";   // Xanh dương
                return "#6B7280"; // Xám mặc định
            }
        }
    }
}