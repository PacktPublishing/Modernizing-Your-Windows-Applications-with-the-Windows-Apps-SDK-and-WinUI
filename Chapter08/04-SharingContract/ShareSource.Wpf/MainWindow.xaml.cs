using System.Windows;
using System.Windows.Interop;

namespace ShareSource.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnShare(object sender, RoutedEventArgs e)
        {
            var myHwnd = new WindowInteropHelper(this).EnsureHandle();
            var dataTransferManager = DataTransferManagerHelper.GetForWindow(myHwnd);
            dataTransferManager.DataRequested += (obj, args) =>
            {
                args.Request.Data.SetText(txtToShare.Text);
                args.Request.Data.Properties.Title = "Share Example";
                args.Request.Data.Properties.Description = "A demonstration on how to share";
            };

            DataTransferManagerHelper.ShowShareUIForWindow(myHwnd);

        }
    }
}
