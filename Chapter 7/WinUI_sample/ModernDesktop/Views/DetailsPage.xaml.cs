using Microsoft.UI.Xaml.Controls;
using ModernDesktop.ViewModels;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace ModernDesktop.Views
{
    public sealed partial class DetailsPage : Page
    {
        public DetailsViewModel ViewModel { get; }

        public DetailsPage()
        {
            this.InitializeComponent();
            ViewModel = Ioc.Default.GetService<DetailsViewModel>();
        }
    }
}
