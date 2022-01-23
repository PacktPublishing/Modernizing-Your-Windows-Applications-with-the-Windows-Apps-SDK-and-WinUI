using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Navigation.ViewModels;

namespace Navigation.Views
{
    public sealed partial class DetailPage : Page
    {
        public DetailPageViewModel ViewModel { get; set; }

        public DetailPage()
        {
            this.InitializeComponent();
            ViewModel = Ioc.Default.GetService<DetailPageViewModel>();  
        }
    }
}
