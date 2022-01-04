using Microsoft.UI.Xaml.Controls;
using Microsoft.Windows.AppLifecycle;
using System;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.DataTransfer.ShareTarget;

namespace ShareTarget.Pages
{
    public sealed partial class SharePage : Page
    {
        private ShareOperation shareOperation;

        public SharePage()
        {
            this.InitializeComponent();
        }

        private void OnComplete(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            shareOperation.ReportCompleted();
        }

        private async void Page_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            var instance = AppInstance.GetCurrent().GetActivatedEventArgs();
            if (instance.Kind == ExtendedActivationKind.ShareTarget)
            {
                var args = instance.Data as IShareTargetActivatedEventArgs;
                shareOperation = args.ShareOperation;
                string text = await args.ShareOperation.Data.GetTextAsync();
                string title = args.ShareOperation.Data.Properties.Title;
                txtSharedText.Text = text;
                txtTitle.Text = title;
            }
        }
    }
}
