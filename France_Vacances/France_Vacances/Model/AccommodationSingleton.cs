using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices.ComTypes;
using France_Vacances.Persistency;
using France_Vacances.ViewModel;

namespace France_Vacances.Model
{
    public class AccommodationSingleton
    {
        public static AccommodationModel _accommodationModel;
        private static AccommodationSingleton Instance { get; set; }

        public AccommodationSingleton()
        {
            _accommodationModel = new AccommodationModel();
            _accommodationModel.BookedDays = new List<DateTimeOffset>();
        }

        public static AccommodationSingleton GetInstance()
        {
            if (Instance == null)
            {
                Instance = new AccommodationSingleton();
            }
            return Instance;
        }
        //TODO Selection
        public static void SelectAcc(AccommodationModel acc)
        {
            _accommodationModel = acc;
            _accommodationModel.BookedDays = new List<DateTimeOffset>();
        }

        public static AccommodationModel GetAccommodation()
        {
            return _accommodationModel;
        }

        public static string GetAccommodationId()
        {
            return _accommodationModel.AccommodationId;
        }

        public static string GetAccommodationName()
        {
            return _accommodationModel.Name;
        }

        public static string GetAccommodationDescription()
        {
            return _accommodationModel.Description;
        }

        public static double GetAccommodationPrice()
        {
            return _accommodationModel.Price;
        }

        public static string GetAccommodationStreetName()
        {
            return _accommodationModel.StreetName;
        }

        public static string GetAccommodationCity()
        {
            return _accommodationModel.City;
        }

        public static string GetAccommodationPostalCode()
        {
            return _accommodationModel.PostalCode;
        }

        public static string GetAccommodationRegion()
        {
            return _accommodationModel.Region;
        }

        public static byte GetAccommodationRooms()
        {
            return _accommodationModel.Rooms;
        }

        public static byte GetAccommodationPersons()
        {
            return _accommodationModel.Persons;
        }

        public static List<string> GetAccommodationFacilities()
        {
            return _accommodationModel.Facilities;
        }

        public static List<string> GetAccommodationImages()
        {
            return _accommodationModel.Images;
        }

        public static double GetAccommodationReviewScore()
        {
            return _accommodationModel.ReviewScore;
        }

        public static string GetAccommodationStars()
        {
            return _accommodationModel.Stars;
        }

        public static List<DateTimeOffset> GetBookedDays()
        {
            return _accommodationModel.BookedDays;
        }

        public static void SetBookedDay(DateTimeOffset bookedDay)
        {
            _accommodationModel.BookedDays.Add(bookedDay);
        }
    }
}
