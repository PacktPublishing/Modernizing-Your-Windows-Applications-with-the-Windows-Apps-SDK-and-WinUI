using Animations.Pages;
using Microsoft.UI.Xaml;

namespace Animations
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            App.ShellFrame = ShellFrame;
            ShellFrame.Content = new MainPage();
        }
    }
}
