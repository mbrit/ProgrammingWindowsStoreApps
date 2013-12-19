using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace StreetFoo.Client.UI
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    [ViewModel(typeof(IEditReportPageViewModel))]
    public sealed partial class EditReportPage : ResponsiveStreetFooPage
    {
        public EditReportPage()
        {
            this.InitializeComponent();
            this.InitializeViewModel();
        }

        private async void HandleMoreButton(object sender, RoutedEventArgs e)
        {
            var popup = new PopupMenu();
            popup.Commands.Add(new UICommand("Take Picture", async (args) =>
            {

                //// try and unsnap, then show...
                //if (ApplicationView.TryUnsnap())
                //    this.ViewModel.TakePhotoCommand.Execute(null);

                await this.ShowAlertAsync("Make the app full screen to use the camera.");

            }));
            popup.Commands.Add(new UICommand("Capture Location", (args) => 
            {
                var viewModel = (IEditReportPageViewModel)this.GetViewModel();
                viewModel.CaptureLocationCommand.Execute(null);
            }));

            // show...
            await popup.ShowAsync(((FrameworkElement)sender).GetPointForContextMenu());
        }
    }
}
