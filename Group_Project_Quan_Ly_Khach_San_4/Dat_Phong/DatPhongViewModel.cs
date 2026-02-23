using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
namespace Group_Project_Quan_Ly_Khach_San_Nhom4.Dat_Phong
{
    public class DatPhongViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<RoomType> RoomTypes { get; set; }
        public ObservableCollection<Booking> Bookings { get; set; }

        public string CustomerName { get; set; }
        public int NumberOfGuests { get; set; } = 1;

        public DateTime CheckInDate { get; set; } = DateTime.Today;
        public DateTime CheckOutDate { get; set; } = DateTime.Today.AddDays(1);

        public RoomType SelectedRoomType { get; set; }

        public ICommand BookCommand { get; }
        public ICommand OpenListCommand { get; }

        public DatPhongViewModel()
        {
            RoomTypes = new ObservableCollection<RoomType>
            {
                new RoomType("Phòng đơn", 300000),
                new RoomType("Phòng đôi", 500000),
                new RoomType("Phòng VIP", 1200000)
            };

            SelectedRoomType = RoomTypes[0];
            Bookings = new ObservableCollection<Booking>();

            BookCommand = new RelayCommand(Book);
            OpenListCommand = new RelayCommand(OpenList);
        }

        void Book()
        {
            if (string.IsNullOrWhiteSpace(CustomerName))
            {
                MessageBox.Show("Vui lòng nhập họ tên!");
                return;
            }

            Bookings.Add(new Booking
            {
                CustomerName = CustomerName,
                RoomType = SelectedRoomType.Name,
                CheckIn = CheckInDate,
                CheckOut = CheckOutDate,
                Nights = (CheckOutDate - CheckInDate).Days,
                TotalPrice = (CheckOutDate - CheckInDate).Days * SelectedRoomType.Price
            });

            MessageBox.Show("Đặt phòng thành công!");
        }

        void OpenList()
        {
            DanhSachDatPhong w = new DanhSachDatPhong();
            w.DataContext = this;
            w.ShowDialog();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class RoomType
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public RoomType(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
    }

    public class Booking
    {
        public string CustomerName { get; set; }
        public string RoomType { get; set; }
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
        public event EventHandler CanExecuteChanged;
    }
}
