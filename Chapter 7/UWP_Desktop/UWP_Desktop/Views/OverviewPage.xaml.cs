using System;

using UWP_Desktop.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.Storage;

namespace UWP_Desktop.Views
{
    public sealed partial class OverviewPage : Page
    {
        public OverviewViewModel ViewModel { get; }

        public OverviewPage() : this(App.Current.Services.GetService<OverviewViewModel>()) {}

        public OverviewPage(OverviewViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            Application.Current.Suspending += Current_Suspending;
        }

        private void Current_Suspending(object sender, Windows.ApplicationModel.SuspendingEventArgs e)
        {
            ApplicationData.Current.LocalSettings.Values["OverviewViewModel"] = ViewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ViewModel.LoadData();
        }
    }
}
