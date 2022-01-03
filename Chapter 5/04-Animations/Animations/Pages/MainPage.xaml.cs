using Animations.Models;
using CommunityToolkit.WinUI.UI.Animations;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using System;
using System.Collections.Generic;

namespace Animations.Pages
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            var people = new List<Person>
            {
                new Person { Name = "Matteo", Surname = "Pagani", BirthDate = new DateTime(1983, 9, 3), Budget = 50, Stocks = 5 },
                new Person { Name = "Marc", Surname = "Plogas", BirthDate = new DateTime(1980, 5, 2), Budget = 200, Stocks = 30 },
                new Person { Name = "John", Surname = "Doe", BirthDate = new DateTime(1990, 2, 9), Budget = 120, Stocks = 12 }
            };

            lstPeople.ItemsSource = people;
        }

        private void OnSimpleAnimation(object sender, RoutedEventArgs e)
        {
            AnimationBuilder.Create()
                .Opacity(from: 0, to: 1)
                .RotationInDegrees(from: 30, to: 0)
                .Start(MyButton);
        }

        private async void OnCustomAnimation(object sender, RoutedEventArgs e)
        {
            await AnimationBuilder.Create()
                .Opacity(from: 0,
                        to: 1,
                        duration: TimeSpan.FromSeconds(3),
                        repeat: RepeatOption.Forever)
                .RotationInDegrees(from: 30, to: 0)
                .StartAsync(MyButton);
        }

        private void OnKeyFrameAnimation(object sender, RoutedEventArgs e)
        {
            AnimationBuilder.Create()
                .Opacity().NormalizedKeyFrames(x =>
                            x.KeyFrame(0.0, 0)
                            .KeyFrame(0.5, 0.5)
                            .KeyFrame(1.0, 1))
                .Start(MyButton);
        }

        private void OnGoToSecondPage(object sender, RoutedEventArgs e)
        {
            App.ShellFrame.Navigate(typeof(DetailPage));
        }

        private void lstPeople_ItemClick(object sender, ItemClickEventArgs e)
        {
            Person person = e.ClickedItem as Person;
            App.ShellFrame.Navigate(typeof(DetailPersonPage), person, new SuppressNavigationTransitionInfo());
        }
    }
}
