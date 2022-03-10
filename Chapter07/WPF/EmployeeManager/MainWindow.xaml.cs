using System.Windows;
using EmployeeManager.Configuration;
using EmployeeManager.Framework.Navigation;
using EmployeeManager.Views;
using Microsoft.Practices.Unity;

namespace EmployeeManager
{
    public partial class MainWindow : Window
    {
        private readonly IUnityContainer _container = UnityConfiguration.CreateContainer();

        public MainWindow()
        {
            InitializeComponent();

            Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            _container.RegisterInstance<INavigationContentControl>(clientArea);

            var viewService = _container.Resolve<INavigationService>();
            await viewService.NavigateToViewModel<OverviewViewModel, NavigationParameter>(NavigationContext.Default);
        }
    }
}