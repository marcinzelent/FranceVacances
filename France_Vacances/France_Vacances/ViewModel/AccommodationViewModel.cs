using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using France_Vacances.Annotations;
using France_Vacances.Methods;
using France_Vacances.Model;

namespace France_Vacances.ViewModel
{
    public class AccommodationViewModel : INotifyPropertyChanged
    {
        private DateTimeOffset _startDateTime = DateTimeOffset.Now;
        private DateTimeOffset _endDateTime = DateTimeOffset.Now.AddDays(6);
        private double _overallPrice;

        public AccommodationViewModel()
        {
            DisplayedAccommodationModel = AccommodationSingleton.GetAccommodation();
            OverallPrice = (EndDateTime - StartDateTime).Days * DisplayedAccommodationModel.Price;
            GetBookingInfoCommand = new RelayCommand(GetBookingInfo);
        }

        private void CountPrice()
        {
            OverallPrice = (EndDateTime - StartDateTime).Days * DisplayedAccommodationModel.Price;
        }

        private void GetBookingInfo()
        {
            BookingSingleton.SetBookingStartDateTime(StartDateTime);
            BookingSingleton.SetBookingEndDateTime(EndDateTime);
            BookingSingleton.SetBookingPrice(OverallPrice);
        }

        public AccommodationModel DisplayedAccommodationModel { get; set; }

        public DateTimeOffset StartDateTime
        {
            get { return _startDateTime; }
            set
            {
                _startDateTime = value;
                OnPropertyChanged(nameof(StartDateTime));
                CountPrice();
            }
        }

        public string StartDate => _startDateTime.Date.ToString("dd.MM.yyyy");

        public DateTimeOffset EndDateTime
        {
            get { return _endDateTime; }
            set
            {
                _endDateTime = value;
                OnPropertyChanged(nameof(EndDateTime));
                CountPrice();
            }
        }

        public string EndDate => _endDateTime.Date.ToString("dd.MM.yyyy");

        public double OverallPrice
        {
            get { return _overallPrice; }
            set
            {
                _overallPrice = value;
                OnPropertyChanged(nameof(OverallPrice));
            }
        }

        public RelayCommand GetBookingInfoCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
