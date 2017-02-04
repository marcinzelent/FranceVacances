using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace France_Vacances.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BookingView : Page
    {
        public BookingView()
        {
            this.InitializeComponent();
        }

        private void Click(object sender, RoutedEventArgs e)
        {
            if (FirstName.Text == "") FirstName.BorderBrush = new SolidColorBrush(Colors.Red);
            if (LastName.Text == "") LastName.BorderBrush = new SolidColorBrush(Colors.Red);
            if (BirthDate.Date == null) BirthDate.BorderBrush = new SolidColorBrush(Colors.Red);
            if (Adress.Text == "") Adress.BorderBrush = new SolidColorBrush(Colors.Red);
            if (City.Text == "") City.BorderBrush = new SolidColorBrush(Colors.Red);
            if (PostalCode.Text == "") PostalCode.BorderBrush = new SolidColorBrush(Colors.Red);
            if (Country.Text == "") Country.BorderBrush = new SolidColorBrush(Colors.Red);
            if (EmailAddress.Text == "") EmailAddress.BorderBrush = new SolidColorBrush(Colors.Red);
            if (PhoneNumber.Text == "") PhoneNumber.BorderBrush = new SolidColorBrush(Colors.Red);
        }
    }
}
