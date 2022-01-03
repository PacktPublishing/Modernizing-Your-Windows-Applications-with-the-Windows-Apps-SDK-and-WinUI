using Microsoft.UI.Xaml;

namespace Themes
{

    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void OnChangeTheme(object sender, RoutedEventArgs e)
        {
            if (App.m_window.Content is FrameworkElement rootElement)
            {
                if (rootElement.RequestedTheme == ElementTheme.Light)
                {
                    rootElement.RequestedTheme = ElementTheme.Dark;
                }
                else
                {
                    rootElement.RequestedTheme = ElementTheme.Light;
                }
            }
        }
    }
}
