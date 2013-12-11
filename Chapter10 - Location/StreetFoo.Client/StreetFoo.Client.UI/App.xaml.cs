using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TinyIoC;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.Search;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.ApplicationSettings;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=234227

namespace StreetFoo.Client.UI
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used when the application is launched to open a specific file, to display
        /// search results, and so forth.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
            //#if DEBUG
            //            if (System.Diagnostics.Debugger.IsAttached)
            //            {
            //                this.DebugSettings.EnableFrameRateCounter = true;
            //            }
            //#endif

            // start up our runtime...
            await StreetFooRuntime.Start("Client");

            // create a temporary view model...
            var logonViewModel = TinyIoCContainer.Current.Resolve<ILogonPageViewModel>();
            logonViewModel.Initialize(new NullViewModelHost());

            // what next?
            var targetPage = typeof(LogonPage);
            if (await logonViewModel.RestorePersistentLogonAsync())
                targetPage = typeof(ReportsPage);

            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                if (!rootFrame.Navigate(typeof(LogonPage), e.Arguments))
                {
                    throw new Exception("Failed to create initial page");
                }
            }
            // Ensure the current window is active
            Window.Current.Activate();

            // register for data transfer...
            var manager = DataTransferManager.GetForCurrentView();
            manager.DataRequested += manager_DataRequested;

            // search...
            var search = SearchPane.GetForCurrentView();
            search.PlaceholderText = "Report title";
            search.QuerySubmitted += search_QuerySubmitted;
            search.SuggestionsRequested += search_SuggestionsRequested;

            // settings...
            var settings = SettingsPane.GetForCurrentView();
            settings.CommandsRequested += settings_CommandsRequested;
        }

        void settings_CommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
        {
            args.Request.ApplicationCommands.Add(new SettingsCommand("PrivacyStatement", "Privacy statement",  async (e) => 
                { 
                    await SettingsInteractionHelper.ShowPrivacyStatementAsync(); 
                }));

            args.Request.ApplicationCommands.Add(new SettingsCommand("MySettings", "My Settings", (e) =>
                {
                    var flyout = new SettingsFlyout1();
                    flyout.Show();
                }));

            args.Request.ApplicationCommands.Add(new SettingsCommand("Help", "Help", (e) => { ShowHelp(); }));
        }

        internal static void ShowHelp()
        {
            var flyout = new BasicFlyout(new HelpPane());
            flyout.Width = BasicFlyoutWidth.Wide;
            flyout.Show();
        }

        async void search_SuggestionsRequested(SearchPane sender, SearchPaneSuggestionsRequestedEventArgs args)
        {
            var deferral = args.Request.GetDeferral();
            try
            {
                await SearchInteractionHelper.PopulateSuggestionsAsync(args.QueryText, args.Request.SearchSuggestionCollection);
            }
            finally
            {
                deferral.Complete();
            }
        }

        static void manager_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            // find the view model and dereference...
            if (Window.Current != null)
            {
                var viewModel = Window.Current.GetViewModel();
                if (viewModel != null)
                    viewModel.ShareDataRequested(sender, args);
            }
        }

        private class NullViewModelHost : IViewModelHost
        {
            public Task ShowAlertAsync(ErrorBucket errors)
            {
                return null;
            }

            public Task ShowAlertAsync(string message)
            {
                return null;
            }

            public void ShowView(Type viewModelInterfaceType, object parameter = null)
            {
            }

            public void HideAppBar()
            {
            }

            public void ShowAppBar()
            {
            }

            public void GoBack()
            {
            }
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }

        /// <summary>
        /// Invoked when the application is activated to display search results.
        /// </summary>
        /// <param name="args">Details about the activation request.</param>
        protected override void OnSearchActivated(Windows.ApplicationModel.Activation.SearchActivatedEventArgs args)
        {
        }

        void search_QuerySubmitted(SearchPane sender, SearchPaneQuerySubmittedEventArgs args)
        {
            var frame = Window.Current.Content as Frame;
            if (frame == null)
            {
                frame = new Frame();
                Window.Current.Content = frame;
            }

            // show and activate...
            frame.ShowView(typeof(ISearchResultsPageViewModel), args.QueryText);
            Window.Current.Activate();
        }

        /// <summary>
        /// Invoked when the application is activated as the target of a sharing operation.
        /// </summary>
        /// <param name="args">Details about the activation request.</param>
        protected override async void OnShareTargetActivated(Windows.ApplicationModel.Activation.ShareTargetActivatedEventArgs args)
        {
            try
            {
                // start...
                await StreetFooRuntime.Start("Client");

                // logon?
                var logon = TinyIoCContainer.Current.Resolve<ILogonPageViewModel>();
                logon.Initialize(new NullViewModelHost());
                if (await logon.RestorePersistentLogonAsync())
                {
                    var shareTargetPage = new ShareTargetPage();
                    shareTargetPage.Activate(args);
                }
                else
                {
                    var notLoggedOnPage = new NotLoggedOnPage();
                    notLoggedOnPage.Activate(args);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw ex;
            }
        }
    }
}
