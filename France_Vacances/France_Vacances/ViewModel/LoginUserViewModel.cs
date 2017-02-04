using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using CoreFtp.Infrastructure;
using France_Vacances.Methods;
using France_Vacances.Annotations;
using France_Vacances.Model;
using France_Vacances.Persistency;
using France_Vacances.View;
using Microsoft.Xaml.Interactions.Core;
using Newtonsoft.Json;

namespace France_Vacances.ViewModel
{
    class LoginUserViewModel : INotifyPropertyChanged
    {
        private UserSingleton _currentUser;
        private UserModel _loginInfo;
        private UserModel _loadedUser;
        private UserModel _adminUser;
        private RelayCommand _doLogin;
        private RelayCommand _logOutCommand;
        private Facade _facade;
        private string _displayVisibility;
        private string _displayText;

        // Constructor
        public LoginUserViewModel()
        {
            _facade = new Facade();
            _doLogin = new RelayCommand(DoLoginDelegate);
            _logOutCommand = new RelayCommand(LogOut); 

            _currentUser = UserSingleton.GetInstance();
            _loginInfo = new UserModel();
            _loadedUser = new UserModel();

        }

        // Region with the properties

        #region Properties

        public UserModel LoginInfo
        {
            get { return this._loginInfo; }
            set
            {
                this._loginInfo = value;
                OnPropertyChanged(nameof(LoginInfo));
            }
        }

        public UserModel LoadedUser
        {
            get { return this._loadedUser; }
            set
            {
                this._loadedUser = value;
                OnPropertyChanged(nameof(LoadedUser));
            }
        }

        public UserModel AdminUser
        {
            get { return this._adminUser; }
            set
            {
                this._adminUser = value;
                OnPropertyChanged(nameof(AdminUser));
            }
        }

        public RelayCommand DoLogin
        {
            get { return this._doLogin; }
            set { this._doLogin = value; }
        }

        public string DisplayVisibility
        {
            get { return this._displayVisibility; }
            set
            {
                this._displayVisibility = value;
                OnPropertyChanged(nameof(DisplayVisibility));
            }
        }

        // this property display the error text message 
        public string DisplayText
        {
            get { return this._displayText; }
            set
            {
                this._displayText = value;
                OnPropertyChanged(nameof(DisplayText));
            }
        }

        #endregion

        // Goes through the entire collection of users 
        // adds new user if user name is available else
        // user get prompted that the user name is unavailable
        public async void DoLoginDelegate()
        {
            try
            {

                string user =  OnlineOperations.DownloadString("http://retroth.ml/france_vacances/users/" + _loginInfo.UserName + ".json");
                LoadedUser = JsonConvert.DeserializeObject<UserModel>(user);

                DisplayVisibility = Visibility.Collapsed.ToString();


                    if (_loginInfo.UserName == null || _loginInfo.Password == null && _loginInfo.UserName == "" ||
                        _loginInfo.Password == "")
                    {
                        DisplayVisibility = Visibility.Visible.ToString();
                        DisplayText = "No user name or password entered";
                        
                    }
                    if (LoadedUser.UserName.Equals(LoginInfo.UserName) && LoadedUser.Password.Equals(LoginInfo.Password))
                    {
                        FrameActivate act = new FrameActivate();
                        _currentUser.LogIn(LoadedUser);
                        act.ActivateShell(typeof(MainView));
                        
                    }

                    if (!LoadedUser.Password.Equals(_loginInfo.Password) && LoadedUser.UserName.Equals(_loginInfo.UserName))
                    {
                        DisplayVisibility = Visibility.Visible.ToString();
                        DisplayText = "Wrong Password";
                       
                    }

                    if (!LoadedUser.UserName.Equals(LoginInfo.UserName))
                    {
                        DisplayVisibility = Visibility.Visible.ToString();
                        DisplayText = "No user with that user name";
                        
                    }

                    
                
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
        }


        private void LogOut()
        {
            FrameActivate act = new FrameActivate();
            _currentUser.LogOut();
            act.ActivateShell(typeof(MainView));

        }

        public RelayCommand LogOutCommand
        {
            get { return _logOutCommand; }
            set { _logOutCommand = value; }
        }

        // INotifyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
