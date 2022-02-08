using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Themes.Inline
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void OnChangeTheme(object sender, RoutedEventArgs e)
        {
            if (App.MainWindow.Content is FrameworkElement rootElement)
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
