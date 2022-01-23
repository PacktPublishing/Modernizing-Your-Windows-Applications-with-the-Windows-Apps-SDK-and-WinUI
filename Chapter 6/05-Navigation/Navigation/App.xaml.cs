using CommunityToolkit.Mvvm.DependencyInjection;
using Messages.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Navigation.Contracts;
using Navigation.Services;
using Navigation.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Navigation
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            Ioc.Default.ConfigureServices(new ServiceCollection()
              .AddSingleton<INavigationService, NavigationService>()
              .AddSingleton<IDatabaseService, DatabaseService>()
              .AddTransient<MainPageViewModel>()
              .AddTransient<DetailPageViewModel>()
              .BuildServiceProvider());

            Frame shellFrame = new Frame
            {
                Content = new MainPage()
            };

            MainWindow.Content = shellFrame;
            MainWindow.Activate();
        }

        public static Window MainWindow { get; set; } = new Window() { Title = "Navigation" };
    }
}
