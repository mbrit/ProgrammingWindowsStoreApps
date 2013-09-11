using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

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
            // get the concrete handler and as the frame to flip... (note we use the ViewFactory,
            // not the ViewModelFactory here...)

            foreach (var type in this.GetType().GetTypeInfo().Assembly.GetTypes())
            {
                var attr = (ViewModelAttribute)type.GetCustomAttribute<ViewModelAttribute>();
                if (attr != null && viewModelType.IsAssignableFrom(attr.ViewModelInterfaceType))
                {
                    // show...
                    this.Frame.Navigate(type);
                }
            }
        }

        protected override void OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // ok...
            this.GetModel().Activated();
        }
    }
}
