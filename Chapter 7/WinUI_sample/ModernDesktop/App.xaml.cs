using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using ModernDesktop.Contracts;
using ModernDesktop.Data;
using ModernDesktop.Services;
using ModernDesktop.ViewModels;
using ModernDesktop.Views;
using System;
using Microsoft.Windows.System.Power;
using CommunityToolkit.Mvvm.DependencyInjection;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ModernDesktop
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
            var serviceProvider = ConfigureServices();
            Ioc.Default.ConfigureServices(serviceProvider);
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets the current <see cref="App"/> instance in use
        /// </summary>
        public new static App Current => (App)Application.Current;

        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .AddUserSecrets<App>(optional: true)
                .Build();

            services.AddDbContext<EmployeeContext>(options =>
                options.UseSqlServer(config.GetConnectionString("EmployeeContext")));
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IDataService, SqlDataService>();
            services.AddTransient<IDialogService, DialogService>();
            services.AddTransient<OverviewViewModel>();
            services.AddTransient<DetailsViewModel>();
            return services.BuildServiceProvider();
        }

        public static Window MainWindow { get; } = new Window() { Title = "MainWindow" };

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            var eventArgs = AppInstance.GetCurrent().GetActivatedEventArgs();
            if (eventArgs.Kind == ExtendedActivationKind.File)
            {
                var fileActivationArguments = eventArgs.Data as FileActivatedEventArgs;
                m_window.FilePath = fileActivationArguments.Files[0].Path;
            }


            var shellFrame = new Frame
            {
                Content = new MainPage()
            };

            MainWindow.Content = shellFrame;
            MainWindow.Activate();  

        }

        //private void SetupPowerManager()
        //{
        //    PowerManager.DisplayStatusChanged += PowerManager_DisplayStatusChanged;
        //    PowerManager.SystemSuspendStatusChanged += PowerManager_SystemSuspendStatusChanged;
        //}

        //private void PowerManager_DisplayStatusChanged(object sender, object e)
        //{
        //    if (PowerManager.DisplayStatus == DisplayStatus.On) { /*do things*/ }
        //    else if (PowerManager.DisplayStatus == DisplayStatus.Off) { /*stop things*/ }
        //}

        //private void PowerManager_SystemSuspendStatusChanged(object sender, object e)
        //{
        //    if (PowerManager.SystemSuspendStatus == SystemSuspendStatus.Entering) { /* stop things*/ }
        //}
    }
}
