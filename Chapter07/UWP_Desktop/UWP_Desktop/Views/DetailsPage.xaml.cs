using System;

using UWP_Desktop.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Microsoft.Extensions.DependencyInjection;
using UWP_Desktop.Models;

namespace UWP_Desktop.Views
{
    public sealed partial class DetailsPage : Page
    {
        public DetailsViewModel ViewModel { get; }

        public DetailsPage() : this(App.Current.Services.GetService<DetailsViewModel>()) {}

        public DetailsPage(DetailsViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ObservableEmployee oe;
            if (e.Parameter == null) oe = new ObservableEmployee(new Employee());
            else oe = (ObservableEmployee)e.Parameter;
            ViewModel.SelectedEmployee = oe;
        }
    }
}
