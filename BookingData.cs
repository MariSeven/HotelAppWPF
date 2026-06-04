using System;
using System.Collections.Generic;
using System.Text;

namespace HotelAppWPF
{
    public class BookingData
    {
        static int _nextBookingId = 0;
        public int BookingID { get; }
        public string BookingFullNameLodger { get; set; }
        public int BookingNumberRoom { get; set; }
        public DateOnly BookingDateStart { get; set; }
        public DateOnly BookingDateStop { get; set; }
        public decimal BookingPrice { get; set; }
        public BookingData (string bookingFullNameLodger, int bookingNumberRoom, DateOnly bookingDateStart, DateOnly bookingDateStop)
        {
            decimal price = 0;
            _nextBookingId++;
            BookingID = _nextBookingId;
            BookingFullNameLodger = bookingFullNameLodger;
            BookingNumberRoom = bookingNumberRoom;
            BookingDateStart = bookingDateStart;
            BookingDateStop = bookingDateStop;
            foreach (RoomsData room in RoomsData.roomData)
            {
                if (room.RoomNumber == bookingNumberRoom)
                {
                    price = room.RoomPrice;
                    break;
                }
            }
            BookingPrice = price* (bookingDateStop.DayNumber - bookingDateStart.DayNumber);
        }
        public static List<BookingData> bookingData { get; } = new List<BookingData>
        {
            new BookingData("Иванов Иван Иванович",2,new DateOnly(2026,06,02),new DateOnly(2026,06,05)),
            new BookingData("Петров Петр Петрович",4,new DateOnly(2026,06,01),new DateOnly(2026,06,10))
        };
    }
}
