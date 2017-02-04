using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices.ComTypes;
using France_Vacances.Persistency;
using France_Vacances.ViewModel;

namespace France_Vacances.Model
{
    public class BookingSingleton
    {
        private static BookingModel _bookingModel = new BookingModel();
        private static BookingSingleton Instance { get; set; }

        public static BookingSingleton GetInstance()
        {
            if (Instance == null)
            {
                Instance = new BookingSingleton();
            }
            return Instance;
        }

        public static void SetBookingEndDateTime(DateTimeOffset bookingEndDateTime)
        {
            _bookingModel.BookingEndDateTime = bookingEndDateTime;
        }

        public static long GetBookingId()
        {
            return _bookingModel.BookingId;
        }

        public static string GetAccommodationId()
        {
            return _bookingModel.AccommodationId;
        }

        public static double GetBookingPrice()
        {
            return _bookingModel.Price;
        }

        public static DateTimeOffset GetBookingStartDateTime()
        {
            return _bookingModel.BookingStartDateTime;
        }

        public static DateTimeOffset GetBookingEndDateTime()
        {
            return _bookingModel.BookingEndDateTime;
        }

        public static void SetBookingPrice(double price)
        {
            _bookingModel.Price = price;
        }

        public static void SetBookingStartDateTime(DateTimeOffset bookingStartDateTime)
        {
            _bookingModel.BookingStartDateTime = bookingStartDateTime;
        }
    }
}
