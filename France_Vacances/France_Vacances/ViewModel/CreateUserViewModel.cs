using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
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

namespace France_Vacances.ViewModel
{
    class CreateUserViewModel : INotifyPropertyChanged
    {
        private ReadOnlyCollection<FtpNodeInformation> _users;
        private UserModel _newUser;
        
        private Facade _facade;

        private string _displayUserNameErrorText;
        private string _displayRePasswordErrorText;
        private string _displayAddressErrorText;
        private string _displayCountryErrorText;
        private string _displayCityErrorText;
        private string _displayFirstNameErrorText;
        private string _displayLastNameErrorText;
        private string _displayEmailErrorText;
        private string _displayPostCodeErrorText;
        private string _displayPhoneNrErrorText;
        private string _repeatedPassword;
        private string _repeatedEmail;

        // Constructor
        public CreateUserViewModel()
        {
            _facade = new Facade();
            DoCreateNewUser = new RelayCommand(DoCreateNewUserDeligate);
            _newUser = new UserModel();
            _newUser.BirthDate = DateTimeOffset.Now;

            LoadUserFiles();
        }

        // Region with the properties

        #region Properties

        public UserModel NewUser
        {
            get { return this._newUser; }
            set
            {
                this._newUser = value;
                OnPropertyChanged(nameof(NewUser));
            }
        }


        public ReadOnlyCollection<FtpNodeInformation> Users
        {
            get { return this._users; }
            set
            {
                this._users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        public string RepeatedPassword
        {
            get { return _repeatedPassword; }
            set
            {
                _repeatedPassword = value;
                OnPropertyChanged(nameof(RepeatedPassword));
            }
        }

        public string RepeatedEmail
        {
            get { return _repeatedEmail; }
            set
            {
                _repeatedEmail = value;
                OnPropertyChanged(nameof(RepeatedEmail));
            }
        }


        public RelayCommand DoCreateNewUser { get; set; }

        public string DisplayUserNameVisibility { get; set; }
        public string DisplayRePasswordVisibility { get; set; }
        public string DisplayAddressVisibility { get; set; }
        public string DisplayCityVisibility { get; set; }
        public string DisplayCountryVisibility { get; set; }
        public string DisplayFirstNameVisibility { get; set; }
        public string DisplayLastNameVisibility { get; set; }
        public string DisplayEmailVisibility { get; set; }
        public string DisplayReEmailVisibility { get; set; }
        public string DisplayPostCodeVisibility { get; set; }
        public string DisplayPhoneNrVisibility { get; set; }




        // this property display the error text message 
        public string DisplayUserNameErrorText
        {
            get { return _displayUserNameErrorText;}
            set
            {
                _displayUserNameErrorText = value;
                OnPropertyChanged(nameof(DisplayUserNameErrorText));
            }
        }

        public string DisplayRePasswordErrorText
        {
            get { return _displayRePasswordErrorText; }
            set
            {
                _displayRePasswordErrorText = value;
                OnPropertyChanged(nameof(DisplayRePasswordErrorText));
            }
        }

        public string DisplayAddressErrorText
        {
            get { return _displayAddressErrorText; }
            set
            {
                _displayAddressErrorText = value;
                OnPropertyChanged(nameof(DisplayAddressErrorText));
            }
        }

        public string DisplayCityErrorText
        {
            get { return _displayCityErrorText; }
            set
            {
                _displayCityErrorText = value;
                OnPropertyChanged(nameof(DisplayCityErrorText));
            }
        }

        public string DisplayCountryErrorText
        {
            get { return _displayCountryErrorText; }
            set
            {
                _displayCountryErrorText = value;
                OnPropertyChanged(nameof(DisplayCountryErrorText));
            }
        }

        public string DisplayFirstNameErrorText
        {
            get { return _displayFirstNameErrorText; }
            set
            {
                _displayFirstNameErrorText = value;
                OnPropertyChanged(nameof(DisplayFirstNameErrorText));
            }
        }

        public string DisplayLastNameErrorText
        {
            get { return _displayLastNameErrorText; }
            set
            {
                _displayLastNameErrorText = value;
                OnPropertyChanged(nameof(DisplayLastNameErrorText));
            }
        }

        public string DisplayEmailErrorText
        {
            get { return _displayEmailErrorText; }
            set
            {
                _displayEmailErrorText = value;
                OnPropertyChanged(nameof(DisplayEmailErrorText));
            }
        }
        public string DisplayReEmailErrorText {
            get { return _displayRePasswordErrorText; }
            set
            {
                _displayRePasswordErrorText = value;
                OnPropertyChanged(nameof(DisplayReEmailErrorText));
            }
        }
        public string DisplayPostCodeErrorText {
            get { return _displayPostCodeErrorText; }
            set
            {
                _displayPostCodeErrorText = value;
                OnPropertyChanged(nameof(DisplayPostCodeErrorText));
            }
        }

        public string DisplayPhoneErrorText
        {
            get { return _displayPhoneNrErrorText; }
            set
            {
                _displayPhoneNrErrorText = value;
                OnPropertyChanged(nameof(DisplayPhoneErrorText));
            }
        }

        #endregion

        // Goes through the entire collection of users 
        // addes new user if user name is available else
        // user get prompted that the user name is unavailable
        public void DoCreateNewUserDeligate()
        {
            try
            {
                var i = 1;
                DisplayUserNameVisibility = Visibility.Collapsed.ToString();
                _newUser.UserID =
                    (int) (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;


                foreach (FtpNodeInformation user in _users)
                {
                    string userName = user.Name.Substring(0, user.Name.LastIndexOf('.'));

                    if (!userName.Equals(_newUser.UserName) && i == _users.Count)
                    {
                        FrameActivate act = new FrameActivate();
                        _facade.SaveUser(_newUser);
                        act.ActivateShell(typeof(LoginView));
                    }

                    if (_newUser.UserName == null || _newUser.Password == null)
                    {
                        DisplayUserNameVisibility = Visibility.Visible.ToString();
                        DisplayUserNameErrorText = "No user name or password entered";
                    }

                    if (userName.Equals(NewUser.UserName))
                    {
                        DisplayUserNameVisibility = Visibility.Visible.ToString();
                        DisplayUserNameErrorText = "Unavailable";
                    }

                    if (_newUser.Password != _repeatedPassword)
                    {
                        DisplayRePasswordVisibility = Visibility.Visible.ToString();
                        DisplayRePasswordErrorText = "Passwords do not match";
                    }

                    if (string.IsNullOrEmpty(_newUser.Address))
                    {
                        DisplayAddressVisibility = Visibility.Visible.ToString();
                        DisplayAddressErrorText = "No address has been entered";
                    }

                    if (string.IsNullOrEmpty(_newUser.City))
                    {
                        DisplayCityVisibility = Visibility.Visible.ToString();
                        DisplayCityErrorText = "No city has been entered";
                    }

                    if (string.IsNullOrEmpty(_newUser.Country))
                    {
                        DisplayCountryVisibility = Visibility.Visible.ToString();
                        DisplayCountryErrorText = "No country has been entered";
                    }

                    if (string.IsNullOrEmpty(_newUser.FirstName))
                    {
                        DisplayFirstNameVisibility = Visibility.Visible.ToString();
                        DisplayFirstNameErrorText = "No first name has been entered";
                    }

                    if (string.IsNullOrEmpty(_newUser.LastName))
                    {
                        DisplayLastNameVisibility = Visibility.Visible.ToString();
                        DisplayLastNameErrorText = "No last name has been entered";
                    }

                    if (string.IsNullOrEmpty(_newUser.EmailAddress))
                    {
                        DisplayEmailVisibility = Visibility.Visible.ToString();
                        DisplayEmailErrorText = "No e-mail address has been entered";
                    }


                    if (_newUser.EmailAddress != RepeatedEmail)
                    {
                        DisplayReEmailVisibility = Visibility.Visible.ToString();
                        DisplayReEmailErrorText = "E-mail addresses do not match";
                    }

                    if (string.IsNullOrEmpty(_newUser.PostalCode))
                    {
                        DisplayPostCodeVisibility = Visibility.Visible.ToString();
                        DisplayPostCodeErrorText = "No post code has been entered";
                    }

                    if (string.IsNullOrEmpty(_newUser.PhoneNumber))
                    {
                        DisplayPhoneNrVisibility = Visibility.Visible.ToString();
                        DisplayPhoneErrorText = "No phone number has been entered";
                        break;
                    }

                    i++;
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
        }

        private async void LoadUserFiles()
        {
            _users = await OnlineOperations.GetListOfFiles("/users/");
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
