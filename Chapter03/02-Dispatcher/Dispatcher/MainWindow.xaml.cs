using CommunityToolkit.WinUI;
using Microsoft.UI.Xaml;
using System.Threading.Tasks;

namespace Dispatcher
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private async void OnUpdateMessage(object sender, RoutedEventArgs e)
        {
            var dispatcher = Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();
            await Task.Run(() =>
            {
                dispatcher.TryEnqueue(() =>
                {
                    txtMessage.Text = "This message has been updated from a background thread";
                });
            });
        }

        private async void OnUpdateMessageWithToolkit(object sender, RoutedEventArgs e)
        {
            var dispatcher = Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();
            await Task.Run(async () =>
            {
                await DispatcherQueue.EnqueueAsync(() =>
                {
                    txtMessage.Text = "This message has been updated from a background thread";
                });
            });
        }
    }
}
