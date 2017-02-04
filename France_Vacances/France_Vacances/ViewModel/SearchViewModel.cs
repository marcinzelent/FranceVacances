using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using France_Vacances.Model;
using System.IO;
using System.Runtime.CompilerServices;
using Windows.Storage;
using Windows.UI.Popups;
using France_Vacances.Annotations;
using France_Vacances.Methods;
using France_Vacances.View;
using Newtonsoft.Json;


namespace France_Vacances.ViewModel
{
    public class SearchViewModel : INotifyPropertyChanged
    {
        private static ObservableCollection<AccommodationModel> _accommodationModels = new ObservableCollection<AccommodationModel>();
        private static ObservableCollection<AccommodationModel> _resultsCollection = new ObservableCollection<AccommodationModel>();
        private static ObservableCollection<AccommodationModel> _firstResultsCollection = new ObservableCollection<AccommodationModel>();
        private static AccommodationModel _searchedAccommodationModel = new AccommodationModel {Persons = 1, Rooms = 1};
        private static StorageFile _accommodationsFile;
        private static RelayCommand _findAccommodationsCommand;
        private static DateTimeOffset _searchedStartDate = DateTimeOffset.Now;
        private static DateTimeOffset _searchedEndDate = DateTimeOffset.Now;

        static SearchViewModel()
        {         
            DownloadAccomodations();
            _findAccommodationsCommand = new RelayCommand(FindAccommodations);
        }

        //Method for downloading accommodations
        private static async void DownloadAccomodations()
        {
            //Download Uri content to string
            string responseString = OnlineOperations.DownloadString("http://retroth.ml/france_vacances/accommodations.json");
            if (responseString != null)
            {
                //Create JSON file and write string with content to it
                _accommodationsFile = await ApplicationData.Current.LocalFolder.CreateFileAsync("accommodations.json", CreationCollisionOption.ReplaceExisting);
                File.WriteAllText(_accommodationsFile.Path, responseString);

                //Load created JSON file into collection
                _accommodationsFile = await ApplicationData.Current.LocalFolder.GetFileAsync("accommodations.json");
                _accommodationModels = JsonConvert.DeserializeObject<ObservableCollection<AccommodationModel>>(File.ReadAllText(_accommodationsFile.Path));
                AccommodationsCollection.SetAccommodationsCollection(_accommodationModels);
            }
        }

        //Method for searching accommodations using search form
        private static void FindAccommodations()
        {
            _firstResultsCollection.Clear();
            _resultsCollection.Clear();
            if(!string.IsNullOrWhiteSpace(_searchedAccommodationModel.Region))
            {
                if (_searchedEndDate < _searchedStartDate)
                {
                    MessageDialog messageDialog = new MessageDialog("The check-out date can't be before check-in date!");
                    messageDialog.ShowAsync();
                }
                foreach (var accommodation in _accommodationModels)
                {
                    if ((accommodation.Region.Equals(_searchedAccommodationModel.Region, StringComparison.OrdinalIgnoreCase) || accommodation.City.Equals(_searchedAccommodationModel.Region, StringComparison.OrdinalIgnoreCase)) && accommodation.Rooms >= _searchedAccommodationModel.Rooms && accommodation.Persons >= _searchedAccommodationModel.Persons)
                    {
                        _firstResultsCollection.Add(accommodation);
                    }
                }
                foreach (var accommodation in _firstResultsCollection)
                {
                    if (accommodation.BookedDays != null)
                    {
                        if (!accommodation.BookedDays.Contains(_searchedStartDate.Date) && !accommodation.BookedDays.Contains(_searchedEndDate.Date)) _resultsCollection.Add(accommodation);
                    }
                    else _resultsCollection.Add(accommodation);
                }
            }
        }
        
        //AccommodationModel object for keeping search parameters
        public AccommodationModel SearchedAccommodationModel
        {
            get { return _searchedAccommodationModel; }
            set
            {
                _searchedAccommodationModel = value; 
                OnPropertyChanged(nameof(SearchedAccommodationModel));
            }
        }

        public DateTimeOffset SearchedStartDate
        {
            get { return _searchedStartDate; }
            set
            {
                _searchedStartDate = value;
                OnPropertyChanged(nameof(SearchedStartDate));
            }
        }

        public DateTimeOffset SearchedEndDate
        {
            get { return _searchedEndDate; }
            set
            {
                _searchedEndDate = value;
                OnPropertyChanged(nameof(SearchedEndDate));
            }
        }

        //RelayCommand for calling searching method in View
        public RelayCommand FindAccommodationModelsCommand
        {
            get { return _findAccommodationsCommand; }
            set { _findAccommodationsCommand = value; }
        }

        //Collection of accommodations matching search parameters
        public ObservableCollection<AccommodationModel> ResultsCollection
        {
            get { return _resultsCollection; }
            set { _resultsCollection = value; }
        }

        //Lists with possible ComboBoxes choices in search form
        public List<byte> Persons { get; set; } = new List<byte> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
        public List<byte> Rooms { get; set; } = new List<byte> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

        //Collections of regions to display on CatalogView
        public ObservableCollection<string> MountainRegions { get; set; } = new ObservableCollection<string>
        {
            "Auvergne",
            "Franche Comte",
            "Languedoc-Roussillon",
            "Lorraine",
            "Midi-Pyrenees",
            "Provence Alpes Cote",
            "d’Azur",
            "Rhone-Alpes"
        };

        public ObservableCollection<string> SeaRegions { get; set; } = new ObservableCollection<string>
        {
            "Aquitaine",
            "Basse Normandie",
            "Bretagne",
            "Pays de la Loire",
            "Picardie",
            "Poitou-Charentes",
            "Provence Alpes Cote",
            "d’Azur"
        };

        public ObservableCollection<string> CountrysideRegions { get; set; } = new ObservableCollection<string>
        {
            "Alsace",
            "Aquitane",
            "Centre Val de Loire",
            "Languedoc-Roussillon",
            "Provence Alpes Cote",
            "d’Azur"
        };

        public ObservableCollection<string> IslandRegions { get; set; } = new ObservableCollection<string>
        {
            "Corse",
            "Guadeloupe",
            "Martinique",
            "Maurice",
            "Reunion"
        };

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}