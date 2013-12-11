using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreetFoo.Client
{
    public class NullViewModelHost : IViewModelHost
    {
        public Task ShowAlertAsync(ErrorBucket errors)
        {
            return null;
        }

        public Task ShowAlertAsync(string message)
        {
            return null;
        }

        public void ShowView(Type viewModelInterfaceType, object parameter = null)
        {
        }

        public void HideAppBar()
        {
        }

        public void ShowAppBar()
        {
        }

        public void GoBack()
        {
        }
    }
}
