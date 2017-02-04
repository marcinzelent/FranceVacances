using System;
using System.Linq.Expressions;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using France_Vacances.Model;
using France_Vacances.ViewModel;

namespace France_Vacances.View
{
    public sealed partial class SearchView

    {
        public SearchView()
        {

            InitializeComponent();

        }

        private void FindAccommodationsButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (DestinationTextBox.Text == "") DestinationTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
        }

        

        private void AccommodationsListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AccommodationModel selectedAccommodationModel = (AccommodationModel)AccommodationsListView.SelectedItem;
            if (selectedAccommodationModel != null)
            {
                AccommodationSingleton.SelectAcc(selectedAccommodationModel);
                Frame.Navigate(typeof(AccommodationView));
            }
            AccommodationsListView.SelectedItem = null;
        }
    }
}
