using Microsoft.Windows.AppLifecycle;
using System;
using System.Windows;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.DataTransfer.ShareTarget;

namespace ShareTarget.Wpf
{
    /// <summary>
    /// Interaction logic for ShareWindow.xaml
    /// </summary>
    public partial class ShareWindow : Window
    {
        private ShareOperation shareOperation;
        public ShareWindow()
        {
            InitializeComponent();
        }

        private void OnComplete(object sender, RoutedEventArgs e)
        {
            shareOperation.ReportCompleted();
        }
        private async void Window_Loaded(object sender, RoutedEventArgs e)
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
