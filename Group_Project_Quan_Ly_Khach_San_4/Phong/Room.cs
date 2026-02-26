using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_Project_Quan_Ly_Khach_San_4.Phong
{
    internal class Room
    {
        private string iconKind;
        private string name;
        private string status;
        private string imagePath;
        private string capacity;
        private string statusColor;
        private bool hasPool;
        public Room() { }
        public string IconKind { get => iconKind; set => iconKind = value; }
        public string Name { get => name; set => name = value; }
        public string Status { get => status; set => status = value; }
        public string ImagePath { get => imagePath; set => imagePath = value; }
        public string Capacity { get => capacity; set => capacity = value; }
        public string StatusColor { get => statusColor; set => statusColor = value; }
        public bool HasPool { get => hasPool; set => hasPool = value; }

        public Room(string iconKind, string name, string status, string imagePath, string capacity, string statusColor, bool hasPool)
        {
            this.iconKind = iconKind;
            this.name = name;
            this.status = status;
            this.imagePath = imagePath;
            this.capacity = capacity;
            this.statusColor = statusColor;
            this.hasPool = hasPool;
        }
        public string StatusIcon
        {
            get
            {
                if (Status == "Sẵn sàng") return "✔";
                if (Status == "Đang bận") return "🔒"; // Icon ổ khóa
                if (Status == "Đang dọn") return "🧹"; // Icon cái chổi
                return "❓";
            }
        }

        public string PoolVisibility
        {
            get { return HasPool ? "Visible" : "Collapsed"; }
        }

        public string ButtonDatPhongVisibility { get; set; } = "Visible";
        public string ButtonThanhToanVisibility { get; set; } = "Collapsed";
    }
}
