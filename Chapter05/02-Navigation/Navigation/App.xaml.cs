using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Navigation
{
    public partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();
        }

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window = new MainWindow();
            m_window.Activate();
        }

        public MainWindow m_window;

        public static Frame ShellFrame { get; set; }
    }
}
