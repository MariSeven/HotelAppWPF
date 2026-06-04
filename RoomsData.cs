using System;
using System.Collections.Generic;
using System.Text;

namespace HotelAppWPF
{
    public class RoomsData
    {
        static int _nextRoomId = 0;
        public int RoomID { get;}
        public int RoomNumber { get; set; }
        public string RoomClass { get; set; }
        public int RoomCapacity { get; set; }
        public decimal RoomPrice { get; set; }
        public string RoomStatus { get; set; }
        public RoomsData(int roomNumber, string roomClass, int roomCapacity, decimal roomPrice, string roomStatus)
        {
            _nextRoomId++;
            RoomID = _nextRoomId;
            this.RoomNumber = roomNumber;
            this.RoomClass = roomClass;
            this.RoomCapacity = roomCapacity;
            this.RoomPrice = roomPrice;
            this.RoomStatus = roomStatus;
        }
        public static List<RoomsData> roomData { get; } = new List<RoomsData>
        {
            new RoomsData(1,"Стандарт",1,2500m,"свободен"),
            new RoomsData(2,"Стандарт",1,2500m,"занят"),
            new RoomsData(3,"Стандарт",2,3000m,"свободен"),
            new RoomsData(4,"Стандарт",2,3000m,"занят"),
            new RoomsData(11,"Стандарт",3,4000m,"свободен"),
            new RoomsData(12,"Стандарт",4,5000m,"свободен"),
            new RoomsData(13,"Полулюкс",2,4500m,"свободен"),
            new RoomsData(14,"Полулюкс",2,4500m,"свободен"),
            new RoomsData(21,"Люкс",1,4500m,"свободен"),
            new RoomsData(22,"Люкс",2,6000m,"свободен"),
            new RoomsData(23,"Люкс",2,6000m,"свободен"),
            new RoomsData(24,"Люкс",2,6000m,"свободен")
        };
    }
}
