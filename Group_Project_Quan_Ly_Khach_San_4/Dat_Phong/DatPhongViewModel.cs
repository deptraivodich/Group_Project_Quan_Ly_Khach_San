using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Input;
// Đảm bảo namespace này khớp với file Class1.cs của bạn
using Group_Project_Quan_Ly_Khach_San_4;

namespace Group_Project_Quan_Ly_Khach_San_Nhom4.Dat_Phong
{
    public class DatPhongViewModel : INotifyPropertyChanged
    {
        // Các thuộc tính hỗ trợ Binding
        public ObservableCollection<RoomType> RoomTypes { get; set; }
        public ObservableCollection<Booking> Bookings { get; set; }

        private string _customerName;
        public string CustomerName
        {
            get => _customerName;
            set { _customerName = value; OnPropertyChanged("CustomerName"); }
        }

        public int NumberOfGuests { get; set; } = 1;
        public DateTime CheckInDate { get; set; } = DateTime.Today;
        public DateTime CheckOutDate { get; set; } = DateTime.Today.AddDays(1);
        public RoomType SelectedRoomType { get; set; }
        public string SelectedRoomName { get; set; } // Nhận từ trang Lễ tân

        public ICommand BookCommand { get; }
        public ICommand OpenListCommand { get; }

        public DatPhongViewModel()
        {
            // Khởi tạo dữ liệu loại phòng
            RoomTypes = new ObservableCollection<RoomType>
            {
                new RoomType("Phòng đơn", 300000),
                new RoomType("Phòng đôi", 500000),
                new RoomType("Phòng VIP", 1200000)
            };

            SelectedRoomType = RoomTypes[0];
            Bookings = new ObservableCollection<Booking>();

            // Khởi tạo Command
            BookCommand = new RelayCommand(Book);
            OpenListCommand = new RelayCommand(OpenList);

            // Tải dữ liệu cũ từ SQL
            LoadBookingsFromDatabase();
        }

        private void LoadBookingsFromDatabase()
        {
            try
            {
                Class1 db = new Class1();
                string query = "SELECT CustomerName, RoomName, CheckIn, CheckOut, Nights, TotalPrice FROM Bookings ORDER BY BookingID DESC";
                DataTable dt = db.ExecuteQuery(query);

                if (dt != null)
                {
                    Bookings.Clear();
                    foreach (DataRow dr in dt.Rows)
                    {
                        Bookings.Add(new Booking
                        {
                            CustomerName = dr["CustomerName"].ToString(),
                            RoomType = dr["RoomName"].ToString(),
                            CheckIn = Convert.ToDateTime(dr["CheckIn"]),
                            CheckOut = Convert.ToDateTime(dr["CheckOut"]),
                            Nights = Convert.ToInt32(dr["Nights"]),
                            TotalPrice = Convert.ToDecimal(dr["TotalPrice"])
                        });
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine("Lỗi tải dữ liệu: " + ex.Message); }
        }

        void Book()
        {
            if (string.IsNullOrWhiteSpace(CustomerName))
            {
                MessageBox.Show("Vui lòng nhập họ tên khách hàng!");
                return;
            }

            try
            {
                Class1 db = new Class1();
                int nights = (CheckOutDate - CheckInDate).Days;
                if (nights <= 0) nights = 1;
                decimal total = nights * SelectedRoomType.Price;

                // 1. Cập nhật màu đỏ cho phòng
                string sqlUpdateRoom = $@"UPDATE Rooms 
                                        SET StatusText = N'Đang dùng', 
                                            CustomerName = N'{CustomerName}', 
                                            BackgroundColor = '#E74C3C' 
                                        WHERE RoomName = N'{SelectedRoomName}'";

                // 2. Lưu vào lịch sử đặt phòng (Phải chạy script SQL tạo bảng Bookings trước)
                string sqlInsertBooking = $@"INSERT INTO Bookings (CustomerName, RoomName, CheckIn, CheckOut, Nights, TotalPrice) 
                                             VALUES (N'{CustomerName}', N'{SelectedRoomName}', '{CheckInDate:yyyy-MM-dd}', 
                                                     '{CheckOutDate:yyyy-MM-dd}', {nights}, {total})";

                if (db.ExecuteNonQuery(sqlUpdateRoom) && db.ExecuteNonQuery(sqlInsertBooking))
                {
                    Bookings.Insert(0, new Booking
                    {
                        CustomerName = CustomerName,
                        RoomType = SelectedRoomName,
                        CheckIn = CheckInDate,
                        CheckOut = CheckOutDate,
                        Nights = nights,
                        TotalPrice = total
                    });

                    MessageBox.Show($"Đặt thành công {SelectedRoomName}!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thực hiện đặt phòng: " + ex.Message);
            }
        }

        void OpenList()
        {
            DanhSachDatPhong w = new DanhSachDatPhong();
            w.DataContext = this;
            w.ShowDialog();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    // --- CÁC CLASS BỔ TRỢ ĐỂ HẾT LỖI CS0246 ---

    public class RoomType
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public RoomType(string name, decimal price) { Name = name; Price = price; }
    }

    public class Booking
    {
        public string CustomerName { get; set; }
        public string RoomType { get; set; } // Hiển thị tên phòng ví dụ "Phòng 01"
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int Nights { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        public RelayCommand(Action execute) => _execute = execute;
        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter) => _execute();
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}