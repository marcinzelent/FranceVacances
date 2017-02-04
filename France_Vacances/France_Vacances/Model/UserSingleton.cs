using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace France_Vacances.Model
{
    class UserSingleton
    {
        private UserModel _user;

        private static UserSingleton Instance { get; set; }

        private UserSingleton()
        {
            _user = new UserModel();
        }

        public static UserSingleton GetInstance()
        {
            if (Instance == null)
            {
                Instance = new UserSingleton();
            }
            return Instance;
        }

        public void LogIn(UserModel user)
        {
            _user = user;
        }

        public void LogOut()
        {
            _user = new UserModel();
        }

        public UserModel GetCurrentUser()
        {
            return _user;
        }

        public string GetCurrentUserName()
        {
            if (_user != null) return _user.UserName;
            else return null;
        }

        public string GetCurrentUserPassword()
        {
            return _user.Password;
        }

        public string GetCurrentUserFirstName()
        {
            return _user.FirstName;
        }

        public string GetCurrentUserLastName()
        {
            return _user.LastName;
        }

        public DateTimeOffset GetCurrentUserBirthDate()
        {
            return _user.BirthDate;
        }

        public string GetCurrentUserAddress()
        {
            return _user.Address;
        }
        public string GetCurrentUserCity()
        {
            return _user.City;
        }

        public string GetCurrentUserPostalCode()
        {
            return _user.PostalCode;
        }

        public string GetCurrentUserCountry()
        {
            return _user.Country;
        }

        public string GetCurrentUserEmailAddress()
        {
            return _user.EmailAddress;
        }

        public string GetCurrentUserPhoneNumber()
        {
            return _user.PhoneNumber;
        }

        public void SetCurrentUserName(string userName)
        {
            _user.UserName = userName;
        }

        public void SetCurrentUserPassword(string password)
        {
            _user.Password = password;
        }

        public void SetCurrentUserFirstName(string firstName)
        {
            _user.FirstName = firstName;
        }

        public void SetCurrentUserLastName(string lastName)
        {
            _user.LastName = lastName;
        }

        public void SetCurrentUserBirthDate(DateTimeOffset birthDate)
        {
            _user.BirthDate = birthDate;
        }

        public void SetCurrentUserAddress(string address)
        {
            _user.Address = address;
        }

        public void SetCurrentUserCity(string city)
        {
            _user.City = city;
        }

        public void SetCurrentUserPostalCode(string postalCode)
        {
            _user.PostalCode = postalCode;
        }

        public void SetCurrentUserCountry(string country)
        {
            _user.Country = country;
        }

        public void SetCurrentUserEmailAddress(string email)
        {
            _user.EmailAddress = email;
        }

        public void SetCurrentUserPhoneNumber(string phoneNumber)
        {
            _user.PhoneNumber = phoneNumber;
        }
    }
}
