using System;

using UWP_Desktop.ViewModels;

using Windows.UI.Xaml.Controls;

namespace UWP_Desktop.Views
{
    // TODO WTS: Change the icons and titles for all NavigationViewItems in ShellPage.xaml.
    public sealed partial class ShellPage : Page
    {
        public ShellViewModel ViewModel { get; } 

        public ShellPage(ShellViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            ViewModel.Initialize(shellFrame, navigationView, KeyboardAccelerators);

            DataContext = ViewModel;
        }
    }
}
