using Microsoft.UI.Xaml;

namespace ShareSource
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void OnShare(object sender, RoutedEventArgs e)
        {
            var myHwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
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
