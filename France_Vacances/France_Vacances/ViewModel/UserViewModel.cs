using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using France_Vacances.Annotations;
using France_Vacances.Methods;
using France_Vacances.Model;
using Newtonsoft.Json;

namespace France_Vacances.ViewModel
{
    class UserViewModel : INotifyPropertyChanged
    {
        private UserModel _displayedUser = UserSingleton.GetInstance().GetCurrentUser();
        private RelayCommand _saveUserInfoCommand;
        
        public UserViewModel()
        {
            if(DisplayedUser.BirthDate.Equals(new DateTimeOffset(0001,01,01,00,00,00,TimeSpan.Zero))) DisplayedUser.BirthDate = DateTimeOffset.Now;
            _saveUserInfoCommand = new RelayCommand(SaveUserInfo);
        }

        public UserModel DisplayedUser
        {
            get { return _displayedUser; }
            set
            {
                _displayedUser = value;
                OnPropertyChanged(nameof(DisplayedUser));
            }
        }

        private async void SaveUserInfo()
        {
            UserSingleton.GetInstance().LogIn(_displayedUser);
            StorageFile userFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(_displayedUser.UserName + ".json", CreationCollisionOption.ReplaceExisting);
            File.WriteAllText(userFile.Path, JsonConvert.SerializeObject(_displayedUser));
            OnlineOperations.UploadToFtp(userFile.Name, "/users/");
        }

        public RelayCommand SaveUserInfoCommand
        {
            get { return _saveUserInfoCommand; }
            set { _saveUserInfoCommand = value; }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
