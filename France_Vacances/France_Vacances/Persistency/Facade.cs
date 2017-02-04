using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using France_Vacances.Methods;
using France_Vacances.Model;
using Newtonsoft.Json;

namespace France_Vacances.Persistency
{
    class Facade
    {
        private StorageFolder _localFolder = ApplicationData.Current.LocalFolder;

        //*****************************************************User****************************************************
        private ObservableCollection<UserModel> _users = new ObservableCollection<UserModel>();
        private StorageFile _usersFile;
        private static string _userFileName = "Users.json";

        // loads list and serializes into a json file
        public async Task<ObservableCollection<UserModel>> LoadUsersFile()
        {
            _usersFile = await _localFolder.GetFileAsync(_userFileName);
            _users = JsonConvert.DeserializeObject<ObservableCollection<UserModel>>(
                File.ReadAllText(_usersFile.Path));

            return (ObservableCollection<UserModel>)_users;
        }

        public async void SaveUser(UserModel newUser)
        {
            newUser.UserID = (int)(DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
            StorageFile userFile = await ApplicationData.Current.LocalFolder.CreateFileAsync( newUser.UserName + ".json", CreationCollisionOption.FailIfExists);
            File.WriteAllText(userFile.Path, JsonConvert.SerializeObject(newUser));
            OnlineOperations.UploadToFtp(userFile.Name, "/users/");
        }

        // Deserialize json file and return as a observableCollection
        

        //**************************************************Accommodatiion***********************************************

        private ObservableCollection<AccommodationModel> _accommodations;
        private StorageFile _accommodationFile;
        private static string _accommodationFileName = "Accomodation.json";


        // loads list and serializes into a json file
        public async void SaveAccomodation(ObservableCollection<AccommodationModel> accommodation)
        {
            try
            {
                _accommodationFile =
                    await _localFolder.CreateFileAsync(_accommodationFileName, CreationCollisionOption.ReplaceExisting);
                File.WriteAllText(_accommodationFile.Path, JsonConvert.SerializeObject(accommodation));
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
        }

        // Deserialize json file and return as a observableCollection
        public async Task<ObservableCollection<AccommodationModel>> LoadAccommodationFile()
        {
            _accommodationFile = await _localFolder.GetFileAsync(_accommodationFileName);
            _accommodations = JsonConvert.DeserializeObject<ObservableCollection<AccommodationModel>>(
                File.ReadAllText(_accommodationFile.Path));

            return (ObservableCollection<AccommodationModel>) _accommodations;
        }

        //***************************************************************************************************************
    }
}
