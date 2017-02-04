using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices.ComTypes;
using France_Vacances.Persistency;
using France_Vacances.ViewModel;

namespace France_Vacances.Model
{
    public class AccommodationsCollection
    {
        private static ObservableCollection<AccommodationModel> _accommodationsCollection = new ObservableCollection<AccommodationModel>();
        private static AccommodationsCollection Instance { get; set; }

        public static AccommodationsCollection GetInstance()
        {
            if (Instance == null)
            {
                Instance = new AccommodationsCollection();
            }
            return Instance;
        }

        public static ObservableCollection<AccommodationModel> GetAccommodationsCollection()
        {
            return _accommodationsCollection;
        }

        public static void SetAccommodationsCollection(ObservableCollection<AccommodationModel> accommodationsCollection)
        {
            _accommodationsCollection = accommodationsCollection;
        }
        
    }
}
