using System.Globalization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using France_Vacances.ViewModel;

namespace France_Vacances.View
{
    public sealed partial class CatalogView
    {
        public CatalogView()
        { 
            InitializeComponent();
            MyListView.SelectedItem = null;
        }

        private void ListViewBase_OnItemClick(object sender, ItemClickEventArgs e)
        {
            Frame.Navigate(typeof(SearchView));
        }
    }
}
