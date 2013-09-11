using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Windows.Devices.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Items Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234233

namespace StreetFoo.Client.UI
{
    /// <summary>
    /// A page that displays a collection of item previews.  In the Split Application this page
    /// is used to display and select one of the available groups.
    /// </summary>
    [ViewModel(typeof(IReportsPageViewModel))]
    public sealed partial class ReportsPage : StreetFooPage
    {

        private IReportsPageViewModel Model { get; set; }

        public ReportsPage()
        {
            this.InitializeComponent();

            // setup model...
            this.InitializeViewModel();

            //// ok...
            //this.itemGridView.PointerPressed += itemGridView_PointerPressed;
            //this.itemGridView.IsItemClickEnabled = true;
            //this.itemGridView.ItemClick += itemListView_ItemClick;
        }

        void itemListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            //if (this.itemListView.SelectedItem == e.ClickedItem)
            //    this.itemListView.SelectedItem = null;
            //else
            //    this.itemListView.SelectedItem = e.ClickedItem;

            //// pass...
            //this.Model.SelectedItems = (ReportItem)this.itemListView.SelectedItem;
        }

        void itemGridView_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (e.GetCurrentPoint(this).Properties.IsRightButtonPressed)
            {
                this.BottomAppBar.IsOpen = true;
                e.Handled = true;
            }
        }
    }
}
