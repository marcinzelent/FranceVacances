using System;
using System.Collections.ObjectModel;
using System.IO;
using Windows.Storage;
using Windows.UI.Popups;
using France_Vacances.Methods;
using France_Vacances.Model;
using Newtonsoft.Json;

namespace France_Vacances.ViewModel
{
    public sealed class AnnouncementViewModel
    {
        private static readonly StorageFolder LocalFolder = ApplicationData.Current.LocalFolder;
        private static StorageFile _announcementsFile;
        public static ObservableCollection<AnnouncementModel> AnnouncementModels { get; set; } = new ObservableCollection<AnnouncementModel>();

        static AnnouncementViewModel()
        {
            DownloadAnnouncements();
        }
        
        //Method for downloading announcements
        private static async void DownloadAnnouncements()
        {
            try
            {
                //Download Uri content to string
                string responseString = OnlineOperations.DownloadString("http://retroth.ml/france_vacances/announcements.json");
                if (responseString != null)
                {
                    //Create JSON file and write string with content to it
                    _announcementsFile = await LocalFolder.CreateFileAsync("announcements.json", CreationCollisionOption.ReplaceExisting);
                    File.WriteAllText(_announcementsFile.Path, responseString);

                    //Load created JSON file into collection
                    _announcementsFile = await LocalFolder.GetFileAsync("announcements.json");
                    AnnouncementModels = JsonConvert.DeserializeObject<ObservableCollection<AnnouncementModel>>(File.ReadAllText(_announcementsFile.Path));
                }
                //If cannot download announcements, create fake announcement with error to display on MainView
                else AnnouncementModels.Add(new AnnouncementModel { Content = "Connection error :(", ColumnSpan = 8, RowSpan = 8 });
            }
            catch (Exception ex)
            {
                MessageDialog messageDialog = new MessageDialog(ex.Message);
                await messageDialog.ShowAsync();
            }
            
        }
    }
}
