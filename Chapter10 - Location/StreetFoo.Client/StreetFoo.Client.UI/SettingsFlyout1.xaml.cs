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

        public SettingsFlyout1(UserControl control):this()
        {
            StackPanel.Children.Add(control);
        }
    }
}
