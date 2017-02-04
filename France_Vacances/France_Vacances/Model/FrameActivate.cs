using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace France_Vacances.Model
{
    class FrameActivate
    {
        public void ActivateFrameNavigation(Type type)
        {
            var frame = (Frame) Window.Current.Content;
            frame.Navigate(type);
            Window.Current.Content = frame;
            Window.Current.Activate();

        }

        public void ActivateShell(Type type)
        {
            AppShell appShell = new AppShell(); 

            appShell = Window.Current.Content as AppShell;
            appShell.AppFrame.Navigate(type);
            Window.Current.Content = appShell;
            Window.Current.Activate();
            if(UserSingleton.GetInstance().GetCurrentUserName()!=null) appShell.MakeStackPanel2Visible();
            else appShell.MakeStackPanel1Visible();
        }


    }
}
