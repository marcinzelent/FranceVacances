using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using France_Vacances.Controls;
using France_Vacances.Model;
using France_Vacances.View;

namespace France_Vacances
{
    /// <summary>
    /// The "chrome" layer of the app that provides top-level navigation with
    /// proper keyboarding navigation.
    /// </summary>
    public sealed partial class AppShell : Page
    {
        // Declare the top level nav items
        private List<NavMenuItem> navMenuItems = new List<NavMenuItem>(
            new[]
            {
                new NavMenuItem()
                {
                    Symbol = Symbol.Home,
                    Label = "Home",
                    DestPage = typeof(MainView),
                    IsSelected = true
                },
                new NavMenuItem()
                {
                    Symbol = Symbol.Find,
                    Label = "Search",
                    DestPage = typeof(SearchView)
                },
                new NavMenuItem()
                {
                    Symbol = Symbol.Map,
                    Label = "Catalog",
                    DestPage = typeof(CatalogView)
                },
                new NavMenuItem()
                {
                    Symbol = Symbol.Contact,
                    Label = "My account",
                    DestPage = typeof(UserView)
                },
               
                new NavMenuItem()
                {
                    Symbol = Symbol.Help,
                    Label = "About",
                    DestPage = typeof(AboutView)
                }
            });

        public static AppShell Current = null;

        /// <summary>
        /// Initializes a new instance of the AppShell, sets the static 'Current' reference,
        /// adds callbacks for Back requests and changes in the SplitView's DisplayMode, and
        /// provide the nav menu list with the data to display.
        /// </summary>
        public AppShell()
        {
            InitializeComponent();

            List<NavMenuItem> topNavMenuItems = navMenuItems.GetRange(0, 3);
            List<NavMenuItem> bottomNavMenuItems = navMenuItems.GetRange(3, 2);

            NavMenuList.ItemsSource = topNavMenuItems;
            NavMenuList2.ItemsSource = bottomNavMenuItems;
        }

        public Frame AppFrame { get { return Frame; } }
        
        #region Navigation

        /// <summary>
        /// Navigate to the Page for the selected <paramref name="listViewItem"/>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="listViewItem"></param>
        private void NavMenuList_ItemInvoked(object sender, ListViewItem listViewItem)
        {
            foreach (var i in navMenuItems)
            {
                i.IsSelected = false;
            }

            var item = (NavMenuItem)((NavMenuListView)sender).ItemFromContainer(listViewItem);

            if (item != null)
            {
                item.IsSelected = true;
                if (item.DestPage != null &&
                    item.DestPage != AppFrame.CurrentSourcePageType)
                {
                    AppFrame.Navigate(item.DestPage, item.Arguments);
                }
            }
        }

        /// <summary>
        /// Ensures the nav menu reflects reality when navigation is triggered outside of
        /// the nav menu buttons.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnNavigatingToPage(object sender, NavigatingCancelEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
            {
                var item = (from p in navMenuItems where p.DestPage == e.SourcePageType select p).SingleOrDefault();
                if (item == null && AppFrame.BackStackDepth > 0)
                {
                    // In cases where a page drills into sub-pages then we'll highlight the most recent
                    // navigation menu item that appears in the BackStack
                    foreach (var entry in this.AppFrame.BackStack.Reverse())
                    {
                        item = (from p in navMenuItems where p.DestPage == entry.SourcePageType select p).SingleOrDefault();
                        if (item != null)
                            break;
                    }
                }

                foreach (var i in navMenuItems)
                {
                    i.IsSelected = false;
                }
                if (item != null)
                {
                    item.IsSelected = true;
                }

                var container = (ListViewItem)NavMenuList.ContainerFromItem(item);

                // While updating the selection state of the item prevent it from taking keyboard focus.  If a
                // user is invoking the back button via the keyboard causing the selected nav menu item to change
                // then focus will remain on the back button.
                if (container != null) container.IsTabStop = false;
                NavMenuList.SetSelectedItem(container);
                if (container != null) container.IsTabStop = true;
            }
        }

        #endregion

        /// <summary>
        /// Public method to allow pages to open SplitView's pane.
        /// Used for custom app shortcuts like navigating left from page's left-most item
        /// </summary>
        public void OpenNavePane()
        {
            TogglePaneButton.IsChecked = true;
        }
        public void MakeStackPanel1Visible()
        {
            StackPanel1.Visibility = Visibility.Visible;
            StackPanel2.Visibility = Visibility.Collapsed;
        }

        public void MakeStackPanel2Visible()
        {
            StackPanel1.Visibility = Visibility.Collapsed;
            StackPanel2.Visibility = Visibility.Visible;
        }

        private void GoToLoginView(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LoginView));
        }

        private void GoToCreateUserView(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CreateUserView));
        }
        private void GoToUserView(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(UserView));
        }
    }
}
