using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using Windows.Storage;
using Windows.UI.Popups;
using France_Vacances.Annotations;
using France_Vacances.Methods;
using France_Vacances.Model;
using France_Vacances.View;
using Newtonsoft.Json;

namespace France_Vacances.ViewModel
{
    public class BookingViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<BookingModel> _bookingModels = new ObservableCollection<BookingModel>();
        private BookingModel _bookingTemplate = new BookingModel();
        private RelayCommand _makeBookingCommand;

        public BookingViewModel()
        {
            _makeBookingCommand = new RelayCommand(MakeBooking);
        }

        private async void MakeBooking()
        {
            BookingTemplate.BookingId = (long)(DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
            BookingTemplate.AccommodationId = AccommodationSingleton.GetAccommodationId();
            BookingTemplate.BookerFirstName = UserSingleton.GetInstance().GetCurrentUserFirstName();
            BookingTemplate.BookerLastName = UserSingleton.GetInstance().GetCurrentUserLastName();
            BookingTemplate.BookerBirthDate = UserSingleton.GetInstance().GetCurrentUserBirthDate();
            BookingTemplate.BookerAddress = UserSingleton.GetInstance().GetCurrentUserAddress();
            BookingTemplate.BookerCity = UserSingleton.GetInstance().GetCurrentUserCity();
            BookingTemplate.BookerPostalCode = UserSingleton.GetInstance().GetCurrentUserPostalCode();
            BookingTemplate.BookerCountry = UserSingleton.GetInstance().GetCurrentUserCountry();
            BookingTemplate.BookerEmailAddress = UserSingleton.GetInstance().GetCurrentUserEmailAddress();
            BookingTemplate.BookerPhoneNumber = UserSingleton.GetInstance().GetCurrentUserPhoneNumber();
            BookingTemplate.Price = BookingSingleton.GetBookingPrice();
            BookingTemplate.BookingStartDateTime = BookingSingleton.GetBookingStartDateTime();
            BookingTemplate.BookingEndDateTime = BookingSingleton.GetBookingEndDateTime();
            StorageFile bookingFile = await ApplicationData.Current.LocalFolder.CreateFileAsync("booking" + _bookingTemplate.BookingId + ".json", CreationCollisionOption.FailIfExists);
            File.WriteAllText(bookingFile.Path, JsonConvert.SerializeObject(BookingTemplate));
            OnlineOperations.UploadToFtp(bookingFile.Name, "/bookings/");
            for (DateTimeOffset date = BookingTemplate.BookingStartDateTime; BookingTemplate.BookingEndDateTime.CompareTo(date) > 0;date=date.AddDays(1.0))
            {
                 AccommodationSingleton.SetBookedDay(date.Date);
            }
            ObservableCollection<AccommodationModel> accommodationsCollection =
                AccommodationsCollection.GetAccommodationsCollection();
            foreach (var accommodation in accommodationsCollection)
            {
                if (accommodation.AccommodationId == BookingTemplate.AccommodationId)
                {
                    accommodation.BookedDays = AccommodationSingleton.GetBookedDays();
                }
            }
            AccommodationsCollection.SetAccommodationsCollection(accommodationsCollection);
            StorageFile accommodationsFile = await ApplicationData.Current.LocalFolder.CreateFileAsync("accommodations.json", CreationCollisionOption.ReplaceExisting);
            File.WriteAllText(accommodationsFile.Path, JsonConvert.SerializeObject(AccommodationsCollection.GetAccommodationsCollection()));
            OnlineOperations.UploadToFtp("accommodations.json","/");
            MessageDialog messageDialog = new MessageDialog("You successfully booked this accommodation!");
            messageDialog.ShowAsync();
            FrameActivate frameActivate = new FrameActivate();
            frameActivate.ActivateShell(typeof(MainView));
        }

        public ObservableCollection<BookingModel> BookingModels
        {
            get { return _bookingModels; }
            set { _bookingModels = value; }
        }

        public BookingModel BookingTemplate
        {
            get { return _bookingTemplate; }
            set
            {
                _bookingTemplate = value;
                OnPropertyChanged(nameof(BookingTemplate));
            }
        }

        public RelayCommand MakeBookingCommand
        {
            get { return _makeBookingCommand; }
            set { _makeBookingCommand = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
