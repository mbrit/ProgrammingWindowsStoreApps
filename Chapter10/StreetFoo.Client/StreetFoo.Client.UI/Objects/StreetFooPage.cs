using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.System;
using Windows.UI.Xaml;

namespace StreetFoo.Client.UI
{
    public class StreetFooPage : Page, IViewModelHost
    {
        Task IViewModelHost.ShowAlertAsync(string message)
        {
            return PageExtender.ShowAlertAsync(this, message).AsTask();
        }

        Task IViewModelHost.ShowAlertAsync(ErrorBucket errors)
        {
            return PageExtender.ShowAlertAsync(this, errors).AsTask();
        }

        public void ShowView(Type viewModelType, object parameter = null)
        {
            this.Frame.ShowView(viewModelType, parameter);
        }

        protected override void OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // ok...
            this.GetModel().Activated(e.Parameter);
        }

        public void ShowAppBar()
        {
            if (this.BottomAppBar != null)
                this.BottomAppBar.IsOpen = true;
        }

        public void HideAppBar()
        {
            if (this.BottomAppBar != null)
                this.BottomAppBar.IsOpen = false;
        }

        protected override void OnKeyUp(KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.F1)
                App.ShowHelp();
            else
                base.OnKeyUp(e);
        }

        public void GoBack()
        {
            if (this.Frame != null && this.Frame.CanGoBack)
                this.Frame.GoBack();
        }

        protected virtual void GoBack(object sender, RoutedEventArgs e)
        {
            this.GoBack();
        }
    }
}
