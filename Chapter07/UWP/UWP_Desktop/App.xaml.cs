using System;

using UWP_Desktop.Services;

using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UWP_Desktop.Contracts;
using UWP_Desktop.Data;
using UWP_Desktop.ViewModels;

namespace UWP_Desktop
{
    public sealed partial class App : Application
    {
        private ActivationService activationService;

        public new static App Current => (App)Application.Current;
        public IServiceProvider Services { get; }

        public App()
        {
            Services = ConfigureServices();
            InitializeComponent();
            UnhandledException += OnAppUnhandledException;

            // Deferred execution until used. Check https://docs.microsoft.com/dotnet/api/system.lazy-1 for further info on Lazy<T> class.
            this.activationService = CreateActivationService();
        }

        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            if (!args.PrelaunchActivated)
            {
                await this.activationService.ActivateAsync(args);
            }
        }

        protected override void OnFileActivated(FileActivatedEventArgs args)
        {
            var file = args.Files[0];
            //open a dedicated page to display the selected file
        }

        protected override async void OnActivated(IActivatedEventArgs args)
        {
            await this.activationService.ActivateAsync(args);
        }


        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .AddUserSecrets<App>(optional: true)
                .Build();
            
            services.AddDbContext<EmployeeContext>(options => options.UseInMemoryDatabase("employees"));
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IDataService, EFDataService>();
            services.AddTransient<IDialogService, DialogService>();
            services.AddTransient<ShellViewModel>();
            services.AddTransient<OverviewViewModel>();
            services.AddTransient<DetailsViewModel>();
            return services.BuildServiceProvider();
        }

        private void OnAppUnhandledException(object sender, Windows.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            // TODO WTS: Please log and handle the exception as appropriate to your scenario
            // For more info see https://docs.microsoft.com/uwp/api/windows.ui.xaml.application.unhandledexception
        }

        private ActivationService CreateActivationService()
        {
            return new ActivationService(this, Services.GetService<INavigationService>(), typeof(Views.OverviewPage), new Lazy<UIElement>(CreateShell));
        }

        private UIElement CreateShell()
        {
            return new Views.ShellPage(Services.GetService<ShellViewModel>());
        }
    }
}
