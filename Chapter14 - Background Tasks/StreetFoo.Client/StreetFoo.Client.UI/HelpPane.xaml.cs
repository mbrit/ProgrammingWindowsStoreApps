using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TinyIoC;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace StreetFoo.Client.UI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    [ViewModel(typeof(IHelpPaneViewModel))]
    public sealed partial class HelpPane : MvvmAwareControl
    {
        public HelpPane()
        {
            this.InitializeComponent();

            // model...
            this.InitializeViewModel();
        }
    }
}
