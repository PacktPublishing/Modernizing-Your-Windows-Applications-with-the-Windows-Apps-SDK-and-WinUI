using Messages.Views;
using Microsoft.UI.Xaml;

namespace Navigation
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            ShellFrame.Content = new MainPage();
        }
    }
}
