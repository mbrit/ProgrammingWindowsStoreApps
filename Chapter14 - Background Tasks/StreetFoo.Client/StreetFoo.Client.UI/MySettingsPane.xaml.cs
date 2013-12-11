using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TinyIoC;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace StreetFoo.Client.UI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    [ViewModel(typeof(IMySettingsPaneViewModel))]
    public sealed partial class MySettingsPane : MvvmAwareControl
    {
        private IMySettingsPaneViewModel ViewModel { get; set; }

        public MySettingsPane()
        {
            this.InitializeComponent();
            this.InitializeViewModel();
        }
    }
}
