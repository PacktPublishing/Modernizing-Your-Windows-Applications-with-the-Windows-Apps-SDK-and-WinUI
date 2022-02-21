using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Messaging;
using Messages.Messages;
using Messages.ViewModels;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;

namespace Messages.Views
{
    public sealed partial class MainPage : Page, IRecipient<StartAnimationMessage>
    {
        public MainViewModel ViewModel { get; set; }

        public MainPage()
        {
            this.InitializeComponent();
            ViewModel = Ioc.Default.GetService<MainViewModel>();
            WeakReferenceMessenger.Default.Register<StartAnimationMessage>(this);
        }

        public void Receive(StartAnimationMessage message)
        {
            Storyboard sb = Resources["AddButtonAnimation"] as Storyboard;
            sb.Begin();
        }
    }
}
