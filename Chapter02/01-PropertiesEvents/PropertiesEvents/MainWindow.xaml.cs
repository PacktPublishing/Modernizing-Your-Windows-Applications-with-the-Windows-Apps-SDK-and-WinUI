using Microsoft.UI.Xaml;

namespace PropertiesEvents
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void OnChangeText(object sender, RoutedEventArgs e)
        {
            txtMessage.Text = "This is a new message";
        }
    }
}
