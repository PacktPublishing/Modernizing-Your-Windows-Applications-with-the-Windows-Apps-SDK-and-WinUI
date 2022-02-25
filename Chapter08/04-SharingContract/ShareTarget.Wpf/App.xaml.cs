using Microsoft.Windows.AppLifecycle;
using System.Windows;

namespace ShareTarget.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Window window;
            var instance = AppInstance.GetCurrent().GetActivatedEventArgs();
            if (instance.Kind == ExtendedActivationKind.ShareTarget)
            {
                window = new ShareWindow();
            }
            else
            {
                window = new MainWindow();
            }

            window.Show();
        }
    }
}
