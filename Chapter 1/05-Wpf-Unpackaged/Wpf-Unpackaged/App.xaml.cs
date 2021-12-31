using Microsoft.Windows.ApplicationModel.DynamicDependency;
using System.Windows;

namespace Wpf_Unpackaged
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Bootstrap.Initialize(0x00010000);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Bootstrap.Shutdown();
        }
    }
}
