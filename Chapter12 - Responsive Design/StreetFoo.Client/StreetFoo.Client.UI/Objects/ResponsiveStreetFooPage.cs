using Windows.UI.Xaml;

namespace StreetFoo.Client.UI.Objects
{
    public class ResponsiveStreetFooPage : StreetFooPage
    {
        public ResponsiveStreetFooPage()
        {
            SizeChanged += OnSizeChanged;
        }
        private const double SmallMode = 320;
        private const double MediumMode = 500;

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width <= SmallMode)
            {
                VisualStateManager.GoToState(this,"Small", true);
            }
            else if (e.NewSize.Width <= MediumMode && e.NewSize.Width > SmallMode)
            {
                VisualStateManager.GoToState(this, "Medium", true);
            }
            else if (e.NewSize.Width <= MediumMode)
            {
                VisualStateManager.GoToState(this, "Default", true);
            }
        }
    }
}
