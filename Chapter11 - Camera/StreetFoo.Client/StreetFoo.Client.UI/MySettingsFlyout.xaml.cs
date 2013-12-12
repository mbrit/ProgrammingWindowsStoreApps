using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Settings Flyout item template is documented at http://go.microsoft.com/fwlink/?LinkId=273769

namespace StreetFoo.Client.UI
{
    public sealed partial class MySettingsFlyout : SettingsFlyout
    {
        private UserControl UserControl { get; set; }

        public MySettingsFlyout()
        {
            this.InitializeComponent();
        }

        public MySettingsFlyout(UserControl control)
            : this()
        {
            // set the user control...
            this.UserControl = control;
            this.StackPanel.Children.Add(control);

            // subscribe...
            this.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            // set the title and the width...
            this.Title = ((IViewModel)this.UserControl.DataContext).Caption;
            this.Width = this.UserControl.Width;
        }
    }
}
