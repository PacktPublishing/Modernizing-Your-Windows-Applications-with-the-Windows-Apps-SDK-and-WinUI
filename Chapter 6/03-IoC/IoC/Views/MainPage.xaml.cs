using CommunityToolkit.Mvvm.DependencyInjection;
using IoC.ViewModels;
using Microsoft.UI.Xaml.Controls;
using System.Diagnostics;

namespace IoC.Views
{
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; set; }

        public MainPage()
        {
            this.InitializeComponent();
            ViewModel = Ioc.Default.GetService<MainViewModel>();
        }
    }
}
