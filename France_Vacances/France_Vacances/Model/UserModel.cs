using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace France_Vacances.Model
{
    public class UserModel
    {

        //Definitions
        private int _userId;
        private string _userName;
        private string _password;
        private string _emailAddress;
        private string _phoneNumber;
        private string _firstName;
        private string _lastName;
        private DateTimeOffset _birthDate;
        private string _address;
        private string _city;
        private string _postalCode;
        private string _country;
        //Properties
        #region properties

        public int UserID
        {
            get { return this._userId; }
            set { this._userId = value; }
        }

        public string UserName
        {
            get { return this._userName; }
            set { this._userName = value; }
        }

        public string Password
        {
            get { return this._password; }
            set { this._password = value; }
        }

        public string FirstName
        {
            get { return this._firstName; }
            set { this._firstName = value; }
        }

        public string LastName
        {
            get { return this._lastName; }
            set { this._lastName = value; }
        }

        public DateTimeOffset BirthDate
        {
            get { return this._birthDate; }
            set { this._birthDate = value; }
        }

        public string Address
        {
            get { return this._address; }
            set { this._address = value; }
        }

        public string City
        {
            get { return this._city; }
            set { this._city = value; }
        }

        public string PostalCode
        {
            get { return this._postalCode; }
            set { this._postalCode = value; }
        }

        public string Country
        {
            get { return this._country; }
            set { this._country = value; }
        }

        public string EmailAddress
        {
            get { return this._emailAddress; }
            set { this._emailAddress = value; }
        }

        public string PhoneNumber
        {
            get { return this._phoneNumber; }
            set { this._phoneNumber = value; }
        }

        #endregion
    }
}
