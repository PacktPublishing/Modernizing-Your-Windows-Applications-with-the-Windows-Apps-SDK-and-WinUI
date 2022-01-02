using Microsoft.UI.Xaml;
using System.Diagnostics;

namespace Localization
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void OnGetTranslation(object sender, RoutedEventArgs e)
        {
            var label = AppResourceManager.Instance.GetString("MyButton.Content");
            Debug.WriteLine(label);
        }
    }
}
