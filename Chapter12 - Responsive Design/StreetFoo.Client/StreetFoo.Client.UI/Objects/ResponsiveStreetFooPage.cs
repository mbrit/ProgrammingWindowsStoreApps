using Windows.UI.Xaml;

namespace StreetFoo.Client.UI
{
    public class ResponsiveStreetFooPage : StreetFooPage
    {
        public ResponsiveStreetFooPage()
        {
            SizeChanged += OnSizeChanged;
        }
        private const double SmallMode = 320;

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            VisualStateManager.GoToState(this, e.NewSize.Width <= SmallMode ? "Small" : "Default", true);
        }
    }
}
