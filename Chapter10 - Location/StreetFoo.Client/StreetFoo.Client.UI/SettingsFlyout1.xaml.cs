using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Settings Flyout item template is documented at http://go.microsoft.com/fwlink/?LinkId=273769

namespace StreetFoo.Client.UI
{
    public sealed partial class SettingsFlyout1 : SettingsFlyout
    {
        public SettingsFlyout1()
        {
            this.InitializeComponent();
        }

        private readonly UserControl _userControl;

        public SettingsFlyout1(UserControl control):this()
        {
            _userControl = control;
            StackPanel.Children.Add(control);
            this.Loaded +=OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            this.Title = _userControl.DataContext.ToString();
        }
    }
}
