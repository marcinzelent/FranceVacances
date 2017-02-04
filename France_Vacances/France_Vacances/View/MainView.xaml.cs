using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using France_Vacances.ViewModel;

namespace France_Vacances.View
{
    public sealed partial class MainView
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void FindAccommodationsButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (DestinationTextBox.Text == "") DestinationTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
            else Frame.Navigate(typeof(SearchView));
        }
    }
}