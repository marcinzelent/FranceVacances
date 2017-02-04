using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class AccommodationView : Page
    {
        private int i=0;
        public AccommodationView()
        {
            this.InitializeComponent();
        }

        private void GoToBookingView(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(BookingView));
        }

        private void PreviousImage(object sender, RoutedEventArgs e)
        {
            if(i!=0) i--;
            Binding myBinding = new Binding();
            myBinding.Path = new PropertyPath("DisplayedAccommodationModel.Images" + "[" + i + "]");
            BindingOperations.SetBinding(ImageViewerBackground, ImageBrush.ImageSourceProperty, myBinding);
        }
        private void NextImage(object sender, RoutedEventArgs e)
        {
            if(i!=4)i++;
            Binding myBinding = new Binding();
            myBinding.Path = new PropertyPath("DisplayedAccommodationModel.Images" + "[" + i + "]");
            BindingOperations.SetBinding(ImageViewerBackground,ImageBrush.ImageSourceProperty,myBinding);
        }
    }
}
