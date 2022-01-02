using Microsoft.UI.Xaml;

namespace Instancing
{
    public sealed partial class MainWindow : Window
    {
        public string FilePath { get; set; }

        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void StackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            txtFilePath.Text = FilePath;
        }
    }
}
