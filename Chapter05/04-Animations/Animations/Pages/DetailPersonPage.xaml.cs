using Animations.Models;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace Animations.Pages
{
    public sealed partial class DetailPersonPage : Page
    {
        public Person Person { get; set; }

        public DetailPersonPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Person = e.Parameter as Person;
        }
    }
}
