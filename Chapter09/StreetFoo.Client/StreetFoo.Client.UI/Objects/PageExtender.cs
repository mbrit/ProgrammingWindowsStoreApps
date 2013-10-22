using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using TinyIoC;
using Windows.Foundation;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;

namespace StreetFoo.Client.UI
{
    // extension methods for presenting MessageDialog instances...
    internal static class PageExtender
    {
        internal static IAsyncOperation<IUICommand> ShowAlertAsync(this IViewModelHost page, ErrorBucket errors)
        {
            return ShowAlertAsync(page, errors.GetErrorsAsString());
        }

        internal static IAsyncOperation<IUICommand> ShowAlertAsync(this IViewModelHost page, string message)
        {
            // show...
            MessageDialog dialog = new MessageDialog(message != null ? message : string.Empty);
            return dialog.ShowAsync();
        }

        internal static void InitializeViewModel(this IViewModelHost host, IViewModel model = null)
        {
            // if we don't get given a model?
            if (model == null)
            {
                var attr = (ViewModelAttribute)host.GetType().GetTypeInfo().GetCustomAttribute<ViewModelAttribute>();
                if (attr != null)
                    model = (IViewModel)TinyIoCContainer.Current.Resolve(attr.ViewModelInterfaceType);
                else
                    throw new InvalidOperationException(string.Format("Page '{0}' is not decorated with ViewModelAttribute.", host));
            }

            // setup...
            model.Initialize((IViewModelHost)host);
            ((FrameworkElement)host).DataContext = model;
        }

        internal static IViewModel GetModel(this IViewModelHost page)
        {
            return ((Control)page).DataContext as IViewModel;
        }
    }
}
