using CommunityToolkit.WinUI.UI.Controls;
using Microsoft.UI.Xaml.Controls;
using ModernDesktop.ViewModels;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace ModernDesktop.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OverviewPage : Page
    {
        public OverviewViewModel ViewModel { get; }

        public OverviewPage()
        {
            this.InitializeComponent();
            ViewModel = Ioc.Default.GetService<OverviewViewModel>();
        }

        private void DataGrid_OnSorting(object? sender, DataGridColumnEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}
