using Commands.ViewModels;
using Microsoft.UI.Xaml.Controls;
using System.Diagnostics;

namespace Commands.Views
{
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; set; }

        public MainPage()
        {
            this.InitializeComponent();
            ViewModel = new MainViewModel();
        }

        private void OnAddPerson(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            Debug.WriteLine($"{ViewModel.Name} {ViewModel.Surname}");
        }
    }
}
