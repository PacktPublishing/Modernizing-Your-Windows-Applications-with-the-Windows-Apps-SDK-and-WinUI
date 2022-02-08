using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Navigation.ViewModels;

namespace Messages.Views
{
    public sealed partial class MainPage : Page
    {
        public MainPageViewModel ViewModel { get; set; }

        public MainPage()
        {
            this.InitializeComponent();
            ViewModel = Ioc.Default.GetService<MainPageViewModel>();
        }
    }
}
